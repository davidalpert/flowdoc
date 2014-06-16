using System;
using System.Collections.Generic;
using System.IO.Abstractions;
using System.Linq;
using System.Text;
using FlowDoc.CLI.Commands;

namespace FlowDoc.CLI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            new InitCommand(new FileSystem())
            {
                Options = new InitOptions()
                          {
                              TargetDirectory = args[1]
                          }
            }.Execute();
        }
    }
}
