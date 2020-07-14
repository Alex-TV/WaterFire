
using Engine.Models.Interfaces;
using EngineCore.GameLoop.Entitys.Models;

namespace GameLoop.Entitys
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
