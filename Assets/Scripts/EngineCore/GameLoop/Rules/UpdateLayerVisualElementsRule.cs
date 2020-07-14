
using Engine.Pipeline;
using Engine.Rules;
using EngineCore.GameLoop.Pipeline;
using GameLoop.Entitys;

namespace EngineCore.GameLoop.Rules
{
   public class UpdateLayerVisualElementsRule : Rule<ElementGridEntity>
    {
        public override void CheckRule(ElementGridEntity entity, PipelineEngine engine)
        {
            if (entity == null)
            {
                return;
            }
            new UpdateLayerVisualElementsPipeline(engine, entity, this);
        }
    }
}
