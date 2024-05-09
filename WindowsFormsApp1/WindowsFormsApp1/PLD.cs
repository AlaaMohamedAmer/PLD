
using System;
using System.IO;
using System.Runtime.Serialization;
using com.calitha.goldparser.lalr;
using com.calitha.commons;
using System.Windows.Forms;

namespace com.calitha.goldparser
{

    [Serializable()]
    public class SymbolException : System.Exception
    {
        public SymbolException(string message) : base(message)
        {
        }

        public SymbolException(string message,
            Exception inner) : base(message, inner)
        {
        }

        protected SymbolException(SerializationInfo info,
            StreamingContext context) : base(info, context)
        {
        }

    }

    [Serializable()]
    public class RuleException : System.Exception
    {

        public RuleException(string message) : base(message)
        {
        }

        public RuleException(string message,
                             Exception inner) : base(message, inner)
        {
        }

        protected RuleException(SerializationInfo info,
                                StreamingContext context) : base(info, context)
        {
        }

    }

    enum SymbolConstants : int
    {
        SYMBOL_EOF                   =  0, // (EOF)
        SYMBOL_ERROR                 =  1, // (Error)
        SYMBOL_WHITESPACE            =  2, // Whitespace
        SYMBOL_MINUS                 =  3, // '-'
        SYMBOL_EXCLAMEQ              =  4, // '!='
        SYMBOL_RPAREN                =  5, // ')'
        SYMBOL_TIMES                 =  6, // '*'
        SYMBOL_DIV                   =  7, // '/'
        SYMBOL_COLON                 =  8, // ':'
        SYMBOL_LBRACE                =  9, // '{'
        SYMBOL_RBRACE                = 10, // '}'
        SYMBOL_PLUS                  = 11, // '+'
        SYMBOL_LT                    = 12, // '<'
        SYMBOL_LTEQ                  = 13, // '<='
        SYMBOL_EQ                    = 14, // '='
        SYMBOL_EQEQ                  = 15, // '=='
        SYMBOL_GT                    = 16, // '>'
        SYMBOL_GTEQ                  = 17, // '>='
        SYMBOL_BEGIN                 = 18, // Begin
        SYMBOL_BOOLEAN               = 19, // boolean
        SYMBOL_CASE                  = 20, // case
        SYMBOL_DEFAULT               = 21, // default
        SYMBOL_DO                    = 22, // do
        SYMBOL_ELSE                  = 23, // else
        SYMBOL_END                   = 24, // End
        SYMBOL_FLOAT                 = 25, // float
        SYMBOL_FOR                   = 26, // for
        SYMBOL_IDENTIFIER            = 27, // identifier
        SYMBOL_IF                    = 28, // if
        SYMBOL_IN                    = 29, // in
        SYMBOL_INT                   = 30, // int
        SYMBOL_RANGELPAREN           = 31, // 'range('
        SYMBOL_STRING                = 32, // string
        SYMBOL_SWITCH                = 33, // switch
        SYMBOL_WHILE                 = 34, // while
        SYMBOL_ARITHMETIC_EXPRESSION = 35, // <arithmetic_expression>
        SYMBOL_ASSIGNMENT            = 36, // <assignment>
        SYMBOL_CASE_STATEMENTS       = 37, // <case_statements>
        SYMBOL_DO_WHILE_LOOP         = 38, // <do_while_loop>
        SYMBOL_ELSE_CLAUSE           = 39, // <else_clause>
        SYMBOL_EXPRESSION            = 40, // <expression>
        SYMBOL_FOR_LOOP              = 41, // <for_loop>
        SYMBOL_IF_STATEMENT          = 42, // <if_statement>
        SYMBOL_LOGICAL_EXPRESSION    = 43, // <logical_expression>
        SYMBOL_PROGRAM               = 44, // <program>
        SYMBOL_STATEMENT             = 45, // <statement>
        SYMBOL_STATEMENT_LIST        = 46, // <statement_list>
        SYMBOL_SWITCH_STATEMENT      = 47, // <switch_statement>
        SYMBOL_VALUE                 = 48, // <value>
        SYMBOL_WHILE_LOOP            = 49  // <while_loop>
    };

