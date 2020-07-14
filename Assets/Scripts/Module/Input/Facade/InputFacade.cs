
using Module.Input.Facade.Controllers;
using Scripts.Controllers.Helpers;

namespace Module.Input.Facade
{
    public class InputFacade : IInputFacade
    {
        private readonly IInputController _inputController;
        public FieldCoords MouseDown { get; private set; }
        public FieldCoords MouseMove { get; private set; }
        public FieldCoords MouseUp { get; private set; }

        public InputFacade(IInputController inputController)
        {
            _inputController = inputController;
        }

        public void Init()
        {
            _inputController.MouseDown += HandleInputControllerMouseDown;
            _inputController.MouseMove += HandleInputControllerMouseMove;
            _inputController.MouseUp += HandleInputControllerMouseUp;
        }

        private void HandleInputControllerMouseUp(object sender, FieldCoords coords)
        {
            MouseUp = coords;
        }

        private void HandleInputControllerMouseMove(object sender, FieldCoords coords)
        {
            //MouseMove = coords;
            //MouseUp = coords;
        }

        private void HandleInputControllerMouseDown(object sender, FieldCoords coords)
        {
            MouseDown = coords;
            MouseMove = coords;
            MouseUp = coords;
        }
    }
}
