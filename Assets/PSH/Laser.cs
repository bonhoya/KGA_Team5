using System.Collections;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class Laser : MonoBehaviour
{
    public Transform[] pathPoints1; // ��θ� �̷� ������
    public Transform[] pathPoints2; // ��θ� �̷� ������
    public Transform[] pathPoints3; // ��θ� �̷� ������
    public Transform[][] paths;
    public float speed = 50f;//������ �ӵ�
    public float length = 0.5f;//������ ����
    private LineRenderer lr;

    void Start()
    {
        lr = GetComponent<LineRenderer>();
        lr.positionCount = 2;

        paths = new Transform[3][];
        paths[0] = pathPoints1;
        paths[1] = pathPoints2;
        paths[2] = pathPoints3;
    }
    public void MoveLaser(int n)
    {
        switch (n)
        {
            case 1:
                StartCoroutine(MoveLaserOnce(0));
                break;
            case 2:
                StartCoroutine(PlayLaserSequence());
                break;
            default:
                break;
        }

    }
    IEnumerator PlayLaserSequence()
    {
        yield return StartCoroutine(MoveLaserOnce(1));
        yield return StartCoroutine(MoveLaserOnce(2));
    }


    IEnumerator MoveLaserOnce(int n)
    {
        lr.enabled = true;

        for (int i = 0; i < paths[n].Length - 1; i++)
        {
            Vector3 start = paths[n][i].position;
            Vector3 end = paths[n][i + 1].position;
            float dist = Vector3.Distance(start, end);
            float t = 0;

            while (t < 1f)
            {
                t += Time.deltaTime * speed / dist;
                Vector3 currentPos = Vector3.Lerp(start, end, t);
                lr.SetPosition(0, Vector3.Lerp(start, end, Mathf.Clamp01(t - length))); // ����
                lr.SetPosition(1, currentPos); // �Ӹ�
                yield return null;
            }
        }

        // �� �������
        lr.enabled = false;
    }
}
