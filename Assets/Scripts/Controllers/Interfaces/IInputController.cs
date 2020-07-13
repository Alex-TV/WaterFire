
using System;
using Scripts.Controllers.Helpers;

namespace Controllers.Interfaces
{
    public interface IInputController : ICustomUpdatable
    {
        event EventHandler<FieldCoords> MouseDown;
        event EventHandler<FieldCoords> MouseUp;
        event EventHandler<FieldCoords> MouseDoubleClick;
        event EventHandler<FieldCoords> MouseMove;
    }
}