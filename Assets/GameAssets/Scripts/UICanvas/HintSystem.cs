using Hapiga.Ads;
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
    [SerializeField] GameObject question;
    [SerializeField] GameObject revealPanel;
    [SerializeField] TextMeshProUGUI hintCost; 
    [SerializeField] TextMeshProUGUI hintText;
    [SerializeField] TextMeshProUGUI hintTextReward;
    [SerializeField] TextMeshProUGUI lightBulbAmount;
    [SerializeField] UIPanel getBulbPanel;
    public void SetUpLevelData(int level)
    {
        if (hintAvailable)
        {
            yes.gameObject.SetActive(false);
            hintTextReward.gameObject.SetActive(true);
            hintText.gameObject.SetActive(false);
            //hintText.text = currentLevelData.rewardHint;
        }
        else
        {
            hintTextReward.gameObject.SetActive(false);
            hintText.gameObject.SetActive(true);

            question.SetActive(true); 
            revealPanel.SetActive(false);
            yes.gameObject.SetActive(true);
            //hintText.text = "DO YOU wANT ANY TIPS ?";
        }
        //currentLevelData = levelData.levelPrefabs[level];
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
        revealPanel.SetActive(true);
        question.SetActive(false);
        hintAvailable = true;

        hintTextReward.gameObject.SetActive(true);
        hintText.gameObject.SetActive(false);


        //hintText.text = currentLevelData.rewardHint;
        lightBulb -= cost;
        GameConfig.Instance.LightBulb = lightBulb;
        UpdateCostText();
        AudioManager.Instance.PlaySFX("GetHint");
        yes.gameObject.SetActive(false);
    }

    public void DisableHint()
    {
        Hide();
        AudioManager.Instance.PlaySFX("ShowHintPanel");
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
        AdManager.Instance.ShowRewardedVideo(CallbackRewardLB, "get_light_bulb");
        //CallbackRewardLB();
    }
    private void CallbackRewardLB()
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
