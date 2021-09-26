using Compiler.Core.Interfaces;

namespace Compiler.Core.Engine
{
    public class CompilerEngine
    {
        private readonly IParser _parser;

        public CompilerEngine(IParser parser)
        {
            this._parser = parser;
        }

        public void Run()
        {
            var intermediateCode = this._parser.Parse();
        }
    }
}