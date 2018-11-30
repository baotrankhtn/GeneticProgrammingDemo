using System;

/*
 * 
 *          Node (Terminal or Function)
 *        /     \
 *      Left    Right
 * 
 */
namespace GeneticProgrammingDemo
{
    public class Node
    {
		public enum Type {
			FUNCTION, 
            TERMINAL
		}

		private Type type = Type.TERMINAL;
		private double value;
		private Node left;
		private Node right;

        public Node()
        {
        }

        /*
         * Get result of tree
         */
		public double getResult() 
		{
			if (type == Type.TERMINAL) {
				return value;
			} else {
				return left.getResult() + right.getResult();
			}
		}
    }
}
