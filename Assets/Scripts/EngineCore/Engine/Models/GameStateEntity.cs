
using Engine.Models.Interfaces;

namespace Engine.Models
{
    public class GameStateEntity : IGameStateEntity
    {
        private static GameStateEntity _empty;

        public static GameStateEntity Empty => _empty = _empty ?? new GameStateEntity();

        private GameStateEntity() { }
    }
}
