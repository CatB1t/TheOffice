using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent)), RequireComponent(typeof(BotAnimationController))]
public class BotNavigation : MonoBehaviour
{
    [Header("References")]
    [SerializeField] Transform followTransform;
    [SerializeField] Transform baseTransform;

    private NavMeshAgent _navMeshAgent;
    private Transform _currentTarget;
    private BotAnimationController _botAnimator;

    private void Awake()
    {
        _currentTarget = baseTransform;
    }

    private void Start()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _botAnimator = GetComponent<BotAnimationController>();
    }

    void Update()
    {
        RotateTowardDestination();
        _navMeshAgent.destination = _currentTarget.position;
        _botAnimator.UpdatePlayerSpeed(_navMeshAgent.velocity.magnitude/_navMeshAgent.speed);
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
    public void GoToDestination() => _currentTarget = followTransform;

    public void SitOnChair(Vector3 sitPoint)
    {
        // Logic
        _botAnimator.SitOnChair(true);
    }
    public void StepOutOfChair()
    {
        _botAnimator.SitOnChair(false);
    }
}
