using Compiler.Core.Expressions;
using System;
using Compiler.Core.Models.Parser;
using Environment = System.Environment;
using Type = Compiler.Core.Models.Parser.Type;

namespace Compiler.Core.Statements
{
    public class AssignationStatement : Statement
    {
        public AssignationStatement(Id id, TypedExpression expression)
        {
            Id = id;
            Expression = expression;
        }

        public Id Id { get; }
        public TypedExpression Expression { get; }

        public override string Generate(int tabs)
        {
            var code = "\n";
            code += GetCodeInit(tabs);
            if (Id.GetExpressionType()==Type.IntList || Id.GetExpressionType() == Type.FloatList || Id.GetExpressionType() == Type.StringList)
            {
                code += $"const {Id.Generate()} = [{Expression.Generate()},{Expression.Generate()}];{Environment.NewLine}";
            }
            else
            {
                code += $"var {Id.Generate()} = {Expression.Generate()}{Environment.NewLine}";
            }
            return code;
        }

        public override void Interpret()
        {
            EnvironmentManager.UpdateVariable(Id.Token.Lexeme, Expression.Evaluate());
        }

        public override void ValidateSemantic()
        {
            if ( Id.Type == Type.Void)
            {
                return;
            }
            if (Id.GetExpressionType() != Expression.GetExpressionType())
            {
                throw new ApplicationException($"Type {Id.GetExpressionType()} is not assignable to {Expression.GetExpressionType()}");
            }
        }
    }
}
