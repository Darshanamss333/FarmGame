using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour 
{
    private void Update()
    {
        LookAtCamera();
    }

    [SerializeField]
    Transform _bar;
    public void HealthUpdate(float _max , float _value)
    {
        _bar.transform.localScale = new Vector3(Mathf.InverseLerp(0, _max, _value), 1, 1);
    }

    void LookAtCamera()
    {
        transform.LookAt(Camera.main.transform, Vector3.up);
    }
}
