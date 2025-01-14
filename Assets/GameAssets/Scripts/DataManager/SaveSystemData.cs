using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public static class SaveSystemData
{
    static string savepath = Path.Combine(Application.persistentDataPath, "NeiDatas");

    #region player Data
    public static string playerDataFileName = "playerData.json";
    public static void SavePlayer(GameData _gameData)
    {
        if (!Directory.Exists(savepath))
        {
            Directory.CreateDirectory(savepath);
        }

        string path = Path.Combine(savepath, playerDataFileName);
        PlayerData playerData = new PlayerData(_gameData);
        string json = JsonUtility.ToJson(playerData);

        File.WriteAllText(path, json);

        Debug.Log("File saved at " + path);
    }
    public static PlayerData LoadPlayerData()
    {
        string path = Path.Combine(savepath, playerDataFileName);

        if (File.Exists(path))
        {
            try
            {
                string json = File.ReadAllText(path);
                PlayerData data = JsonUtility.FromJson<PlayerData>(json);

                if (data == null)
                {
                    Debug.LogError("Deserialization failed. Data is null.");
                }

                return data;
            }
            catch (Exception e)
            {
                Debug.LogError("Failed to load file: " + e.Message);
                return null;
            }
        }
        else
        {
            Debug.Log("Save file not found in " + path);
            return null;
        }
    }
    #endregion

    #region setting Data
    public static string SettingDataFileName = "settings.json";
    public static void SaveSettingsData(bool _IsMusicOn, bool __IsSFXOn)
    {
        if (!Directory.Exists(savepath))
        {
            Directory.CreateDirectory(savepath);
        }

        string path = Path.Combine(savepath, SettingDataFileName);
        SettingData data = new SettingData(_IsMusicOn, __IsSFXOn);
        string json = JsonUtility.ToJson(data);

        File.WriteAllText(path, json);

        Debug.Log("File saved at " + path);
    }

    public static SettingData LoadSettingsData()
    {
        string path = Path.Combine(savepath, SettingDataFileName);

        if (File.Exists(path))
        {
            try
            {
                string json = File.ReadAllText(path);

                SettingData data = JsonUtility.FromJson<SettingData>(json);

                if (data == null)
                {
                    Debug.LogError("Deserialization failed. Data is null.");
                }

                return data;
            }
            catch (Exception e)
            {
                Debug.LogError("Failed to load file: " + e.Message);
                return null;
            }
        }
        else
        {
            Debug.Log("Save file not found in " + path);
            return null;
        }
    }
    #endregion

    #region language
    public static string LanguageDataFileName = "language.json";
    public static void SaveLanguage(string language)
    {
        if (!Directory.Exists(savepath))
        {
            Directory.CreateDirectory(savepath);
        }

        string path = Path.Combine(savepath, LanguageDataFileName);
        File.WriteAllText(path, language);

        Debug.Log("Language saved at " + path);
    }
    public static string LoadLanguage()
    {
        string path = Path.Combine(savepath, LanguageDataFileName);

        if (File.Exists(path))
        {
            try
            {
                string language = File.ReadAllText(path);
                Debug.Log("Language loaded: " + language);
                return language;
            }
            catch (Exception e)
            {
                Debug.LogError("Failed to load language file: " + e.Message);
                return "English";
            }
        }
        else
        {
            Debug.Log("Language file not found in " + path);
            return "English";
        }
    }

    #endregion

}
