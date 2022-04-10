using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    private NavMeshAgent agent;
    private Animator anim;
    public float attackDistance;
    public Transform player;

    private void Start()
    {
        anim = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.Find("Human").GetComponent<Transform>();
    }

    private void Update()
    {
        float distance;
        distance = Vector3.Distance(transform.position, player.position);

        if(distance <= attackDistance)
        {
            FaceTarget();
            Attack();
        }
        else
        {
            agent.SetDestination(player.position);
        }

        if(agent.velocity.magnitude > 0.2f)
        {
            anim.SetFloat("Speed", 1);
        }
        else
        {
            anim.SetFloat("Speed", 0);
        }
    }

    private void FaceTarget()
    {
        Vector3 direction = player.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5);
    }

    private void Attack()
    {
        Debug.Log("Attacking");
        anim.SetTrigger("Attack");
    }
}
