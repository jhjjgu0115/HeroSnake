using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class KeyEntry : MonoBehaviour
{

    public TextMeshProUGUI actionText;
    public TextMeshProUGUI currentKeyText;

    public void Initialize(ActionSet action,KeyCode key)
    {
        actionText.text = action.ToString();
        currentKeyText.text = key.ToString();
    }

    public void Clicked()
    {
        KeyBinder.Instance.ChangeKey(this);
    }

}
