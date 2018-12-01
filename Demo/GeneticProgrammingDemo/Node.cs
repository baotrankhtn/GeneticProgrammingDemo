using System;
using System.Collections;
using System.Collections.Generic;

/*
         Node (Terminal or Function)
        /     \
      Left    Right

  - Terminal: Float values, Range [-1000, 1000]
  - Function: +, -, *, sin , cos

 */
namespace GeneticProgrammingDemo
{
	public class Node
	{

		// Constants
		private static Function[] FUNCTIONS = (Function[])Enum.GetValues(typeof(Function));
		private static Type[] TYPES = (Type[])Enum.GetValues(typeof(Type));

		public enum Function
		{
			ADD,
			SUBTRACT,
			MULTIPLY,
			SIN, // right = null
			COS, // right = null
		}

		public enum Type
		{
			FUNCTION,
			TERMINAL
		}

		private String name = "";
		private Type type = Type.TERMINAL;
		private Function funtion = Function.ADD; // If not leaft node
		private double value; // If leaft node

		private Node parent;
		private Node left;
		private Node right;

		public Node(Type type, double value, String name = "")
		{
			this.name = name;
			this.type = type;
			this.value = value;
		}

		public Node(Type type, Function function, Node left, Node right = null, String name = "")
		{
			this.name = name;
			this.type = type;
			this.funtion = function;
			this.left = left;
			this.right = right;
			if (left != null) {
				left.parent = this;
			}
			if (right != null) {
				right.parent = this;
			}
		}

		/*
         *  Mutation
         *  
         */
		public void Mutation()
		{
			Console.WriteLine("\n================== MUTATION =================\n");
            
			// Generate all node names
			List<String> names = getNames(this);
   
			// Random node name
			String chosenNodeName = names[NumberUtils.GenerateRamdomInteger(0, names.Count - 1)];

			// Find node based on name
			Node chosenNode = Find(this, chosenNodeName);
			if (chosenNode == null)
			{
				Console.WriteLine("Please run again");
				return;
			}

			// Original tree
			Console.WriteLine("- Original expression: " + GetExpression());
			Console.WriteLine("- Original tree: ");
			PrintTree(this, "", true);

			// Mutation
			Console.WriteLine("- Mutation at node " + chosenNode.name + ": ");
			if (chosenNode.type == Type.FUNCTION)
			{ // Function
				Function newFunction = FUNCTIONS[NumberUtils.GenerateRamdomInteger(0, FUNCTIONS.Length - 1)];
				while (chosenNode.funtion == newFunction)
				{
					newFunction = FUNCTIONS[NumberUtils.GenerateRamdomInteger(0, FUNCTIONS.Length - 1)];
				}
				Console.Write("Function " + chosenNode.GetContent() + " change to ");
				chosenNode.funtion = newFunction;
				Console.Write(chosenNode.GetContent());
				switch (chosenNode.funtion)
				{
					case Function.SIN:
					case Function.COS:
						chosenNode.right = null;
						break;
				}
			}
			else
			{ // Terminal
				Console.Write("Value " + chosenNode.value + " change to ");
				chosenNode.value = NumberUtils.GenerateRandomDouble(-1000, 1000);
				Console.Write(chosenNode.value);
			}

			// New tree
			Console.WriteLine("\n\n- New expression: " + GetExpression());
			Console.WriteLine("- New tree: ");
			PrintTree(this, "", true);
		}

        /*
         * Crossover
         */
		public void Crossover(Node otherParent) {
			Console.WriteLine("\n\n================== CROSSOVER =================\n");

			// Generate all node names
            List<String> parent1Names = getNames(this);
			List<String> parent2Names = otherParent.getNames(otherParent);

            // Random node name
			String chosenNodeName1 = parent1Names[NumberUtils.GenerateRamdomInteger(1, parent1Names.Count - 1)]; // 1 to avoid root
			String chosenNodeName2 = parent2Names[NumberUtils.GenerateRamdomInteger(1, parent2Names.Count - 1)];

            // Find node based on name
			Node chosenNode1 = Find(this, chosenNodeName1);
			Node chosenNode2 = Find(otherParent, chosenNodeName2);
			if (chosenNode1 == null || chosenNode2 == null)
            {
                Console.WriteLine("Please run again");
                return;
            }

			// Original tree
			Console.WriteLine("- Parent 1: ");
            Console.WriteLine("+ Original expression: " + GetExpression());
            Console.WriteLine("+ Original tree: ");
            PrintTree(this, "", true);

			Console.WriteLine("\n- Parent 2: ");
			Console.WriteLine("+ Original expression: " + otherParent.GetExpression());
            Console.WriteLine("+ Original tree: ");
			PrintTree(otherParent, "", true);
            
            // Crossover
			Console.WriteLine("\n- Crossover at {0} of parent 1 and {1} of parent 2", chosenNode1.name, chosenNode2.name);
			Node choseNode1Parent = chosenNode1.parent;
			Node choseNode2Parent = chosenNode2.parent;
			if (choseNode1Parent == null || choseNode2Parent == null)
            {
                Console.WriteLine("Please run again");
                return;
            }
			Node temp = chosenNode1;
			if (choseNode1Parent.left != null && string.Equals(choseNode1Parent.left.name, chosenNode1.name))
			{
				choseNode1Parent.left = chosenNode2;
			}
			else if (choseNode1Parent.right != null && string.Equals(choseNode1Parent.right.name, chosenNode1.name))
			{
				choseNode1Parent.right = chosenNode2;
			}

			if (choseNode2Parent.left != null && string.Equals(choseNode2Parent.left.name, chosenNode2.name))
            {
                choseNode2Parent.left = temp;
            }
            else if (choseNode2Parent.right != null && string.Equals(choseNode2Parent.right.name, chosenNode2.name))
            {
                choseNode2Parent.right = temp;
            }

			// New tree
            Console.WriteLine("\n- Child 1: ");
            Console.WriteLine("+ Expression: " + GetExpression());
            Console.WriteLine("+ Tree: ");
            PrintTree(this, "", true);

            Console.WriteLine("\n- Child 2: ");
            Console.WriteLine("+ Expression: " + otherParent.GetExpression());
            Console.WriteLine("+ Tree: ");
            PrintTree(otherParent, "", true);
		}

