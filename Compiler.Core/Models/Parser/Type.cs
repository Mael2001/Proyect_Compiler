﻿using System;
using Compiler.Core.Enum;

namespace Compiler.Core.Models.Parser
{
    public class Type : IEquatable<Type>
    {
        public string Lexeme { get; }

        public TokenType TokenType { get; }
        public Type(string lexeme, TokenType tokenType)
        {
            Lexeme = lexeme;
            TokenType = tokenType;
        }

        public static Type Int => new Type("int", TokenType.IntConstant);
        public static Type IntList => new Type("List<int>", TokenType.IntListConstant);
        public static Type Float => new Type("float", TokenType.FloatConstant);
        public static Type FloatList => new Type("List<float>", TokenType.FloatListConstant);
        public static Type String => new Type("string", TokenType.StringConstant);
        public static Type StringList => new Type("List<string>", TokenType.StringListConstant);
        public static Type Bool => new Type("bool", TokenType.BoolConstant);
        public static Type Void => new Type("void", TokenType.BasicType);
        public static Type Class => new Type("class", TokenType.ClassKeyword);
        public static Type Func => new Type("func", TokenType.FunctionKeyword);
        public static Type Date => new Type("Date", TokenType.DateTimeKeyword);


        public bool Equals(Type other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Lexeme == other.Lexeme && TokenType == other.TokenType;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj.GetType() != this.GetType() ? false : Equals((Type)obj);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Lexeme, (int)TokenType);
        }

        public static bool operator ==(Type a, Type b) => a is { } && a.Equals(b);

        public static bool operator !=(Type a, Type b) => a is { } && !a.Equals(b);

        public override string ToString()
        {
            return Lexeme;
        }
    }
}
