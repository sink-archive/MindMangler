using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MindMangler
{
	internal partial class Program
	{
		public static void InterpreterMain(string code, bool verboseOn)
		{
			List<uint> memoryBlock = new List<uint>(); // Brainfuck memory cells

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
					int loopCounter = 0;
					while (memoryBlock[pointer] > 0)
					{
						if (loopCounter == loopCommands.Count)
						{
							loopCounter = 0;
						} // Avoid out of range exception
						switch (loopCommands[loopCounter++].ToString())
						{
                            default:
                                if (verboseOn)
                                {
                                    if (char.IsWhiteSpace(commands[counter - 1]))
                                    {
                                        Console.WriteLine($"Skipped character #{(counter - 1).ToString()}: Is whitespace");
                                    }
                                    Console.WriteLine($"character #{(counter - 1).ToString()} ({commands[counter].ToString()}) is not a Brainfuck instruction.");
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
                                Console.Write(Convert.ToChar(memoryBlock[pointer]).ToString()); // print key
                                break;
                            case ",":
                                string character = Console.ReadKey().KeyChar.ToString();
                                if (!char.TryParse(character, out char pressedKey))
                                {
                                    Console.WriteLine("Invalid Input.");
                                    Environment.Exit(2);
                                    return;
                                }

                                memoryBlock[pointer] = Convert.ToUInt16(pressedKey); // Get keycode of entered character and store it.
                                break;
                        }
					}
					looping = false;
					counter += loopCommands.Count + 1;
				} // Loops

				switch (commands[counter++].ToString())
				{
					default:
						if (verboseOn)
						{
							if (char.IsWhiteSpace(commands[counter - 1]))
							{
								Console.WriteLine($"Skipped character #{(counter - 1).ToString()}: Is whitespace");
							}
							Console.WriteLine($"character #{(counter - 1).ToString()} ({commands[counter].ToString()}) is not a Brainfuck instruction.");
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
						Console.Write(Convert.ToChar(memoryBlock[pointer]).ToString()); // print key
						break;
					case ",":
						string character = Console.ReadKey().KeyChar.ToString();
                        if (!char.TryParse(character, out char pressedKey))
                        {
                            Console.WriteLine("Invalid Input.");
                            Environment.Exit(2);
                            return;
                        }
						
						memoryBlock[pointer] = Convert.ToUInt16(pressedKey); // Get keycode of entered character and store it.
						break;
					
					// Oh god no loops live down here
					case "[":
						if (memoryBlock[pointer] == 0)
						{
							looping = false;
							break;
						} // Break if current cell is 0
						looping = true; // Set looping flag
						int loopCounter = counter; // make a loop counter to use for getting all code upto the ]
						while (commands[loopCounter].ToString() != "]")
						{
							loopCommands.Add(Convert.ToChar(commands[loopCounter].ToString()));
							loopCounter++;
						} // Put all commands in the loop into a list
						break;
					case "]":
						looping = false;
						if (verboseOn)
						{
							Console.WriteLine("Exit Loop");
						}
						break; // Oh god no how do you do loops halp no this is too hard
				}
			} while (counter < code.Length);
		}
	}
}