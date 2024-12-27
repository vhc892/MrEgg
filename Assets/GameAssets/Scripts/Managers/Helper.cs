using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using UnityEngine;
namespace Helper
{
    [Serializable]
    public struct LevelData
    {
        public Vector2 startPos;
        public GameObject prefabs;
        public string hint;
        public string rewardHint;
        public int reward_amount;
    }

    public enum GameMode 
    { 
        Pause,
        Menu,
        Gameplay
    }
}