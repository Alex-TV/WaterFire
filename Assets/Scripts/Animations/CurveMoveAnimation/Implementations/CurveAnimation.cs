using System;
using System.Threading.Tasks;
using UnityEngine;

namespace Animations.CurveMoveAnimation.Implementations
{
    public class CurveAnimation : MonoBehaviour
    {
        [SerializeField] private float _duration =default;
        [SerializeField] private AnimationCurve _curveType = default;
        [SerializeField] private Vector2 _amplitude = default;
        [SerializeField] private Vector3 _targetPosition = default;
        [SerializeField] private float _a = 10f;


        public async void Play(Vector3 position, Action callBack)
        {
            _targetPosition = position;
            await PlayAnimation();
            callBack.Invoke();
        }

        private async Task PlayAnimation()
        {
            var time = 0f;
            Vector2 startPosition = transform.position;
            while(time <= _duration)
            {
                var progress = time / _duration ;
                var sinValue = GetPositionAdditionalValue(_curveType, progress);

                var updatedPosition = Vector2.Lerp(startPosition, _targetPosition, progress);
                updatedPosition += sinValue * _amplitude;
                transform.position = updatedPosition;

                time += Time.deltaTime;
                await Task.Yield();
            }
            transform.position = _targetPosition;
        }

        private float GetPositionAdditionalValue(AnimationCurve type, float progress)
        {
            return progress * type.Evaluate(progress);
        }
    }
}
