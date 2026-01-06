using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace DsoftGameKit
{
    public class UiTest : MonoBehaviour
    {
        void Start()
        {
            WindowUi _wind = DsoftUi.Create<WindowUi>("hello",null);
            HorizonthalLayoutUi _layout = DsoftUi.Create<HorizonthalLayoutUi>("", _wind.Body);
            WindowUi _wind2 = DsoftUi.Create<WindowUi>("hello2", _layout.Body);
            WindowUi _wind3 = DsoftUi.Create<WindowUi>("hello3", _layout.Body);

        }
    }

}
