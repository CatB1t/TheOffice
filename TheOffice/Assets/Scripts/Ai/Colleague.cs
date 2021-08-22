using UnityEngine;
using UnityEngine.AI;
public class Colleague : MonoBehaviour
{

    [SerializeField] Transform playerFollow;

    private NavMeshAgent _navMeshAgent;

    void Start()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        _navMeshAgent.destination = playerFollow.position;
    }
}
