
using Engine.Models;
using Engine.Pipeline;
using Engine.Rules.Interfaces;
using EngineCore.GameLoop.Entities;
using Module.Levels.Facade;

namespace EngineCore.GameLoop.Pipeline
{
    public class LoadLevelPipeline : PipelineStage<GameStateEntity>
    {
        private ILevelFacade _levelFacade;

        public LoadLevelPipeline(PipelineEngine engine, GameStateEntity entity, IRule rule) : base(engine, entity, rule)
        {
            _levelFacade = engine.LevelFacade;
        }

        protected override void Processing()
        {
            var startLevel = _levelFacade.LoadLevel(1);
            var levelModelEntity = new LevelModelEntity(startLevel);
            FinishStage(levelModelEntity);
        }
    }
}
