using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelData", menuName = "Game/Level Data")]
public class LevelPrefabData : ScriptableObject
{
    public GameObject[] levelPrefabs;
}
