using System;

namespace GeneticProgrammingDemo
{
	class MainClass
	{
		public static void Main(string[] args)
		{
			testNode1();
			testNode2();
			testNode3();
            testNode4();
		}



		// ==================== NODE TEST =======================
		/*
         *    F = sin(180) + 12 - (-2.2 * 13) = 40.6
         * 
         *                         + (A)     <--- Level 0
         *                       /   \
		 *                  sin (B)    - (C)    
         *                    /       /   \
		 *              180 (D)   12 (E)  * (F) 
         *                               /   \
		 *                          -2.2 (G)  13 (H)
         * 
         */
		public static void testNode1()
		{
			Node d = new Node(Node.Type.TERMINAL, 180);
			Node e = new Node(Node.Type.TERMINAL, 12);
			Node g = new Node(Node.Type.TERMINAL, -2.2);
			Node h = new Node(Node.Type.TERMINAL, 13);

			Node f = new Node(Node.Type.FUNCTION, Node.Function.MULTIPLY, g, h);
			Node c = new Node(Node.Type.FUNCTION, Node.Function.SUBTRACT, e, f);
			Node b = new Node(Node.Type.FUNCTION, Node.Function.SIN, d);
			Node a = new Node(Node.Type.FUNCTION, Node.Function.ADD, b, c);

			Console.WriteLine(a.getResult());
			Console.WriteLine(a.getExpression());
			a.printTree(a, "", true);
		}

		/*
         *    F = sin[cos(-200) + [sin(900) * (-3) + (-9)]] = -0.173
         * 
         *                       sin (A)     <--- Level 0
         *                         |
         *                         + (B)
         *                       /   \
         *                  cos (C)    + (D)    
         *                    /       /   \
         *              -200 (E)   * (F)  -9 (G) 
         *                       /   \
         *                   sin (H)  -3(I)
         *                      |
		 *                   900 (K)
         */
        public static void testNode2()
        {
            Node k = new Node(Node.Type.TERMINAL, 900);
            Node i = new Node(Node.Type.TERMINAL, -3);
			Node g = new Node(Node.Type.TERMINAL, -9);
            Node e = new Node(Node.Type.TERMINAL, -200);
            
            Node h = new Node(Node.Type.FUNCTION, Node.Function.SIN, k);
			Node f = new Node(Node.Type.FUNCTION, Node.Function.MULTIPLY, h, i);
            Node d = new Node(Node.Type.FUNCTION, Node.Function.ADD, f, g);
			Node c = new Node(Node.Type.FUNCTION, Node.Function.COS, e);
			Node b = new Node(Node.Type.FUNCTION, Node.Function.ADD, c, d);
            Node a = new Node(Node.Type.FUNCTION, Node.Function.SIN, b);

            Console.WriteLine(a.getResult());
			Console.WriteLine(a.getExpression());
			a.printTree(a, "", true);
        }

		/*
         *    F1 = sin(-7.2) = -0.125
         * 
         *                         sin (A)     <--- Level 0
         *                       /   
		 *                    -7.2 (B)
         */      
		public static void testNode3()
		{
			Node b = new Node(Node.Type.TERMINAL, -7.2);
			Node a = new Node(Node.Type.FUNCTION, Node.Function.SIN, b);

			Console.WriteLine(a.getResult());
			Console.WriteLine(a.getExpression());
			a.printTree(a, "", true);
		}

		/*
         *    F1 = cos(1000) = 0.173
         * 
         *                         cos (A)     <--- Level 0
         *                       /   
         *                    1000 (B)
         */
        public static void testNode4()
        {
            Node b = new Node(Node.Type.TERMINAL, 1000);
            Node a = new Node(Node.Type.FUNCTION, Node.Function.COS, b);
            
            Console.WriteLine(a.getResult());
			Console.WriteLine(a.getExpression());
			a.printTree(a, "", true);
        }
	}
}
