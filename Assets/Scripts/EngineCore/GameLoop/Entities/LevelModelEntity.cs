
using Engine.Models.Interfaces;
using Module.Levels.Models;

namespace EngineCore.GameLoop.Entities
{
    public class LevelModelEntity : IGameStateEntity
    {
        public LevelConfigModel LevelConfigModel { get; }

        public LevelModelEntity(LevelConfigModel levelConfigModel)
        {
            LevelConfigModel = levelConfigModel;
        }
    }
}
