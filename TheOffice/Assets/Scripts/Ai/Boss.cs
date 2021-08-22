using System.Collections;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class Boss : MonoBehaviour
{
    [Header("References")]
    [SerializeField] Transform playerFollow;
    [SerializeField] Transform office;

    [Header("Boss settings")]
    [SerializeField] float viewingAngle = 45f;
    [SerializeField] float distanceFromPlayer = 3f;
    [SerializeField] float timeToLookForPlayer = 10f;
    [SerializeField] float timeOnBreak = 30f;

    private NavMeshAgent _navMeshAgent;
    private bool _followPlayer = false;

    void Start()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
        StartCoroutine(LookForPlayer()); // Periodically go and check the player
    }

    void Update()
    {
        RotateTowardDestination();
        if(_followPlayer)
        { 
            _navMeshAgent.destination = playerFollow.position;
            if (IsPlayerInSight())
            {
                Debug.Log("Player caught!");
            }
        }
    }

    void RotateTowardDestination()
    {
        // TODO Understand this
        Vector3 lookPos = _navMeshAgent.destination - transform.position;
        lookPos.y = 0;
        Quaternion rotation = Quaternion.LookRotation(lookPos);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, 5f);
    }

    IEnumerator LookForPlayer()
    {
        while (true) 
        {
            _followPlayer = true;
            yield return new WaitForSeconds(timeToLookForPlayer);
            _followPlayer = false;
            _navMeshAgent.destination = office.position;
            yield return new WaitForSeconds(timeOnBreak);
        }
    }


    bool IsPlayerInSight()
    {
        float angle = Vector3.Angle(transform.forward, playerFollow.position - transform.position);
        float distance = Vector3.Distance(transform.position, playerFollow.position);

        if (angle <= viewingAngle && viewingAngle >= -viewingAngle  && distance < distanceFromPlayer)
            return true;

        return false;
    }
}
