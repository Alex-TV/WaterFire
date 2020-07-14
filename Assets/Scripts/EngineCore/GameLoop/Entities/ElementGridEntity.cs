
using Engine.Models.Interfaces;
using EngineCore.GameLoop.Entities.Models;

namespace GameLoop.Entities
{
    public class ElementGridEntity : IGameStateEntity
    {
        public VisualElementModel[,] Grid { get; }

        public ElementGridEntity(VisualElementModel[,] grid)
        {
            Grid = grid;
        }
    }
}
