
using Engine.Models.Interfaces;
using Engine.Pipeline;
using Engine.Rules.Interfaces;

namespace Assets.Scripts.EngineCore.Engine.Pipeline
{
    public class ReturnEntityPipeline<T> : PipelineStage<T> where T : class, IGameStateEntity
    {
        public ReturnEntityPipeline(PipelineEngine engine, T entity, IRule rule) : base(engine, entity, rule)
        {
        }

        protected override void Processing()
        {
            FinishStage(Entity);
        }
    }
}
