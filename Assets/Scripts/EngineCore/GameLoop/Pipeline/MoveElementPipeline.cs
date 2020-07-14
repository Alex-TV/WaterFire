
using Engine.Pipeline;
using Engine.Rules.Interfaces;
using EngineCore.GameLoop.Entities;
using EngineCore.GameLoop.Helpers;
using Scripts.Controllers.Helpers;

namespace EngineCore.GameLoop.Pipeline
{
    public class MoveElementPipeline : PipelineStage<MoveElementEntity>
    {
        public MoveElementPipeline(PipelineEngine engine, MoveElementEntity entity, IRule rule) : base(engine, entity, rule)
        {
        }

        protected override void Processing()
        {
            var startCoords = Entity.StartMoveCoords;
            var endCoords = Entity.EndMoveCoords;

            var startElement = Entity.GridEntity.Grid[startCoords.X, startCoords.Y];
            var endElement = Entity.GridEntity.Grid[endCoords.X, endCoords.Y];
            startElement.View.Move(CoordinateConverter.FieldCoordsToPosition(endCoords), () =>
            {
                Entity.GridEntity.Grid[endCoords.X, endCoords.Y] = startElement;
                Entity.GridEntity.Grid[startCoords.X, startCoords.Y] = endElement;
                FinishStage(Entity.GridEntity);
            });
            if (endElement.Name != GameElementType.Empty)
            {
                endElement.View.Move(CoordinateConverter.FieldCoordsToPosition(startCoords), () => {});
            }
        }
    }
}
