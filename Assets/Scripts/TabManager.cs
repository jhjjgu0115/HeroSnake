using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TabManager : MonoBehaviour
{
    [SerializeField]
    List<TabButton> tabList = new List<TabButton>();
    [SerializeField]
    List<TabPage> pageList = new List<TabPage>();
    TabButton selectedTab;

    public E_Axis startAxis=E_Axis.Horizontal;
    
    public Vector2 nonSelectedOffset = new Vector2();

    private void OnEnable()
    {
        if(tabList.Count>0)
        {
            tabList[0].Clicked();
        }
    }
    void Start ()
    {
        tabList.AddRange(GetComponentsInChildren<TabButton>());

        float anchorStart = 1;
        if(startAxis==E_Axis.Horizontal)
        {
            anchorStart = 0;
        }
        else
        {
            anchorStart = 1;
        }
        float anchorinterval = (float)1/transform.childCount;
        for (int index = 0;index<transform.childCount;index++)
        {
            RectTransform temp = tabList[index].GetComponent<RectTransform>();
            tabList[index].parentManager = this;
            if(startAxis==E_Axis.Horizontal)
            {
                temp.anchorMin = new Vector2(anchorStart, 0);
                temp.anchorMax = new Vector2(anchorStart + anchorinterval, 1);
                temp.offsetMax -= nonSelectedOffset;
                anchorStart += anchorinterval;
            }
            else
            {
                temp.anchorMin = new Vector2(0, anchorStart - anchorinterval);
                temp.anchorMax = new Vector2(1, anchorStart);
                temp.offsetMin -= nonSelectedOffset;
                anchorStart -= anchorinterval;
            }

            
        }
        if(tabList.Count>0)
        {
            selectedTab = tabList[0];
            ShowPage(0);
            if (startAxis==E_Axis.Horizontal)
            {
                selectedTab.GetComponent<RectTransform>().offsetMax += nonSelectedOffset;
            }
            else
            {
                selectedTab.GetComponent<RectTransform>().offsetMin += nonSelectedOffset;
            }
            selectedTab.Clicked();
        }

	}
    void ShowPage(int index)
    {
        if(index< pageList.Count)
        {
            //보여준다.
        }
    }
    public void TabSelected(TabButton tab)
    {
        selectedTab.IsSelected(false);
        tab.IsSelected(true);
        if (startAxis == E_Axis.Horizontal)
        {
            selectedTab.GetComponent<RectTransform>().offsetMax -= nonSelectedOffset;
            tab.GetComponent<RectTransform>().offsetMax += nonSelectedOffset;
        }
        else
        {
            selectedTab.GetComponent<RectTransform>().offsetMin -= nonSelectedOffset;
            tab.GetComponent<RectTransform>().offsetMin += nonSelectedOffset;
        }
        selectedTab = tab;
        ShowPage(tabList.FindIndex(t=>t==tab));
    }

}
