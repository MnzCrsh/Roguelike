using Player;
using UnityEngine;
using UnityEngine.AI;
using Zenject;

  //TODO: Move enemy stats to the new ScriptableObject
  //TODO: Move NavMeshAgent to EnemyBehavior
  //TODO: Move methods update to EnemyBehavior

namespace Enemy
{
    public class AIManager : MonoBehaviour
    {
        public bool EnemyIsMoving { get; private set; }
        public bool EnemyIsAttacking { get; private set; }
        
        [Header("Variables")]
        [SerializeField] private LayerMask playerLayer;
        [SerializeField] private Transform player;
        [SerializeField] private NavMeshAgent agent;
        [SerializeField] private EnemyBehavior enemyBehavior;

        [Header("Attack Stats")]
        [SerializeField] private Transform attackPoint;
        [SerializeField] private float attackSpeed;
        [SerializeField] private float attackRange;
        [SerializeField] private float attackDamage;
        [SerializeField] private float attackDelay;
        private float lastAttackTime;

        [Header("States")]
        [SerializeField] private float enemyVisionRange;
        private bool playerInVisionRange;
        private bool playerInAttackRange;


        [Inject]
        public void Construct(PlayerBehavior playerTransform)
        {
            player = playerTransform.transform;
        }

        private void Start()
        {
            WarpAgent();
            agent = gameObject.GetComponent<NavMeshAgent>();
            enemyBehavior = gameObject.GetComponent<EnemyBehavior>();
        }

        private void Update()
        {
            if (agent.enabled != true)
            {
                return;
            }
            
            CheckPlayerInRange();
            CheckEnemyState();
        }

        private void CheckEnemyState()
        {
            if (playerInVisionRange && !playerInAttackRange)
            {
                ChasePlayer();
                EnemyIsMoving = true;
            }
            else
            {
                EnemyIsMoving = false;
            }

            if (playerInVisionRange && playerInAttackRange)
            {
                Invoke(nameof(AttackPlayer),attackDelay);
            }
        }

        //HACK: Disabled NavMeshAgent will appear at the player spawn position, so it will be placed correctly at the NavMesh.
        public void WarpAgent()
        {
            agent.Warp(player.position);
        }

        private void CheckPlayerInRange()
        {
            var position = transform.position;
            
            playerInVisionRange = Physics.CheckSphere(position,
                enemyVisionRange, playerLayer);

            playerInAttackRange = Physics.CheckSphere(position,
                attackRange, playerLayer);
        }

        private void ChasePlayer()
        {
            agent.SetDestination (player.position);
        }

        private void AttackPlayer()
        {
            if (!enemyBehavior.IsAlive)
            {
                print("Enemy is dead and can't attack");
                return;
            }
            
            agent.SetDestination(transform.position);
            var attackPointPosition = attackPoint.position;
            
            if (Time.time - lastAttackTime >= 1f / attackSpeed)
            {
                EnemyIsAttacking = true;
                lastAttackTime = Time.time;

                //Debug attack speed
                Debug.DrawRay(attackPointPosition, player.position, Color.blue, 1f);

                //Attack implementation
                Collider[] hitPlayer = Physics.OverlapSphere(attackPointPosition,
                    attackRange, playerLayer);

                foreach (Collider playerCollider in hitPlayer)
                {
                    playerCollider.GetComponent<PlayerBehavior>().GetDamage(attackDamage);
                    Debug.Log("Hit: " + playerCollider.name);
                }
            }
            else
            {
                EnemyIsAttacking = false;
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