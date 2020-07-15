
using System;
using System.Collections;
using UnityEngine;
using View.Helpers;

namespace View
{
    class MainPanelView : UIComponentView
    {
        [SerializeField] private GameObject _levelWinText = default;
        [SerializeField] private float _timeShowLevelWinText =2f;

        public override UIViewType ViewType { get; }
        public override event EventHandler<UIViewActionArgs> ActionDone;

        public override void SendAction(ViewActionType actionType, CustomObject data)
        {
            switch (actionType)
            {
                case ViewActionType.LevelWin:
                    StartCoroutine(ShowLevelWindCoroutine());
                    break;
            }
        }

        private IEnumerator ShowLevelWindCoroutine()
        {
            _levelWinText.gameObject.SetActive(true);
            yield return new WaitForSeconds(_timeShowLevelWinText);
            _levelWinText.gameObject.SetActive(false);
            ActionDone?.Invoke(this, new UIViewActionArgs(ViewActionType.NextLevelButtonClick, CustomObject.Empty));
        }
    }
}
