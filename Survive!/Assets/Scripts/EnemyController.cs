using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    private NavMeshAgent agent;
    public float attackDistance;
    public Transform player;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.Find("Human").GetComponent<Transform>();
    }

    private void Update()
    {
        agent.SetDestination(player.position);
    }
}
