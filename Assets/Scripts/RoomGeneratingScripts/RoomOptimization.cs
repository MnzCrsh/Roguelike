using UnityEngine;

namespace Room
{
    public class RoomOptimization : MonoBehaviour
    {
        private void ActivateRoom(bool state)
        {
            gameObject.SetActive(state);
        }
        
        private void OnBecameInvisible()
        {
            ActivateRoom(false);
        }

        private void OnBecameVisible()
        {
            ActivateRoom(true);
        }
    }
}