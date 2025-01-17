public static class GameEvents
{
    #region Ingame Events
    public delegate void OnLevelStart();
    public static OnLevelStart onLevelStart;    
    public static void LevelStart()
    {
        onLevelStart?.Invoke();
    }

    public delegate void OnLevelFinish();
    public static OnLevelFinish onLevelFinish;
    public static void LevelFinish()
    {
        onLevelFinish?.Invoke();
    }

    public delegate void OnLevelPause();
    public static OnLevelPause onLevelPause;
    public static void LevelPause()
    {
        onLevelPause?.Invoke();
    }

    public delegate void OnLevelResume();
    public static OnLevelResume onLevelResume;
    public static void LevelResume()
    {
        onLevelResume?.Invoke();
    }

    public delegate void OnLevelRestart();
    public static OnLevelRestart onLevelRestart;
    public static void LevelRestart()
    {
        onLevelRestart?.Invoke();
    }

    public delegate void OnDoorUnlocked();
    public static OnDoorUnlocked onDoorUnlocked;
    public static void UnlockDoor()
    {
        onDoorUnlocked?.Invoke();
    }

    public delegate void OnReturnToMenu();
    public static OnReturnToMenu onReturnToMenu;
    public static void ReturnToMenu()
    {
        onReturnToMenu?.Invoke();
    }
    #endregion

    #region VALUES
    public delegate void OnLevelIndexChange();
    public static OnLevelIndexChange onLevelIndexChange;
    public static void LevelIndexChange()
    {
        onLevelIndexChange?.Invoke();
    }

    public delegate void OnLightBulbChange();
    public static OnLightBulbChange onLightBulbChange;

    public static void LightBulbChange()
    {
        onLightBulbChange?.Invoke();
    }
    #endregion

    #region UI
    public delegate void OnDisableIngameButton();
    public static OnDisableIngameButton onDisableIngameButton;
    public static void DisableIngameButton()
    {
        onDisableIngameButton?.Invoke();
    }
    #endregion
}
