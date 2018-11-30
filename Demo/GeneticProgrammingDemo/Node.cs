using System;

/*
         Node (Terminal or Function)
        /     \
      Left    Right

Terminal: Float values
Function: +, -, *, sin , cos

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
	}
}