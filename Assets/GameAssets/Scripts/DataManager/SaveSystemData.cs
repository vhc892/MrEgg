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
}
