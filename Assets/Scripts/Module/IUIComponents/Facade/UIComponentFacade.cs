
using System;
using System.Collections.Generic;
using View;
using View.Helpers;

namespace Module.IUIComponents.Facade
{
    public class UIComponentFacade : IUIComponentFacade
    {
        public event EventHandler<UIViewActionArgs> ActionDone;
        private readonly List<IUIComponentView> _components = new List<IUIComponentView>();
        public void RegComponent(IUIComponentView component)
        {
            component.ActionDone += HandleComponentActionDone;
            component.Destroyed += HandleComponentDestroyed;
            _components.Add(component);
            component.Init();
        }

        public void OnSendAction(ViewActionType actionType, CustomObject data)
        {
            foreach (var uiComponentView in _components)
            {
                uiComponentView?.SendAction(actionType, data);
            }
        }

        public void UnregComponent(IUIComponentView component)
        {
            if (component == null)
            {
                return;
            }
            if (_components.Contains(component))
            {
                _components.Remove(component);
            }
        }

        private void HandleComponentActionDone(object sender, UIViewActionArgs args)
        {
            foreach (var uiComponentView in _components)
            {
                if (uiComponentView != sender)
                {
                    uiComponentView?.SendAction(args.ActionType, args.Data);
                }
            }
            ActionDone?.Invoke(sender, args);
        }

        private void HandleComponentDestroyed(object sender, EventArgs e)
        {
            UnregComponent(sender as IUIComponentView);
        }
    }
}
