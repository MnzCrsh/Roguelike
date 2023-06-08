using System;
using UnityEngine;

//TODO: Add isAlive bool
//TODO: Add OnBecomeInvisible SetActive false
namespace Enemy
{
    public class EnemyBehavior : MonoBehaviour, IEnemy
    {
        public static bool isAlive { get; private set; } = true;
        
        [SerializeField] private EnemyAnimations enemyAnimations;
        [SerializeField] private float _hp = 10;
        private float maxHp;

        private void Awake()
        {
            maxHp = EnemyHp;
        }

        private void Start()
        {
            enemyAnimations.GetComponent<EnemyAnimations>();
        }

        private void Update()
        {
            Move();
            Attack();
            BilboardEnemySprite();
        }

        public float EnemyHp
        {
            get => _hp;
            private set => _hp = value;
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
            isAlive = false;
            
            enemyAnimations.PlayDeathAnimation();
            
            GetComponent<AIManager>().AgentIsActive(false);
            Debug.Log("Enemy has been slayed");
        }

        public void GetDamage(float damage)
        {
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
            DisableEnemy();
        }

        private void DisableEnemy()
        {
            if (isAlive)
            {
                return;
            }

            gameObject.SetActive(false);
        }

        private void OnDisable()
        {
            EnemyHp = maxHp;
        }
    }
}