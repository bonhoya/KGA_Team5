using UnityEngine;

public class PlayerCameraController : MonoBehaviour
{
    [Header("Camera Speed Adjust")]
    [SerializeField] private Camera mainCamera;

    [Header("Camera Speed Adjust")]
    [SerializeField] private float cameraMoveSpeed;
    [SerializeField] private float cameraMouseMoveSpeed;
    [SerializeField] private float cameraMouseZoomSpeed;

    [Header("ZoomLimit")]
    [SerializeField] private float cameraZoomInLimit;
    [SerializeField] private float cameraZoomOutLimit;

    private Vector3 inputKeyPos;
    private Vector2 inputMousePos;
    private Vector3 hitRayPos;
    private float mouseScroll;

    private int layerNum;
    private string hitTarget;

    private void Update()
    {
        MouseMove();
        MouseZoomInOut();
        DetectCamera();
        if (hitTarget == "Stage1")
        {
            SceneChangeManager.Instance.ChangeScene("Map1");
        }
        if(hitTarget == "Stage2" && GameManager.Instance.isClearedStageOne == true)
        {
            SceneChangeManager.Instance.ChangeScene("Map2");
        }
        if (hitTarget == "Stage3" && GameManager.Instance.isClearedStageTwo == true)
        {
            SceneChangeManager.Instance.ChangeScene("Map3");
        }
        if (hitTarget == "Stage4" && GameManager.Instance.isClearedStageThr == true)
        {
            SceneChangeManager.Instance.ChangeScene("Map4");
        }
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
