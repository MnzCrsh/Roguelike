using UnityEngine;
using UnityEngine.Serialization;

//TODO: Move player stats to SO
//TODO: Change GO to Construct or field injection
//TODO: Implement Death()
namespace Player
{
    public class PlayerBehavior : MonoBehaviour, IPlayer
    {
        [SerializeField] private PlayerCombat combat;
        [SerializeField] private PlayerController controller;
        [SerializeField] private PlayerAnimations playerAnimation;
        [FormerlySerializedAs("_hp")]
        [SerializeField] private float hp;
        [FormerlySerializedAs("_armor")]
        [SerializeField] private int armor;

        public float PlayerHp
        {
            get => hp;
            private set => hp = value;
        }

        void Update()
        {
            Attack();
            Move();
        }

        public void Attack()
        {
            playerAnimation.PlayAttackAnimation();
            combat.PlayerAttack();
        }

        public void Death()
        {
            Debug.Log("You are dead");
        }

        public void GetDamage(float damage)
        {
            playerAnimation.PlayOnHitAnimation();

            PlayerHp -= damage % armor;

            #region Debug incoming damage

            Debug.Log("Incoming damage: " + damage + " " 
                      + "Current HP: " + PlayerHp 
                      + "Damage recieved: " + " " + (damage % armor));
            
            #endregion

            if (PlayerHp <= 0)
            {
                Death();
            }
        }

        public void Move()
        {
            playerAnimation.PlayRunAnimation();

            controller.Dodge();
            controller.MovePlayer();
            controller.BilboardPlayerSprite();
        }

        public void PickUpItem()
        {
            throw new System.NotImplementedException();
        }
    }
}
