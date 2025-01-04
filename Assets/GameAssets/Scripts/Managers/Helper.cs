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
    public enum Levels
    {
        level1,
        level2,
        level3,
        level4,
        level5,
        level6,
        level7,
        level8,
        level9,
        level10,
        level11,
        level12,
        level13,
        level14,
        level15,
        level16,
        level17,
        level18,
        level19,
        level20,
        level21,
        level22,
        level23,
        level24,
        level25,
        level26,
        level27,
        level28,
        level29,
        level30
    }
}