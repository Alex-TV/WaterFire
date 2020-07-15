
using System.Collections.Generic;
using Engine.Pipeline;
using Engine.Rules;
using EngineCore.GameLoop.Entities;
using EngineCore.GameLoop.Entities.Models;
using EngineCore.GameLoop.Helpers;
using GameLoop.Entities;
using Scripts.Controllers.Helpers;

namespace EngineCore.GameLoop.Rules
{
    public class DropRule : Rule<ElementGridEntity>
    {
        public override bool CheckRule(ElementGridEntity entity, PipelineEngine engine)
        {
            if (entity == null)
            {
                return true;
            }

            var moveElementCoordsList = new List<MoveElementCoordsModel>();
            foreach (var moveElement in FindDropElements(entity.Grid))
            {
                moveElementCoordsList.Add(moveElement);
            }

            if (moveElementCoordsList.Count == 0)
            {
                return false;
            }

            var moveElementEntity = new MoveElementEntity(entity, moveElementCoordsList);
            new LambdaPipelineStage<MoveElementEntity>(engine, t =>   t, moveElementEntity, this, "Add MoveElementEntity to sate");
            return true;
        }

        private IEnumerable<MoveElementCoordsModel> FindDropElements(VisualElementModel[,] grid)
        {
            for (var coll = 0; coll < grid.GetLength(0); coll++)
            {
                for (var row = 0; row < grid.GetLength(1); row++)
                {
                    var currentElement = grid[coll, row];
                    if (currentElement.Name == GameElementType.Empty)
                    {
                        var endMove = new FieldCoords(coll,row);
                        for (var dropRow = row; dropRow < grid.GetLength(1); dropRow++)
                        {
                            var dropElement = grid[coll, dropRow];
                            if (dropElement.Name != GameElementType.Empty)
                            {
                                var startMove = new FieldCoords(coll, dropRow);
                                yield return new MoveElementCoordsModel(startMove,endMove );
                                endMove = startMove;
                            }
                        }
                    }
                }
            }
        }
    }
}
