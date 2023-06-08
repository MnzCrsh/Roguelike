using System;
using UnityEngine;

namespace Pool
{
    [Serializable]
    public class MPool
    {
        public string poolTag;
        public GameObject prefab;
        public int size;
    }
}