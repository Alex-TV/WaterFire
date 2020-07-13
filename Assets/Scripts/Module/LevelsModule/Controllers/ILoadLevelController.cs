
using Module.Levels.Models;

namespace Module.Levels.Controllers
{
    public interface ILoadLevelController
    {
        bool TryLoadLevel(int levelNum, out LevelConfigModel level);
        bool TryLoadRandom(out LevelConfigModel level);
    }
}
