using UnityEngine;

// TODO rename class probably
public class SeekPlayer : MonoBehaviour
{

    [Header("References")]
    [SerializeField] Transform player;

    [Header("Vision settings")]
    [SerializeField] float viewingAngle = 45f;
    [SerializeField] float distanceFromPlayer = 3f;

    private bool _shouldLookForPlayer = false;

    public bool IsSeeking { get { return _shouldLookForPlayer; } set { _shouldLookForPlayer = value; } }


    private void Update()
    {
        if (_shouldLookForPlayer)
        {
            if (IsPlayerInSight())
            {
                // TODO use unity event to tell game manager player is caught.
                Debug.Log("Player caught!");
            }
        }
    }

  
    bool IsPlayerInSight()
    {
        Vector3 direction = player.position - transform.position;
        float angle = Vector3.Angle(transform.forward, direction);
        float distance = Vector3.Distance(transform.position, player.position);

        if (angle <= viewingAngle && viewingAngle >= -viewingAngle && distance < distanceFromPlayer)
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, direction, out hit, distanceFromPlayer))
            { 
                if(hit.collider.CompareTag("Player"))
                    return true;
            }
        }

        return false;
    }
}
