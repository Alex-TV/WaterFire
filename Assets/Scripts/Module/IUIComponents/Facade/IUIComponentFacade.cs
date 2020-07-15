
using System;
using View;
using View.Helpers;

namespace Module.IUIComponents.Facade
{
    public interface IUIComponentFacade
    {
        event EventHandler<UIViewActionArgs> ActionDone;
        void RegComponent(IUIComponentView component);
        void UnregComponent(IUIComponentView component);
        void OnSendAction(ViewActionType actionType, CustomObject data);
    }
}
