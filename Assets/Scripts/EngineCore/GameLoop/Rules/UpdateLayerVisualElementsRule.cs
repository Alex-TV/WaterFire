
using Engine.Pipeline;
using Engine.Rules;
using EngineCore.GameLoop.Pipeline;
using GameLoop.Entities;

namespace EngineCore.GameLoop.Rules
{
    public class UpdateLayerVisualElementsRule : Rule<ElementGridEntity>
    {
        public override bool CheckRule(ElementGridEntity entity, PipelineEngine engine)
        {
            if (entity == null)
            {
                return true;
            }
            new UpdateLayerVisualElementsPipeline(engine, entity, this);
            return true;
        }
    }
}
