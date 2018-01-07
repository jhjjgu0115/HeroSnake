using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
[RequireComponent(typeof(RectTransform))]
public class TabButton : MonoBehaviour
{
    
    Image image;
    RectTransform rect;

    public TabManager parentManager;
    public Sprite selectedImage;
    public Sprite noneSelectedImage;

    public void Clicked()
    {
        parentManager.TabSelected(this);
    }

    public void IsSelected(bool isSelect)
    {
        if (isSelect)
        {
            image.sprite = selectedImage;
        }
        else
        {
            image.sprite = noneSelectedImage;
        }
    }

    // Use this for initialization
    void Awake()
    {
        image = GetComponent<Image>();
        rect = GetComponent<RectTransform>();
    }
    private void Start()
    {
        
    }
}
