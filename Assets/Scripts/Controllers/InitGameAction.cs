
using Controllers.Interfaces;
using Engine.Models.Interfaces;
using EngineCore.GameLoop.Entitys;
using GameLoop;
using GameLoop.Helpers;
using GameLoop.Model;
using Module.Levels.Facade;
using Module.VisualElementsModule.Facade;
using Scripts.Controllers.Helpers;
using UnityEngine;

namespace Controllers
{
    /// <summary> Инициализация игры. </summary>
    public class InitGameAction : MonoBehaviour
    {
        private MainEngineController _mainEngineController;
        private IInputController _inputController;
        private IUpdateController _updateController;
        private ILevelFacade _levelFacade;
        private IGameStateModel _gameStateModel;
        private IVisualElementsFacade _visualElementsFacade;


        private void Awake()
        {
            Bind();
            Init();
        }

        private void Bind()
        {
            _levelFacade = new LevelFacade();
            _inputController = new InputController();
            _visualElementsFacade = new VisualElementsFacade();

            _gameStateModel = new GameStateModel();
            _mainEngineController = new MainEngineController(_gameStateModel, _levelFacade, _visualElementsFacade);

            var objUpdateController = new GameObject("UpdateController");
            _updateController = objUpdateController.AddComponent<UpdateController>();
            _updateController.AddComponent(UpdatablesName.MainEngineController, _mainEngineController);
            _updateController.AddComponent(UpdatablesName.InputController, _inputController);
        }

        private void Init()
        {
            _levelFacade.Init();
            _visualElementsFacade.Init();

            _updateController.Active();
            _mainEngineController.EngineRequest(EventTypeEnum.LevelStart);
        }
    }
}