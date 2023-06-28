using UnityEngine;

namespace Player
{
    public class ThirdPersonCamera : MonoBehaviour
    {
        [SerializeField] private Transform lookAt;
        [SerializeField] private Transform camTransform;

        private Camera cam;
        
        [Header("Variables")]
        [SerializeField] private float distance;
        [SerializeField] private float currentX = 0f;
        [SerializeField] private float currentY;
        [SerializeField] private LayerMask walls;
        [SerializeField] private Vector2 camDistanceMinMax = new Vector2 (1f, 5f);

        private void Start()
        {
            camTransform = transform;
            cam = Camera.main;
        }

        private void LateUpdate()
        {
            MoveCamera();
            CameraCollisionCheck();
        }

        /// <summary>
        /// Get camera direction
        /// </summary>
        /// <returns></returns>
        private Vector3 GetDir()
        {
            return new Vector3(0f, 0f, -distance);
        }

        //Camera with fixed rotation
        private void MoveCamera()
        {
            Quaternion rotation = Quaternion.Euler(currentY, currentX, 0);
            camTransform.position = lookAt.position + rotation * GetDir();
        }

        //Zooms camera on collision with the wall
        private void CameraCollisionCheck()
        {
            Vector3 desiredCameraPosition = transform.TransformPoint
                (GetDir() * camDistanceMinMax.y);
            
            RaycastHit hit;
     
            Debug.DrawLine(lookAt.transform.position,
                desiredCameraPosition, Color.magenta);
            
            if (Physics.Linecast(lookAt.transform.position,
                    desiredCameraPosition, out hit, walls))
            {
                distance = Mathf.Clamp(hit.distance * 0.9f,
                    camDistanceMinMax.x, camDistanceMinMax.y);
            }
            else
            {
                distance = camDistanceMinMax.y;
            }
        }
    }
}