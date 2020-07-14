
using Engine.Pipeline;
using Engine.Rules;
using EngineCore.GameLoop.Entities;
using EngineCore.GameLoop.Pipeline;

namespace EngineCore.GameLoop.Rules
{
    public class InitLevelRule : Rule<LevelModelEntity>
    {
        public override bool CheckRule(LevelModelEntity entity, PipelineEngine engine)
        {
            if (entity == null)
            {
                return true;
            }

            if (entity.LevelConfigModel.FieldModel.RowsCount == 0)
            {
                return false;
            }

            if (entity.LevelConfigModel.FieldModel.CollCount == 0)
            {
                return false;
            }

            new InitLevelPipeline(engine, entity, this);
            return true;
        }
    }
}
