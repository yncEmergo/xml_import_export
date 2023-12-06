using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjectDisplayManager : MonoBehaviour
{
    [SerializeField] private XMLManager xmlManager;
    [SerializeField] private GameObject objectDisplayPrefab;

    private void Start()
    {
        SetupItemdisplays();
    }

    private void SetupItemdisplays () 
    { 
        if (xmlManager == null || xmlManager.objects == null || xmlManager.objects.Count == 0)
        {
            Debug.LogWarning("Abort");
            return;
        }

        foreach (SaveObject saveObject in xmlManager.objects)
        {
            GameObject newObjectDisplay = Instantiate(objectDisplayPrefab, transform);
            newObjectDisplay.GetComponent<ObjectDisplay>().Setup(saveObject);
        }
        LayoutRebuilder.ForceRebuildLayoutImmediate(GetComponent<RectTransform>());

    }
}
