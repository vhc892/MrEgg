using Hapiga.UI;
using Helper;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HintSystem : BaseUI
{
    public LevelPrefabData levelData;
    private LevelData currentLevelData;
    public int cost = 1;
    private int lightBulb;

    [SerializeField] Button no, yes;
    [SerializeField] TextMeshProUGUI hintCost;
    [SerializeField] TextMeshProUGUI hintText;
    [SerializeField] TextMeshProUGUI lightBulbAmount;
    [SerializeField] UIPanel getBulbPanel;
    public void SetUpLevelData(int level)
    {
        hintText.text = "DO YOU GET ANY TIPS ?";
        currentLevelData = levelData.levelPrefabs[level];
        hintCost.text = "-" + cost.ToString();  
        lightBulb = GameConfig.Instance.LightBulb;
        UpdateCostText();
    }

    void UpdateCostText()
    {
        lightBulbAmount.text = lightBulb.ToString();
    }

    public void ShowHint()
    {
        hintText.text = currentLevelData.rewardHint;
        lightBulb -= cost;
        GameConfig.Instance.LightBulb = lightBulb;
    }

    public void DisableHint()
    {
        Hide();
    }

    public void Hint()
    {
        if (GameConfig.Instance.LightBulb - cost < 0)
        {
            getBulbPanel.Show(true);
        }
        else
        {
            ShowHint();
        }
    }

    public void GetLightBulb()
    {
        lightBulb += cost;
        GameConfig.Instance.LightBulb = lightBulb;
        getBulbPanel.Hide(true);
        Show();
    }

    private void OnEnable()
    {
        GameEvents.onLightBulbChange += UpdateCostText;
    }
    private void OnDisable()
    {
        GameEvents.onLightBulbChange -= UpdateCostText;
    }
}
