public class PlayerData
{
    public int levelPass;
    public int currentLevel;
    public int lightBulb;

    public PlayerData(GameData _gameData)
    {
        this.levelPass = _gameData.LevelPass;
        this.currentLevel = _gameData.CurrentLevel;
        this.lightBulb = _gameData.LightBulb;
    }
}
