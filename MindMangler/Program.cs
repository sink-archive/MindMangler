using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;

namespace MindMangler
{
  public partial class Program
  {
    public static void Main(string[] args)
    {
      bool verbose = false;
      string filePath;
      string codeToRun;
      
      Console.WriteLine("MindMangler by Cain Atkinson. (https://cainy-a.github.io)\nA Brainfuck interpreter written in C#. Works on (some) basic programs that usually have no nested loops.\n++++++++++[>+>+++>+++++++>++++++++++<<<<-]>>>++.>+.+++++++..+++.<<++.>+++++++++++++++.>.+++.------.--------.<<+.<. Works Fine.");
      if (args.Length == 0 | (args.Length == 1 && args[0] == "-v"))
      {
        Console.WriteLine("Please type the path to a file containing brainfuck instructions, or type 'raw' to enter from command line'.");
        filePath = Console.ReadLine();
        if (filePath == "raw")
        {
          Console.WriteLine("Enter Raw Brainfuck Instructions.");
          codeToRun = Console.ReadLine();
          InterpreterMain(codeToRun, verbose);
          Environment.Exit(0);
          return;
        }
      }
      else
      {
        filePath = args[0];
      }
      
      if (args.Contains("-v") | args.Contains("--verbose"))
      {
        verbose = true;
      }
      if (args.Contains("--help") | args.Contains("-h"))
      {
        Console.WriteLine("Mindmangle [filepath] [options]\nPlease add a file path.\n-v or --verbose makes it tell you if a non-brainfuck instruction was encountered (not recommended at all).");
        return;
      }
      if (args.Length > 2)
      {
        Console.WriteLine("Please use less arguments");
        return;
      }
      
      try
      {
        StreamReader streamReader = new StreamReader(filePath);
        codeToRun = streamReader.ReadToEnd();
      }
      catch (Exception e)
      {
        Console.WriteLine("That's not a valid path.");
        Environment.Exit(1);
        return;
      } // Read the passed in file to a variable.
      Console.WriteLine($"Interpreting '{filePath}' as a brainfuck file.\nPress enter to begin execution"); // Tell the user which file will be interpreted.
      Console.ReadLine();
      Console.WriteLine();
      
      InterpreterMain(codeToRun, verbose); // Setup done, start Interpreting!!!
    }
  }
}