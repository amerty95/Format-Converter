using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace FormatConverter
{
    public class IBFormat
    {
        string RootName;
        object Value;

        public IBFormat()
        {

        }
        public void CreateRoot(string rootName, object value)
        {
            RootName = rootName;
            Value = value;
        }

        public string GetRootName()
        {
            return RootName;
        }

        private static XmlDocument XmlDocumentConverter(string xmlText)
        {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(xmlText);

            return doc;
        }

        public static IBNode ConvertXml2IBF(string xmlText)
        {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(xmlText);
            
            IBNode node = new IBNode(doc.ChildNodes[0].Name, doc.ChildNodes[0].Attributes["name"]?.Value, null);
            IterateInnerXml(doc.ChildNodes[0].InnerXml, node);
            return node;
        }

        private static void IterateXml(XmlNode xmlElement, IBNode node)
        {
            foreach(XmlNode elem in xmlElement.ChildNodes)
            {
                IBNode childNode = new IBNode(elem.Name, elem.Value, node); //value .ChildNodes[0]
                if (elem.HasChildNodes)
                {
                    IterateXml(elem, childNode);
                }
            }
        }

        private static void IterateInnerXml(string xmlText, IBNode parentNode)
        {
            if (string.IsNullOrEmpty(xmlText))
            {
                return;
            }
            XmlDocument doc = XmlDocumentConverter(xmlText);
            foreach(XmlNode elem in doc.ChildNodes)
            {
                IBNode node = new IBNode(elem.Name, elem.Attributes["name"].Value, parentNode);
                foreach(XmlNode elem2 in elem.ChildNodes)
                {
                    IterateInnerXml(elem2.OuterXml, node);
                }
                
            }
        }

        public object GetValue()
        {
            return Value;
        }
    }
}
