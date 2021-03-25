using System.Collections.Generic;

namespace EuObjParser.Models.Parsing
{
	public class Node
	{
		public Node(ParentNode parent)
		{
			Parent = parent;
			if(Parent != null)
			{
				Parent.Add(this);
			}
		}

		public void RemoveSelf()
		{
			if (Parent != null)
			{
				Parent.Remove(this);
			}
		}

		public string StringValue => this is ValueNode vn ? vn.Value : null;
		public List<Node> NodeValue => this is ParentNode pn ? pn.Value : null;

		public string Key { get; set; }

		public ParentNode Parent { get; set; }
	}

	public class ParentNode : Node
	{
		public List<Node> Value { get; set; } = new List<Node>();
		public ParentNode(ParentNode parent) : base(parent) { }
		public void Add(Node node)
		{
			Value.Add(node);
		}

		public void Remove(Node node)
		{
			Value.Remove(node);
		}

		public void Replace(Node node, Node node2)
		{
			var index = Value.IndexOf(node);
			Value.Remove(node);
			Value.Insert(index, node2);
		}
	}

	public class ValueNode : Node
	{
		public ValueNode(ParentNode parent): base(parent) { }
		public string Value { get; set; }
	}
}
