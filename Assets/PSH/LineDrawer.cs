using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class LaserScrollEffect : MonoBehaviour
{
    public float scrollSpeed = 2f;
    private LineRenderer lineRenderer;
    private Material lineMaterial;
    private float offset;

    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineMaterial = lineRenderer.material;
    }

    void Update()
    {
        offset += Time.deltaTime * scrollSpeed;
        lineMaterial.SetTextureOffset("_MainTex", new Vector2(offset, 0));
    }
}
