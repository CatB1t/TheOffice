using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class Colleague : MonoBehaviour
{
    [Header("Colleague Setttings")]
    [SerializeField] private float timeOnDesk = 10f;
    [SerializeField] private float timeOutOfDesk = 3f;

    private BotNavigation _botNavigation;
    private SeekPlayer _seekPlayer;

    void Start()
    {
        _botNavigation = GetComponent<BotNavigation>();
        _seekPlayer = GetComponent<SeekPlayer>(); // TODO this is not right to do

        if(_seekPlayer)
            StartCoroutine(PatrolAndSeek());
        else
            StartCoroutine(Patrol());

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
