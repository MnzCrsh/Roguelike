using UnityEngine;

namespace Room
{
    public class RoomBehavior : MonoBehaviour
    {
        //1)Up 2)Down 3)Left 4)Right
        [SerializeField] private GameObject[] walls;
        [SerializeField] private GameObject[] doors;

        public void UpdateRoom(bool[] status)
        {
            for (int i = 0; i < status.Length; i++)
            {
                doors[i].SetActive(!status[i]);
                walls[i].SetActive(!status[i]);
            }
        }
    }
}
