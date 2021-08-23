using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class Colleague : MonoBehaviour
{
    [Header("Colleague Setttings")]
    [SerializeField] private float timeOnDesk = 10f;
    [SerializeField] private float timeOutOfDesk = 3f;

    private BotNavigation _botNavigation;

    void Start()
    {
        _botNavigation = GetComponent<BotNavigation>();
        StartCoroutine(Patrol());
    }

    IEnumerator Patrol()
    {
        while (true)
        {
            // TODO optimize
            _botNavigation.GoToBase();
            yield return new WaitForSeconds(timeOnDesk);
            _botNavigation.FollowPlayer();
            // TODO optimize
            yield return new WaitForSeconds(timeOutOfDesk);
        }
    }
    
}
