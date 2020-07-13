using System;
using UnityEngine;

namespace Module.Levels.Models
{
    [Serializable]
    public class LevelConfigModel
    {
        [SerializeField] private int _num = default;
        [SerializeField] private LevelGameFieldModel _fieldModel = default;

        public int Num => _num;

        public LevelGameFieldModel FieldModel => _fieldModel;
    }
}