    enum RuleConstants : int
    {
        RULE_PROGRAM_BEGIN_END                                   =  0, // <program> ::= Begin <statement_list> End
        RULE_STATEMENT_LIST                                      =  1, // <statement_list> ::= <statement> <statement_list>
        RULE_STATEMENT_LIST2                                     =  2, // <statement_list> ::= <statement>
        RULE_STATEMENT                                           =  3, // <statement> ::= <assignment>
        RULE_STATEMENT2                                          =  4, // <statement> ::= <if_statement>
        RULE_STATEMENT3                                          =  5, // <statement> ::= <while_loop>
        RULE_STATEMENT4                                          =  6, // <statement> ::= <for_loop>
        RULE_STATEMENT5                                          =  7, // <statement> ::= <switch_statement>
        RULE_STATEMENT6                                          =  8, // <statement> ::= <do_while_loop>
        RULE_ASSIGNMENT_IDENTIFIER_EQ                            =  9, // <assignment> ::= identifier '=' <expression>
        RULE_IF_STATEMENT_IF_COLON                               = 10, // <if_statement> ::= if <expression> ':' <statement_list> <else_clause>
        RULE_ELSE_CLAUSE_ELSE_COLON                              = 11, // <else_clause> ::= else ':' <statement_list>
        RULE_ELSE_CLAUSE                                         = 12, // <else_clause> ::= 
        RULE_WHILE_LOOP_WHILE_COLON                              = 13, // <while_loop> ::= while <expression> ':' <statement_list>
        RULE_DO_WHILE_LOOP_DO_COLON_WHILE                        = 14, // <do_while_loop> ::= do ':' <statement_list> while <expression>
        RULE_FOR_LOOP_FOR_IDENTIFIER_IN_RANGELPAREN_RPAREN_COLON = 15, // <for_loop> ::= for identifier in 'range(' <expression> ')' ':' <statement_list>
        RULE_SWITCH_STATEMENT_SWITCH_IDENTIFIER_LBRACE_RBRACE    = 16, // <switch_statement> ::= switch identifier '{' <case_statements> '}'
        RULE_CASE_STATEMENTS_CASE_COLON                          = 17, // <case_statements> ::= case <value> ':' <statement_list> <case_statements>
        RULE_CASE_STATEMENTS_DEFAULT_COLON                       = 18, // <case_statements> ::= default ':' <statement_list>
        RULE_EXPRESSION                                          = 19, // <expression> ::= <arithmetic_expression>
        RULE_EXPRESSION2                                         = 20, // <expression> ::= <logical_expression>
        RULE_EXPRESSION3                                         = 21, // <expression> ::= <value>
        RULE_ARITHMETIC_EXPRESSION_PLUS                          = 22, // <arithmetic_expression> ::= <value> '+' <value>
        RULE_ARITHMETIC_EXPRESSION_MINUS                         = 23, // <arithmetic_expression> ::= <value> '-' <value>
        RULE_ARITHMETIC_EXPRESSION_TIMES                         = 24, // <arithmetic_expression> ::= <value> '*' <value>
        RULE_ARITHMETIC_EXPRESSION_DIV                           = 25, // <arithmetic_expression> ::= <value> '/' <value>
        RULE_LOGICAL_EXPRESSION_EQEQ                             = 26, // <logical_expression> ::= <value> '==' <value>
        RULE_LOGICAL_EXPRESSION_EXCLAMEQ                         = 27, // <logical_expression> ::= <value> '!=' <value>
        RULE_LOGICAL_EXPRESSION_LT                               = 28, // <logical_expression> ::= <value> '<' <value>
        RULE_LOGICAL_EXPRESSION_GT                               = 29, // <logical_expression> ::= <value> '>' <value>
        RULE_LOGICAL_EXPRESSION_LTEQ                             = 30, // <logical_expression> ::= <value> '<=' <value>
        RULE_LOGICAL_EXPRESSION_GTEQ                             = 31, // <logical_expression> ::= <value> '>=' <value>
        RULE_VALUE_IDENTIFIER                                    = 32, // <value> ::= identifier
        RULE_VALUE_INT                                           = 33, // <value> ::= int
        RULE_VALUE_FLOAT                                         = 34, // <value> ::= float
        RULE_VALUE_STRING                                        = 35, // <value> ::= string
        RULE_VALUE_BOOLEAN                                       = 36  // <value> ::= boolean
    };

    public class MyParser
    {
        private LALRParser parser;

