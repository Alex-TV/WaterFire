
using Assets.Scripts.View;
using EngineCore.GameLoop.Helpers;

namespace Module.VisualElementsModule.Facade
{
    public interface IVisualElementsFacade
    {
        void Init();
        GameElementView CreateVisualElement(GameElementType name);
        void DestroyVisualElement(GameElementView view);
    }
}
