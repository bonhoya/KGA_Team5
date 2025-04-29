using UnityEngine;



public class WaypointPath : MonoBehaviour
{
    private void OnDrawGizmos()
    {
        
        if (transform.childCount == 0) return;

        
        for (int i = 0; i < transform.childCount; i++)
        {
            Transform waypoint = transform.GetChild(i);
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(waypoint.position, 0.2f);

            
            Transform nextWaypoint = transform.GetChild((i + 1) % transform.childCount);
            Gizmos.color = Color.yellow;
            Gizmos.DrawLine(waypoint.position, nextWaypoint.position);
        }
    }
}
