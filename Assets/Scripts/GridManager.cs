using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GridManager : MonoBehaviour
{
    static GridManager instance;
    public static GridManager Instance
    {
        get
        {
            if (!instance)
            {
                instance = FindObjectOfType<GridManager>();
                if (!instance)
                {
                    GameObject container = new GameObject();
                    container.name = "GridManager";
                    instance = container.AddComponent<GridManager>();
                }
            }
            return instance;
        }
    }
    GridLayoutGroup gridLayout;
    public int GridCount=0;
    float width;
    float height;
    // Use this for initialization
    void Start ()
    {
        gridLayout = GetComponent<GridLayoutGroup>();
        GridCount = transform.childCount;
    }
	
	// Update is called once per frame
	void Update ()
    {
        float width = gameObject.GetComponent<RectTransform>().rect.width;
        float height = gameObject.GetComponent<RectTransform>().rect.height;
        Vector2 newSize;
        switch (gridLayout.startAxis)
        {
            case GridLayoutGroup.Axis.Horizontal:
                newSize = new Vector2(width / GridCount, height);
                break;
            case GridLayoutGroup.Axis.Vertical:
                newSize = new Vector2(width, height / GridCount);
                break;
            default:
                newSize = new Vector2(width, height);
                break;
        }
        gameObject.GetComponent<GridLayoutGroup>().cellSize = newSize;

    }
}
