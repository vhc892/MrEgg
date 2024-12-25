using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelDisplay : MonoBehaviour
{
    [SerializeField] LevelPrefabData levelPrefabs;
    [SerializeField] LevelSelection levelSelectorPrefab;
    int levelPerGroup = 10;

    GameConfig gameConfig;

    private void Awake()
    {
        gameConfig = GameConfig.Instance;
    }

    private void Start()
    {
        OnStart();
    }

    private void OnStart()
    {
        int totalLevels = levelPrefabs.levelPrefabs.Length;

        GameObject currentGroup = null;
        for (int i  = 0; i < totalLevels; i++)
        {
            if (i % levelPerGroup == 0)
            {
                // Instantiate a new group
                GameObject group = new GameObject("Group " + i/10);
                group.transform.SetParent(transform);

                //Set properties for the group
                RectTransform groupRect = group.AddComponent<RectTransform>();
                GridLayoutGroup groupGrid = group.AddComponent<GridLayoutGroup>();
                groupRect.anchoredPosition = new Vector2(i * 2400, 0);
                groupRect.sizeDelta = new Vector2(900, 350);
                groupGrid.cellSize = new Vector2(150, 150);
                groupGrid.spacing = new Vector2(20, 20);
                groupGrid.childAlignment = TextAnchor.MiddleCenter;

                currentGroup = group;
            }
            LevelSelection levelSelector = Instantiate(levelSelectorPrefab, currentGroup.transform);
            levelSelector.OnInit(i);
        }
    }
}
