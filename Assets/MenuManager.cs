using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    [SerializeField]
    Dictionary<string, GameObject> menuDict = new Dictionary<string, GameObject>();
    List<GameObject> menuList = new List<GameObject>();
    public GameObject mainMenu;
    public GameObject title;
    public void ShowWindow(string menuName)
    {
        menuDict[menuName].SetActive(true);
        mainMenu.SetActive(false);
        title.SetActive(false);
    }
    public void CloseWindow()
    {
        mainMenu.SetActive(true);
        title.SetActive(true);
    }
    private void Start()
    {
        for(int index=0;index<transform.childCount;index++)
        {
            menuDict.Add(transform.GetChild(index).name, transform.GetChild(index).gameObject);
        }
    }

}
