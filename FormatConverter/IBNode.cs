using System;
using System.Collections.Generic;
using System.Text;

namespace FormatConverter
{
    public class IBNode
    {
        int Id;
        public object Value;
        string Name;
        private IBNode ParentNode;
        private List<IBNode> ChildNodes;

        public IBNode(string name, object value, IBNode parentNode)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new Exception("Node name can not be null or empty");
            }
            this.ParentNode = parentNode;
            this.Name = name;
            this.Value = value;
            if (parentNode != null)
            {
                parentNode.AddChildNode(this);
            }
            ChildNodes = new List<IBNode>();
        }

        public void AddChildNode(IBNode childNode)
        {
            ChildNodes.Add(childNode);
        }

        public int GetId()
        {
            return Id;
        }

        public void SetId(int id)
        {
            this.Id = id;
        }

        public string GetName()
        {
            return Name;
        }

        public object GetValue()
        {
            return Value;
        }

        public bool IsRoot()
        {
            return ParentNode == null;
        }

        public IBNode GetParentNode()
        {
            return ParentNode;
        }

        public List<IBNode> GetChildNodes()
        {
            return ChildNodes;
        }
    }
}
