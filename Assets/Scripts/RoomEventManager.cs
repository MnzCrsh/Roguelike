using Enemy;
using UnityEngine;

public class RoomEventManager : MonoBehaviour
{
    private void Update()
    {
        CheckEnemyInRoom();
    }

    private void CheckEnemyInRoom()
    {
        if (!EnemyBehavior.isAlive)
        {
            throw new System.NotImplementedException();
        }
    }
}
