public class PlayerData
{
    public int levelPass;
    public int currentLevel;

    public PlayerData(GameData _gameData)
    {
        this.levelPass = _gameData.LevelPass;
        this.currentLevel = _gameData.CurrentLevel;
    }
}
