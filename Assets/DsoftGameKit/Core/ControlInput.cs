using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace DsoftGameKit
{
    public class ControlInput : MonoBehaviour
    {
        public enum InputEventEnum
        {
            OnDown, OnHold, OnUp
        }

        [System.Serializable]
        public class InputUnitClass
        {
            public int _Id;
            public InputEventEnum _Event;
            public Vector3 _StartPos;
            public Vector3 _CurrentPos;
            public Vector3 _DeltaPos;
            public Vector3 _Dir;
            public Vector3 _FirstDir;
            public float _Time;
            public bool _Click;
        }

        [System.Serializable]
        public class InputClass
        {
            public List<InputUnitClass> _Units;

            public InputClass()
            {
                _Units = new List<InputUnitClass>();
            }

            public bool _IsMultiInput
            {
                get
                {
                    if (_Units[0]._Id != -1 && _Units[1]._Id != -1)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }

            public InputUnitClass GetFist()
            {
                if (_Units[0]._Id == -1)
                {
                    return _Units[1];
                }
                else
                {
                    return _Units[0];
                }

            }

            public InputUnitClass GetByIndex(int i)
            {
                return _Units[i];
            }
        }

        public InputClass _Inputs = new InputClass();

        InputUnitClass GetInput(int _id)
        {
            InputUnitClass _result = null;
            for (int i = 0; i < _Inputs._Units.Count; i++)
            {
                if (_Inputs._Units[i]._Id == _id)
                {
                    _result = _Inputs._Units[i];
                }
            }

            if (_result == null)
            {
                for (int i = 0; i < _Inputs._Units.Count; i++)
                {
                    if (_Inputs._Units[i]._Id == -1 && _result == null)
                    {
                        _result = _Inputs._Units[i];
                    }
                }
            }

            return _result;
        }

        void DeleteInput(int _id)
        {
            for (int i = 0; i < _Inputs._Units.Count; i++)
            {
                if (_Inputs._Units[i]._Id == _id) _Inputs._Units[i]._Id = -1;
            }
        }

        private void Update()
        {
            if (_Inputs._Units.Count < 2)
            {
                InputUnitClass _new = new InputUnitClass();
                _new._Id = -1;
                _Inputs._Units.Add(_new);
            }

            if (Input.touchCount > 0)
            {
                for (int i = 0; i < Input.touchCount; i++)
                {
                    if (i == 0 | i == 1)
                    {
                        Touch t = Input.touches[i];

                        InputUnitClass _currentInput = GetInput(t.fingerId);

                        if (t.phase == TouchPhase.Began)
                        {
                            _currentInput._Id = t.fingerId;
                            _currentInput._Event = InputEventEnum.OnDown;
                            _currentInput._StartPos = t.position;
                            _currentInput._CurrentPos = t.position;
                            _currentInput._DeltaPos = t.position;
                            _currentInput._Dir = Vector3.zero;
                            _currentInput._FirstDir = Vector3.zero;
                            _currentInput._Time = 0;
                            _currentInput._Click = false;
                            Icontrol.ControlInput(_Inputs);
                        }

                        if (t.phase == TouchPhase.Moved | t.phase == TouchPhase.Stationary)
                        {
                            _currentInput._Event = InputEventEnum.OnHold;
                            _currentInput._CurrentPos = t.position;
                            _currentInput._Dir = _currentInput._CurrentPos - _currentInput._DeltaPos;
                            if (_currentInput._FirstDir == Vector3.zero) _currentInput._FirstDir = _currentInput._Dir;
                            _currentInput._Time += Time.deltaTime;
                            Icontrol.ControlInput(_Inputs);
                        }

                        if (t.phase == TouchPhase.Ended | t.phase == TouchPhase.Canceled)
                        {
                            _currentInput._Event = InputEventEnum.OnUp;
                            _currentInput._CurrentPos = t.position;
                            _currentInput._Dir = _currentInput._CurrentPos - _currentInput._DeltaPos;
                            if (_currentInput._Time < 0.2f) _currentInput._Click = true;
                            Icontrol.ControlInput(_Inputs);
                            DeleteInput(t.fingerId);
                        }

                        _currentInput._DeltaPos = t.position;
                    }
                }
            }

            //---------------mouse
            if(Application.isEditor)
            {
                InputUnitClass _currentInput = GetInput(0);

                if (Input.GetMouseButtonDown(0))
                {
                    _currentInput._Id = 0;
                    _currentInput._Event = InputEventEnum.OnDown;
                    _currentInput._StartPos = Input.mousePosition;
                    _currentInput._CurrentPos = Input.mousePosition;
                    _currentInput._DeltaPos = Input.mousePosition;
                    _currentInput._Dir = Vector3.zero;
                    _currentInput._FirstDir = Vector3.zero;
                    _currentInput._Time = 0;
                    _currentInput._Click = false;
                    Icontrol.ControlInput(_Inputs);
                }

                if (Input.GetMouseButton(0))
                {
                    _currentInput._Event = InputEventEnum.OnHold;
                    _currentInput._CurrentPos = Input.mousePosition;
                    _currentInput._Dir = _currentInput._CurrentPos - _currentInput._DeltaPos;
                    if (_currentInput._FirstDir == Vector3.zero) _currentInput._FirstDir = _currentInput._Dir;
                    _currentInput._Time += Time.deltaTime;
                    Icontrol.ControlInput(_Inputs);
                }

                if (Input.GetMouseButtonUp(0))
                {
                    _currentInput._Event = InputEventEnum.OnUp;
                    _currentInput._CurrentPos = Input.mousePosition;
                    _currentInput._Dir = _currentInput._CurrentPos - _currentInput._DeltaPos;
                    if (_currentInput._Time < 0.2f) _currentInput._Click = true;
                    Icontrol.ControlInput(_Inputs);
                    DeleteInput(0);
                }

                _currentInput._DeltaPos = Input.mousePosition;


                InputUnitClass _secondInput = GetInput(1);
                if (Input.GetKeyDown(KeyCode.LeftShift))
                {
                    _secondInput._Id = 1;
                    _secondInput._Event = InputEventEnum.OnDown;
                    _secondInput._StartPos = Input.mousePosition;
                    _secondInput._CurrentPos = _secondInput._StartPos;
                    _secondInput._DeltaPos = _secondInput._StartPos;
                    _secondInput._Dir = Vector3.zero;
                    _secondInput._FirstDir = Vector3.zero;
                    _secondInput._Time = 0;
                    _secondInput._Click = false;
                    Icontrol.ControlInput(_Inputs);
                }

                if (Input.GetKey(KeyCode.LeftShift))
                {
                    _secondInput._Event = InputEventEnum.OnHold;
                    _secondInput._CurrentPos = _secondInput._StartPos;
                    _secondInput._Dir = Vector3.zero;
                    Icontrol.ControlInput(_Inputs);
                }

                if (Input.GetKeyUp(KeyCode.LeftShift))
                {
                    _secondInput._Event = InputEventEnum.OnUp;
                    _secondInput._CurrentPos = _secondInput._StartPos;
                    _secondInput._Dir = Vector3.zero;
                    Icontrol.ControlInput(_Inputs);
                    DeleteInput(1);
                }

                _secondInput._DeltaPos = _secondInput._StartPos;
            }
        }

        IControlInput Icontrol;
        private void Start()
        {
            Icontrol = GetComponent<IControlInput>();
        }
    }

}
