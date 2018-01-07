using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using TMPro;
using System;


public class KeyBinder : MonoBehaviour
{
    static KeyBinder instance;
    public static KeyBinder Instance
    {
        get
        {
            if (!instance)
            {
                instance = FindObjectOfType<KeyBinder>();
                if (!instance)
                {
                    GameObject container = new GameObject();
                    container.name = "KeyBinder";
                    instance = container.AddComponent<KeyBinder>();
                }
            }
            return instance;
        }
    }

    public KeyEntry keyEntry;
    public GridLayoutGroup gridArea;

    public delegate void KeyEvent(E_Direction direction);
    public event KeyEvent Up;
    public event KeyEvent Left;
    public event KeyEvent Down;
    public event KeyEvent Right;


    Dictionary<E_Direction, KeyCode> keyDict = new Dictionary<E_Direction, KeyCode>();
    KeyEntry currentKey;



    void Start()
    {
        foreach (E_Direction key in Enum.GetValues(typeof(E_Direction)))
        {
            keyDict.Add(key, (KeyCode)Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString(key.ToString(),KeyCode.W.ToString())));
        }
        if(gridArea)
        {
            foreach (E_Direction key in Enum.GetValues(typeof(E_Direction)))
            {
                KeyEntry tmp = Instantiate(keyEntry, gridArea.transform);
                tmp.Initialize(key, keyDict[key]);
                tmp.name = key.ToString();
            }
        }
        
    }
    private void Update()
    {
        if (Input.GetKeyDown(keyDict[E_Direction.Up]))
        {
            if(Up!=null) Up(E_Direction.Up);
        }
        if (Input.GetKeyDown(keyDict[E_Direction.Left]))
        {
            if (Left != null) Left(E_Direction.Left);
        }
        if (Input.GetKeyDown(keyDict[E_Direction.Down]))
        {
            if (Down != null) Down(E_Direction.Down);
        }
        if (Input.GetKeyDown(keyDict[E_Direction.Right]))
        {
            if (Right != null) Right(E_Direction.Right);
        }
    }
    public KeyCode pressedKey;
    private void OnGUI()
    {
        if(currentKey != null)
        {
            Event e = Event.current;
            
            if(e.isKey)
            {
                keyDict[(E_Direction)Enum.Parse(typeof(E_Direction), currentKey.name)] = e.keyCode;
                currentKey.currentKeyText.text = e.keyCode.ToString();
                currentKey = null;
            }
        }
        SaveKeySet();
    }


    public void ChangeKey(KeyEntry clicked)
    {
        if(currentKey != null)
        {
        }
        currentKey = clicked;
    }
    public void SaveKeySet()
    {
        foreach(var key in keyDict)
        {
            PlayerPrefs.SetString(key.Key.ToString(), key.Value.ToString());
        }
        PlayerPrefs.Save();
    }
}
