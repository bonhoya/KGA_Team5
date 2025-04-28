using UnityEngine;

public class WaveLine : MonoBehaviour
{
    public LineRenderer lineRenderer;
    public Vector3[] points1;
    public Vector3[] points2;

    [Range(0, 5)][SerializeField] private int yPos = 0;
    private void Start()
    {
        // 선 그릴 포인트 설정
        points1 = new Vector3[]
        {
            new Vector3(0, yPos, -20),
            new Vector3(0, yPos, -40),
            new Vector3(20, yPos, -40),
            new Vector3(20, yPos, -20),
            new Vector3(20, yPos, 20),
            new Vector3(40, yPos, 20),
            new Vector3(40, yPos, -40)
        };
        points2 = new Vector3[]
        {
             new Vector3(0, yPos, 20),
             new Vector3(0, yPos, 40),
             new Vector3(40, yPos, 40),
             new Vector3(40, yPos, -40)
        };
    }
    public void DrawPath(int n)//n은 1또는 2
    {
        if (n==1)
        {
            lineRenderer.startColor = Color.blue;  
            lineRenderer.endColor = Color.red;
            lineRenderer.positionCount = points1.Length;
            lineRenderer.SetPositions(points1);
        }
        else if (n==2)
        {
            lineRenderer.positionCount = points2.Length;
            lineRenderer.SetPositions(points2);
        }
    }
    public void HidePath()
    {
        lineRenderer.enabled = false;
    }
}
