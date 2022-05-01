using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    [SerializeField] private float _spawnPointGizmoRadius;
    public SpawnArea SpawnArea;
    
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, _spawnPointGizmoRadius);
    }
}
