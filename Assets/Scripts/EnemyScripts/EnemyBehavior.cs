using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Enemy
{
    public class EnemyBehavior : MonoBehaviour, IEnemy
    {
        public bool IsAlive { get; private set; } = true;
        public event Action OnDied;
        
        [SerializeField] private EnemyAnimations enemyAnimations;
        [FormerlySerializedAs("_hp")]
        [SerializeField] private float hp = 10;
        private float maxHp;
        private AIManager aiManager;

        private void Awake()
        {
            maxHp = EnemyHp;
        }

        private void Start()
        {
            aiManager = GetComponent<AIManager>();
            enemyAnimations.GetComponent<EnemyAnimations>();
        }

        private void Update()
        {
            BilboardEnemySprite();
            Move();
            Attack();
        }

        public float EnemyHp
        {
            get => hp;
            private set => hp = value;
        }

        public void Attack()
        {
            enemyAnimations.PlayAttackAnimation();
        }

        //TODO: Delete
        public void ChasePlayer()
        {
            throw new System.NotImplementedException();
        }

        public void Death()
        {
            OnDied?.Invoke();
            
            IsAlive = false;
            enemyAnimations.PlayDeathAnimation();
            aiManager.AgentIsActive(false);
            print("Enemy has been slayed");
        }
        
        public void GetDamage(float damage)
        {
            if (!IsAlive) return;
            
            enemyAnimations.PlayOnHitAnimation();

            EnemyHp -= damage;

            if (EnemyHp < 0)
            {
                Death();
            }
        }

        public void Move()
        {
            enemyAnimations.PlayRunAnimation();
        }

        //TODO: Add dissolve VFX
        public void SpawnEnemy()
        {
            throw new System.NotImplementedException();
        }

        //TODO: Move to EnemyAnimations
        private void BilboardEnemySprite()
        {
            if (Camera.main != null)
            {
                transform.rotation = Quaternion.Euler(0f, Camera.main.transform.rotation.eulerAngles.y, 0f);
            }
        }

        private void OnBecameInvisible()
        {
            if (IsAlive) return;
            
            DisableEnemy();
        }

        private void DisableEnemy()
        {
            gameObject.SetActive(false);
        }

        private void OnDisable()
        {
            EnemyHp = maxHp;
        }

        private void OnEnable()
        {
            IsAlive = true;
        }
    }
}