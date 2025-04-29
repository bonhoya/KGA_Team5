using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public static event Action OnCameraMoveDone;
    public RectTransform uiRect;

    public GameObject UI;//화면상단 ui전부

    public void StartCameraMove()
    {
        StartCoroutine(CameraMoveCoroutine());
    }

    private IEnumerator CameraMoveCoroutine()
    {
        float duration = 2f; // 움직이는 데 걸리는 시간
        float elapsed = 0f;

        Vector3 startRotation = new Vector3(-4f, -90f, 0f);
        Vector3 endRotation = new Vector3(60f, -90f, 0f);

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


            yield return null; // 다음 프레임까지 기다림
        }

        // 최종 위치 보정
        transform.localEulerAngles = endRotation;

        OnCameraMoveDone?.Invoke();
    }
}
