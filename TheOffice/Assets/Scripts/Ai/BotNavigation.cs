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
    private Transform _currentTarget;
    private BotAnimationController _botAnimator;

    private Vector3 _lastPosition;
    private Quaternion _lastRotation;
    private bool _overrideNavControl = false;

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
        if (_overrideNavControl)
            return;

        RotateTowardDestination();
        _navMeshAgent.SetDestination(_currentTarget.position);
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

    public void GoToBase() => _currentTarget = baseTransform;
    public void GoToDestination() => _currentTarget = followTransform;


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

    public void GoChaos()
    {
        // TODO pick an empty random spot on Map from ChaosManager
        // go to it and start running in circles
        _navMeshAgent.speed = 4f;
        _botAnimator.GoChaos();
    }
}
