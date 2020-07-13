
using Controllers.Interfaces;
using GameLoop;
using GameLoop.Model;
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


        private void Awake()
        {
            InitGame();
        }

        private void InitGame()
        {
            _inputController = new InputController();
            _mainEngineController = new MainEngineController(new GameStateModel());

            var objUpdateController = new GameObject("UpdateController");
            _updateController = objUpdateController.AddComponent<UpdateController>();
            _updateController.AddComponent(UpdatablesName.MainEngineController, _mainEngineController);
            _updateController.AddComponent(UpdatablesName.InputController, _inputController);
            _updateController.Active();
        }
    }
}