
using System;
using UnityEngine;
using View.Helpers;

namespace View
{
    public abstract class UIComponentView : MonoBehaviour, IUIComponentView
    {
        public event EventHandler Destroyed;

        public abstract UIViewType ViewType { get; }
        public abstract event EventHandler<UIViewActionArgs> ActionDone;
        public abstract void SendAction(ViewActionType actionType, CustomObject data);

        public virtual void Init() { }

        protected virtual void Clear() { }

        private void OnDestroy()
        {
            Clear();
            Destroyed?.Invoke(this, EventArgs.Empty);
        }
    }
}
