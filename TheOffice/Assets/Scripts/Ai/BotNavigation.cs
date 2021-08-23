using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class BotNavigation : MonoBehaviour
{
    [Header("References")]
    [SerializeField] Transform followTransform;
    [SerializeField] Transform baseTransform;

    private NavMeshAgent _navMeshAgent;
    private Transform _currentTarget;

    private void Awake()
    {
        _currentTarget = baseTransform;
    }

    private void Start()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        RotateTowardDestination();
        _navMeshAgent.destination = _currentTarget.position;
    }

    void RotateTowardDestination()
    {
        // TODO Understand this
        Vector3 lookPos = _navMeshAgent.destination - transform.position;
        lookPos.y = 0;
        if (lookPos.sqrMagnitude < 0.001f)
            return;
        Quaternion rotation = Quaternion.LookRotation(lookPos);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, 5f);
    }

    public void GoToBase() => _currentTarget = baseTransform;
    public void FollowPlayer() => _currentTarget = followTransform;
}
