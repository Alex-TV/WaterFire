using System;
using EngineCore.GameLoop.Helpers;
using UnityEngine;

namespace Module.Levels.Models
{
    [Serializable]
    public class LevelGameCellModel
    {
        [SerializeField] private GameElementType _element = default;

        public GameElementType Element => _element;
    }
}
