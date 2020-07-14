
using Engine.Models;
using Engine.Pipeline;
using Engine.Rules;
using EngineCore.GameLoop.Pipeline;

namespace EngineCore.GameLoop.Rules
{
    public class LoadLevelRule : Rule<GameStateEntity>
    {
        public override bool CheckRule(GameStateEntity entity, PipelineEngine engine)
        {
            if (entity == null)
            {
                return true;
            }
            new LoadLevelPipeline(engine, GameStateEntity.Empty, this);
            return true;
        }
    }
}
