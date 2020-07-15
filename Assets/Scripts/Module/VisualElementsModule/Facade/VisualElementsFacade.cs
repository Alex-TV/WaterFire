
using System.Linq;
using Assets.Scripts.View;
using EngineCore.GameLoop.Helpers;
using Module.VisualElementsModule.Providers;
using UnityEngine;

namespace Module.VisualElementsModule.Facade
{
    public class VisualElementsFacade : IVisualElementsFacade
    {
        private IVisualElementsProvider _elementsProvider;

        public void Init()
        {
            _elementsProvider = Resources.Load<VisualElementsProvider>("GameData/Views/VisualElementsList");
        }

        public GameElementView CreateVisualElement(GameElementType name)
        {
            var elementsDescription = _elementsProvider.Elements.FirstOrDefault(e => e.Name == name);
            if (elementsDescription == null)
            {
                throw new System.Exception($"No description for item {name}");
            }
            return Object.Instantiate(elementsDescription.View);
        }

        public void DestroyVisualElement(GameElementView view)
        {
            Object.Destroy(view.gameObject);
        }
    }
}
