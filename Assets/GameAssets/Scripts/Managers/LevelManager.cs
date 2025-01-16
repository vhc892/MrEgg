using Assets.SimpleLocalization.Scripts;
using Helper;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;
   
    //LEVELS
    public LevelPrefabData levelData;
    private GameObject currentLevelPrefab;
    public Levels levelIndex;

    private CharController player;
    public CameraFollow cameraFollow;
    private Vector3 initialCameraPosition;


    [SerializeField] Collider2D col;

    [ShowIf("levelIndex", Levels.level29), EnableIf("levelIndex", Levels.level29)]
    [SerializeField] private GameObject boxPf;
    private List<GameObject> boxes = new List<GameObject>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }
    private void Start()
    {
        initialCameraPosition = Camera.main.transform.position;
        //levelIndex = PlayerPrefs.GetInt("SelectedLevel", 0);
        //LoadLevel(GameConfig.Instance.CurrentLevel);
    }
    public void LoadLevel(int level)
    {
        Camera.main.transform.position = initialCameraPosition;
        if (transform.childCount > 0)
            Destroy(transform.GetChild(0).gameObject);

        col.enabled = false;
        cameraFollow.isCameraFollow = false;
        GameManager.Instance.player.canJumpManyTimes = false;


        if (level < 0 || level > levelData.levelPrefabs.Count)
        {
            Debug.Log(level);
            Debug.LogError($"Level index {level} is out of range. Available levels: 1 to {levelData.levelPrefabs.Count}");
            return;
        }
        if (currentLevelPrefab != null)
        {
            Destroy(currentLevelPrefab);
        }
        levelIndex = levelData.levelPrefabs[level].levelIndex;
        LoadLevelProperties();
        currentLevelPrefab = Instantiate(levelData.levelPrefabs[level].prefabs, this.transform);
        currentLevelPrefab.transform.SetSiblingIndex(0);
        SetUpLevel setUpLevel = currentLevelPrefab.GetComponent<SetUpLevel>();
        setUpLevel.SetLevelData(levelData.levelPrefabs[level]);

        //GameManager.Instance.buttonSequence.StartSequence();
        //LocalizationManager.Language = SaveSystemData.LoadLanguage();
        //LocalizationManager.OnLocalizationChanged?.Invoke();
        //UIManager.Instance.OnLevelLoaded();
    }

    private void LoadLevelProperties()
    {
        switch (levelIndex)
        {
            case Levels.level7:
                cameraFollow.isCameraFollow = true;
                break;
            case Levels.level25:
                cameraFollow.isCameraFollow = true;
                break;
            case Levels.level10:
                GameManager.Instance.player.canJumpManyTimes = true;
                break;
            case Levels.level18:
                GameManager.Instance.player.canJumpManyTimes = true;
                break;
            case Levels.level29:
                col.enabled = true;
                break;
            case Levels.level34:
                GameConfig.Instance.TurnOnMusic();
                Debug.Log("music on");
                break;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayerChild"))
        {
            player = collision.GetComponentInParent<CharController>();
        }
    }

    public void SpawnOnRestart()
    {
        if (player)
        {
            GameObject box = Instantiate(boxPf, transform);
            box.transform.SetParent(transform);
            box.transform.SetSiblingIndex(1);
            box.transform.position = player.endPosition;
            box.GetComponent<Rigidbody2D>().isKinematic = true;
            box.GetComponent<Collider2D>().isTrigger = false;
            boxes.Add(box);
        }
    }

    private void DeleteOnFinishLevel()
    {
        foreach (var item in boxes)
        {
            Destroy(item);
        }
    }

    private void OnEnable()
    {
        GameEvents.onReturnToMenu += DeleteOnFinishLevel;
        GameEvents.onLevelFinish += DeleteOnFinishLevel;
    }
    private void OnDisable()
    {
        GameEvents.onReturnToMenu -= DeleteOnFinishLevel;
        GameEvents.onLevelFinish -= DeleteOnFinishLevel;
    }
}
