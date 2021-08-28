using UnityEngine;

[RequireComponent(typeof(BotNavigation))]
public class BotBrain : MonoBehaviour
{
    [Header("Bot Patrolling Settings")]
    [SerializeField] private float timeToWaitInBase = 10f;
    [SerializeField] private float timeToWaitOutOfBase = 3f;
    [SerializeField] LayerMask botInteractableMask;
    [SerializeField] bool shouldInteractOnReach = true;
    [SerializeField] private bool shouldInteractOnAwake = false;

    private BotNavigation _botNavigation;

    #region Patrolling 
    private bool _isCalled = false;
    private bool _currentFlag = true;
    private float _timeOnHold = 0;
    private float _timeToWaitAfterDestination = 0;
    #endregion

    protected virtual void Start()
    {
        _botNavigation = GetComponent<BotNavigation>();

        GoToNextDestination();

        if(shouldInteractOnAwake)
            LookForInteraction();
    }

    private void Update()
    {
        // TODO, maybe there's a way to refactor this?
        if (!_botNavigation.IsPending() && !_isCalled)
        {
            _isCalled = true;
            OnReachDestination();
        }

        if(_isCalled)
        {
            _timeOnHold += Time.deltaTime;
            if(_timeOnHold > _timeToWaitAfterDestination)
            {
                _timeOnHold = 0;
                _isCalled = false;
                GoToNextDestination();
            }
        }
    }

    protected virtual void OnLeaveNotBase()
    {
#if false
        Debug.Log("Leaving not base");
#endif
    }

    protected virtual void OnReachDestination()
    {
#if false
        Debug.Log("Reached destination");
#endif

        if (_currentFlag)
        {
            OnReachNotBase();
        }
        else
        {
            OnReachBase();
        }

        LookForInteraction();
    }

    protected virtual void OnReachBase()
    {
#if false
        Debug.Log("Reached base");
#endif
    }

    protected virtual void OnReachNotBase()
    {
#if false
        Debug.Log("Reached not base");
#endif
    }

    protected virtual void OnLeaveBase()
    {
#if false
        Debug.Log("Leaving base");
#endif
    }

    private void GoToNextDestination()
    {
        LookForInteraction();

        if (_currentFlag) // If last position was not base
        {
            OnLeaveNotBase();
            _botNavigation.GoToBase();
            _timeToWaitAfterDestination = timeToWaitInBase;
        }
        else // If last position was base
        {
            OnLeaveBase();
            _botNavigation.GoToDestination();
            _timeToWaitAfterDestination = timeToWaitOutOfBase;
        }

        _currentFlag = !_currentFlag;
    }

    private void LookForInteraction()
    {
        if (_chaosModeOn || !shouldInteractOnReach)
            return;

        Collider[] list = new Collider[1];
        int num = Physics.OverlapSphereNonAlloc(transform.position, 2f, list, botInteractableMask);
        BotInteractable scriptRef;

        if (num > 0) 
        {
            Debug.Log(list[0].gameObject.name);
            scriptRef = list[0].GetComponent<BotInteractable>();
            scriptRef.Interact(this);
            _IsInteracted = !_IsInteracted;
        }
    }

    private bool _chaosModeOn = false;
    private bool _IsInteracted = false;

    public void GoChaos()
    {
        if (_IsInteracted)
            LookForInteraction();

        _chaosModeOn = true;
        timeToWaitInBase = 0;
        timeToWaitOutOfBase = 0;
        _timeToWaitAfterDestination = 0;

        AudioSource srcRef = GetComponent<AudioSource>();
        srcRef.Play();
        _botNavigation.GoChaos();
    }
}
