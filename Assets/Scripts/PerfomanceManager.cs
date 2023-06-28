using System;
using UnityEngine;
using Application = UnityEngine.Device.Application;

namespace GameSettings
{
    public class PerfomanceManager : MonoBehaviour
    {
        private void Awake()
        {
            AdjustFramerate(60);
        }

        
        /// <summary>
        /// Lock frame rate
        /// </summary>
        /// <param name="fpsLimit"></param>
        private static void AdjustFramerate(int fpsLimit)
        {
            Application.targetFrameRate = fpsLimit;
        }
    }
}