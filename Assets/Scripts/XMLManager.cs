using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Xml;
using System.Xml.Serialization;
using System.IO;
using System.Xml.Linq;
using Unity.VisualScripting;
using System.Linq;

public class XMLManager : MonoBehaviour
{
    [SerializeField] private string dataName;

    public List<SaveObject> objects = new List<SaveObject>();

    public void SaveItems() 
    {
        for (int i = 0; i < objects.Count; i++)
        {
            SaveItem(objects[i], dataName + i.ToString());
        }
    }

    private static string FilePath
    {
        get => Application.dataPath + "/StreamingAssets/XML/";
    }

    private void SaveItem(SaveObject data, string name)
    {
        XDocument xmlDocument = new XDocument();
        XElement rootElement = new XElement("SaveObject");

        // Create child elements for properties
        XElement objectNameElement = new XElement("objectName", data.objectName);
        XElement gUIDElement = new XElement("gUID", data.gUID);

        List<XElement> dialogueElements = new List<XElement>();
        foreach (string dialogueItem in data.dialogue)
        {
            XElement dialogueElement = new XElement("dialogue", dialogueItem);
            dialogueElements.Add(dialogueElement);
        }

        // Add child elements to the root element
        rootElement.Add(objectNameElement, gUIDElement);
        rootElement.Add(dialogueElements);

        // Add the root element to the XML document
        xmlDocument.Add(rootElement);

        // Save the XML document to a file
        xmlDocument.Save(FilePath + name + ".xml");

    }

    public void LoadItem()
    {
        for (int i = 0; i < objects.Count; i++) 
        { 
            string path = Application.dataPath + "/StreamingAssets/XML/" + dataName + i + ".xml";
            StreamReader reader = new StreamReader(path);
            string xmlString = reader.ReadToEnd();
            reader.Close();

            SaveObject obj = XMLParser.ParseXml(xmlString);

            if (obj == null)
                return;

            Debug.Log(obj.objectName);
            Debug.Log(obj.gUID);
            foreach (string dialogue in obj.dialogue)
            {
                Debug.Log(dialogue);
            }
        }
    }
}