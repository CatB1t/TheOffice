using UnityEngine;


// TODO maybe rename to BotBrain?
public class Colleague : MonoBehaviour
{
    [Header("Colleague Setttings")]
    [SerializeField] private float timeOnDesk = 10f;
    [SerializeField] private float timeOutOfDesk = 3f;
    [SerializeField] LayerMask botInteractableMask;

    private BotNavigation _botNavigation;
    private SeekPlayer _seekPlayer;

    private bool _interacted = false;

    #region Patrolling 
    private bool _isCalled = false;
    private bool _currentFlag = true;
    private float _timeOnHold = 0;
    private float _timeToWaitAfterDestination = 0;
    #endregion

    void Start()
    {
        _botNavigation = GetComponent<BotNavigation>();
        _seekPlayer = GetComponent<SeekPlayer>(); // TODO this is not right to do

        GoToNextDestination();
    }

    private void Update()
    {
        // TODO, maybe there's a way to refactor this?
        if (!_botNavigation.IsPending() && !_isCalled)
        {
            Debug.Log("Reached destination");
            _isCalled = true;
            if (!_interacted)
                LookForInteraction();
        }

        if(_isCalled)
        {
            _timeOnHold += Time.deltaTime;
            if(_timeOnHold > _timeToWaitAfterDestination)
            {
                _isCalled = false;
                _timeOnHold = 0;
                GoToNextDestination();
            }
        }
    }

    private void GoToNextDestination()
    {
        if(_currentFlag)
        {
            _botNavigation.GoToBase();
            _timeToWaitAfterDestination = timeOnDesk;
        }
        else
        {
            _botNavigation.GoToDestination();
            _timeToWaitAfterDestination = timeOutOfDesk;
        }

        _currentFlag = !_currentFlag;
    }

    void LookForInteraction()
    {
        Collider[] list = Physics.OverlapSphere(transform.position, 1f, botInteractableMask);
        BotInteractable scriptRef;

        if (list.Length > 0) 
        { 
            scriptRef = list[0].GetComponent<BotInteractable>();
            scriptRef.Interact(_botNavigation);
            _interacted = true;
        }
    }
}
