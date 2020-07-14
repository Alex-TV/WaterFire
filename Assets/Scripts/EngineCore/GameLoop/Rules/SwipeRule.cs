
using Assets.Scripts.EngineCore.Engine.Pipeline;
using Engine.Pipeline;
using Engine.Rules;
using EngineCore.GameLoop.Entities;
using EngineCore.GameLoop.Helpers;
using GameLoop.Entities;
using Scripts.Controllers.Helpers;
using UnityEngine;

namespace EngineCore.GameLoop.Rules
{
    public class SwipeRule : Rule<ElementGridEntity>
    {
        public override bool CheckRule(ElementGridEntity entity, PipelineEngine engine)
        {
            if (entity == null)
            {
                return true;
            }

            if (engine.InputFacade.MouseDown == engine.InputFacade.MouseUp)
            {
                return false;
            }

            if (engine.InputFacade.MouseDown.X >= entity.Grid.GetLength(0))
            {
                return false;
            }

            if (engine.InputFacade.MouseDown.X < 0)
            {
                return false;
            }

            if (engine.InputFacade.MouseDown.Y >= entity.Grid.GetLength(1))
            {
                return false;
            }

            if (engine.InputFacade.MouseDown.Y < 0)
            {
                return false;
            }

            var moveDirection = MouseMoveCoordsToDirecting(engine.InputFacade.MouseDown, engine.InputFacade.MouseUp);
            var endCoords = engine.InputFacade.MouseDown + moveDirection;

            if (endCoords.X < 0)
            {
                return false;
            }

            if (endCoords.Y < 0)
            {
                return false;
            }

            if (endCoords.Y >= entity.Grid.GetLength(1))
            {
                return false;
            }

            if (endCoords.X >= entity.Grid.GetLength(0))
            {
                return false;
            }

            var startMoveElement = entity.Grid[engine.InputFacade.MouseDown.X, engine.InputFacade.MouseDown.Y];

            if (startMoveElement.Name == GameElementType.Empty)
            {
                return false;
            }

            var endMoveElement = entity.Grid[endCoords.X, endCoords.Y];

            if (moveDirection == FieldCoords.top)
            {
                if (endMoveElement.Name == GameElementType.Empty)
                {
                    return false;
                }
            }

            var moveElementEntity = new MoveElementEntity(entity, engine.InputFacade.MouseDown, endCoords);
            new ReturnEntityPipeline<MoveElementEntity>(engine, moveElementEntity, this);
            return true;
        }

        private FieldCoords MouseMoveCoordsToDirecting(FieldCoords mouseDownCoords, FieldCoords mouseUpCoords)
        {
            var deltaCor = mouseUpCoords - mouseDownCoords;
            if (Mathf.Abs(deltaCor.X) == Mathf.Abs(deltaCor.Y)) return FieldCoords.zero;
            if (Mathf.Abs(deltaCor.X) > Mathf.Abs(deltaCor.Y))
                return new FieldCoords(deltaCor.X / Mathf.Abs(deltaCor.X), 0);
            return new FieldCoords(0, deltaCor.Y / Mathf.Abs(deltaCor.Y));
        }
    }
}
