
using Controllers.Interfaces;
using GameLoop;
using GameLoop.Helpers;
using GameLoop.Model;
using Module.Input.Facade;
using Module.Input.Facade.Controllers;
using Module.IUIComponents.Facade;
using Module.Levels.Facade;
using Module.VisualElementsModule.Facade;
using Scripts.Controllers.Helpers;
using UnityEngine;
using View;
using View.Helpers;

namespace Controllers
{
    /// <summary> Инициализация игры. </summary>
    public class InitGameAction : MonoBehaviour
    {
        private MainEngineController _mainEngineController;
        private IInputController _inputController;
        private IUpdateController _updateController;
        private ILevelFacade _levelFacade;
        private IVisualElementsFacade _visualElementsFacade;
        private IInputFacade _inputFacade;
        private IUIComponentFacade _uiComponentFacade;

        private void Awake()
        {
            Bind();
            Init();
        }

        private void Bind()
        {
            _uiComponentFacade = new UIComponentFacade();
            _levelFacade = new LevelFacade(_uiComponentFacade);
            _inputController = new InputController();
            _visualElementsFacade = new VisualElementsFacade();
            _inputFacade = new InputFacade(_inputController);

            _mainEngineController = new MainEngineController(_levelFacade, _visualElementsFacade, _inputController, _inputFacade, _uiComponentFacade);

            var objUpdateController = new GameObject("UpdateController");
            _updateController = objUpdateController.AddComponent<UpdateController>();
            _updateController.AddComponent(UpdatablesName.MainEngineController, _mainEngineController);
            _updateController.AddComponent(UpdatablesName.InputController, _inputController);
        }

        private void Init()
        {

            var uiComponents = FindObjectsOfType<UIComponentView>();
            if (uiComponents != null)
            {
                foreach (var uiComponentView in uiComponents)
                {
                    _uiComponentFacade.RegComponent(uiComponentView);
                }
            }
            _uiComponentFacade.ActionDone += HandleUiComponentFacadeActionDone;
            
            _levelFacade.Init();
            _levelFacade.LoadLevel(1);
            _visualElementsFacade.Init();
            _inputFacade.Init();
            _updateController.Active();
            _mainEngineController.Init(new GameStateModel());
            _mainEngineController.EngineRequest(EventTypeEnum.LevelStart);
        }

        private void HandleUiComponentFacadeActionDone(object sender, UIViewActionArgs args)
        {
            switch (args.ActionType)
            {
                case ViewActionType.NextLevelButtonClick:
                    _visualElementsFacade.RemoveAllVisualElement();
                    _levelFacade.LoadNextLevel();
                    _mainEngineController.Clear();
                    _mainEngineController.Init(new GameStateModel());
                    _mainEngineController.EngineRequest(EventTypeEnum.LevelStart);
                    break;
            }
        }
    }
}