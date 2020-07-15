
using Assets.Scripts.View;
using Engine.Pipeline;
using Engine.Rules.Interfaces;
using EngineCore.GameLoop.Entities;
using EngineCore.GameLoop.Entities.Models;
using EngineCore.GameLoop.Helpers;
using Module.VisualElementsModule.Facade;

namespace EngineCore.GameLoop.Pipeline
{
   public class DestroyElementsPipeline: PipelineStage<MatchElementsEntity>
   {
       private readonly IVisualElementsFacade _elementsFacade;
       private int _dieCount;

        public DestroyElementsPipeline(PipelineEngine engine, MatchElementsEntity entity, IRule rule) : base(engine, entity, rule)
        {
            _elementsFacade = engine.VisualElementsFacade;
        }

        protected override void Processing()
        {
            foreach (var elementsCoords in Entity.MatchElementsCoords)
            {
                var destroyElement = Entity.GridEntity.Grid[elementsCoords.X, elementsCoords.Y];
                if (destroyElement.Name == GameElementType.Empty)
                {
                    continue;
                    
                }

                _dieCount++;
                destroyElement.View.Die(HandleDie);
                Entity.GridEntity.Grid[elementsCoords.X, elementsCoords.Y] = new VisualElementModel(null, GameElementType.Empty);
            }
        }

        private void HandleDie(GameElementView view)
        {
            _elementsFacade.DestroyVisualElement(view);
            _dieCount--;
            if (_dieCount <= 0)
            {
                FinishStage(Entity.GridEntity);
            }
        }
    }
}
