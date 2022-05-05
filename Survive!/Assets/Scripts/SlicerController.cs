using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class SlicerController : Stats
{
    public GameObject itemToDrop;
    public float dropChance;

    public Slider healthBar;
    public Text healthText;

    public float attackRange;
    public float attackSpeed;

    private Animator anim;

    private Transform player;
    private float distance;

    private NavMeshAgent agent;
    private bool canAttack = true;

    public LayerMask groundMask;
    private void Start()
    {
        currentHealth = maxHealth;
        source = GetComponent<AudioSource>();
        anim = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.Find("Human").GetComponent<Transform>();
    }

    private void Update()
    {
        distance = Vector3.Distance(transform.position, player.position);
        if (distance <= attackRange)
        {
            FaceTarget();
            Attack();
        }
        else if(agent.isOnNavMesh)
        {
            agent.SetDestination(player.position);
        }

        if (agent.velocity.magnitude > 0.1f) anim.SetFloat("Speed", 1);
        else anim.SetFloat("Speed", 0);
    }

    public void FaceTarget()
    {
        Vector3 dir = player.position - transform.position;
        Quaternion lookRot = Quaternion.LookRotation(new Vector3(dir.x, 0, dir.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRot, 2 * Time.deltaTime);
    }

    public override void TakeDMG(float dmg)
    {
        base.TakeDMG(dmg);
        currentHealth -= dmg;
        healthBar.value = currentHealth / maxHealth;
        healthText.text = (int)currentHealth / maxHealth * 100 + "%";

        if (currentHealth <= 0)
        {
            if (itemToDrop != null)
            {
                int rand = Random.Range(0, 100);

                if (rand <= dropChance)
                {
                    Vector3 spawnPos;
                    Physics.Raycast(transform.position + Vector3.up * 2.5f, Vector3.down, out RaycastHit hit, 10, groundMask);
                    spawnPos = hit.point;
                    Instantiate(itemToDrop, spawnPos, itemToDrop.transform.rotation);
                }
            }

            EnemySpawner.enemyCount -= 1;
            Invoke("Remove", dmgSound.length);
        }
    }

    public void Attack()
    {
        if (canAttack)
        {
            int attack = Random.Range(0, 3);

            switch (attack)
            {
                case 0:
                    anim.SetTrigger("Spin");
                    break;
                case 1:
                    anim.SetTrigger("Both");
                    break;
                case 2:
                    anim.SetTrigger("Right");
                    break;
            }

            canAttack = false;
            Invoke("AllowAttack", attackSpeed);
        }
    }
    private void AllowAttack()
    {
        canAttack = true;
    }
}
