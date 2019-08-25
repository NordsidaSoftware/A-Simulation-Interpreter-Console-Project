using System;

namespace Simland
{
    internal class Interpreter
    {
       
        public bool HasErrors { get; internal set; }
        public string result;

        internal void Interpret(string source)
        {
            Lexer lex = new Lexer(source);
            lex.scan();
            Parser parse = new Parser();
            if (!lex.HasErrors )
            {
                foreach (Token token in lex.Tokens)
                {
                    result += " " + token.ToString();
                }
            }
        }

        internal string PresentErrors()
        {
            return "ERRORS!";
        }
    }
}