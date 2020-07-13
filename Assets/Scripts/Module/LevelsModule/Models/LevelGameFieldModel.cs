
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Module.Levels.Models
{
    [Serializable]
    public class LevelGameFieldModel
    {
        [SerializeField] private int _cellsCount = default;
        [SerializeField] private int _rowsCount = default;
        [SerializeField] private List<LevelGameCellModel> _grid = default;

        public int CellsCount => _cellsCount;
        public int RowsCount => _rowsCount;
        public IReadOnlyList<LevelGameCellModel> Grid => _grid;
    }
}
