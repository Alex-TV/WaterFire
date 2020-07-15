
using System;

namespace View.Helpers
{
    public class UIViewActionArgs : EventArgs
    {
        public ViewActionType ActionType { get; set; }
        public CustomObject Data { get; set; }

        public new static UIViewActionArgs Empty => new UIViewActionArgs(ViewActionType.Undefined);

        public UIViewActionArgs(ViewActionType actionType, CustomObject customObject)
        {
            ActionType = actionType;
            Data = customObject;
        }

        public UIViewActionArgs(ViewActionType actionType)
        {
            ActionType = actionType;
            Data = CustomObject.Empty;
        }

    }
}
