
using Engine.Pipeline;
using Engine.Rules;
using EngineCore.GameLoop.Entities;
using EngineCore.GameLoop.Pipeline;

namespace EngineCore.GameLoop.Rules
{
    public class MoveElementRule : Rule<MoveElementEntity>
    {
        public override bool CheckRule(MoveElementEntity entity, PipelineEngine engine)
        {
            if (entity == null)
            {
                return true;
            }

            var previous = new MoveElementPipeline(engine, entity, this);
            new UpdateLayerVisualElementsPipeline(engine, entity.GridEntity, this).AddPreviouse(previous);
            return true;
        }
    }
}
