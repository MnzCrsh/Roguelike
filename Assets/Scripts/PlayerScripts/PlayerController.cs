using UnityEngine;
using UnityEngine.Serialization;

//TODO: Change current controller to axis
namespace Player
{
    [RequireComponent(typeof(Rigidbody))]
    public class PlayerController : MonoBehaviour
    {
        public bool PlayerIsMoving { get; private set; }

        [FormerlySerializedAs("_playerSpeed")] 
        [SerializeField] private float playerSpeed;
        private Rigidbody rb;
        private Camera mainCamera;
        private bool ismainCameraNotNull;

        public float PlayerSpeed
        {
            get => playerSpeed;
            private set => playerSpeed = value;
        }

        private void Start()
        {
            ismainCameraNotNull = mainCamera != null;
            mainCamera = Camera.main;
            rb = GetComponent<Rigidbody>();
        }

        public void BilboardPlayerSprite()
        {
            if (ismainCameraNotNull)
            {
                transform.rotation = Quaternion.Euler(0f, mainCamera.transform.rotation.eulerAngles.y, 0f);
            }
        }

        //WASD controller
        public void MovePlayer()
        {
                //Move Forward
                if (Input.GetKey(KeyCode.W))
                {
                    rb.velocity = new Vector3(0f, 0f, z: PlayerSpeed * Time.fixedDeltaTime);
                    PlayerIsMoving = true;
                }

                //Move Left
                else if (Input.GetKey(KeyCode.A))
                {
                    rb.velocity = new Vector3(x: -PlayerSpeed * Time.fixedDeltaTime, 0f, 0f);
                    PlayerIsMoving = true;
                }

                //Move Back
                else if (Input.GetKey(KeyCode.S))
                {
                    rb.velocity = new Vector3(0f, 0f, z: -PlayerSpeed * Time.fixedDeltaTime);
                    PlayerIsMoving = true;
                }

                //Move Right
                else if (Input.GetKey(KeyCode.D))
                {
                    rb.velocity = new Vector3(x: PlayerSpeed * Time.fixedDeltaTime, 0f, 0f);
                    PlayerIsMoving = true;
                }
                
                else
                {
                    PlayerIsMoving = false;
                }
        }
    
        //TODO: Add new variable for dash power
        //TODO: Add coroutine
        //TODO: Add VFX to PlayerAnimations
        public void Dodge()
        {
            if (Input.GetKey(KeyCode.D) && Input.GetKeyDown(KeyCode.Space))
            {
                rb.AddForce(x: 20, 0f, 0f, ForceMode.Impulse);
                print("Dash right");
            }

            if (Input.GetKey(KeyCode.A) && Input.GetKeyDown(KeyCode.Space))
            {
                rb.AddForce(x: -20, 0f, 0f, ForceMode.Impulse);
                print("Dash left");
            }
        }
    }
}
