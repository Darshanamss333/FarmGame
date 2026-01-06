using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DsoftGameKit
{
    public class SimpleTransformControlVehicle : MonoBehaviour
    {
        [SerializeField] FloatReference _AccInput;
        [SerializeField] FloatReference _Accelaration;
        [SerializeField] FloatReference _MaxSpeed;
        [SerializeField] FloatReference _SteeringInput;
        [SerializeField] FloatReference _SteeringValue;
        [SerializeField] FloatReference _MaxSteeringValue;
        [SerializeField] bool _SteeringRotation = true;
        [SerializeField] GameObject _VehicelModel;
        Path3D _path;

        private void Update()
        {
            Accelaration();
            CarSliding();
            Steerin();
        }

        float _Speed;
        void Accelaration()
        {
            if (_AccInput.Value > 0)
            {
                if (_MaxSpeed.Value > _Speed)
                {
                    _Speed += Time.deltaTime * _AccInput.Value * _Accelaration.Value;
                }
            }
            else
            {
                _Speed = Mathf.Clamp(_Speed - Time.deltaTime, 0, _MaxSpeed.Value);
            }

            transform.Translate(new Vector3(0, 0, _Speed * Time.deltaTime), Space.Self);
        }

        Vector3 _deltaPos;
        public float angle;
        [SerializeField] FloatReference _SlidingValue;
        void CarSliding()
        {
            //if(_deltaPos == Vector3.zero) _deltaPos = transform.position;
            if(Vector3.Distance(_deltaPos, transform.position) > 1)
            {
                Vector3 _dir = transform.position - _deltaPos;
                _dir.y = 0;
                angle = Vector3.SignedAngle(_dir, transform.forward, Vector3.up);

                if (_VehicelModel) _VehicelModel.transform.localPosition = new Vector3(Mathf.Lerp(_VehicelModel.transform.localPosition.x, angle * -1 * _SlidingValue.Value, Time.deltaTime), 0, 0);
                _deltaPos = transform.position;
            }
        }


        void Steerin()
        {
            if (_VehicelModel)
            {
                float _speedFactor = Mathf.InverseLerp(0,10, _Speed);

                if(_SteeringRotation) _VehicelModel.transform.localRotation = Quaternion.Lerp(_VehicelModel.transform.localRotation,
                Quaternion.Euler(0, _SteeringInput.Value * 30, 0), Time.deltaTime * _SteeringValue.Value * _speedFactor);

                _VehicelModel.transform.localPosition = new Vector3(Mathf.Clamp(Mathf.Lerp(_VehicelModel.transform.localPosition.x,
                    _VehicelModel.transform.localPosition.x + _SteeringInput.Value, Time.deltaTime * _SteeringValue.Value * _speedFactor),-_MaxSteeringValue.Value,_MaxSteeringValue.Value)
                    , 0, 0);
            }
        }
    }
}
