
using EngineCore.GameLoop.Helpers;
using Scripts.Controllers.Helpers;

namespace EngineCore.GameLoop.Entities.Models
{
    public class CreateVisualElementModel
    {
        public GameElementType Name { get; }
        public FieldCoords CellCoords { get; }

        public CreateVisualElementModel(FieldCoords cellCoords, GameElementType name)
        {
            CellCoords = cellCoords;
            Name = name;
        }
    }
}
