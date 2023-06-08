using System;
using UnityEngine;

namespace Room
{
    [Serializable]
    public class RoomSpawnRules
    {
        [Tooltip("Drag and drop room prefab here")]
        public GameObject room;

        [SerializeField] private Vector2Int minPosition;
        [SerializeField] private Vector2Int maxPosition;
        [SerializeField] private bool requiredRoom;

        public int ProbabilityOfSpawning(int x, int y)
        {
            //0: Can't spawn 1: Might spawn 2: Has to spawn
            if (x >= minPosition.x && x <= maxPosition.x
                && y >= minPosition.y && y <= maxPosition.y)
            {
                return requiredRoom ? 2 : 1;
            }

            return 0;
        }
    }
}
