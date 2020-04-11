using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;

namespace MindMangler
{
  internal partial class Program
  {
    public static void Main(string[] args)
    {
      bool verbose = false;
      
      Console.WriteLine("MindMangler by Cain Atkinson. (https://cainy-a.github.io)\nA Brainfuck interpreter written in C#");
      if (args.Contains("-v") | args.Contains("--verbose"))
      {
        verbose = true;
      }
      if (args.Contains("--help") | args.Contains("-h") | args.Length == 0)
      {
        Console.WriteLine("Mindmangle [filepath] [options]\nPlease add a file path.\n-v or --verbose makes it tell you if a non-brainfuck instruction was encountered (not recommended at all).");
        return;
      }
      if (args.Length > 2)
      {
        Console.WriteLine("Please use less arguments");
        return;
      }
      string filePath = args[0];
      StreamReader streamReader = new StreamReader(filePath, Encoding.UTF8);
      string codeToRun;
      try
      {
        codeToRun = streamReader.ReadToEnd();
      }
      catch (Exception e)
      {
        Console.WriteLine("That's not a valid path.");
        throw;
        return;
      } // Read the passed in file to a variable.
      
      InterpreterMain(codeToRun, verbose);
    }

    /*/// <summary>
    /// Turns a string into a List<char>
    /// </summary>
    public static List<char> StringDeconstructor(string input)
    {
      List<char> toReturn = new List<char>();
      foreach (var letter in input)
      {
        toReturn.Add(letter);
      }
      return toReturn;
    }*/
  }
}