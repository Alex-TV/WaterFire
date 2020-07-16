using UnityEngine;

namespace Controllers.Helpers
{
    public class CameraConstantWidth : MonoBehaviour
    {
        [SerializeField] private Vector2 _defaultResolution = new Vector2(720, 1280);
        [Range(0f, 1f)] [SerializeField] private float _widthOrHeight = 0;
        [SerializeField] private Camera _сamera = default;

        private void Start()
        {
            var initialSize = _сamera.orthographicSize;
            var targetAspect = _defaultResolution.x / _defaultResolution.y;
            var initialFov = _сamera.fieldOfView;

            var horizontalFov = CalcVerticalFov(initialFov, 1 / targetAspect);
            if (_сamera.orthographic)
            {
                var constantWidthSize = initialSize * (targetAspect / _сamera.aspect);
                _сamera.orthographicSize = Mathf.Lerp(constantWidthSize, initialSize, _widthOrHeight);
            }
            else
            {
                var constantWidthFov = CalcVerticalFov(horizontalFov, _сamera.aspect);
                _сamera.fieldOfView = Mathf.Lerp(constantWidthFov, initialFov, _widthOrHeight);
            }
        }

        private float CalcVerticalFov(float hFovInDeg, float aspectRatio)
        {
            var hFovInRads = hFovInDeg * Mathf.Deg2Rad;
            var vFovInRads = 2 * Mathf.Atan(Mathf.Tan(hFovInRads / 2) / aspectRatio);
            return vFovInRads * Mathf.Rad2Deg;
        }
    }
}
