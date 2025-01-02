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

    private bool hintAvailable = false;

    [SerializeField] Button no, yes;
    [SerializeField] TextMeshProUGUI hintCost;
    [SerializeField] TextMeshProUGUI hintText;
    [SerializeField] TextMeshProUGUI lightBulbAmount;
    [SerializeField] UIPanel getBulbPanel;
    public void SetUpLevelData(int level)
    {
        if (hintAvailable)
        {
            yes.gameObject.SetActive(false);
            hintText.text = currentLevelData.rewardHint;
        }
        else
        {

            yes.gameObject.SetActive(true);
            hintText.text = "DO YOU GET ANY TIPS ?";
        }
        currentLevelData = levelData.levelPrefabs[level% 12];
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
        hintAvailable = true;
        hintText.text = currentLevelData.rewardHint;
        lightBulb -= cost;
        yes.gameObject.SetActive(false);
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

    private void ResetHint()
    {
        hintAvailable = false;
    }

    private void OnEnable()
    {
        GameEvents.onLevelStart += ResetHint;
        GameEvents.onLightBulbChange += UpdateCostText;
    }
    private void OnDisable()
    {
        GameEvents.onLevelStart -= ResetHint;
        GameEvents.onLightBulbChange -= UpdateCostText;
    }
}
