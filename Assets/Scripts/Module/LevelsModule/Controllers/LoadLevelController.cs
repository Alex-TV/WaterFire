
using System.Linq;
using Module.Levels.Models;
using Module.Levels.Providers;
using UnityEngine;

namespace Module.Levels.Controllers
{
    public class LoadLevelController : ILoadLevelController
    {
        private readonly ILevelProvider _levelProvider;

        public LoadLevelController(ILevelProvider levelProvider)
        {
            _levelProvider = levelProvider;
        }

        public bool TryLoadLevel(int levelNum, out LevelConfigModel level)
        {
            level = LoadLevel(levelNum);
            return level != null;
        }

        public bool TryLoadRandom(out LevelConfigModel level)
        {
            var minLevelNum = _levelProvider.Levels.Select(l => l.Num).Min();
            var maxLevelNum = _levelProvider.Levels.Select(l => l.Num).Max();
            var randomLevelNum = Random.Range(minLevelNum, maxLevelNum + 1);
            level = LoadLevel(randomLevelNum);
            return level != null;
        }

        private LevelConfigModel LoadLevel(int levelNum)
        {
            return _levelProvider.Levels.FirstOrDefault(l => l.Num == levelNum);
        }
    }
}
