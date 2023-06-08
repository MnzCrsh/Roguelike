using UnityEngine;

namespace Player
{
    [RequireComponent(typeof(Rigidbody))]
    public class PlayerController : MonoBehaviour
    {
        public bool playerIsMoving { get; private set; }

        [SerializeField] private float _playerSpeed;
        private Rigidbody _rb;

        public float PlayerSpeed
        {
            get => _playerSpeed;
            set => _playerSpeed = value;
        }

        private void Start()
        {
            _rb = GetComponent<Rigidbody>();
        }

        public void BilboardPlayerSprite()
        {
            transform.rotation = Quaternion.Euler(0f, Camera.main.transform.rotation.eulerAngles.y, 0f);
        }

        //WASD controller
        public void MovePlayer()
        {
                //Move Forward
                if (Input.GetKey(KeyCode.W))
                {
                    _rb.velocity = new Vector3(0f, 0f, z: PlayerSpeed * Time.fixedDeltaTime);
                    playerIsMoving = true;
                }

                //Move Left
                else if (Input.GetKey(KeyCode.A))
                {
                    _rb.velocity = new Vector3(x: -PlayerSpeed * Time.fixedDeltaTime, 0f, 0f);
                    playerIsMoving = true;
                }

                //Move Back
                else if (Input.GetKey(KeyCode.S))
                {
                    _rb.velocity = new Vector3(0f, 0f, z: -PlayerSpeed * Time.fixedDeltaTime);
                    playerIsMoving = true;
                }

                //Move Right
                else if (Input.GetKey(KeyCode.D))
                {
                    _rb.velocity = new Vector3(x: PlayerSpeed * Time.fixedDeltaTime, 0f, 0f);
                    playerIsMoving = true;
                }
                
                else
                {
                    playerIsMoving = false;
                }
        }
    
        //TODO: Add new variable for dash power
        //TODO: Add coroutine
        //TODO: Add method to ICharacter
        //TODO: Add VFX to PlayerAnimations
        public void Dodge()
        {
            if (Input.GetKey(KeyCode.D) && Input.GetKeyDown(KeyCode.Space))
            {
                _rb.AddForce(x: 20, 0f, 0f, ForceMode.Impulse);
                Debug.Log("Dash");
            }

            if (Input.GetKey(KeyCode.A) && Input.GetKeyDown(KeyCode.Space))
            {
                _rb.AddForce(x: -20, 0f, 0f, ForceMode.Impulse);
                Debug.Log("Dash");
            }
        }
    }
}
