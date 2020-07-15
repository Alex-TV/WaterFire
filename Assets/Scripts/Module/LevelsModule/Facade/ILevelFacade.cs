

using Module.Levels.Models;

namespace Module.Levels.Facade
{
    public interface ILevelFacade
    {
        void Init();
        LevelConfigModel LoadLevel(int levelNum);
        LevelConfigModel LoadNextLevel();
        LevelConfigModel GetCurrentLevel();
    }
}
