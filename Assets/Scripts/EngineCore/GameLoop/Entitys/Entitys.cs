using Engine.Models;

namespace GameLoop.Entitys
{

    public class GameStateEntity : IGameStateEntity
    {
        private static GameStateEntity _empty;
        public static GameStateEntity Empty => _empty = _empty ?? new GameStateEntity();

        private GameStateEntity() { }
    }

    public class ElementGridEntity : IGameStateEntity
    {
    }
}
