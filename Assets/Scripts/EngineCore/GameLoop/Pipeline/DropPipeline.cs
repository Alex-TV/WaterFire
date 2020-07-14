

using Engine.Pipeline;
using Engine.Rules.Interfaces;
using GameLoop.Entities;

namespace EngineCore.GameLoop.Pipeline
{
    public class DropPipeline : PipelineStage<ElementGridEntity>
    {
        public DropPipeline(PipelineEngine engine, ElementGridEntity entity, IRule rule) : base(engine, entity, rule)
        {
        }

        protected override void Processing()
        {
            throw new System.NotImplementedException();
        }
    }
}
