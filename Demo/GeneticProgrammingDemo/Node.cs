using System;

/*
         Node (Terminal or Function)
        /     \
      Left    Right

  - Terminal: Float values
  - Function: +, -, *, sin , cos

 */
namespace GeneticProgrammingDemo
{
	public class Node
	{
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

		private Type type = Type.TERMINAL;
		private Function funtion = Function.ADD; // If not leaft node
		private double value; // If leaft node
		private Node left;
		private Node right;

		public Node(Type type, double value)
		{
			this.type = type;
			this.value = value;
		}

		public Node(Type type, Function function, Node left, Node right = null)
		{
			this.type = type;
			this.funtion = function;
			this.left = left;
			this.right = right;
		}

        /*
         * Get child count
         */
		public int getChildCount() {
			int count = 0;
			if (left != null) count++;
			if (right != null) count++;
			return count;
		}

        /*
         * Get content of node
         */
		public String getContent() {
			String content = "";
			switch(type) {
				case Type.TERMINAL:
					content = value.ToString();
					break;
				case Type.FUNCTION:
					switch(funtion) {
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
		public double getResult()
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
								result = left.getResult() + right.getResult();
								break;
							case Function.SUBTRACT:
								result = left.getResult() - right.getResult();
								break;
							case Function.MULTIPLY:
								result = left.getResult() * right.getResult();
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
								result = Math.Sin(left.getResult() * Math.PI / 180.0);
								break;
							case Function.COS:
								result = Math.Cos(left.getResult() * Math.PI / 180.0);
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
		public void printTree(Node node, String indent, bool isLast) {
			if (node != null)
			{
				Console.WriteLine(indent + "~~ " + node.getContent());
				indent += isLast ? "   " : "|  ";

				for (int i = 0; i < node.getChildCount(); i++)
				{
					if (i == 0)
					{
						printTree(node.left, indent, i == node.getChildCount() - 1);
					}
					else
					{
						printTree(node.right, indent, i == node.getChildCount() - 1);
					}
				}
			}
		}

		/*
         * Display the function 
         */
		public String getExpression()
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
								strFunction = String.Format(getFunctionStringFormat(), this.left.getExpression());
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
								strFunction = String.Format(getFunctionStringFormat(), left.getExpression(), right.getExpression());
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
		private String getFunctionStringFormat()
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