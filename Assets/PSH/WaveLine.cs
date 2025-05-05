using System.Collections.Generic;
using UnityEngine;

public class WaveLine : MonoBehaviour
{
    public LineRenderer lineRenderer;
    private Vector3[] points0;
    private Vector3[] points1;
    private Vector3[] points2;
    private Vector3[] points3;
    private Vector3[] points4;
    private Vector3[] points5;
    private List<Vector3[]> paths = new List<Vector3[]>();

    [Range(0, 5)][SerializeField] private int yPos = 0;
    private void Start()
    {
        // 선 그릴 포인트 설정 이거 코드 하나로 퉁칠려면 음
        // 스테이지당 웨이브포인트 2개임 1스테이지 = 01, 2스테이지 = 23 ...
        points0 = new Vector3[]
        {
            new Vector3(0, yPos, 0-30),
            new Vector3(50, yPos, 0-30),
            new Vector3(50, yPos, 30-30),
            new Vector3(0, yPos, 30-30),
            new Vector3(0, yPos, 60-30),
            new Vector3(50, yPos, 60-30)
        };
        points1 = new Vector3[]
        {           
        };
        points2 = new Vector3[]
        {
            new Vector3(0, yPos, -20),
            new Vector3(0, yPos, -40),
            new Vector3(20, yPos, -40),
            new Vector3(20, yPos, -20),
            new Vector3(20, yPos, 20),
            new Vector3(40, yPos, 20),
            new Vector3(40, yPos, -40)
        };
        points3 = new Vector3[]
        {
             new Vector3(0, yPos, 20),
             new Vector3(0, yPos, 40),
             new Vector3(40, yPos, 40),
             new Vector3(40, yPos, -40)
        };
        points4 = new Vector3[]
        {
            new Vector3(0, yPos, 40),
             new Vector3(0, yPos, 0),
             new Vector3(40, yPos, 0),
             new Vector3(40, yPos, -40)
        };
        points5 = new Vector3[]
        {
            new Vector3(20, yPos, 40),
             new Vector3(20, yPos, 0),
             new Vector3(40, yPos, 0),
             new Vector3(40, yPos, -40)
        };

        paths.Add(points0);
        paths.Add(points1);
        paths.Add(points2);
        paths.Add(points3);
        paths.Add(points4);
        paths.Add(points5);
    }
    public void DrawPath(int stagenum, int n)//stagenum은 1부터 n은 1또는 2
    {
        lineRenderer.startColor = Color.blue;
        lineRenderer.endColor = Color.red;

        lineRenderer.positionCount = paths[stagenum * 2 + n - 3].Length;
        lineRenderer.SetPositions(paths[stagenum * 2 + n - 3]);

    }
    public void HidePath()
    {
        lineRenderer.enabled = false;
    }
}
