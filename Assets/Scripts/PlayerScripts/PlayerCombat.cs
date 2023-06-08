using UnityEngine;
using Enemy;

namespace Player
{
    public class PlayerCombat : MonoBehaviour
    {
        [HideInInspector]public bool playerIsAttacking = false;

        [Header("Variables")]
        [SerializeField] private Transform attackPoint;
        [SerializeField] private LayerMask enemyLayers;

        [Header("Player attack stats")]
        [SerializeField] private float range = 0.5f;
        [SerializeField] private float playerDamage = 5;
        [SerializeField] private float playerAttackSpeed = 4f;

        private float lastAttackTime = 0f;

        public void PlayerAttack()
        {
            if (Time.time >= lastAttackTime)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    playerIsAttacking = true;

                    //Debug player attack and attack speed
                    Debug.DrawRay(attackPoint.position, attackPoint.position * 2, Color.red);

                    //Casts attack sphere AKA Player attack
                    Collider[] hitEnemies = Physics.OverlapSphere(attackPoint.position, range, enemyLayers);

                    //Deals damage to the every enemy in attack range
                    foreach (Collider enemy in hitEnemies)
                    {
                        enemy.GetComponent<EnemyBehavior>().GetDamage(playerDamage);

                        Debug.Log("Hit:" + enemy.name);
                    }

                    //Delay between player attacks
                    lastAttackTime = Time.time + 1f / playerAttackSpeed;
                }
            }
            else
            {
                playerIsAttacking = false;
            }
        }

        //Debug player attack range
        private void OnDrawGizmosSelected()
        {
            Gizmos.DrawWireSphere(attackPoint.position, range);  
        }
    }
}