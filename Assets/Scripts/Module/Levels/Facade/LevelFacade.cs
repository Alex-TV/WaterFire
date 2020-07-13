
using System;
using Module.Levels.Controllers;
using Module.Levels.Models;
using Module.Levels.Providers;
using UnityEngine;

namespace Module.Levels.Facade
{
    public class LevelFacade : ILevelFacade
    {
        private ILoadLevelController _levelController;
        private ILevelProvider _levelProvider;

        public void Init()
        {
            _levelProvider = Resources.Load<LevelProvider>("GameData/Levels/LevelList");
            _levelController = new LoadLevelController(_levelProvider);
        }

        public LevelConfigModel LoadLevel(int levelNum)
        {
            if (_levelController.TryLoadLevel(levelNum, out var level))
            {
                return level;
            }

            if (_levelController.TryLoadRandom(out var levelRnd))
            {
                return levelRnd;
            }

            throw new Exception("No levels");
        }
    }
}
