using System;
using Animations.CurveMoveAnimation.Implementations;
using UnityEngine;

namespace Assets.Scripts.View
{
    public class GameElementView : MonoBehaviour
    {
        [SerializeField] private Animator _animator = default;
        [SerializeField] private SpriteRenderer _sprite = default;
        [SerializeField] private CurveAnimation _curveAnimation = default;

        private Action _dieCallBack;

        private void Start()
        {
            _animator.SetFloat("RandomStart", UnityEngine.Random.Range(0f,1f));
        }

        public void SetOrderInLayer(int layer)
        {
            _sprite.sortingOrder = layer;
        }

        public void Die(Action callBack)
        {
            _dieCallBack = callBack;
            _animator.SetTrigger("IsDestroy");
        }

        private void OnEndDie()
        {
            _dieCallBack?.Invoke();
        }

        public void Move(Vector3 position, Action callBack)
        {
            _curveAnimation.Play(position, callBack);
        }
    }
}
