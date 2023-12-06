using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveObject
{
    public string objectName;
    public string gUID;
    public List<string> dialogue;

    public SaveObject (string _objectName, string _gUID, List<string> _dialogue) 
    { 
        objectName = _objectName;
        gUID = _gUID;
        dialogue = _dialogue;
    }
}
