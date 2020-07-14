
using Engine.Pipeline;
using Engine.Rules;
using EngineCore.GameLoop.Entitys;
using EngineCore.GameLoop.Pipeline;

namespace EngineCore.GameLoop.Rules
{
    public class CreateVisualElementsRule: Rule<CreateVisualElementsEntity>
    {
        public override void CheckRule(CreateVisualElementsEntity entity, PipelineEngine engine)
        {
            if (entity == null)
            {
                return;
            }

            if (entity.Elements?.Count == 0)
            {
                return;
            }

            new CreateVisualElementsPipeline(engine, entity, this);
        }
    }
}
