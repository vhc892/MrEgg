using Hapiga.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelDisplay : BaseUI
{
    [SerializeField] LevelPrefabData levelPrefabs;
    [SerializeField] LevelSelection levelSelectorPrefab;
    public RectTransform levelPage;
    int levelPerGroup = 10;

    GameConfig gameConfig;

    private void Awake()
    {
        gameConfig = GameConfig.Instance;
    }

    private void Start()
    {

    }

    public void OnStart()
    {
        int totalLevels = levelPrefabs.levelPrefabs.Count;

        GameObject currentGroup = null;
        for (int i  = 0; i < totalLevels; i++)
        {
            if (i % levelPerGroup == 0)
            {
                // Instantiate a new group
                string groupName = "Group " + i / 10;
                Transform existingGroup = levelPage.Find(groupName);
                if (existingGroup != null)
                {
                    Destroy(existingGroup.gameObject);
                }
                GameObject group = new GameObject(groupName);
                group.transform.SetParent(levelPage);
                group.transform.localScale = Vector3.one;


                //Set properties for the group
                RectTransform groupRect = group.AddComponent<RectTransform>();
                GridLayoutGroup groupGrid = group.AddComponent<GridLayoutGroup>();
                groupRect.anchoredPosition = new Vector2(i * 2400, 0);
                groupRect.sizeDelta = new Vector2(1768, 769);
                groupGrid.cellSize = new Vector2(191, 193);
                groupGrid.spacing = new Vector2(30, 40);
                groupGrid.childAlignment = TextAnchor.MiddleCenter;
                groupGrid.constraint = GridLayoutGroup.Constraint.FixedRowCount;
                groupGrid.constraintCount = 2;

                currentGroup = group;
            }
            LevelSelection levelSelector = Instantiate(levelSelectorPrefab, currentGroup.transform);
            levelSelector.OnInit(i);
        }
    }
}
