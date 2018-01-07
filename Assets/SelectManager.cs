using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine; 

public class SelectManager : MonoBehaviour
{
    TabManager tabManager;

    private void Start()
    {
        tabManager = GetComponentInChildren<TabManager>();
    }
    public void ClassSelect(string className)
    {
        E_Class slectedClass = (E_Class)Enum.Parse(typeof(E_Class), className);
        Debug.Log(slectedClass);

    }


}
