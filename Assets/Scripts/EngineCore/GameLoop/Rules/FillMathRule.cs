
using System.Collections.Generic;
using Engine.Pipeline;
using Engine.Rules;
using EngineCore.GameLoop.Entities;
using EngineCore.GameLoop.Entities.Models;
using EngineCore.GameLoop.Helpers;
using EngineCore.GameLoop.Pipeline;
using GameLoop.Entities;
using Scripts.Controllers.Helpers;

namespace EngineCore.GameLoop.Rules
{
    public class FillMathRule : Rule<ElementGridEntity>
    {
        public override bool CheckRule(ElementGridEntity entity, PipelineEngine engine)
        {
            if (entity == null)
            {
                return true;
            }

            if (TryFillIslandsMatch(entity.Grid, out var match))
            {
                var matchElementsCoords = new List<FieldCoords>();
                for (var col = 0; col < entity.Grid.GetLength(0); col++)
                {
                    for (var row = 0; row < entity.Grid.GetLength(1); row++)
                    {
                        if (match[col, row])
                        {
                            matchElementsCoords.Add(new FieldCoords(col, row));
                        }
                    }
                }

                new DestroyElementsPipeline(engine, new MatchElementsEntity(entity, matchElementsCoords), this);
                return true;
            }
            return false;
        }

        public bool  TryFillIslandsMatch(VisualElementModel[,] grid, out bool[,] match)
        {
            int colsCount = grid.GetLength(0);
            int rowsCount = grid.GetLength(1);

            var matchList = new List<FieldCoords>();
            var result = false;
            match = new bool[colsCount, rowsCount];

            for (var row = 0; row < rowsCount; row++)
            {
                for (var col = 0; col < colsCount - 1; col++)
                {
                    matchList.Clear();
                    var startingPoint = new FieldCoords(col, row);
                    matchList.Add(startingPoint);
                    for (col++; col < colsCount; col++)
                    {
                        if (grid[startingPoint.X, startingPoint.Y].Name == GameElementType.Empty ||
                            grid[col, row].Name == GameElementType.Empty ||
                            grid[startingPoint.X, startingPoint.Y].Name !=
                            grid[col, row].Name)
                        {
                            break;
                        }

                        matchList.Add(new FieldCoords(col, row));
                    }
                    col--;
                    if (matchList.Count < 3)
                    {
                        continue;
                    }

                    foreach (var coords in matchList)
                    {
                        match[coords.X, coords.Y] = true;
                        result = true;
                    }
                       
                }
            }
            for (var col = 0; col < colsCount; col++)
            {
                for (var row = 0; row < rowsCount - 1; row++)
                {
                    matchList.Clear();
                    var startingPoint = new FieldCoords(col, row);
                    matchList.Add(startingPoint);
                    for (row++; row < rowsCount; row++)
                    {
                        if (grid[startingPoint.X, startingPoint.Y].Name == GameElementType.Empty ||
                            grid[col, row].Name == GameElementType.Empty ||
                            grid[startingPoint.X, startingPoint.Y].Name !=
                            grid[col, row].Name)
                        {
                            break;
                        }

                        matchList.Add(new FieldCoords(col, row));
                    }
                    row--;
                    if (matchList.Count < 3)
                    {
                        continue;
                    }

                    foreach (var coords in matchList)
                    {
                        match[coords.X, coords.Y] = true;
                        result = true;
                    }
                }
            }
            return result;
        }

    }
}
