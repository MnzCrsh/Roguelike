// using UnityEngine;
// using UnityEngine.AddressableAssets;
// using UnityEngine.ResourceManagement.AsyncOperations;
//
// namespace Room
// {
//     public class SpawnLevelAddresables : MonoBehaviour
//     {
//         [SerializeField] private AssetReference[] roomAssets;
//
//         public void LevelAddresables()
//         {
//             for (int i = 0; i < roomAssets.Length; i++)
//             {
//                 GetAddresables(i);
//             }
//         }
//
//         private void GetAddresables(int i)
//         {
//             roomAssets[i].LoadAssetAsync<GameObject>().Completed += (asyncOperationHandle) =>
//             {
//                 if (asyncOperationHandle.Status.Equals(AsyncOperationStatus.Succeeded))
//                 {
//                     Instantiate(asyncOperationHandle.Result);
//                 }
//                 else
//                 {
//                     Debug.Log("Failed to load");
//                 }
//             };
//         }
//     }
// }