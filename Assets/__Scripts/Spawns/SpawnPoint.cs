using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    private void OnDrawGizmos() 
    {
        // show gizmo on screen (spawn point locations)
        Gizmos.DrawWireSphere(transform.position, 0.25f);
    }
}
