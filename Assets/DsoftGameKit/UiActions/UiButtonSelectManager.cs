using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace DsoftGameKit
{
    public class UiButtonSelectManager : MonoBehaviour
    {
        [SerializeField] bool HorizanthalControl = true;
        [SerializeField] bool VerticalControl = true;

        int _index;

        private void OnEnable()
        {
            UpdateIndex();
        }


        bool _horizanthalReady;
        bool _VerticalReady;
        bool _SubmitReady;
        bool _CancelReady;
        private void Update()
        {
            if(HorizanthalControl)
            {
                if (Input.GetAxis("Horizontal") > 0 && _horizanthalReady)
                {
                    _horizanthalReady = false;
                    _index = (int)Mathf.Repeat(_index + 1, transform.childCount);
                    UpdateIndex();
                }

                if (Input.GetAxis("Horizontal") < 0 && _horizanthalReady)
                {
                    _horizanthalReady = false;
                    _index = (int)Mathf.Repeat(_index - 1, transform.childCount);
                    UpdateIndex();
                }

                if (Input.GetAxis("Horizontal") == 0 && _horizanthalReady == false)
                {
                    _horizanthalReady = true;
                }
            }


            if (VerticalControl)
            {
                if (Input.GetAxis("Vertical") > 0 && _VerticalReady)
                {
                    _VerticalReady = false;
                    _index = (int)Mathf.Repeat(_index - 1, transform.childCount);
                    UpdateIndex();
                }

                if (Input.GetAxis("Vertical") < 0 && _VerticalReady)
                {
                    _VerticalReady = false;
                    _index = (int)Mathf.Repeat(_index + 1, transform.childCount);
                    UpdateIndex();
                }

                if (Input.GetAxis("Vertical") == 0 && _VerticalReady == false)
                {
                    _VerticalReady = true;
                }
            }


            if (Input.GetAxis("Submit") > 0 && _SubmitReady)
            {
                _SubmitReady = false;
                Submit();
            }

            if (Input.GetAxis("Submit") == 0 && _SubmitReady == false)
            {
                _SubmitReady = true;
            }


            if (Input.GetAxis("Cancel") > 0 && _CancelReady)
            {
                _CancelReady = false;
                Cancel();
            }

            if (Input.GetAxis("Cancel") == 0 && _CancelReady == false)
            {
                _CancelReady = true;
            }
        }

        [SerializeField] GameObject _Arraw;
        void UpdateIndex()
        {
            _Arraw.transform.position = transform.GetChild(_index).transform.position;
        }


        [SerializeField] UnityEvent _SubmitEvent;
        void Submit()
        {
            transform.GetChild(_index).GetComponent<Button>().onClick.Invoke();
            _SubmitEvent?.Invoke();
        }

        [SerializeField] UnityEvent _CancelEvent;
        void Cancel()
        {
            _CancelEvent?.Invoke();
        }
    }
}
