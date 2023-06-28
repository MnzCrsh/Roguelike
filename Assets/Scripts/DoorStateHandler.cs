using System;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

namespace RoomEvents
{
   public class DoorStateHandler : MonoBehaviour
   {
      [field: SerializeField] public List<Transform> Doors { get; private set; } = new();

      private Transform door;
      
      [Header("Door transform settings")]
      [SerializeField] private Vector3 positionToMove;
      [SerializeField] private float doorAnimationPlayTime;

      [Header("Shake animation settings")]
      [SerializeField] private int shakePlayTime;
      [SerializeField] private float shakeStrength;
      [SerializeField] private int shakeVibrato;
      [SerializeField] private float shakeRandomness;
      [SerializeField] private bool shakeSnapping;
      [SerializeField] private bool shakeFadeOut;

      private void Start()
      {
         EnemySpawner.OnRoomCleared += ChangeDoorState;
      }

      private void HandleDoors(bool open)
      {
         foreach (var door in Doors)
         {
            door.transform.DOShakePosition(shakePlayTime, shakeStrength,
               shakeVibrato, shakeRandomness, shakeSnapping, shakeFadeOut);
            
            if (open.Equals(true))
            {
               print("Door has been opened");
               door.transform.DOMoveY(-positionToMove.y, doorAnimationPlayTime);
            }
            else
            {
               print("Door has been closed");
               door.transform.DOMoveY(positionToMove.y, doorAnimationPlayTime);
            }
         }
      }

      private void ChangeDoorState()
      {
         HandleDoors(open:true);
         EnemySpawner.OnRoomCleared -= ChangeDoorState;
      }

      private void OnTriggerEnter(Collider other)
      {
         if (other.CompareTag("Player"))
         {
            HandleDoors(open:false);
         }
      }
   }
}