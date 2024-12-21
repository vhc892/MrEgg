using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelSelection : MonoBehaviour
{
    [SerializeField] private bool unlocked;
    [SerializeField] private Image lockImage;

    private void Start()
    {
        InitializeLevel();
        //PlayerPrefs.DeleteAll();
    }

    private void InitializeLevel()
    {
        int previousLevelIndex = int.Parse(gameObject.name) - 1;
        if (PlayerPrefs.GetInt("Level" + previousLevelIndex + "Win", 0) == 1)
        {
            unlocked = true;
        }
        lockImage.gameObject.SetActive(!unlocked);
    }

    
    public void PressSelection()
    {
        if (unlocked)
        {
            int level = int.Parse(gameObject.name);
            Debug.Log("load level");
            PlayerPrefs.SetInt("SelectedLevel", level);
            PlayerPrefs.Save();
            SceneManager.LoadScene(1);
        }
    }
}
