using System.Collections;
using UnityEngine;

[RequireComponent(typeof(BotNavigation))]
[RequireComponent(typeof(SeekPlayer))]
public class Boss : MonoBehaviour
{
   
    [Header("Patrolling Settings")]
    [SerializeField] float timeToLookForPlayer = 10f;
    [SerializeField] float timeOnBreak = 30f;

    private BotNavigation _botNavigation;
    private SeekPlayer _seekPlayer;

    void Start()
    {
        _botNavigation = GetComponent<BotNavigation>();
        _seekPlayer = GetComponent<SeekPlayer>();
        StartCoroutine(Patrol()); // Periodically go and check the player
    }

    IEnumerator Patrol()
    {
        while (true)
        {
            _botNavigation.GoToDestination();
            _seekPlayer.IsSeeking = true;
            yield return new WaitForSeconds(timeToLookForPlayer);
            _botNavigation.GoToBase();
            _seekPlayer.IsSeeking = false;
            yield return new WaitForSeconds(timeOnBreak);
        }
    }

}
