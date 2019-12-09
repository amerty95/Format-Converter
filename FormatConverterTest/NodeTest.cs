using FormatConverter;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace FormatConverterTest
{
    public class NodeTest
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        [TestCase("City", "Istanbul")]
        [TestCase("City", null)]
        public void CreateNodeTest(string name, object value)
        {
            IBNode node = new IBNode(name, value, null);
            Assert.AreEqual(name, node.GetName());
            Assert.AreEqual(value, node.GetValue());
        }

        [Test]
        [TestCase(null, "City")]
        [TestCase("", "City")]
        public void NullorEmptyNameTest(string name, object value)
        {
            Exception exception = Assert.Throws<Exception>(() => new IBNode(name, value, null));
            Assert.AreEqual("Node name can not be null or empty", exception.Message);
        }

        [Test]
        [TestCase("City", "Istanbul")]
        public void CreateRootNode(string name, object value)
        {
            IBNode node = new IBNode(name, value, null);
            Assert.True(node.IsRoot());            
        }

        [Test]
        [TestCase("City", "Istanbul", "Country", "Turkey")]       
        public void AddNode(string childName, object childValue, string parentName, object parentValue)
        {
            IBNode parentNode = new IBNode(parentName, parentValue, null);
            IBNode childNode = new IBNode(childName, childValue, parentNode);

            Assert.AreEqual(parentNode, childNode.GetParentNode());
        }

        [Test]
        [TestCase("City", "Istanbul", "Country", "Turkey")]
        public void AddChild(string childName, object childValue, string parentName, object parentValue)
        {
            IBNode parentNode = new IBNode(parentName, parentValue, null);
            IBNode childNode = new IBNode(childName, childValue, parentNode);

            List<IBNode> nodes = parentNode.GetChildNodes();
            Assert.AreEqual(1, nodes.Count);
            Assert.AreEqual(childNode, nodes[0]);           
        }

        [Test]
        [TestCase("City", "Istanbul", "City", "Trabzon", "Country", "Turkey")]
        public void AddChildren(string child1Name, object child1Value, string child2Name, object child2Value, string parentName, object parentValue)
        {
            IBNode parentNode = new IBNode(parentName, parentValue, null);
            IBNode childNode1 = new IBNode(child1Name, child1Value, parentNode);
            IBNode childNode2 = new IBNode(child2Name, child2Value, parentNode);


            List<IBNode> nodes = parentNode.GetChildNodes();
            Assert.AreEqual(2, nodes.Count);
            Assert.AreEqual(childNode1, nodes[0]);
            Assert.AreEqual(childNode2, nodes[1]);
        }

        [Test]
        [TestCase("City", "Istanbul", "City", "Trabzon", "Country", "Turkey")]
        public void CheckChildrenAfterInitialization(string child1Name, object child1Value, string child2Name, object child2Value, string parentName, object parentValue)
        {
            IBNode parentNode = new IBNode(parentName, parentValue, null);
           
            Assert.AreEqual(0, parentNode.GetChildNodes().Count);
        }

        [Test]
        [TestCase("City", "Istanbul", "City", "Trabzon", "Country", "Turkey")]
        public void AddLevel3Child(string name1, object value1, string name2, object value2, string name3, object value3)
        {
            IBNode node1 = new IBNode(name1, value1, null);
            IBNode node2 = new IBNode(name2, value2, node1);
            IBNode node3 = new IBNode(name3, value3, node2);


            List<IBNode> nodes1 = node1.GetChildNodes();
            List<IBNode> nodes2 = node2.GetChildNodes();

            Assert.AreEqual(1, nodes1.Count);
            Assert.AreEqual(1, nodes2.Count);

            Assert.AreEqual(node2, nodes1[0]);
            Assert.AreEqual(node3, nodes2[0]);
        }

    }
}
