using System;
using Player;
using UnityEngine;
using UnityEngine.AI;
using Zenject;

  //TODO: Move enemy stats to ScriptableObject
  //TODO: Move NavMeshAgent to EnemyBehavior
  //TODO: Move methods update to EnemyBehavior

namespace Enemy
{
    public class AIManager : MonoBehaviour
    {
        [Header("Variables")]
        [SerializeField] private LayerMask playerLayer;
        [SerializeField] private Transform player;
        [SerializeField] private NavMeshAgent agent;

        [Header("Attack Stats")]
        [SerializeField] private Transform attackPoint;
        [SerializeField] private float attackSpeed;
        [SerializeField] private float attackRange;
        [SerializeField] private float attackDamage;
        private float lastAttackTime;

        [Header("States")]
        [SerializeField] private float enemyVisionRange;
        private bool playerInVisionRange;
        private bool playerInAttackRange;

        public bool enemyIsMoving { get; private set; }
        public bool enemyIsAttacking { get; private set; }

        [Inject]
        public void Construct(PlayerBehavior _playerTransform)
        {
            player = _playerTransform.transform;
        }

        private void Start()
        {
            agent = gameObject.GetComponent<NavMeshAgent>();
        }

        private void Update()
        {
            if (agent.enabled == true)
            {
                CheckPlayerInRange();
                CheckEnemyState();
            }
        }

        private void CheckEnemyState()
        {
            if (playerInVisionRange && !playerInAttackRange)
            {
                ChasePlayer();
                enemyIsMoving = true;
            }
            else
            {
                enemyIsMoving = false;
            }

            if (playerInVisionRange && playerInAttackRange)
            {
                AttackPlayer();
            }
        }

        private void CheckPlayerInRange()
        {
            playerInVisionRange = Physics.CheckSphere(transform.position,
                enemyVisionRange, playerLayer);

            playerInAttackRange = Physics.CheckSphere(transform.position,
                attackRange, playerLayer);
        }

        private void ChasePlayer()
        {
            agent.SetDestination (player.position);
        }

        private void AttackPlayer()
        {
            agent.SetDestination(transform.position);

            if (Time.time - lastAttackTime >= 1f / attackSpeed)
            {
                enemyIsAttacking = true;
                lastAttackTime = Time.time;

                //Debug attack speed
                Debug.DrawRay(attackPoint.position, player.position, Color.blue, 1f);

                //Attack implementation
                Collider[] hitPlayer = Physics.OverlapSphere(attackPoint.position,
                    attackRange, playerLayer);

                foreach (Collider playerCollider in hitPlayer)
                {
                    playerCollider.GetComponent<PlayerBehavior>().GetDamage(attackDamage);
                    Debug.Log("Hit: " + playerCollider.name);
                }
            }
            else
            {
                enemyIsAttacking = false;
            }
        }

        /// <summary>
        /// Enable or disable enemy NavMeshAgent
        /// </summary>
        /// <param name="state"></param>
        public void AgentIsActive(bool state)
        {
            this.agent.enabled = state;
        }

        private void OnEnable()
        {
            AgentIsActive(true);
        }

        private void OnDisable()
        {
            AgentIsActive(false);
        }

        private void OnDrawGizmosSelected()
        {
            //Debug vision range
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(transform.position, enemyVisionRange);

            //Debug attack range
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(attackPoint.position, attackRange);

            //Debug player position
            if (playerInVisionRange) 
            {
                Gizmos.color = Color.blue;
                Gizmos.DrawWireSphere(player.position, 0.5f);
            }
        }
    }
}