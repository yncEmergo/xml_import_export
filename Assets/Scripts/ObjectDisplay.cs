using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using System.Text;
using UnityEngine.UI;

public class ObjectDisplay : MonoBehaviour
{
    private SaveObject objectReference;
    private TMP_Text text;

    public void Setup(SaveObject saveObject) 
    { 
        objectReference = saveObject;
        text = GetComponentInChildren<TMP_Text>();

        DisplayItem();
    }

    private void DisplayItem() 
    { 
        if (objectReference == null) 
        { 
            Debug.LogWarning("No item reference found!");
            return;
        }

        StringBuilder stringBuilder = new StringBuilder();

        stringBuilder.Append("Object Name: " + objectReference.objectName).AppendLine();
        stringBuilder.Append("Object ID: " + objectReference.gUID).AppendLine();
        for (int i = 0; i < objectReference.dialogue.Count; i++)
        {
            stringBuilder.Append("Dialogue " + i + ": " + objectReference.dialogue[i]).AppendLine();
        }

        text.text = stringBuilder.ToString();
        LayoutRebuilder.ForceRebuildLayoutImmediate(GetComponent<RectTransform>()); 
    }
}
