using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FPSCounter : MonoBehaviour
{
    [SerializeField]
    Text Fps;
    private void Update()
    {
        float current = 0;
        current = (int)(1f / Time.unscaledDeltaTime);
        Fps.text = "fps " + current.ToString();
    }

}
