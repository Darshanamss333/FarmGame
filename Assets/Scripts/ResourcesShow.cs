using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DsoftGameKit
{
    public class ResourcesShow : MonoBehaviour
    {
        public static Transform ResourcesUiParent
        {
            get
            {
                return GameObject.Find("ResourcesUiParent").transform;
            }
        }

        public static Vector3 ResourcesUiParentWorldPos
        {
            get
            {
                return Camera.main.ScreenToWorldPoint(new Vector3(ResourcesUiParent.transform.position.x, ResourcesUiParent.transform.position.y, 1));
            }
        }

        public static void Drop(ResourcesManager.ResType _type , Vector3 _start, Vector3 _end)
        {
            GameObject _new = Instantiate(Resources.Load("ResorcesShow") as GameObject);
            ResourcesShow _show = _new.gameObject.AddComponent<ResourcesShow>();
            SpriteRenderer _sp = _new.transform.GetChild(0).GetComponent<SpriteRenderer>();

            foreach (var item in AppManager.Data._Pack._List)
            {
                if (item.GetType() == _type.GetType()) _sp.sprite = item._Image;
            }

            Wait _newwait = new Wait(0.3f);
            Farmer _fm = AppManager.Farmer;
            GameObject _uipos = ResourcesUiParent.gameObject;
            //Vector3 _endpos = Camera.main.ScreenToWorldPoint(new Vector3(_uipos.transform.position.x, _uipos.transform.position.y, 10));
            _newwait.OnWaitAction += delegate
            {
                _new.transform.position = Vector3.Lerp(_start, _end, _newwait.Tang);
                _new.transform.rotation = Quaternion.LookRotation(Camera.main.transform.forward);
                _new.transform.localScale = Vector3.Distance(Camera.main.transform.position, _new.transform.position) * Vector3.one * 0.05f;
            };

            SoundManager._Instance.PlaySound("PopSound");
            _newwait.OnTimeOutAction += delegate
            {
                Destroy(_new);
            };
        }
    }
}
