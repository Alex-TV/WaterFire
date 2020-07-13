
using System.Collections.Generic;
using Module.VisualElementsModule.Models;
using UnityEngine;

namespace Module.VisualElementsModule.Providers
{
    [CreateAssetMenu(fileName = "VisualElementsList", menuName = "Game Data/View/Create VisualElementsList")]
    public class VisualElementsProvider : ScriptableObject, IVisualElementsProvider
    {
        [SerializeField] private List<VisualElementsDescriptionModel> _elements;

        public IReadOnlyList<VisualElementsDescriptionModel> Elements => _elements;
    }
}
