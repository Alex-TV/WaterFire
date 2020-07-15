

using System;
using Assets.Scripts.View;
using UnityEngine;

namespace Module.VisualElementsModule.Models
{
    [Serializable]
    public class VisualResourcesDescriptionModel
    {
        [SerializeField] private GameObject _view = default;
        [SerializeField] private string _name = default;

        public GameObject View => _view;
        public string Name => _name;
    }
}
