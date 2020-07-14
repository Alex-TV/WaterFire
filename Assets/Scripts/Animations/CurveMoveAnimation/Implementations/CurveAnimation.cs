using System;
using System.Collections;
using Assets.Scripts.Utils.Animations.CurveMoveAnimation.Helpers;
using UnityEngine;

namespace Animations.CurveMoveAnimation.Implementations
{
    public class CurveAnimation : MonoBehaviour
    {
        [SerializeField] private float _duration;
        [SerializeField] private CurveType _curveType;

        [SerializeField] private float _amplitudeX;
        [SerializeField] private float _amplitudeY;

        [SerializeField] private Vector3 _targetPosition;

        public float Duration => _duration;

        public Action AnimationCompleted { get; set; }
        private void OnAnimationCompleted()
        {
            AnimationCompleted?.Invoke();
        }

        public void Play()
        {
            StartCoroutine(AnimationCoroutine(_duration, _targetPosition, _amplitudeX, _amplitudeY));
        }

        public void Play(Vector3 position, Action callBack)
        {
            AnimationCompleted = callBack;
            _targetPosition = position;
            StartCoroutine(AnimationCoroutine(_duration, _targetPosition, _amplitudeX, _amplitudeY));
        }

        private IEnumerator AnimationCoroutine(float duration, Vector3 targetPosition, float jumpAmplitudeX, float jumpAmplitudeY)
        {
            var time = 0f;
            Vector2 startPosition = transform.position;
            while(time <= duration)
            {
                var progress = time / duration;
                var sinValue = GetPositionAdditionalValue(_curveType, progress);

                Vector2 updatedPosition = Vector2.Lerp(startPosition, targetPosition, progress);
                updatedPosition.x += sinValue * jumpAmplitudeX;
                updatedPosition.y += sinValue * jumpAmplitudeY;

                transform.position = updatedPosition;

                time += Time.deltaTime;
                yield return null;
            }
            transform.position = targetPosition;

            OnAnimationCompleted();
        }

        private float GetPositionAdditionalValue(CurveType type, float progress)
        {
            switch (type)
            {
                case CurveType.Sin:
                    return Mathf.Sin(progress * Mathf.PI);                  
                default:
                    return 0f;
            }
        }
    }
}
