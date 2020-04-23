﻿using System;
using System.Collections.Generic;
using System.Linq;

 namespace MindMangler
{
	public enum PossibleOperations
	{
		Increment,
		Decrement,
		PointerForward,
		PointerBackward,
		Print,
		Read,
		StartLoop,
		EndLoop,
		NonInstructionCharacter
	}
	public partial class Program
	{
		public static void InterpreterMain(string code, bool verboseOn = false)
		{
			int[] memoryBlock = new int[3_000]; // Brainfuck memory cells

			List<char> commandsRaw = code.ToList(); // Get a List<char> of characters

			List<PossibleOperations> commands = new List<PossibleOperations>();
			foreach (var command in commandsRaw)
			{
				commands.Add(ParseCharacter(command));
			}
			
			bool looping = false;
			List<char> loopCommandsRaw = new List<char>();
			List<PossibleOperations> loopCommands = new List<PossibleOperations>();
			
			int pointer = 0;

			int counter = 0;
			
			do
			{
				if (looping)
				{
					int loopCounter = 0;
					while (memoryBlock[pointer] > 0 | loopCounter < loopCommandsRaw.Count)
					{
						if (loopCounter == loopCommandsRaw.Count)
						{
							loopCounter = 0;
						} // Avoid out of range exception
						switch (loopCommands[loopCounter++])
						{
                            default:
                                if (verboseOn)
                                {
                                    if (char.IsWhiteSpace(commandsRaw[counter - 1]))
                                    {
                                        Console.WriteLine($"Skipped character #{(counter - 1).ToString()}: Is whitespace");
                                    }
                                    Console.WriteLine($"character #{(counter - 1).ToString()} ({commandsRaw[counter].ToString()}) is not a Brainfuck instruction.");
                                } // Log non-instruction characters.
                                break;
                            case PossibleOperations.PointerForward:
                                pointer++;
                                break;
                            case PossibleOperations.PointerBackward:
                                pointer--;
                                break;
                            case PossibleOperations.Increment:
                                memoryBlock[pointer]++;
                                break;
                            case PossibleOperations.Decrement:
                                memoryBlock[pointer]--;
                                break;
                            case PossibleOperations.Print:
                                Console.Write(Convert.ToChar(memoryBlock[pointer]).ToString()); // print key
                                break;
                            case PossibleOperations.Read:
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
					counter += loopCommandsRaw.Count + 1;
				} // Loops

				switch (commands[counter++])
				{
					default:
						if (verboseOn)
						{
							if (char.IsWhiteSpace(commandsRaw[counter - 1]))
							{
								Console.WriteLine($"Skipped character #{(counter - 1).ToString()}: Is whitespace");
							}
							Console.WriteLine($"character #{(counter - 1).ToString()} ({commandsRaw[counter].ToString()}) is not a Brainfuck instruction.");
						} // Log non-instruction characters.
						break;
					case PossibleOperations.PointerForward:
						pointer++;
						break;
					case PossibleOperations.PointerBackward:
						pointer--;
						break;
					case PossibleOperations.Increment:
						memoryBlock[pointer]++;
						break;
					case PossibleOperations.Decrement:
						memoryBlock[pointer]--;
						break;
					case PossibleOperations.Print:
						Console.Write(Convert.ToChar(memoryBlock[pointer]).ToString()); // print key
						break;
					case PossibleOperations.Read:
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
					case PossibleOperations.StartLoop:
						if (memoryBlock[pointer] == 0)
						{
							looping = false;
							break;
						} // Break if current cell is 0
						looping = true; // Set looping flag
						int loopCounter = counter; // make a loop counter to use for getting all code upto the ]
						while (commandsRaw[loopCounter].ToString() != "]")
						{
							loopCommandsRaw.Add(Convert.ToChar(commandsRaw[loopCounter].ToString()));
							loopCounter++;
						} // Put all commands in the loop into a list
						break;
					case PossibleOperations.EndLoop:
						looping = false;
						if (verboseOn)
						{
							Console.WriteLine("Exit Loop");
						}
						break; // Oh god no how do you do loops halp no this is too hard
				}
			} while (counter < code.Length);
		}

		public static PossibleOperations ParseCharacter(char inputCharacter)
		{
			switch (inputCharacter.ToString())
			{
				case ">":
					return PossibleOperations.PointerForward;
				case "<":
					return PossibleOperations.PointerBackward;
				case "+":
					return PossibleOperations.Increment;
				case "-":
					return PossibleOperations.Decrement;
				case "[":
					return PossibleOperations.StartLoop;
				case "]":
					return PossibleOperations.EndLoop;
				case ".":
					return PossibleOperations.Print; 
				case ",":
					return PossibleOperations.Read;
				default:
					return PossibleOperations.NonInstructionCharacter;
			}
		}
	}
}