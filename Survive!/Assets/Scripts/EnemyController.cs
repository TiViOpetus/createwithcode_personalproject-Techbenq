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

    private EnemyStats stats;
    private bool canAttack = true;

    private void Start()
    {
        stats = GetComponent<EnemyStats>();

        anim = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.Find("Human").GetComponent<Transform>();
    }

    //if enemy is in attacking distance it will attack otherwise go towards player
    private void Update()
    {
        float distance;
        distance = Vector3.Distance(transform.position, player.position);

        if (distance <= attackDistance)
        {
            FaceTarget();
            Attack();
        }
        else
        {
            if(agent.isOnNavMesh)
                agent.SetDestination(player.position);
        }

        if (agent.velocity.magnitude > 0.2f)
        {
            anim.SetFloat("Speed", 1);
        }
        else
        {
            anim.SetFloat("Speed", 0);
        }
    }

    //Faces toward player when not moving
    private void FaceTarget()
    {
        Vector3 direction = player.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5);
    }


    //Attacks the player if it can
    private void Attack()
    {
        if (canAttack)
        {
            anim.SetTrigger("Attack");
            Invoke("AllowAttack", stats.attackSpeed);
            canAttack = false;
        }
    }

    public void CancelAttack()
    {
        if (!canAttack)
        {
            anim.SetTrigger("Cancel");
            canAttack = true;
        }
    }

    //Allows enemy to attack after the cooldown
    private void AllowAttack()
    {
        canAttack = true;
    }
}
