

using Engine.Models.Interfaces;
using Scripts.Controllers.Helpers;

namespace EngineCore.GameLoop.Entities
{
    public class MouseDownEntity : IGameStateEntity
    {
        public FieldCoords Coords { get; }

        public MouseDownEntity(FieldCoords coords)
        {
            Coords = coords;
        }
    }
}