        ListBox lst;
        ListBox ls;
        public MyParser(string filename, ListBox lst, ListBox ls)
        {
            FileStream stream = new FileStream(filename,
                                               FileMode.Open,
                                               FileAccess.Read,
                                               FileShare.Read);
            this.lst = lst;
            this.ls = ls;
            Init(stream);
            stream.Close();
        }

        public MyParser(string baseName, string resourceName)
        {
            byte[] buffer = ResourceUtil.GetByteArrayResource(
                System.Reflection.Assembly.GetExecutingAssembly(),
                baseName,
                resourceName);
            MemoryStream stream = new MemoryStream(buffer);
            Init(stream);
            stream.Close();
        }

        public MyParser(Stream stream)
        {
            Init(stream);
        }

        private void Init(Stream stream)
        {
            CGTReader reader = new CGTReader(stream);
            parser = reader.CreateNewParser();
            parser.TrimReductions = false;
            parser.StoreTokens = LALRParser.StoreTokensMode.NoUserObject;

            parser.OnTokenError += new LALRParser.TokenErrorHandler(TokenErrorEvent);
            parser.OnParseError += new LALRParser.ParseErrorHandler(ParseErrorEvent);
            parser.OnTokenRead += new LALRParser.TokenReadHandler(TokenReadEvent);
        }

        public void Parse(string source)
        {
            NonterminalToken token = parser.Parse(source);
            if (token != null)
            {
                Object obj = CreateObject(token);
                //todo: Use your object any way you like
            }
        }

        private Object CreateObject(Token token)
        {
            if (token is TerminalToken)
                return CreateObjectFromTerminal((TerminalToken)token);
            else
                return CreateObjectFromNonterminal((NonterminalToken)token);
        }

        private Object CreateObjectFromTerminal(TerminalToken token)
        {
            switch (token.Symbol.Id)
            {
                case (int)SymbolConstants.SYMBOL_EOF :
                //(EOF)
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_ERROR :
                //(Error)
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_WHITESPACE :
                //Whitespace
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_MINUS :
                //'-'
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_EXCLAMEQ :
                //'!='
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_RPAREN :
                //')'
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_TIMES :
                //'*'
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_DIV :
                //'/'
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_COLON :
                //':'
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_LBRACE :
                //'{'
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_RBRACE :
                //'}'
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_PLUS :
                //'+'
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_LT :
                //'<'
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_LTEQ :
                //'<='
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_EQ :
                //'='
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_EQEQ :
                //'=='
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_GT :
                //'>'
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_GTEQ :
                //'>='
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_BEGIN :
                //Begin
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_BOOLEAN :
                //boolean
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_CASE :
                //case
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_DEFAULT :
                //default
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_DO :
                //do
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_ELSE :
                //else
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_END :
                //End
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_FLOAT :
                //float
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_FOR :
                //for
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_IDENTIFIER :
                //identifier
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_IF :
                //if
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_IN :
                //in
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_INT :
                //int
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_RANGELPAREN :
                //'range('
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_STRING :
                //string
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_SWITCH :
                //switch
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_WHILE :
                //while
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_ARITHMETIC_EXPRESSION :
                //<arithmetic_expression>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_ASSIGNMENT :
                //<assignment>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_CASE_STATEMENTS :
                //<case_statements>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_DO_WHILE_LOOP :
                //<do_while_loop>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_ELSE_CLAUSE :
                //<else_clause>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_EXPRESSION :
                //<expression>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_FOR_LOOP :
                //<for_loop>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_IF_STATEMENT :
                //<if_statement>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_LOGICAL_EXPRESSION :
                //<logical_expression>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_PROGRAM :
                //<program>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_STATEMENT :
                //<statement>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_STATEMENT_LIST :
                //<statement_list>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_SWITCH_STATEMENT :
                //<switch_statement>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_VALUE :
                //<value>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_WHILE_LOOP :
                //<while_loop>
                //todo: Create a new object that corresponds to the symbol
                return null;

            }
            throw new SymbolException("Unknown symbol");
        }

