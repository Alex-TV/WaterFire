
using System.Collections.Generic;
using Engine.Pipeline;
using Engine.Rules.Interfaces;
using EngineCore.GameLoop.Entities;
using EngineCore.GameLoop.Entities.Models;
using EngineCore.GameLoop.Helpers;
using GameLoop.Entities;
using Scripts.Controllers.Helpers;

namespace EngineCore.GameLoop.Pipeline
{
    public class InitLevelPipeline : PipelineStage<LevelModelEntity>
    {
        public InitLevelPipeline(PipelineEngine engine, LevelModelEntity entity, IRule rule) : base(engine, entity, rule)
        {
        }

        protected override void Processing()
        {
            var createElements = new List<CreateVisualElementModel>();
            var grid = Entity.LevelConfigModel.FieldModel.Grid;
            var collCount = Entity.LevelConfigModel.FieldModel.CollCount;
            var rowsCount = Entity.LevelConfigModel.FieldModel.RowsCount;

            for (var i = 0; i < grid.Count; i++)
            {
                var rowPos = i / collCount;
                var collPos = i - rowPos * collCount;
                createElements.Add(new CreateVisualElementModel(new FieldCoords(collPos, rowPos), grid[i].Element));
            }

            var gridEntity = new ElementGridEntity(new VisualElementModel[collCount, rowsCount]);
            var createElementsEntity = new CreateVisualElementsEntity(createElements, gridEntity);
            var levelStateEntity = new LevelStateEntity(Entity.LevelConfigModel.Num, LevelStateEnum.Play, 0);
            FinishStage(levelStateEntity, createElementsEntity, gridEntity);
        }
    }
}
