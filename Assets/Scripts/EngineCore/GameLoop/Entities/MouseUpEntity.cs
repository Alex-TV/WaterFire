
using Engine.Models.Interfaces;
using Scripts.Controllers.Helpers;

namespace EngineCore.GameLoop.Entities
{
    public class MouseUpEntity : IGameStateEntity
    {
        public FieldCoords Coords { get; }

        public MouseUpEntity(FieldCoords coords)
        {
            Coords = coords;
        }
    }
}
