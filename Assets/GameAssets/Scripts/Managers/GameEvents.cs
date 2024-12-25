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
    public static OnLevelStart onLevelFinish;
    public static void LevelFinish()
    {
        onLevelFinish?.Invoke();
    }
    #endregion
}
