using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml.Serialization;
using System.IO;


[XmlRoot("dialogue")]
public class DialogSettings {

    [XmlElement("node")]
    public Node[] nodes;

    public static DialogSettings Load(TextAsset _xml)
    {
        XmlSerializer serializer = new XmlSerializer(typeof(DialogSettings));
        StringReader reader = new StringReader(_xml.text);
        DialogSettings dial = serializer.Deserialize(reader) as DialogSettings;
        return dial;
        
    }
}

[System.Serializable]
public class Node
{
    [XmlElement("text")]
    public string text;

	[XmlElement("color")]
	public string color;

    
}


