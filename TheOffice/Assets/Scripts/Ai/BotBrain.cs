using UnityEngine;

[RequireComponent(typeof(BotNavigation))]
public class BotBrain : MonoBehaviour
{
    [Header("Bot Patrolling Settings")]
    [SerializeField] private float timeToWaitInBase = 10f;
    [SerializeField] private float timeToWaitOutOfBase = 3f;
    [SerializeField] LayerMask botInteractableMask;

    private BotNavigation _botNavigation;

    private bool _interacted = false;

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
#if UNITY_EDITOR
        Debug.Log("Leaving not base");
#endif
    }

    protected virtual void OnReachDestination()
    {
#if UNITY_EDITOR
        Debug.Log("Reached destination");
#endif

        if(_currentFlag)
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
#if UNITY_EDITOR
        Debug.Log("Reached base");
#endif
    }

    protected virtual void OnReachNotBase()
    {
#if UNITY_EDITOR
        Debug.Log("Reached not base");
#endif
    }

    protected virtual void OnLeaveBase()
    {
#if UNITY_EDITOR
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
        Collider[] list = Physics.OverlapSphere(transform.position, 2f, botInteractableMask);
        BotInteractable scriptRef;

        if (list.Length > 0) 
        { 
            scriptRef = list[0].GetComponent<BotInteractable>();
            scriptRef.Interact(this);
            _interacted = true;
        }
    }

    public void GoChaos()
    {
        timeToWaitInBase = 0;
        timeToWaitOutOfBase = 0;
        _timeToWaitAfterDestination = 0;
        AudioSource srcRef = GetComponent<AudioSource>();
        srcRef.Play();
        _botNavigation.GoChaos();
    }
}
