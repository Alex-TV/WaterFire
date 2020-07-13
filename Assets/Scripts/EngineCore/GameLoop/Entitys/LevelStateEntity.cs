

using Engine.Models.Interfaces;
using EngineCore.GameLoop.Helpers;

namespace EngineCore.GameLoop.Entitys
{
    public class LevelStateEntity : IGameStateEntity
    {
        public int LevelNum { get; }
        public LevelStateEnum LevelState { get; }
        public int MoveCount { get; }

        public LevelStateEntity(int levelNum, LevelStateEnum levelState, int moveCount)
        {
            LevelNum = levelNum;
            LevelState = levelState;
            MoveCount = moveCount;
        }
    }
}
