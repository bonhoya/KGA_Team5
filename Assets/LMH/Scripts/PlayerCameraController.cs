using UnityEngine;

public class PlayerCameraController : MonoBehaviour
{
    [Header("Camera Setting")]
    [SerializeField] private Camera mainCamera;

    [Header("Camera Speed Adjust")]
    [SerializeField] private float cameraMoveSpeed;
    [SerializeField] private float cameraMouseMoveSpeed;

    [Header("ZoomLimit")]
    [SerializeField] private float cameraZoomInLimit;
    [SerializeField] private float cameraZoomOutLimit;

    [Header("SFX Setting")]
    [SerializeField] private AudioClip SelectStageClip;
    private Vector3 inputKeyPos;
    private Vector2 inputMousePos;
    private Vector3 hitRayPos;
    private float mouseScroll;

    private int layerNum;
    private string hitTarget;

    private void Update()
    {
        KeyMove();
        MouseMove();
        MouseZoomInOut();
        DetectCamera();
        if (hitTarget == "Stage4")
        {
            SoundsManager.Instance.SFXPlay("Select", SelectStageClip);
            SceneChangeManager.Instance.ChangeScene("TestBossScene 1");
        }
    }

    private void KeyMove()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        inputKeyPos = new Vector3(x, 0, z).normalized;

        transform.Translate(inputKeyPos * cameraMoveSpeed * Time.deltaTime, Space.World);
    }

    private void MouseMove()
    {
        if (Input.GetMouseButtonDown(1))
        {
            inputMousePos = Input.mousePosition;
        }

        if (Input.GetMouseButton(1))
        {
            Vector2 preMousePos = (Vector2)Input.mousePosition - inputMousePos;
            Vector3 isMovedPos = new Vector3(preMousePos.x, 0, preMousePos.y);
            transform.Translate(isMovedPos * cameraMouseMoveSpeed * Time.deltaTime, Space.World);

            preMousePos = Input.mousePosition;
        }
    }

    private void MouseZoomInOut()
    {
        mouseScroll = Input.mouseScrollDelta.y;
        mainCamera.fieldOfView = Mathf.Clamp(mainCamera.fieldOfView - mouseScroll, cameraZoomInLimit, cameraZoomOutLimit);
    }

    private void OnDrawGizmos()
    {
            Gizmos.color = Color.green;
            Gizmos.DrawLine(transform.position, hitRayPos);
    }

    private void DetectCamera()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hitInfo))
            {
                hitRayPos = hitInfo.point;
                layerNum = hitInfo.collider.gameObject.layer;
                hitTarget = LayerMask.LayerToName(layerNum);
                Debug.Log($"현재 찍힌 위치 {hitRayPos}"); // 위치 확인용 디버그
                Debug.Log($"현재 찍힌 대상 {hitTarget}"); // 대상 확인용 디버그

            }
        }
    }
}
