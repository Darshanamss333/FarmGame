using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


namespace DsoftGameKit
{
    public class LevelManagerWindowUi : MonoBehaviour
    {
        [SerializeField] GameObject _prefab;
        [SerializeField] GameObject _content;
        private void Start()
        {
            for (int i = 0; i < 20; i++)
            {
                GameObject _new = Instantiate(_prefab, _content.transform);
                Button _button = _new.GetComponent<Button>();
                GameObject _lock = _new.transform.Find("Lock").gameObject;
                Text _number = _new.transform.Find("Number").gameObject.GetComponent<Text>();
                GameObject _stars = _new.transform.Find("Stars").gameObject;

                if(LevelManager._Instance.SelectedLevel >= i)
                {
                    int _index = i;
                    _button.onClick.AddListener(delegate
                    {
                        LevelManager._Instance.SelectedLevel = _index;
                        SceneManager.LoadScene("Levels");
                    }
                    );
                    _lock.SetActive(false);
                    _number.gameObject.SetActive(true);
                    _stars.gameObject.SetActive(true);
                }
                else
                {
                    _lock.SetActive(true);
                    _number.gameObject.SetActive(false);
                    _stars.gameObject.SetActive(false);
                }

                _number.text = (i + 1).ToString();
            }
        }
    }

}
