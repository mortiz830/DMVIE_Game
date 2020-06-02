using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextListText : MonoBehaviour
{

    [SerializeField]
    private Text listItem;

    public void SetText(string textString)
    {
        listItem.text = textString;
    }

}
