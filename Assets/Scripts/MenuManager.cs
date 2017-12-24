using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    [SerializeField]
    Dictionary<string, GameObject> menuDict = new Dictionary<string, GameObject>();
    List<GameObject> menuList = new List<GameObject>();
    public List<GameObject> mainList = new List<GameObject>();
    public List<GameObject> NoneChangeList = new List<GameObject>();
    public void ShowWindow(string menuName)
    {
        menuDict[menuName].SetActive(true);
        for (int index = 0; index < mainList.Count; index++)
        {
            mainList[index].SetActive(false);
        }
    }
    public void CloseWindow()
    {
        for (int index = 0; index < mainList.Count; index++)
        {
            mainList[index].SetActive(true);
        }
    }
    private void Start()
    {
        for(int index=0;index<transform.childCount;index++)
        {
            menuDict.Add(transform.GetChild(index).name, transform.GetChild(index).gameObject);
            transform.GetChild(index).gameObject.SetActive(false);
        }
        for(int index=0;index< mainList.Count;index++)
        {
            mainList[index].SetActive(true);
        }
    }

}
