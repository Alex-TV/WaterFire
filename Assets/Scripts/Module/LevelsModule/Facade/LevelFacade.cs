
using System;
using Module.IUIComponents.Facade;
using Module.Levels.Controllers;
using Module.Levels.Models;
using Module.Levels.Providers;
using UnityEngine;
using View.Helpers;

namespace Module.Levels.Facade
{
    public class LevelFacade : ILevelFacade
    {
        private readonly IUIComponentFacade _uiComponentFacade;
        private ILoadLevelController _levelController;
        private ILevelProvider _levelProvider;

        private LevelConfigModel _currentLevel;

        public LevelFacade(IUIComponentFacade uiComponentFacade)
        {
            _uiComponentFacade = uiComponentFacade;
        }

        public void Init()
        {
            _levelProvider = Resources.Load<LevelProvider>("GameData/Levels/LevelList");
            _levelController = new LoadLevelController(_levelProvider);
        }

        public LevelConfigModel GetCurrentLevel()
        {
            return _currentLevel;
        }

        public LevelConfigModel LoadLevel(int levelNum)
        {
            _currentLevel = null;

            if (_levelController.TryLoadLevel(levelNum, out var level))
            {
                _currentLevel = level;
            }
            else if (_levelController.TryLoadRandom(out var levelRnd))
            {
                _currentLevel = levelRnd;
            }

            if (_currentLevel == null)
            {
                throw new Exception("No levels");

            }

            _uiComponentFacade.OnSendAction(ViewActionType.LevelNumUpdate, new CustomObject(_currentLevel.Num));
            return _currentLevel;
        }

        public LevelConfigModel LoadNextLevel()
        {
            int currentLevelNum = _currentLevel?.Num ?? 1;
            currentLevelNum++;
            if (_levelController.TryLoadLevel(currentLevelNum, out var level))
            {
                _currentLevel = level;
                _uiComponentFacade.OnSendAction(ViewActionType.LevelNumUpdate, new CustomObject(_currentLevel.Num));
                return _currentLevel;
            }
            return LoadLevel(1);
        }
    }
}
