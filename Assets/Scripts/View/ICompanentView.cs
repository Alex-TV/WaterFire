
using System;
using View.Helpers;

namespace View
{
    public interface IUIComponentView
    {
        event EventHandler Destroyed;
        UIViewType ViewType { get; }
        event EventHandler<UIViewActionArgs> ActionDone;
        void SendAction(ViewActionType actionType, CustomObject data);
        void Init();
    }
}
