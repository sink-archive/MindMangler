using System;
using System.Collections.Generic;
using System.Linq;

namespace MindMangler
{
	internal partial class Program
	{
		public static void InterpreterMain(string code, bool verboseOn)
		{
			List<int> memoryBlock = new List<int>(30000); // Brainfuck memory cells
			List<char> commands = code.ToList(); // Get a List<char> of characters

			int counter = 0;
			do
			{
				counter++;
				switch (commands[counter].ToString())
				{
					default:
						break;
				}
			} while (counter < code.Length);
		}
	}
}