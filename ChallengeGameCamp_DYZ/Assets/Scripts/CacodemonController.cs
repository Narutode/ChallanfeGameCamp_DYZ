using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;


public class CacodemonController : MonoBehaviour
{
    public NavMeshAgent agent;

    public Transform player;

    public LayerMask whatIsGround, whatIsPlayer;

    public int health = 100;

    public TextMeshProUGUI scoreEndGame, scoreInGame;

    public Transform spawnBullet;

    // Patrouiller
    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;

    // Attaquer
    public float timeBetweenAttacks;
    bool alreadyAttacked;
    public GameObject projectile;  //= GameObject.Find("Bullet");

    // États
    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;


    private void Awake()
    {
        player = GameObject.Find("Player").transform;
        projectile = GameObject.Find("Cacodemon-Bullet");
        agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        // Vérif distance d'attaque et champ de vision
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

        // Attribution des différentes fonctions

        if (!playerInSightRange && !playerInAttackRange)
        {
            Patrolling();
            Debug.Log("Patrol");
        }

        if (playerInSightRange && !playerInAttackRange)
        {        
            ChasePlayer();
            Debug.Log("Chase");

        }

        if (playerInSightRange && playerInAttackRange)
        {

            Debug.Log("Attack");
            AttackPlayer();



        }


    }

    private void Patrolling()
    {
        if (!walkPointSet) SearchWalkPoint();
        if (walkPointSet) agent.SetDestination(walkPoint);

        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        // Point atteint
        if (distanceToWalkPoint.magnitude < 1f) walkPointSet = false;
    }

    private void SearchWalkPoint()
    {
        // Calcul d'un point aléatoire 
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        Debug.Log($"SearchWalkPoint : {walkPoint}");
        Debug.DrawRay(transform.position, -transform.up, Color.green);

        if (Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround)) walkPointSet = true;

        Debug.Log($"WalkPointSet : {walkPointSet}");
    }

    private void ChasePlayer()
    {
        agent.SetDestination(player.position);

    }

    private void AttackPlayer()
    {
        // Ennemi s'arrête pour attaquer
        //agent.SetDestination(transform.position);

        transform.LookAt(player);

        

        if (!alreadyAttacked)
        {
            // Attaque
            transform.LookAt(player);
            Rigidbody rb = Instantiate(projectile, spawnBullet.position, Quaternion.identity).GetComponent<Rigidbody>();
            rb.AddForce(transform.forward * 25f, ForceMode.Impulse);
            //rb.AddForce(transform.up * 5f,ForceMode.Impulse);

            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }

    }

    private void ResetAttack()
    {
        alreadyAttacked = false;

    }

    public void TakeDamage(int damage)
    {
        health -= damage;

        if (health <= 0) Invoke(nameof(DestroyEnemy), .5f);
    }

    private void DestroyEnemy()
    {
        int sig = int.Parse(scoreInGame.text);
        sig += 10;
        scoreInGame.text = sig.ToString();
        scoreEndGame.text = sig.ToString();


        Destroy(gameObject);
    }

   /* private void OnCollisionEnter(Collision collision)
    {

        Debug.Log("hit");
        TakeDamage(10);



    }
   */
}