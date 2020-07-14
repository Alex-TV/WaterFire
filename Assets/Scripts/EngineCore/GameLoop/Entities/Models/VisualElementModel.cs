
using Assets.Scripts.View;
using EngineCore.GameLoop.Helpers;

namespace EngineCore.GameLoop.Entities.Models
{
    public class VisualElementModel
    {
        public GameElementView View { get; }
        public GameElementType Name { get; }

        public VisualElementModel(GameElementView view, GameElementType name)
        {
            View = view;
            Name = name;
        }
    }
}
