
using System.Collections.Generic;
using Module.Levels.Models;

namespace Module.Levels.Providers
{
    public interface ILevelProvider
    {
        IReadOnlyList<LevelConfigModel> Levels { get; }
    }
}
