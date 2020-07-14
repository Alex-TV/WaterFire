
using Engine.Models.Interfaces;
using Scripts.Controllers.Helpers;

namespace EngineCore.GameLoop.Entities
{
   public class MouseCoordsEntity : IGameStateEntity
    {
        public FieldCoords Coords { get; set; }

        public MouseCoordsEntity(FieldCoords coords)
        {
            Coords = coords;
        }
    }
}
