
using System;
using Controllers.Interfaces;
using Scripts.Controllers.Helpers;

namespace Module.Input.Facade.Controllers
{
    public interface IInputController : ICustomUpdatable
    {
        event EventHandler<FieldCoords> MouseDown;
        event EventHandler<FieldCoords> MouseUp;
        event EventHandler<FieldCoords> MouseMove;
    }
}