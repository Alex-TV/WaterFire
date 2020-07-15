using Assets.Scripts.View;
using Engine.Pipeline;
using Engine.Rules.Interfaces;
using EngineCore.GameLoop.Entities;
using EngineCore.GameLoop.Entities.Models;
using EngineCore.GameLoop.Helpers;
using Module.VisualElementsModule.Facade;
using Scripts.Controllers.Helpers;

namespace EngineCore.GameLoop.Pipeline
{
    public class CreateVisualElementsPipeline : PipelineStage<CreateVisualElementsEntity>
    {
        private readonly IVisualElementsFacade _elementsFacade;

        public CreateVisualElementsPipeline(PipelineEngine engine, CreateVisualElementsEntity entity, IRule rule) : base(engine, entity, rule)
        {
            _elementsFacade = engine.VisualElementsFacade;
        }

        protected override void Processing()
        {
            foreach (var createVisualElementModel in Entity.Elements)
            {
                GameElementView elementObj = null;
                if (createVisualElementModel.Name != GameElementType.Empty)
                {
                    elementObj = _elementsFacade.CreateVisualElement(createVisualElementModel.Name);
                    elementObj.transform.localPosition =
                        CoordinateConverter.FieldCoordsToPosition(createVisualElementModel.CellCoords);
                }
                Entity.GridEntity.Grid[createVisualElementModel.CellCoords.X, createVisualElementModel.CellCoords.Y] =
                    new VisualElementModel(elementObj, createVisualElementModel.Name);
            }
            FinishStage(Entity.GridEntity);
        }
    }
}
