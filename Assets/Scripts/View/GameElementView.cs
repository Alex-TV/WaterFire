using System;
using UnityEngine;

namespace Assets.Scripts.View
{
    public class GameElementView : MonoBehaviour
    {
        [SerializeField] private Animator _animator = default;
        [SerializeField] private SpriteRenderer _sprite = default;

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
    }
}
