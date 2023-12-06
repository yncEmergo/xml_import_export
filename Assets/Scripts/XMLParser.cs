using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Xml.Linq;
using UnityEditor;
using UnityEngine;

public static class XMLParser
{
    public static SaveObject ParseXml(string xmlString)
    {
        XDocument document = XDocument.Parse(xmlString);

        try
        {
            XElement bodyElement = document.Root?.Element(XName.Get("body", "http://schemas.microsoft.com/office/word/2003/wordml"));
            if (bodyElement == null)
            {
                Console.WriteLine("No <w:body> element found in the XML.");
                return null;
            }

            string objectName = string.Empty;
            string gUID = string.Empty;
            List<string> dialogueList = new List<string>();


            foreach (XElement textElement in bodyElement.Descendants(XName.Get("p", "http://schemas.microsoft.com/office/word/2003/wordml")))
            {
                if (textElement != null)
                {
                    string text = textElement.Value;
                    if (string.IsNullOrWhiteSpace(text))
                        continue;

                    CheckXElementForHighlightColor(textElement, UnityEngine.Color.blue);

                    if (string.IsNullOrWhiteSpace(objectName))
                    {
                        objectName = text;
                    }
                    else if (string.IsNullOrWhiteSpace(gUID))
                    {
                        gUID = text;
                    }
                    else
                    {
                        dialogueList.Add(text);
                    }
                }
            }

            return new SaveObject(objectName, gUID, dialogueList);
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred during XML parsing: " + ex.Message);
            return null;
        }
    }

    public static bool CheckXElementForHighlightColor (XElement element, UnityEngine.Color c) 
    {
        // Obtain the correct namespace
        XNamespace wNamespace = "http://schemas.microsoft.com/office/word/2003/wordml";

        // Access the nested w:rPr -> w:highlight -> w:val attribute
        var highlightElement = element.Descendants(wNamespace + "r")
                                      .Elements(wNamespace + "rPr")
                                      .Elements(wNamespace + "highlight")
                                      .FirstOrDefault();
        Debug.Log(highlightElement == null);

        if (highlightElement != null)
        {
            XAttribute colorAttribute = highlightElement.Attribute(wNamespace + "val");

            Debug.Log(colorAttribute == null);
            if (colorAttribute != null)
            {
                string colorValue = colorAttribute.Value;
                Debug.Log(colorValue);

                // Here you can convert colorValue to UnityEngine.Color and check with 'c'
                // Make sure you handle the conversion properly as Word's color names might not match Unity's color names

                // if (convertedColor == c)
                //    return true;
            }
        }

        return false;
    }
}
