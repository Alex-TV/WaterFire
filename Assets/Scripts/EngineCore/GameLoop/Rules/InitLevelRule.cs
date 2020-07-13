
using Engine.Pipeline;
using Engine.Rules;
using EngineCore.GameLoop.Entitys;
using EngineCore.GameLoop.Pipeline;

namespace EngineCore.GameLoop.Rules
{
    public class InitLevelRule : Rule<LevelModelEntity>
    {
        public override void CheckRule(LevelModelEntity entity, PipelineEngine engine)
        {
            if (entity == null)
            {
                return;
            }

            if (entity.LevelConfigModel.FieldModel.RowsCount == 0)
            {
                return;
            }

            if (entity.LevelConfigModel.FieldModel.CellsCount == 0)
            {
                return;
            }

            new InitLevelPipeline(engine, entity, this);
        }
    }
}
