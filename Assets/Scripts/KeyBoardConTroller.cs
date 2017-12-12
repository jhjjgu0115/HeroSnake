using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class KeyBoardConTroller : MonoBehaviour
{
    KeyCode keyCode = new KeyCode();
    public Event e;

    void Start ()
    {

	}
    void Update()
    {

    }
    void OnGUI()
    {
        Event e = Event.current;
        if (e.isKey)
            Debug.Log("Detected key code: " + e.keyCode);
    }

}
