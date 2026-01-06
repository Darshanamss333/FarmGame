using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Android;

namespace DsoftGameKit
{
    public static class DsoftFileBrowser
    {
        public static bool AskPermission()
        {
            if (!Application.isEditor && Application.platform == RuntimePlatform.Android)
            {
                if (!Permission.HasUserAuthorizedPermission("android.permission.READ_EXTERNAL_STORAGE"))
                {
                    Permission.RequestUserPermission("android.permission.READ_EXTERNAL_STORAGE");
                }

                if (!Permission.HasUserAuthorizedPermission("android.permission.WRITE_EXTERNAL_STORAGE"))
                {
                    Permission.RequestUserPermission("android.permission.WRITE_EXTERNAL_STORAGE");
                }

                if (Permission.HasUserAuthorizedPermission("android.permission.READ_EXTERNAL_STORAGE") &&
                    Permission.HasUserAuthorizedPermission("android.permission.WRITE_EXTERNAL_STORAGE"))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return true;
            }
        }
        public static void PickDirectory(UnityAction<string> _OnComplete)
        {
            if(AskPermission())
            {
                GameObject _new = GameObject.Instantiate(Resources.Load("DsoftFileBrowserCanvas") as GameObject);
                DsoftFileBrowserHelper _helper = _new.GetComponent<DsoftFileBrowserHelper>();
                _helper.Init(false, _OnComplete);
            }
        }

        public static void PickFile(UnityAction<string> _OnComplete)
        {
            if(AskPermission())
            {
                GameObject _new = GameObject.Instantiate(Resources.Load("DsoftFileBrowserCanvas") as GameObject);
                DsoftFileBrowserHelper _helper = _new.GetComponent<DsoftFileBrowserHelper>();
                _helper.Init(true, _OnComplete);
            }
        }
    }
}
