using Player;
using UnityEngine;
using UnityEngine.VFX;
using Zenject;

namespace Enemy
{
    public class EnemyAnimations : MonoBehaviour, IAnimation
    {
        [SerializeField] private Animator enemyAnimator;
        [SerializeField] private AIManager manager;
        [SerializeField] private VisualEffect bloodBurst;
        [SerializeField] private Transform player;
        
        private bool isDead = false;
        private bool facingRight = false;

        [Inject]
        public void Construct(PlayerBehavior _playerTransform)
        {
            player = _playerTransform.transform;
        }

        private void Update()
        {
            Facing();
        }

        public void PlayAttackAnimation()
        {
            if (manager.enemyIsAttacking)
            {
                enemyAnimator.SetTrigger("Attack");
            }
        }

        public void PlayBattleIdleAnimation()
        {
            throw new System.NotImplementedException();
        }

        public void PlayDeathAnimation()
        {
            isDead = true;
            enemyAnimator.SetTrigger("Death");
        }

        public void PlayIdleAnimation()
        {
            throw new System.NotImplementedException();
        }

        public void PlayOnHitAnimation()
        {
            bloodBurst.GetComponent<VisualEffect>().Play();
            enemyAnimator.SetTrigger("Hurt");
        }

        public void PlayRunAnimation()
        {
            if (manager.enemyIsMoving)
            {
                enemyAnimator.SetInteger("AnimState", 2);
            }
            else
            {
                enemyAnimator.SetInteger("AnimState", 1);
            }
        }

        void Facing()
        {
            if (isDead) return;
            if (player.position.x > transform.position.x && !facingRight)
            {
                //If enemy is by the player right side, flip enemy right
                Flip();
                facingRight = true;
            }
            else if (player.position.x < transform.position.x && facingRight)
            {
                //If enemy is by the player left side, flip enemy left
                Flip();
                facingRight = false;
            }
        }

        void Flip()
        {
            // Switch the way the player is labelled as facing
            facingRight = !facingRight;

            // Multiply the enemy's x local scale by -1
            Vector3 theScale = transform.localScale;
            theScale.x *= -1;
            transform.localScale = theScale;
        }
    }
}