using UnityEngine;
using UnityEngine.AI;
using NavMeshBuilder = UnityEditor.AI.NavMeshBuilder;

namespace Room
{
    public class FloorNavMeshGenerator : MonoBehaviour
    {
        private void Start()
        {
            BakeFloorNavMesh();
        }

        private static void BakeFloorNavMesh()
        {
            NavMeshBuilder.ClearAllNavMeshes();
            NavMeshBuilder.BuildNavMeshAsync();
        }
    }
}