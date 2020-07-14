
using Engine.Pipeline;
using Engine.Rules.Interfaces;
using EngineCore.GameLoop.Helpers;
using GameLoop.Entities;

namespace EngineCore.GameLoop.Pipeline
{
   public class UpdateLayerVisualElementsPipeline : PipelineStage<ElementGridEntity>
    {
        public UpdateLayerVisualElementsPipeline(PipelineEngine engine, ElementGridEntity entity, IRule rule) : base(engine, entity, rule)
        {
        }

        protected override void Processing()
        {
            var layer = 10;
            foreach (var visualElementModel in Entity.Grid)
            {
                if (visualElementModel.Name != GameElementType.Empty)
                {
                    visualElementModel.View.SetOrderInLayer(layer);
                }

                layer++;
            }

            FinishStage(Entity);
        }
    }
}
