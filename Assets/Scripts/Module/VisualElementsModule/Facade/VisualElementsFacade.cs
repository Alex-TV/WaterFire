
using System.Collections.Generic;
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

        private readonly List<GameElementView> _cache = new List<GameElementView>();
        public void Init()
        {
            _elementsProvider = Resources.Load<VisualElementsProvider>("GameData/Views/VisualElementsList");
        }

        public GameElementView CreateVisualElement(GameElementType name)
        {
            var elementsDescription = _elementsProvider.Elements.FirstOrDefault(e => e.Name == name);
            if (elementsDescription == null)
            {
                throw new System.Exception($"No elements description for item {name}");
            }
            var gameElementView = Object.Instantiate(elementsDescription.View);
            _cache.Add(gameElementView);
            return gameElementView;
        }



        public void DestroyVisualElement(GameElementView view)
        {
            if (_cache.Contains(view))
            {
                _cache.Remove(view);
            }
            Object.Destroy(view.gameObject);
        }

        public void RemoveAllVisualElement()
        {
            foreach (var gameElementView in _cache)
            {
                Object.Destroy(gameElementView.gameObject);
            }
            _cache.Clear();
        }
    }
}
