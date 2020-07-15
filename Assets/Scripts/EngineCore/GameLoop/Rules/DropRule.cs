
using System.Collections;
using System.Collections.Generic;
using Engine.Pipeline;
using Engine.Rules;
using EngineCore.GameLoop.Entities.Models;
using GameLoop.Entities;

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

            foreach (var moveElement in FillElements(entity.Grid))
            {
                if (moveElement == null)
                {
                    continue;
                }
            }


            return false;
        }

        private IEnumerable<MoveElementCoordsModel> FillElements(VisualElementModel[,] grid)
        {
            yield return null;
        }
    }
}
