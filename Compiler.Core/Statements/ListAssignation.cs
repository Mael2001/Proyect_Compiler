using Compiler.Core.Expressions;
using System;
using System.Collections.Generic;
using System.Linq;
using Compiler.Core.Models.Lexer;
using Compiler.Core.Models.Parser;
using Environment = System.Environment;
using Type = Compiler.Core.Models.Parser.Type;

namespace Compiler.Core.Statements
{
    public class ListAssination : Statement
    {
        public ListAssination(Id id, List<Token> tokens)
        {
            Id = id;
            Tokens = tokens;
        }

        public Id Id { get; }
        public List<Token> Tokens { get; }

        public override string Generate(int tabs)
        {
            var code = "\n";
            code += GetCodeInit(tabs);
            code += $"const {Id.Generate()} = [";
            foreach (var token in Tokens)
            {
                if (token != Tokens.Last())
                {
                    code += token.Lexeme + ",";
                }
                else
                {
                    code += token.Lexeme;
                }
            }
            code +="];\n";
            return code;
        }

        public override void Interpret()
        {
        }

        public override void ValidateSemantic()
        {
            if (Id.Type != Type.IntList && Id.Type != Type.StringList && Id.Type != Type.FloatList && Id.Type != Type.Void)
            {
                throw new ApplicationException($"Type {Id.GetExpressionType()} is not assignable to List");
            }
        }
    }
}