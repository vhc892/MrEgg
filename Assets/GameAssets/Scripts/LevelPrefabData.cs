using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Helper;
using Sirenix.OdinInspector;

[CreateAssetMenu(fileName = "LevelData", menuName = "Game/Level Data")]
public class LevelPrefabData : ScriptableObject
{
    [TableList]
    public List<LevelData> levelPrefabs;
}
