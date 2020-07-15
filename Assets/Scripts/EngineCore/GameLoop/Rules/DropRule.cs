
using System.Collections.Generic;
using Assets.Scripts.EngineCore.Engine.Pipeline;
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
            foreach (var moveElement in FillElements(entity.Grid))
            {
                if (moveElement == null)
                {
                    continue;
                }
                moveElementCoordsList.Add(moveElement);
            }

            if (moveElementCoordsList.Count == 0)
            {
                return false;
            }
            var moveElementEntity = new MoveElementEntity(entity, moveElementCoordsList);
            new ReturnEntityPipeline<MoveElementEntity>(engine, moveElementEntity, this);

            return true;
        }

        private IEnumerable<MoveElementCoordsModel> FillElements(VisualElementModel[,] grid)
        {

            FieldCoords endMove;
            for (var coll = 0; coll < grid.GetLength(0); coll++)
            {
                for (var row = 0; row < grid.GetLength(1); row++)
                {
                    var currentElement = grid[coll, row];
                    if (currentElement.Name == GameElementType.Empty)
                    {
                        endMove = new FieldCoords(coll,row);
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
