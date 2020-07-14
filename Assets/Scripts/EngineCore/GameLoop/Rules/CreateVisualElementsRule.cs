
using Engine.Pipeline;
using Engine.Rules;
using EngineCore.GameLoop.Entities;
using EngineCore.GameLoop.Pipeline;

namespace EngineCore.GameLoop.Rules
{
    public class CreateVisualElementsRule: Rule<CreateVisualElementsEntity>
    {
        public override bool CheckRule(CreateVisualElementsEntity entity, PipelineEngine engine)
        {
            if (entity == null)
            {
                return true;
            }

            if (entity.Elements?.Count == 0)
            {
                return false;
            }

            new CreateVisualElementsPipeline(engine, entity, this);
            return true;
        }
    }
}
