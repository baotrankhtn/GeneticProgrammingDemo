using System;

namespace GeneticProgrammingDemo
{
	class MainClass
	{
		public static void Main(string[] args)
		{
			DemoMutation();
			DemoCrossover();
			Console.ReadLine();
		}

		// ====================== MUTATION =======================

		/*   
		 * 
         *    Parent = sin(180) + 12 - (-2.2 * 13) = 40.6
         * 
         *                         + (A)     
         *                       /   \
         *                  sin (B)    - (C)    
         *                    /       /   \
         *              180 (D)   12 (E)  * (F) 
         *                               /   \
         *                          -2.2 (G)  13 (H)
         */
		private static void DemoMutation()
		{
			// Original tree
			Node d = new Node(Node.Type.TERMINAL, 180, "D");
			Node e = new Node(Node.Type.TERMINAL, 12, "E");
			Node g = new Node(Node.Type.TERMINAL, -2.2, "G");
			Node h = new Node(Node.Type.TERMINAL, 13, "H");

			Node f = new Node(Node.Type.FUNCTION, Node.Function.MULTIPLY, g, h, "F");
			Node c = new Node(Node.Type.FUNCTION, Node.Function.SUBTRACT, e, f, "C");
			Node b = new Node(Node.Type.FUNCTION, Node.Function.SIN, d, null, "B");
			Node a = new Node(Node.Type.FUNCTION, Node.Function.ADD, b, c, "A");
            
			// Mutated tree
			a.Mutation();
		}

		/*   
         * 
         *    Parent1 = sin(180) + 12 - ((-2.2) * 13) = 40.6
         * 
         *                         + (A1)     
         *                       /   \
         *                  sin (B1)    - (C1)    
         *                    /       /   \
         *              180 (D1)   12 (E1)  * (F1) 
         *                               /   \
         *                          -2.2 (G1)  13 (H1)
         *      
         *
		 *    Parent2 = sin(cos((-200)) + sin(900) * (-3) + (-9)) = -0.173
         * 
         *                       sin (A2)     
         *                         |
         *                         + (B2)
         *                       /   \
         *                  cos (C2)    + (D2)    
         *                    /       /   \
         *              -200 (E2)   * (F2)  -9 (G2) 
         *                       /   \
         *                   sin (H2)  -3(I2)
         *                      |
         *                   900 (J2)
         */
        private static void DemoCrossover()
        {
            // Parent1
            Node d1 = new Node(Node.Type.TERMINAL, 180, "D1");
            Node e1 = new Node(Node.Type.TERMINAL, 12, "E1");
            Node g1 = new Node(Node.Type.TERMINAL, -2.2, "G1");
            Node h1 = new Node(Node.Type.TERMINAL, 13, "H1");

            Node f1 = new Node(Node.Type.FUNCTION, Node.Function.MULTIPLY, g1, h1, "F1");
            Node c1 = new Node(Node.Type.FUNCTION, Node.Function.SUBTRACT, e1, f1, "C1");
            Node b1 = new Node(Node.Type.FUNCTION, Node.Function.SIN, d1, null, "B1");
            Node a1 = new Node(Node.Type.FUNCTION, Node.Function.ADD, b1, c1, "A1");

			// Parent2
			Node j2 = new Node(Node.Type.TERMINAL, 900, "J2");
            Node i2 = new Node(Node.Type.TERMINAL, -3, "I2");
            Node g2 = new Node(Node.Type.TERMINAL, -9, "G2");
            Node e2 = new Node(Node.Type.TERMINAL, -200, "E2");

            Node h2 = new Node(Node.Type.FUNCTION, Node.Function.SIN, j2, null, "H2");
            Node f2 = new Node(Node.Type.FUNCTION, Node.Function.MULTIPLY, h2, i2, "F2");
            Node d2 = new Node(Node.Type.FUNCTION, Node.Function.ADD, f2, g2, "D2");
            Node c2 = new Node(Node.Type.FUNCTION, Node.Function.COS, e2, null, "C2");
            Node b2 = new Node(Node.Type.FUNCTION, Node.Function.ADD, c2, d2, "B2");
            Node a2 = new Node(Node.Type.FUNCTION, Node.Function.SIN, b2, null, "A2");

            // Crossover 
			a1.Crossover(a2);
        }



		// ==================== NODE TESTING =======================
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


		private static void TestNode1()
		{
			Node d = new Node(Node.Type.TERMINAL, 180);
			Node e = new Node(Node.Type.TERMINAL, 12);
			Node g = new Node(Node.Type.TERMINAL, -2.2);
			Node h = new Node(Node.Type.TERMINAL, 13);

			Node f = new Node(Node.Type.FUNCTION, Node.Function.MULTIPLY, g, h);
			Node c = new Node(Node.Type.FUNCTION, Node.Function.SUBTRACT, e, f);
			Node b = new Node(Node.Type.FUNCTION, Node.Function.SIN, d);
			Node a = new Node(Node.Type.FUNCTION, Node.Function.ADD, b, c);

			Console.WriteLine(a.GetResult());
			Console.WriteLine(a.GetExpression());
			a.PrintTree(a, "", true);
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
		 *                   900 (J)
         */
		private static void TestNode2()
		{
			Node j = new Node(Node.Type.TERMINAL, 900);
			Node i = new Node(Node.Type.TERMINAL, -3);
			Node g = new Node(Node.Type.TERMINAL, -9);
			Node e = new Node(Node.Type.TERMINAL, -200);

			Node h = new Node(Node.Type.FUNCTION, Node.Function.SIN, j);
			Node f = new Node(Node.Type.FUNCTION, Node.Function.MULTIPLY, h, i);
			Node d = new Node(Node.Type.FUNCTION, Node.Function.ADD, f, g);
			Node c = new Node(Node.Type.FUNCTION, Node.Function.COS, e);
			Node b = new Node(Node.Type.FUNCTION, Node.Function.ADD, c, d);
			Node a = new Node(Node.Type.FUNCTION, Node.Function.SIN, b);

			Console.WriteLine(a.GetResult());
			Console.WriteLine(a.GetExpression());
			a.PrintTree(a, "", true);
		}

		/*
         *    F1 = sin(-7.2) = -0.125
         * 
         *                         sin (A)     <--- Level 0
         *                       /   
		 *                    -7.2 (B)
         */
		private static void TestNode3()
		{
			Node b = new Node(Node.Type.TERMINAL, -7.2);
			Node a = new Node(Node.Type.FUNCTION, Node.Function.SIN, b);

			Console.WriteLine(a.GetResult());
			Console.WriteLine(a.GetExpression());
			a.PrintTree(a, "", true);
		}

		/*
         *    F1 = cos(1000) = 0.173
         * 
         *                         cos (A)     <--- Level 0
         *                       /   
         *                    1000 (B)
         */
		private static void TestNode4()
		{
			Node b = new Node(Node.Type.TERMINAL, 1000);
			Node a = new Node(Node.Type.FUNCTION, Node.Function.COS, b);

			Console.WriteLine(a.GetResult());
			Console.WriteLine(a.GetExpression());
			a.PrintTree(a, "", true);
		}
	}
}
