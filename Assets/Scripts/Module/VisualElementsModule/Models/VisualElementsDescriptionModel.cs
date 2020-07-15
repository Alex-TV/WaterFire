
using System;
using Assets.Scripts.View;
using EngineCore.GameLoop.Helpers;
using UnityEngine;

namespace Module.VisualElementsModule.Models
{
    [Serializable]
    public class VisualElementsDescriptionModel
    {
        [SerializeField] private GameElementView _view = default;
        [SerializeField] private GameElementType _name = default;

        public GameElementView View => _view;
        public GameElementType Name => _name;

    }
}
