using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
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

    Dictionary<Direction, KeyCode> keyDict = new Dictionary<Direction, KeyCode>();
    KeyEntry currentKey;

    

    void Start()
    {
        foreach (Direction key in Enum.GetValues(typeof(Direction)))
        {
            keyDict.Add(key, (KeyCode)Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString(key.ToString(),KeyCode.W.ToString())));
        }

        foreach (Direction key in Enum.GetValues(typeof(Direction)))
        {
            KeyEntry tmp = Instantiate(keyEntry, gridArea.transform);
            tmp.Initialize(key, keyDict[key]);
            tmp.name = key.ToString();

        }
    }
    private void Update()
    {
        if (Input.GetKeyDown(keyDict[Direction.Up]))
        {
            Debug.Log(Direction.Up.ToString());
        }
        if (Input.GetKeyDown(keyDict[Direction.Left]))
        {
            Debug.Log(Direction.Left.ToString());
        }
        if (Input.GetKeyDown(keyDict[Direction.Down]))
        {
            Debug.Log(Direction.Down.ToString());
        }
        if (Input.GetKeyDown(keyDict[Direction.Right]))
        {
            Debug.Log(Direction.Right.ToString());
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
                keyDict[(Direction)Enum.Parse(typeof(Direction), currentKey.name)] = e.keyCode;
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
