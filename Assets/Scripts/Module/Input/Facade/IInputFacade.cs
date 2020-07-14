
using Scripts.Controllers.Helpers;

namespace Module.Input.Facade
{
    public interface IInputFacade
    {
        FieldCoords MouseDown { get; }
        FieldCoords MouseMove { get; }
        FieldCoords MouseUp { get; }

        void Init();
    }
}
