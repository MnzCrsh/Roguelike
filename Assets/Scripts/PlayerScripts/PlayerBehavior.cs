using UnityEngine;

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
        [SerializeField] private float _hp;
        [SerializeField] private int _armor;

        public float PlayerHP
        {
            get => _hp;
            set => _hp = value;
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

            PlayerHP -= damage % _armor;

            Debug.Log("Incoming damage: " + damage 
                + " " + "Current HP: " + PlayerHP
                + "Damage recieved: " + " " + (damage % _armor));

            if (PlayerHP <= 0)
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
