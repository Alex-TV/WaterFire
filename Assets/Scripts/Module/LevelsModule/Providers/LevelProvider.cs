using System.Collections.Generic;
using Module.Levels.Models;
using UnityEngine;

namespace Module.Levels.Providers
{
    [CreateAssetMenu(fileName = "LevelList", menuName = "Game Data/Levels/Create LevelList")]
    public class LevelProvider : ScriptableObject, ILevelProvider
    {
        [SerializeField] private List<LevelConfigModel> _levels = default;

        public IReadOnlyList<LevelConfigModel> Levels => _levels;
    }
}
