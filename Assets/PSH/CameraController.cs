using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public static event Action OnCameraMoveDone;
    public RectTransform uiRect;

    public GameObject UI;//ȭ���� ui����

    
    public void StartCameraMove()
    {
        StartCoroutine(CameraMoveCoroutine());
    }

    private IEnumerator CameraMoveCoroutine()
    {
        float duration = 2f; // �����̴� �� �ɸ��� �ð�
        float elapsed = 0f;

        Vector3 startRotation = new Vector3(20f, 0f, 0f);
        Vector3 endRotation = new Vector3(45f, 0f, 0f);

        Vector2 startPos = new Vector3(0, 300);
        Vector2 endPos = new Vector3(0, 0);

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / duration;
            t = Mathf.Clamp01(t);
            t = Mathf.SmoothStep(0f, 1f, t);

            Vector3 newRotation = Vector3.Lerp(startRotation, endRotation, t);
            transform.localEulerAngles = newRotation;

            Vector2 newPos = Vector2.Lerp(startPos, endPos, t);
            uiRect.anchoredPosition = newPos;


            yield return null; // ���� �����ӱ��� ��ٸ�
        }

        // ���� ��ġ ����
        transform.localEulerAngles = endRotation;

        OnCameraMoveDone?.Invoke();
    }
}
