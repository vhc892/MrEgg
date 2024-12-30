using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartManager : MonoBehaviour
{
    private void Awake()
    {
        PlayerPrefs.DeleteAll();
    }
    void Start()
    {
        if (PlayerPrefs.GetInt("HasPlayed", 0) == 0)
        {
            Debug.Log("first time");
            UIManager.Instance.menuUI.Close();
            SceneManager.LoadScene(1);
        }
        else
        {
            Debug.Log("Had play before");
        }
    }
}
