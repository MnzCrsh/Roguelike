using UnityEngine;
using UnityEngine.VFX;

namespace Player
{
    public class PlayerAnimations : MonoBehaviour, IAnimation
    {
        [SerializeField] private PlayerController controller;
        [SerializeField] private PlayerCombat combat;
        [SerializeField] private Animator playerAnimator;
        [SerializeField] private VisualEffect bloodBurst;
        private bool facingRight = false;

        private void Start()
        {
            controller.GetComponent<PlayerController>();
        }

        private void Update()
        {
            Facing();
        }

        public void PlayAttackAnimation()
        {
            if (combat.playerIsAttacking)
            {
                playerAnimator.SetTrigger("Attack");
            }
        }

        public void PlayBattleIdleAnimation()
        {
            throw new System.NotImplementedException();
        }

        public void PlayDeathAnimation()
        {
            throw new System.NotImplementedException();
        }

        public void PlayIdleAnimation()
        {
            throw new System.NotImplementedException();
        }

        public void PlayOnHitAnimation()
        {
            bloodBurst.GetComponent<VisualEffect>().Play();
            playerAnimator.SetTrigger("Hurt");
        }

        public void PlayRunAnimation()
        {
            if (controller.playerIsMoving)
            {
                playerAnimator.SetInteger("AnimState", 2);
            }
            else
            {
                playerAnimator.SetInteger("AnimState", 1);
            }
        }

        void Facing()
        {
            if (Input.GetAxisRaw("Horizontal") > 0.5f || Input.GetAxisRaw("Horizontal") < -0.5f)
            {
                if (Input.GetAxisRaw("Horizontal") > 0.5f && !facingRight)
                {
                    //If we're moving right but not facing right, flip the sprite and set facingRight to true.
                    Flip();
                    facingRight = true;
                }
                else if (Input.GetAxisRaw("Horizontal") < 0.5f && facingRight)
                {
                    //If we're moving left but not facing left, flip the sprite and set facingRight to false.
                    Flip();
                    facingRight = false;
                }
            }
        }

        void Flip()
        {
            // Switch the way the player is labelled as facing
            facingRight = !facingRight;

            // Multiply the player's x local scale by -1
            Vector3 theScale = transform.localScale;
            theScale.x *= -1;
            transform.localScale = theScale;
        }
    }
}