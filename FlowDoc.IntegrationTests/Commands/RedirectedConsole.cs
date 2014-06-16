using System;
using System.IO;
using System.Linq;

namespace FlowDoc.IntegrationTests.Commands
{
    public class RedirectedConsole : IDisposable
    {
        readonly TextWriter outBefore;
        readonly TextWriter errBefore;
        readonly StringWriter console;

        public RedirectedConsole()
        {
            this.console = new StringWriter();
            this.outBefore = Console.Out;
            this.errBefore = Console.Error;
            Console.SetOut(this.console);
            Console.SetError(this.console);
        }

        public string Output
        {
            get { return this.console.ToString(); }
        }

        public void Dispose()
        {
            Console.SetOut(this.outBefore);
            Console.SetError(this.errBefore);
            this.console.Dispose();
        }
    }
}