
using System;
using Scripts.Controllers.Helpers;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Module.Input.Facade.Controllers
{
    public sealed class InputController : IInputController
    {
        public event EventHandler<FieldCoords> MouseDown;
        public event EventHandler<FieldCoords> MouseUp;
        public event EventHandler<FieldCoords> MouseMove;

        private const float DeltaRegistration = 10;
        private bool _isMouseDown;
        private bool _isOneClick;
        private float _doubleClickTime;
        private const float Delay = 0.6f;
        private Rect _dubleClickRect = new Rect(0, 0, 0, 0);
        private readonly Vector2 _dubleClickRectSize = new Vector2(50, 50); //область регистрации двойного тапа
        private Vector2 _previousMousePosition = Vector2.zero;

        private Vector3? _buttonDownPosition = null;

        public void CustomUpdate()
        {
            var mousePosition = UnityEngine.Input.mousePosition;

            if (mousePosition.x < 0//проверка на выход за пределы экрана (при сворачивании и разворачивании игры может придти Vector3.positiveInfinity)
                || mousePosition.y < 0
                || mousePosition.x > Screen.width
                || mousePosition.y > Screen.height
                || IsOverGui())
            {
                _isMouseDown = false;
                _isOneClick = false;
                _previousMousePosition = Vector2.zero;
                return;
            }

            if (UnityEngine.Input.GetKeyDown(KeyCode.Mouse0))
            {
                _isMouseDown = true;
                _buttonDownPosition = mousePosition;
                OnMouseDown(CoordinateConverter.CameraPositionToFieldCoords(mousePosition));
                if (!_isOneClick)
                {
                    _isOneClick = true;
                    _doubleClickTime = Time.time;
                    IsOverGui();
                }
                else if (_dubleClickRect.Contains(mousePosition))
                {
                    _isOneClick = false;
                    _buttonDownPosition = mousePosition;
                    return;
                }
                else
                {
                    _isOneClick = false;
                }

                _buttonDownPosition = mousePosition;
                _dubleClickRect =
                    new Rect(
                        new Vector2(mousePosition.x - _dubleClickRectSize.x / 2,
                            mousePosition.y - _dubleClickRectSize.y / 2), _dubleClickRectSize);
            }

            if (_isOneClick && Time.time - _doubleClickTime > Delay)
            {
                _isOneClick = false;
            }

            if (!_isMouseDown)
            {
                return;
            }

            if (UnityEngine.Input.GetKeyUp(KeyCode.Mouse0))
            {
                _isMouseDown = false;
                //if (IsCorrectSwipe())
                //{
                    OnMouseUp(CoordinateConverter.CameraPositionToFieldCoords(mousePosition));
               // }
                _buttonDownPosition = null;
                return;
            }
            if (_buttonDownPosition != null)
                if ((Mathf.Abs(_previousMousePosition.x - UnityEngine.Input.mousePosition.x) > DeltaRegistration
                     || Mathf.Abs(_previousMousePosition.y - UnityEngine.Input.mousePosition.y) > DeltaRegistration)
                    && (Mathf.Abs(_buttonDownPosition.Value.x - UnityEngine.Input.mousePosition.x) > DeltaRegistration
                        || Mathf.Abs(_buttonDownPosition.Value.y - UnityEngine.Input.mousePosition.y) > DeltaRegistration))
                {
                    _isOneClick = false;
                    OnMouseMove(CoordinateConverter.CameraPositionToFieldCoords(_buttonDownPosition.Value) - CoordinateConverter.CameraPositionToFieldCoords(UnityEngine.Input.mousePosition));
                }
        }

        //private bool IsCorrectSwipe()
        //{
        //    if (_buttonDownPosition == null)
        //    {
        //        return false;
        //    }
        //    var coordsStartSwipe = CoordinateConverter.CameraPositionToFieldCoords(_buttonDownPosition.Value);
        //    var coordsEndSwipe = CoordinateConverter.CameraPositionToFieldCoords(UnityEngine.Input.mousePosition);

        //    var biasCoords = coordsStartSwipe - coordsEndSwipe;

        //    if (Mathf.Abs(coordsStartSwipe.X - coordsEndSwipe.X) < Mathf.Abs(coordsStartSwipe.Y - coordsEndSwipe.Y))
        //    {
        //        biasCoords.Y = Mathf.Clamp(biasCoords.Y, -1, 1);
        //        biasCoords.X = 0;
        //        if (biasCoords.Y == 0)
        //        {
        //            return false;
        //        }
        //    }
        //    else
        //    {
        //        biasCoords.X = Mathf.Clamp(biasCoords.X, -1, 1);
        //        biasCoords.Y = 0;
        //        if (biasCoords.X == 0)
        //        {
        //            return false;
        //        }
        //    }

        //    return true;
        //}

        private bool IsOverGui()
        {
            if (EventSystem.current.IsPointerOverGameObject())
            {
                return true;
            }

            if (UnityEngine.Input.touchCount > 0 && UnityEngine.Input.GetTouch(0).phase != TouchPhase.Ended)
            {
                if (EventSystem.current.IsPointerOverGameObject(UnityEngine.Input.GetTouch(0).fingerId))
                {
                    return true;
                }
            }
            return false;
        }

        private void OnMouseDown(FieldCoords coords)
        {
            MouseDown?.Invoke(this, coords);
        }

        private void OnMouseUp(FieldCoords coords)
        {
            MouseUp?.Invoke(this, coords);
        }

        private void OnMouseMove(FieldCoords coords)
        {
            MouseMove?.Invoke(this, coords);
        }
    }
}