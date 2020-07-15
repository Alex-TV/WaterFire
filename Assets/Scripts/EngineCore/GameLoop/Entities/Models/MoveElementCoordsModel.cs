
using Scripts.Controllers.Helpers;

namespace EngineCore.GameLoop.Entities.Models
{
    public class MoveElementCoordsModel
    {
        public FieldCoords StartCoords { get; }
        public FieldCoords EndCoords { get; }

        public MoveElementCoordsModel(FieldCoords startCoords, FieldCoords endCoords)
        {
            StartCoords = startCoords;
            EndCoords = endCoords;
        }
    }
}
