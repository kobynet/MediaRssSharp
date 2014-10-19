using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace MediaRss
{
	//[XmlRoot(ElementName = "scene", Namespace = "http://search.yahoo.com/mrss/")]
	//public class SceneElement : BaseMediaRssElement, IXmlSerializable
	//{
	//    internal const string ELEMENT_NAME = "scene";
		
	//    public String SceneTitle { get; set; }
	//    //public SceneDescriptionElement SceneDescription { get; set; }
	//    //public SceneStartTimeElement SceneStartTime { get; set; }
	//    //public SceneEndTimeElement SceneEndTime { get; set; }

	//    #region IXmlSerializable Members

	//    public XmlSchema GetSchema()
	//    {
	//        return null;
	//    }

	//    public void ReadXml(XmlReader reader)
	//    {
	//        bool isEmpty = reader.IsEmptyElement;

	//        if (reader.HasAttributes)
	//        {
	//            for (int i = 0; i < reader.AttributeCount; i++)
	//            {
	//                reader.MoveToNextAttribute();

	//                if (reader.NamespaceURI == "")
	//                {
	//                    AttributeExtensions.Add(new XmlQualifiedName(reader.LocalName, reader.NamespaceURI), reader.Value);
	//                }
	//            }
	//        }

	//        reader.ReadStartElement();

	//        if (!isEmpty)
	//        {
				
	//            //SceneTitle = reader.ReadContentAsString();
	//            //SceneDescription
	//            //SceneStartTime 
	//            //SceneEndTime 
	//        }

	//        reader.ReadEndElement();
	//    }

	//    public void WriteXml(XmlWriter writer)
	//    {
	//        //if (Url != null)
	//        //{
	//        //    writer.WriteAttributeString("url", NS_URI, Url.ToString());
	//        //}

	//        //writer.WriteAttributeString("filesize", NS_URI, FileSize.ToString());
	//        //writer.WriteAttributeString("bitrate", NS_URI, BitRate.ToString());

	//        //if (Type != null)
	//        //{
	//        //    writer.WriteAttributeString("type", NS_URI, Type);
	//        //}
	//        //if (Expression != null)
	//        //{
	//        //    writer.WriteAttributeString("expression", NS_URI, Expression);
	//        //}

	//        foreach (KeyValuePair<XmlQualifiedName, string> kvp in AttributeExtensions)
	//        {
	//            writer.WriteAttributeString(kvp.Key.Name, kvp.Key.Namespace, kvp.Value);
	//        }

	//        foreach (XElement element in ElementExtensions)
	//        {
	//            element.WriteTo(writer);
	//        }
	//    }
	//    #endregion
	//}
}