        public Object CreateObjectFromNonterminal(NonterminalToken token)
        {
            switch (token.Rule.Id)
            {
                case (int)RuleConstants.RULE_PROGRAM_BEGIN_END :
                //<program> ::= Begin <statement_list> End
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_STATEMENT_LIST :
                //<statement_list> ::= <statement> <statement_list>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_STATEMENT_LIST2 :
                //<statement_list> ::= <statement>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_STATEMENT :
                //<statement> ::= <assignment>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_STATEMENT2 :
                //<statement> ::= <if_statement>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_STATEMENT3 :
                //<statement> ::= <while_loop>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_STATEMENT4 :
                //<statement> ::= <for_loop>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_STATEMENT5 :
                //<statement> ::= <switch_statement>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_STATEMENT6 :
                //<statement> ::= <do_while_loop>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_ASSIGNMENT_IDENTIFIER_EQ :
                //<assignment> ::= identifier '=' <expression>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_IF_STATEMENT_IF_COLON :
                //<if_statement> ::= if <expression> ':' <statement_list> <else_clause>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_ELSE_CLAUSE_ELSE_COLON :
                //<else_clause> ::= else ':' <statement_list>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_ELSE_CLAUSE :
                //<else_clause> ::= 
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_WHILE_LOOP_WHILE_COLON :
                //<while_loop> ::= while <expression> ':' <statement_list>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_DO_WHILE_LOOP_DO_COLON_WHILE :
                //<do_while_loop> ::= do ':' <statement_list> while <expression>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_FOR_LOOP_FOR_IDENTIFIER_IN_RANGELPAREN_RPAREN_COLON :
                //<for_loop> ::= for identifier in 'range(' <expression> ')' ':' <statement_list>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_SWITCH_STATEMENT_SWITCH_IDENTIFIER_LBRACE_RBRACE :
                //<switch_statement> ::= switch identifier '{' <case_statements> '}'
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_CASE_STATEMENTS_CASE_COLON :
                //<case_statements> ::= case <value> ':' <statement_list> <case_statements>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_CASE_STATEMENTS_DEFAULT_COLON :
                //<case_statements> ::= default ':' <statement_list>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_EXPRESSION :
                //<expression> ::= <arithmetic_expression>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_EXPRESSION2 :
                //<expression> ::= <logical_expression>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_EXPRESSION3 :
                //<expression> ::= <value>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_ARITHMETIC_EXPRESSION_PLUS :
                //<arithmetic_expression> ::= <value> '+' <value>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_ARITHMETIC_EXPRESSION_MINUS :
                //<arithmetic_expression> ::= <value> '-' <value>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_ARITHMETIC_EXPRESSION_TIMES :
                //<arithmetic_expression> ::= <value> '*' <value>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_ARITHMETIC_EXPRESSION_DIV :
                //<arithmetic_expression> ::= <value> '/' <value>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_LOGICAL_EXPRESSION_EQEQ :
                //<logical_expression> ::= <value> '==' <value>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_LOGICAL_EXPRESSION_EXCLAMEQ :
                //<logical_expression> ::= <value> '!=' <value>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_LOGICAL_EXPRESSION_LT :
                //<logical_expression> ::= <value> '<' <value>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_LOGICAL_EXPRESSION_GT :
                //<logical_expression> ::= <value> '>' <value>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_LOGICAL_EXPRESSION_LTEQ :
                //<logical_expression> ::= <value> '<=' <value>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_LOGICAL_EXPRESSION_GTEQ :
                //<logical_expression> ::= <value> '>=' <value>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_VALUE_IDENTIFIER :
                //<value> ::= identifier
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_VALUE_INT :
                //<value> ::= int
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_VALUE_FLOAT :
                //<value> ::= float
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_VALUE_STRING :
                //<value> ::= string
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_VALUE_BOOLEAN :
                //<value> ::= boolean
                //todo: Create a new object using the stored tokens.
                return null;

            }
            throw new RuleException("Unknown rule");
        }

        private void TokenErrorEvent(LALRParser parser, TokenErrorEventArgs args)
        {
            string message = "Token error with input: '"+args.Token.ToString()+"'";
            //todo: Report message to UI?
        }

        private void ParseErrorEvent(LALRParser parser, ParseErrorEventArgs args)
        {
            string message = "Parse error caused by token: '" + args.UnexpectedToken.ToString() + "'" + args.UnexpectedToken.Location.LineNr;
            lst.Items.Add(message);
            string m2 = "Expected token:" + args.ExpectedTokens.ToString();
            lst.Items.Add(m2);
            //todo: Report message to UI?
        }
        private void TokenReadEvent(LALRParser parser, TokenReadEventArgs args)
        {
            string info = args.Token.Text + "    \t \t" + (SymbolConstants) args.Token.Symbol.Id;
            ls.Items.Add (info);
        }

    }
}
