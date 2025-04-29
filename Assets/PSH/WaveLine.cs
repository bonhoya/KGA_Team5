using System.Collections.Generic;
using UnityEngine;

public class WaveLine : MonoBehaviour
{
    public LineRenderer lineRenderer;
    public Vector3[] points0;
    public Vector3[] points1;
    public List<Vector3[]> paths = new List<Vector3[]>();

    [Range(0, 5)][SerializeField] private int yPos = 0;
    private void Start()
    {
        // 선 그릴 포인트 설정 이거 코드 하나로 퉁칠려면 음
        // 스테이지당 웨이브포인트 2개임 1스테이지 = 01, 2스테이지 = 23 ...

        points0 = new Vector3[]
        {
            new Vector3(0, yPos, -20),
            new Vector3(0, yPos, -40),
            new Vector3(20, yPos, -40),
            new Vector3(20, yPos, -20),
            new Vector3(20, yPos, 20),
            new Vector3(40, yPos, 20),
            new Vector3(40, yPos, -40)
        };
        points1 = new Vector3[]
        {
             new Vector3(0, yPos, 20),
             new Vector3(0, yPos, 40),
             new Vector3(40, yPos, 40),
             new Vector3(40, yPos, -40)
        };

        paths.Add(points0);
        paths.Add(points1);
    }
    public void DrawPath(int stagenum, int n)//n은 1또는 2
    {
        lineRenderer.startColor = Color.blue;
        lineRenderer.endColor = Color.red;

        lineRenderer.positionCount = paths[stagenum + n - 2].Length;
        lineRenderer.SetPositions(paths[stagenum + n - 2]);

    }
    public void HidePath()
    {
        lineRenderer.enabled = false;
    }
}
