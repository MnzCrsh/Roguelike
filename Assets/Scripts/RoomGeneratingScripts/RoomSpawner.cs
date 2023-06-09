using System;
using System.Collections.Generic;
using UnityEngine;

//TODO: Change prefabs to addresables
namespace Room
{
    public class RoomSpawner : MonoBehaviour
    {
        public GameObject spawnPoint;

        [Header("Room generation settings")]
        [SerializeField] 
        private RoomSpawnRules[] roomPrefabs;

        [Tooltip("Size of the map")]
        [SerializeField] 
        private Vector2Int gridSize;

        [Tooltip("Offset between the generated rooms")]
        [SerializeField] 
        private Vector2 offset;

        [SerializeField] 
        private int startPosition;

        [Tooltip("Limit of the rooms that will be generated")]
        [SerializeField] 
        private int roomLimit;
        [Tooltip("Room# that will fill the level")]
        [SerializeField] 
        private int standartRoomNumber;

        private List<GridCell> gridMap;

        private void Start()
        {
            CreateGrid();
        }

        /// <summary>
        /// Instantiate room prefabs
        /// </summary>
        private void CreateDungeon()
        {
            for (int i = 0; i < gridSize.x; i++)
            {
                for (int j = 0; j < gridSize.y; j++)
                    GenerateRooms(i, j);
            }
        }

        private void GenerateRooms(int i, int j)
        {
            GridCell currentCell = gridMap[(i + j * gridSize.x)];
            if (currentCell._visitedCell)
            {
                int randomRoom = -1;
                List<int> availableRoomsList = new List<int>();

                for (int k = 0; k < roomPrefabs.Length; k++)
                {
                    int probability = roomPrefabs[k].ProbabilityOfSpawning(i, j);

                    if (probability.Equals(2))
                    {
                        randomRoom = k;
                        break;
                    }
                    else if (probability.Equals(1))
                    {
                        availableRoomsList.Add(k);
                    }
                }

                if (randomRoom.Equals(-1))
                {
                    if (availableRoomsList.Count > 0)
                    {
                        randomRoom = availableRoomsList[UnityEngine.Random.Range(0, availableRoomsList.Count)];
                    }
                    else
                    {
                        randomRoom = standartRoomNumber;
                    }
                }

                var newRoom = Instantiate(roomPrefabs[randomRoom].room, new Vector3
                    (i * offset.x, 0, -j * offset.y), Quaternion.identity, transform)
                    .GetComponent<RoomBehavior>();

                newRoom.UpdateRoom(currentCell._cellStatus);
                newRoom.name += " " + i + "-" + j;
            }
        }

        /// <summary>
        /// Creating grid of cells for dungeon
        /// </summary>
        private void CreateGrid()
        {
            int loopCount = 0;
            int currentCell = startPosition;

            gridMap = new List<GridCell>();
            Stack<int> path = new Stack<int>();

            for (int i = 0; i < gridSize.x; i++)
            {
                for (int j = 0; j < gridSize.y; j++)
                {
                    gridMap.Add(new GridCell());
                }
            }

            while (loopCount < roomLimit)
            {
                loopCount++;
                gridMap[currentCell]._visitedCell = true;

                if (currentCell.Equals(gridMap.Count - 1))
                {
                    break;
                }

                List<int> neighbors = CheckNeighbors(currentCell);

                if (neighbors.Count.Equals(0))
                {
                    if (path.Count.Equals(0))
                    {
                        break;
                    }
                    else
                    {
                        currentCell = path.Pop();
                    }
                }
                else
                {
                    path.Push(currentCell);
                    int newCell = neighbors[UnityEngine.Random.Range(0, neighbors.Count)];

                    if (newCell > currentCell)
                    {
                        CheckPathGoingDownOrRight(ref currentCell, ref newCell);
                    }
                    else
                    {
                        CheckPathGoingUpOrLeft(ref currentCell, ref newCell);
                    }
                }
            }

            CreateDungeon();
        }


        /// <summary>
        /// checks if path going up or left, then opens left and down doors 
        /// </summary>
        /// <param name="currentCell"></param>
        /// <param name="newCell"></param>
        private void CheckPathGoingUpOrLeft(ref int currentCell, ref int newCell)
        {
            //checks if path going up or left
            if (newCell + 1 == (currentCell))
            {
                gridMap[currentCell]._cellStatus[3] = true;
                currentCell = newCell;
                gridMap[currentCell]._cellStatus[2] = true;
            }
            else
            {
                gridMap[currentCell]._cellStatus[0] = true;
                currentCell = newCell;
                gridMap[currentCell]._cellStatus[1] = true;
            }
        }

        /// <summary>
        /// checks if path going down or right, then opens down and left doors 
        /// </summary>
        /// <param name="currentCell"></param>
        /// <param name="newCell"></param>
        private void CheckPathGoingDownOrRight(ref int currentCell, ref int newCell)
        {
            //checks if path going down or right
            if (newCell - 1 == (currentCell))
            {
                gridMap[currentCell]._cellStatus[2] = true;
                currentCell = newCell;
                gridMap[currentCell]._cellStatus[3] = true;
            }
            else
            {
                gridMap[currentCell]._cellStatus[1] = true;
                currentCell = newCell;
                gridMap[currentCell]._cellStatus[0] = true;
            }
        }

        private List<int> CheckNeighbors(int cell)
        {
            var neighborPositionUp = (cell - gridSize.x);
            var neighborPositionDown = (cell + gridSize.x);
            var neighborPositionRight = (++cell);
            var neighborPositionLeft = (--cell);

            List<int> neighbors = new List<int>();

            //Check upper neighbor position
            if (cell - gridSize.x >=0 && 
                !gridMap [neighborPositionUp]._visitedCell)
            {
                neighbors.Add(neighborPositionUp);
            }

            //Check down neighbor position
            if (cell + gridSize.x < gridMap.Count && 
                !gridMap[neighborPositionDown]._visitedCell)
            {
                neighbors.Add(neighborPositionDown);
            }

            //Check right neighbor position
            //If dont work, change increment to +1
            if (++cell % gridSize.x != 0 && 
                !gridMap[neighborPositionRight]._visitedCell)
            {
                neighbors.Add(neighborPositionRight);
            }

            //Check left neighbor position
            if (cell % gridSize.x != 0 && 
                !gridMap[neighborPositionLeft]._visitedCell)
            {
                neighbors.Add(neighborPositionLeft);
            }

            return neighbors;
        }
    }
}