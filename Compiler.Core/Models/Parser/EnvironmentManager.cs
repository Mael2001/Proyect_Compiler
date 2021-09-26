using System;
using System.Collections.Generic;
using System.Linq;
using Compiler.Core.Expressions;
using Compiler.Core.Models.Lexer;

namespace Compiler.Core.Models.Parser
{
    public static class EnvironmentManager
    {
        private static readonly List<Environment> Contexts = new List<Environment>();
        private static readonly List<Environment> InterpretContexts = new List<Environment>();

        public static Environment PushContext()
        {
            var env = new Environment();
            Contexts.Add(env);
            InterpretContexts.Add(env);
            return env;
        }

        public static Environment PopContext()
        {
            var lastContext = Contexts.Last();
            Contexts.Remove(lastContext);
            return lastContext;
        }


        public static Symbol GetSymbol(string lexeme)
        {
            for (int i = Contexts.Count - 1; i >= 0; i--)
            {
                var context = Contexts[i];
                var symbol = context.Get(lexeme);
                if (symbol != null)
                {
                    return symbol;
                }
            }

            throw new ApplicationException($"Symbol {lexeme} doesn't exist in current context");
        }

        public static Symbol GetSymbolForEvaluation(string lexeme)
        {
            foreach (var context in InterpretContexts)
            {
                var symbol = context.Get(lexeme);
                if (symbol != null)
                {
                    return symbol;
                }
            }

            throw new ApplicationException($"Symbol {lexeme} doesn't exist in current context");
        }

        public static void AddMethod(string lexeme, Id id, BinaryOperator arguments) =>
            Contexts.Last().AddMethod(lexeme, id, arguments);

        public static void AddVariable(string lexeme, Id id) => Contexts.Last().AddVariable(lexeme, id);

        public static void UpdateVariable(string lexeme, dynamic value)
        {
            for (int i = Contexts.Count - 1; i >= 0; i--)
            {
                var context = Contexts[i];
                var symbol = context.Get(lexeme);
                if (symbol != null)
                {
                    context.UpdateVariable(lexeme, value);
                }
            }
        }
    }
}
    