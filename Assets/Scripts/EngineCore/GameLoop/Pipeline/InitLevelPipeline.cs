
using System.Collections.Generic;
using Engine.Pipeline;
using Engine.Rules.Interfaces;
using EngineCore.GameLoop.Entitys;
using EngineCore.GameLoop.Entitys.Models;
using EngineCore.GameLoop.Helpers;
using GameLoop.Entitys;
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
            var cellsCount = Entity.LevelConfigModel.FieldModel.CellsCount;
            var rowsCount = Entity.LevelConfigModel.FieldModel.RowsCount;

            for (var i = 0; i < grid.Count; i++)
            {
                var rowPos = i / cellsCount;
                var cellPos = i - rowPos * cellsCount;
                createElements.Add(new CreateVisualElementModel(new FieldCoords(cellPos, rowPos), grid[i].Element));
            }

            var gridEntity = new ElementGridEntity(new VisualElementModel[cellsCount, rowsCount]);
            var createElementsEntity = new CreateVisualElementsEntity(createElements, gridEntity);
            var levelStateEntity = new LevelStateEntity(Entity.LevelConfigModel.Num, LevelStateEnum.Play, 0);
            FinishStage(levelStateEntity, createElementsEntity, gridEntity);
        }
    }
}