		/*
         * Find node based on name
         */
		private Node Find(Node node, String strName)
		{
			if (node != null)
			{
				if (string.Equals(node.name, strName))
				{
					return node;
				}
				else
				{
					Node foundNode = Find(node.left, strName);
					if (foundNode == null)
					{
						return Find(node.right, strName);
					}
					return foundNode;
				}
			}
			return null;
		}

		/*
         * Get child count
         */
		public int GetChildCount()
		{
			int count = 0;
			if (left != null) count++;
			if (right != null) count++;
			return count;
		}

        /*
         * Get all node names 
         */
		public List<String> getNames(Node node) {
			List<String> names = new List<String>();
			if (node.name != null) {
				names.Add(node.name);
			}
			if (node.left != null && node.left.name != null) {
				names.AddRange(getNames(node.left));
			}
			if (node.right != null && node.right.name != null)
            {
                names.AddRange(getNames(node.right));
            }
			return names;
		}
        
		/*
         * Get content of node
         */
		public String GetContent()
		{
			String content = "";
			switch (type)
			{
				case Type.TERMINAL:
					content = value.ToString();
					break;
				case Type.FUNCTION:
					switch (funtion)
					{
						case Function.ADD:
							content = "+";
							break;
						case Function.SUBTRACT:
							content = "-";
							break;
						case Function.MULTIPLY:
							content = "*";
							break;
						case Function.SIN:
							content = "sin";
							break;
						case Function.COS:
							content = "cos";
							break;
					}
					break;
			}
			return content;
		}

		/*
         * Get result of tree
         */
		public double GetResult()
		{
			double result = 0;
			switch (type)
			{
				case Type.TERMINAL: // left = null, right = null
					result = this.value;
					break;
				case Type.FUNCTION: // left != null
					if (left != null && right != null)
					{
						switch (funtion)
						{
							case Function.ADD:
								result = left.GetResult() + right.GetResult();
								break;
							case Function.SUBTRACT:
								result = left.GetResult() - right.GetResult();
								break;
							case Function.MULTIPLY:
								result = left.GetResult() * right.GetResult();
								break;
							default:
								result = 0;
								break;
						}

					}
					else if (left != null && right == null)
					{
						switch (funtion)
						{
							case Function.SIN:
								result = Math.Sin(left.GetResult() * Math.PI / 180.0);
								break;
							case Function.COS:
								result = Math.Cos(left.GetResult() * Math.PI / 180.0);
								break;
							default:
								result = 0;
								break;
						}
					}
					break;
				default:
					result = 0;
					break;
			}
			return result;
		}

		/*
         * Display the tree 
         * 
         */
		public void PrintTree(Node node, String indent, bool isLast)
		{
			if (node != null)
			{
				if (node.name != null)
				{
					Console.WriteLine(indent + "+~~ " + node.GetContent() + "(" + node.name + ")");
				}
				else
				{
					Console.WriteLine(indent + "+~~ " + node.GetContent());
				}
				indent += isLast ? "    " : "|   ";

				for (int i = 0; i < node.GetChildCount(); i++)
				{
					if (i == 0)
					{
						PrintTree(node.left, indent, i == node.GetChildCount() - 1);
					}
					else
					{
						PrintTree(node.right, indent, i == node.GetChildCount() - 1);
					}
				}
			}
		}

		/*
         * Display the function 
         */
		public String GetExpression()
		{
			String strFunction = "";
			switch (type)
			{
				case Type.TERMINAL:
					if (value >= 0)
					{
						strFunction = value.ToString();
					}
					else
					{
						strFunction = "(" + value.ToString() + ")";
					}
					break;

				case Type.FUNCTION:
					if (right == null)
					{
						switch (funtion)
						{
							case Function.SIN:
							case Function.COS:
								strFunction = String.Format(GetFunctionStringFormat(), this.left.GetExpression());
								break;
						}
					}
					else
					{
						switch (funtion)
						{
							case Function.ADD:
							case Function.SUBTRACT:
							case Function.MULTIPLY:
								strFunction = String.Format(GetFunctionStringFormat(), left.GetExpression(), right.GetExpression());
								break;
						}
					}
					break;
			}
			return strFunction;
		}

		/*
         *  Function -> String format
         */
		private String GetFunctionStringFormat()
		{
			String strFunction = "";
			switch (funtion)
			{
				case Function.ADD:
					strFunction = "{0}+{1}";
					break;
				case Function.SUBTRACT:
					strFunction = "{0}-({1})";
					break;
				case Function.MULTIPLY:
					strFunction = "{0}*{1}";
					break;
				case Function.SIN:
					strFunction = "sin({0})";
					break;
				case Function.COS:
					strFunction = "cos({0})";
					break;
			}
			return strFunction;
		}
	}
}