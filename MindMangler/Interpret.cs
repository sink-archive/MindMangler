using System;
using System.Collections.Generic;
using System.Linq;

namespace MindMangler
{
	internal partial class Program
	{
		public static void InterpreterMain(string code, bool verboseOn)
		{
			List<int> memoryBlock = new List<int>(); // Brainfuck memory cells

			for (int i = 0; i < 30000; i++)
			{
				memoryBlock.Add(0);
			} // Initialise memoryBlock with 30,000 0s

			List<char> commands = code.ToList(); // Get a List<char> of characters

			bool looping = false;
			List<char> loopCommands = new List<char>();
			
			int pointer = 0;

			int counter = 0;
			
			do
			{
				if (looping)
				{
				
				} // Loops exist in here
				
				switch (commands[counter++].ToString())
				{
					default:
						if (verboseOn)
						{
							Console.WriteLine($"character #{counter.ToString()} ({commands[counter].ToString()}) is not a Brainfuck instruction.");
						} // Log non-instruction characters.
						break;
					case ">":
						pointer++;
						break;
					case "<":
						pointer--;
						break;
					case "+":
						memoryBlock[pointer]++;
						break;
					case "-":
						memoryBlock[pointer]--;
						break;
					case ".":
						Console.Write(Convert.ToChar(memoryBlock[pointer]).ToString()); // get Char from keycode and print it
						break;
					case ",":
						memoryBlock[pointer] = Convert.ToInt32(Console.ReadKey().KeyChar); // Get keycode of entered character and store it.
						break;
					
					// Oh god no loops live down here
					case "[":
						if (memoryBlock[pointer] == 0)
						{
							looping = false;
							break;
						} // Break if current cell is 0
						looping = true; // Set looping flag
						int loopCounter = counter - 1; // make a loop counter to use for getting all code upto the ]
						while (commands[loopCounter].ToString() != "]")
						{
							loopCommands.Add(commands[loopCounter]);
						} // Put all commands in the loop into a list
						break;
					case "]":
						looping = false;
						break; // Oh god no how do you do loops halp no this is too hard
				}
			} while (counter < code.Length);
		}
	}
}