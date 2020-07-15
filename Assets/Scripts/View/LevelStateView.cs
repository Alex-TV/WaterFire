
using System;
using UnityEngine;
using UnityEngine.UI;
using View.Helpers;

namespace View
{
    public class LevelStateView : UIComponentView
    {
        [SerializeField] private Text _levelText = default;
        [SerializeField] private Button _nextLevelButton = default;

        public override UIViewType ViewType { get; } = UIViewType.LevelState;
        public override event EventHandler<UIViewActionArgs> ActionDone;

        private static string _levelTemplate = "Level: {0}";
            
        public override void Init()
        {
            _levelText.text = string.Format(_levelTemplate, string.Empty);
            _nextLevelButton.onClick.AddListener(HandleNextLevelButtonClick);
            base.Init();
        }

        public override void SendAction(ViewActionType actionType, CustomObject data)
        {
            switch (actionType)
            {
                case ViewActionType.LevelNumUpdate:
                    _levelText.text = string.Format(_levelTemplate, data.GetValue<int>());
                    break;
            }
        }

        private void HandleNextLevelButtonClick()
        {
            ActionDone?.Invoke(this, new UIViewActionArgs(ViewActionType.NextLevelButtonClick, CustomObject.Empty));
        }

        protected override void Clear()
        {
            _nextLevelButton.onClick.RemoveListener(HandleNextLevelButtonClick);
            base.Clear();
        }
    }
}
