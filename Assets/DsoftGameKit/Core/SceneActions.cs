using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace DsoftGameKit
{
    public class SceneActions : MonoBehaviour
    {
        public void _RestartScene()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }


        public void _LoadScene(string _value)
        {
            SceneManager.LoadScene(_value);
        }

        public void _LoadSceneAdd(string _value)
        {
            SceneManager.LoadScene(_value, LoadSceneMode.Additive);
        }
    }
}
