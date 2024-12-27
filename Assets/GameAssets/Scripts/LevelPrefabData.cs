using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Helper;

[CreateAssetMenu(fileName = "LevelData", menuName = "Game/Level Data")]
public class LevelPrefabData : ScriptableObject
{
    public List<LevelData> levelPrefabs;
}
