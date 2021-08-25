using UnityEngine;
using UnityEngine.AI;
using System.Collections;

[RequireComponent(typeof(NavMeshAgent)), RequireComponent(typeof(BotAnimationController))]
public class BotNavigation : MonoBehaviour
{
    [Header("References")]
    [SerializeField] Transform followTransform;
    [SerializeField] Transform baseTransform;

    private NavMeshAgent _navMeshAgent;
    private Vector3 _currentTarget;
    private BotAnimationController _botAnimator;

    private Vector3 _lastPosition;
    private Quaternion _lastRotation;
    private bool _overrideNavControl = false;

    private Vector3 followPosition;
    private Vector3 basePosition;

    private void Awake()
    {
        _currentTarget = baseTransform.position;
    }

    private void Start()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _botAnimator = GetComponent<BotAnimationController>();

        followPosition = followTransform.position;
        basePosition = baseTransform.position;
    }

    void Update()
    {
        if (_overrideNavControl)
            return;

        RotateTowardDestination();
        _navMeshAgent.SetDestination(_currentTarget);

        _botAnimator.UpdatePlayerSpeed(_navMeshAgent.velocity.magnitude/_navMeshAgent.speed);
    }

    void RotateTowardDestination()
    {
        // TODO Understand this
        Vector3 lookPos = _navMeshAgent.steeringTarget - transform.position;
        lookPos.y = 0;
        if (lookPos.sqrMagnitude < 0.001f)
            return;
        Quaternion rotation = Quaternion.LookRotation(lookPos);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, 5f);
        //transform.rotation = rotation;
    }

    public void GoToBase() => _currentTarget = basePosition;
    public void GoToDestination() => _currentTarget = followPosition;


    public void SitOnChair(Vector3 sitPoint, Quaternion rotation)
    {
        // Logic
        _overrideNavControl = true;
        _navMeshAgent.enabled = false;

        _lastPosition = transform.position;
        _lastRotation = transform.rotation;

       _botAnimator.SitOnChair(true);

        transform.position = sitPoint;
        transform.rotation = rotation;

        StartCoroutine(StepOutOfChair());
       //botAnimator.UpdatePlayerSpeed(0);
    }
    IEnumerator StepOutOfChair()
    {
        yield return new WaitForSeconds(1f); // TODO hardcoded
        _botAnimator.SitOnChair(false);
        transform.position = _lastPosition;
        transform.rotation = _lastRotation;

        _navMeshAgent.enabled = true;
        _overrideNavControl = false;

        GoChaos(); // TODO test
    }

    private bool _isChaos = false;

    public void GoChaos()
    {
        // TODO use coroutine to go every time a new random spot
        basePosition = PickValidRandomSpot(Random.Range(15, 30));
        followPosition = PickValidRandomSpot(Random.Range(3, 5));
        _isChaos = true;
        _navMeshAgent.speed = 7f;
        _botAnimator.GoChaos();
    }

    public bool IsPending()
    {
        if (!_navMeshAgent.pathPending)
        {
            if (_navMeshAgent.remainingDistance <= _navMeshAgent.stoppingDistance)
            {
                if (!_navMeshAgent.hasPath || _navMeshAgent.velocity.sqrMagnitude == 0f)
                {
                    return false;
                }
            }
        }
        return true;
    }

    private Vector3 PickValidRandomSpot(float radius)
    {
        Vector3 randomDirection = Random.insideUnitSphere * radius;
        randomDirection += transform.position;
        NavMeshHit hit;
        Vector3 finalPosition = Vector3.zero;
        if (NavMesh.SamplePosition(randomDirection, out hit, radius, 1))
        {
            finalPosition = hit.position;
        }
        return finalPosition;
    }

}
