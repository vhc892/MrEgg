using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class LevelSelection : MonoBehaviour
{
    [SerializeField] private Image lockImage;

    public TextMeshProUGUI levelText;

    
    int _levelIndex;
    bool _unlocked;

    public void OnInit(int index)
    {
        _levelIndex = index;
        levelText.text = (index+1).ToString();
        if (index > GameConfig.Instance.LevelPass)
        {
            _unlocked = false;
            lockImage.gameObject.SetActive(true);
        }
        else
        {
            _unlocked = true;
            lockImage.gameObject.SetActive(false);
        }
    }

    
    public void PressSelection()
    {
        if (_unlocked)
        {
            int level = int.Parse(levelText.text);
            GameConfig.Instance.CurrentLevel = level;
            SceneManager.LoadScene(1);
        }
    }
}
