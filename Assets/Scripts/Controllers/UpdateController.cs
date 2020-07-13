
using Controllers.Interfaces;
using Scripts.Controllers.Helpers;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Controllers
{
    public sealed class UpdateController : MonoBehaviour, IUpdateController
    {
        private bool _isActive = true;
        private readonly Dictionary<UpdatablesName, ICustomUpdatable> _components =
            new Dictionary<UpdatablesName, ICustomUpdatable>();
        private readonly Dictionary<UpdatablesName, bool> _enabledComponents = new Dictionary<UpdatablesName, bool>();

        public void Active() => _isActive = true;

        public void AddComponent(UpdatablesName componentName, ICustomUpdatable component)
        {
            _components[componentName] = component;
            _enabledComponents[componentName] = true;
        }

        public void PauseComponent(UpdatablesName componentName) => _enabledComponents[componentName] = false;

        public void ResumeComponent(UpdatablesName componentName) => _enabledComponents[componentName] = true;

        private void Update()
        {
            if (!_isActive)
            {
                return;
            }

            foreach (var component in
                _components.Where(c => _enabledComponents[c.Key]))
            {
                component.Value.CustomUpdate();
            }
        }
    }
}