
using Engine.Pipeline;
using Engine.Rules.Interfaces;
using EngineCore.GameLoop.Entities;
using EngineCore.GameLoop.Helpers;
using Scripts.Controllers.Helpers;

namespace EngineCore.GameLoop.Pipeline
{
    public class MoveElementPipeline : PipelineStage<MoveElementEntity>
    {
        private int _startMoveElements;

        public MoveElementPipeline(PipelineEngine engine, MoveElementEntity entity, IRule rule) : base(engine, entity, rule)
        {
        }

        protected override void Processing()
        {
            foreach (var entityMoveElement in Entity.MoveElementCoords)
            {
                var startCoords = entityMoveElement.StartCoords;
                var endCoords = entityMoveElement.EndCoords;
                var startElement = Entity.GridEntity.Grid[startCoords.X, startCoords.Y];
                var endElement = Entity.GridEntity.Grid[endCoords.X, endCoords.Y];
                _startMoveElements++;
                startElement.View.Move(CoordinateConverter.FieldCoordsToPosition(endCoords), MoveEnd);
                if (endElement.Name != GameElementType.Empty)
                {
                    _startMoveElements++;
                    endElement.View.Move(CoordinateConverter.FieldCoordsToPosition(startCoords), MoveEnd);
                }
                Entity.GridEntity.Grid[endCoords.X, endCoords.Y] = startElement;
                Entity.GridEntity.Grid[startCoords.X, startCoords.Y] = endElement;
            }
        }

        private void MoveEnd()
        {
            _startMoveElements--;
            if (_startMoveElements <= 0)
            {
                FinishStage(Entity.GridEntity);
            }
        }
    }
}
