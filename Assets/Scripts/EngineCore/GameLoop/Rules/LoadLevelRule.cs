
using Engine.Models;
using Engine.Pipeline;
using Engine.Rules;
using EngineCore.GameLoop.Pipeline;

namespace EngineCore.GameLoop.Rules
{
    public class LoadLevelRule : Rule<GameStateEntity>
    {
        public override void CheckRule(GameStateEntity entity, PipelineEngine engine)
        {
            new LoadLevelPipeline(engine, GameStateEntity.Empty, this);
        }
    }
}
