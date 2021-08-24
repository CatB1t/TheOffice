using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class Colleague : MonoBehaviour
{
    [Header("Colleague Setttings")]
    [SerializeField] private float timeOnDesk = 10f;
    [SerializeField] private float timeOutOfDesk = 3f;
    [SerializeField] LayerMask botInteractableMask;

    private BotNavigation _botNavigation;
    private SeekPlayer _seekPlayer;
    private bool _interacted = false;

    void Start()
    {
        _botNavigation = GetComponent<BotNavigation>();
        _seekPlayer = GetComponent<SeekPlayer>(); // TODO this is not right to do

        if(_seekPlayer)
            StartCoroutine(PatrolAndSeek());
        else
            StartCoroutine(Patrol());

    }

    private void Update()
    {
        if(!_interacted)
            LookForInteraction();
    }

    void LookForInteraction()
    {
        // TODO update this!!!!!!!
        // Interaction should happen only at end points of navigation
        // use events with BotNavigation

        Collider[] list = Physics.OverlapSphere(transform.position, 1f, botInteractableMask);
        BotInteractable scriptRef;

        if (list.Length > 0) { 
            scriptRef = list[0].GetComponent<BotInteractable>();
            scriptRef.Interact(_botNavigation);
            _interacted = true;
        }

    }

    IEnumerator Patrol()
    {
        while (true)
        {
            // TODO optimize
            _botNavigation.GoToBase();
            yield return new WaitForSeconds(timeOnDesk);
            _botNavigation.GoToDestination();
            // TODO optimize
            yield return new WaitForSeconds(timeOutOfDesk);
        }
    }

    IEnumerator PatrolAndSeek()
    {
        while (true)
        {
            // TODO optimize
            _botNavigation.GoToBase();
            _seekPlayer.IsSeeking = false;
            yield return new WaitForSeconds(timeOnDesk);
            _seekPlayer.IsSeeking = true;
            _botNavigation.GoToDestination();
            // TODO optimize
            yield return new WaitForSeconds(timeOutOfDesk);
        }
    }

}
