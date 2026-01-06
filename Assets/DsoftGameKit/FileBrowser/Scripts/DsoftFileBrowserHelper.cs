using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using UnityEngine.Events;
using UnityEngine.Android;

namespace DsoftGameKit
{
    public class DsoftFileBrowserHelper : MonoBehaviour
    {
        DirectoryInfo _url;
        DirectoryInfo Url
        {
            get
            {
                return _url;
            }
            set
            {
                _url = value;
                _SelectedFile = null;
                RefreshUi();
            }
        }

        bool _filePickMode = false;
        UnityAction<string> _OnSelect;
        FileInfo _SelectedFile;

        public void Init(bool _fileMode , UnityAction<string> _onSelect)
        {
            _filePickMode = _fileMode;
            _OnSelect = _onSelect;
            Load();
            _CloseButton.onClick.AddListener(delegate
            {
                Close();
            });

            Url = new DirectoryInfo(_data._LastDirectory);
        }

        public class DsoftFileBrowserDataClass
        {
            public string _LastDirectory;

            public DsoftFileBrowserDataClass()
            {
                if (!Application.isEditor && Application.platform == RuntimePlatform.Android)
                {
                    _LastDirectory = "/storage/emulated/0/";
                }
                else
                {
                    _LastDirectory = Application.persistentDataPath;
                }
            }
        }
        DsoftFileBrowserDataClass _data;
        void Load()
        {
            if(SaveLoad.Exists("DsoftFileBrowserData"))
            {
                _data = SaveLoad.Load<DsoftFileBrowserDataClass>("DsoftFileBrowserData");
            }
            else
            {
                _data = new DsoftFileBrowserDataClass();
            }
        }

        void Save()
        {
            _data._LastDirectory = Url.FullName;
            SaveLoad.Save<DsoftFileBrowserDataClass>(_data, "DsoftFileBrowserData");
        }

        void Close()
        {
            Destroy(gameObject);
        }

        [SerializeField] Button _FolderPrefabButton;
        [SerializeField] Button _FilePrefabButton;
        [SerializeField] GameObject _ContentParent;
        [SerializeField] Button _BackButton;
        [SerializeField] Button _SelectButon;
        [SerializeField] Button _CloseButton;
        void RefreshUi()
        {
            if (_ContentParent.transform.childCount > 0)
            {
                foreach (Transform child in _ContentParent.transform)
                {
                    Destroy(child.gameObject);
                }
            }

;
            foreach (DirectoryInfo item in Url.GetDirectories())
            {
                if(item.Name[0] != '.')
                {
                    GameObject _new = Instantiate(_FolderPrefabButton.gameObject, _ContentParent.transform);
                    Button _newButton = _new.GetComponent<Button>();

                    _newButton.onClick.AddListener(delegate
                    {
                        Url = item;
                    });

                    Text _newFolderName = _new.transform.GetChild(1).GetComponent<Text>();
                    _newFolderName.text = item.Name;
                }

            }

            foreach (FileInfo item in Url.GetFiles("*.*"))
            {
                GameObject _new = Instantiate(_FilePrefabButton.gameObject, _ContentParent.transform);
                Button _newButton = _new.GetComponent<Button>();

                _newButton.onClick.AddListener(delegate
                {
                    _SelectedFile = item;
                    RefreshUi();
                });

                if (_SelectedFile != null && _SelectedFile.FullName == item.FullName)
                {
                    _newButton.interactable = false;
                }
                else
                {
                    _newButton.interactable = true;
                }


                Text _newFileName = _new.transform.GetChild(1).GetComponent<Text>();
                _newFileName.text = item.Name;
            }


            if (Url.Parent != null)
            {
                _BackButton.onClick.RemoveAllListeners();
                _BackButton.onClick.AddListener(delegate
                {
                    Url = Url.Parent;
                });
                _BackButton.interactable = true;
            }
            else
            {
                _BackButton.interactable = false;
            }

            _SelectButon.onClick.RemoveAllListeners();
            _SelectButon.onClick.AddListener(delegate
            {
                if(_filePickMode == false)
                {
                    _OnSelect?.Invoke(Url.FullName);
                }
                else
                {
                    _OnSelect?.Invoke(_SelectedFile.FullName);
                }
                Save();
                Close();
            });

            if(_filePickMode == false)
            {
                _SelectButon.interactable = true;
            }
            else
            {
                if (_SelectedFile == null)
                {
                    _SelectButon.interactable = false;
                }
                else
                {
                    _SelectButon.interactable = true;
                }
            }
        }
    }
}
