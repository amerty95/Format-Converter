using FormatConverter;
using NUnit.Framework;
using System.Collections.Generic;

namespace FormatConverterTest
{
    public class FormatTest
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void CreateFormatTest()
        {
            
        }

        [Test]
        [TestCase("<sample></sample>", null)]
        [TestCase("<sample name = \"dummy\"></sample>", "dummy")]
        public void Xml2IBFTest1Level(string xml, object value)
        {
            IBNode ibNode = IBFormat.ConvertXml2IBF(xml);
            Assert.IsTrue(ibNode.IsRoot());
            Assert.AreEqual(value, ibNode.Value);
            Assert.AreEqual("sample", ibNode.GetName());
            Assert.AreEqual(0, ibNode.GetChildNodes().Count);

        }

        [Test]
        [TestCase(@"<Country name = ""Turkey"">
   <City name = ""Istanbul"" >
      <County name = ""Pendik"" />
      <County name = ""Maltepe"" />
   </City >
</Country >", "Turkey", "Istanbul", "Pendik", "Maltepe")]
        public void Xml2IBFTest1Level2(string xml, string country, 
            string city, string county1, string county2)
        {
            IBNode ibNode = IBFormat.ConvertXml2IBF(xml);
            Assert.IsTrue(ibNode.IsRoot());
            Assert.AreEqual(1, ibNode.GetChildNodes().Count);
            Assert.AreEqual("Country", ibNode.GetName());
            Assert.AreEqual(country, ibNode.GetValue());

            IBNode childNode = ibNode.GetChildNodes()[0];
            Assert.AreEqual(2, childNode.GetChildNodes().Count);
            Assert.AreEqual("City", childNode.GetName());
            Assert.AreEqual(city, childNode.GetValue());

            IBNode grandChild1 = childNode.GetChildNodes()[0];
            Assert.AreEqual(0, grandChild1.GetChildNodes().Count);
            Assert.AreEqual("County", grandChild1.GetName());
            Assert.AreEqual(county1, grandChild1.GetValue());

            IBNode grandChild2 = childNode.GetChildNodes()[1];
            Assert.AreEqual(0, grandChild2.GetChildNodes().Count);
            Assert.AreEqual("County", grandChild2.GetName());
            Assert.AreEqual(county2, grandChild2.GetValue());
        }
    }
}