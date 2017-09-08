using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

public class rpn
{
    private Dictionary<string, int> priorities;

    #region supported operators, function const
    private double m;

    public double M
    {
        get
        {
            return m;
        }
        set
        {
            m = value;
        }
    }
    
    private const string addition = "+";
    private const string substraction = "-";
    private const string multiplication = "*";
    private const string division = "/";
    private const string exponentiation = "^";
    
    private const string sin = "sin";
    private const string cos = "cos";
    private const string tg = "tg";
    private const string ctg = "ctg";

    private const string sinh = "sinh";
    private const string cosh = "cosh";
    private const string tgh = "tgh";
    private const string ctgh = "ctgh";

    private const string pi = "Π";
    private const string e = "e";

    private const string sqrt = "sqrt";
    private const string ln = "ln";
    private const string log = "log";
    private const string pow = "pow";

    private const string flor = "flor";
    private const string ceil = "ceil";
    #endregion
    private int parameters(string s)
    {
        switch (s)
        {
            case sin:
            case cos:
            case tg:
            case ctg:
            case sinh:
            case cosh:
            case tgh:
            case ctgh:
            case ln:
            case sqrt:
            case flor:
            case ceil:
            case pi:
            case e:
                return 1;
            case addition:
            case substraction:
            case multiplication:
            case division:
            case exponentiation:
            case pow:
            case log:
                return 2;
            default: throw new ArgumentException("Unknown operator");
        }
    }


    public rpn()
    {
        priorities = new Dictionary<string, int>();
        priorities.Add("(", 0);
        priorities.Add("+", 1);
        priorities.Add("-", 1);
        priorities.Add("*", 2);
        priorities.Add("/", 2);
        priorities.Add("^", 3);

        priorities.Add("sin", 4);
        priorities.Add("cos", 4);
        priorities.Add("tg", 4);
        priorities.Add("ctg", 4);

        priorities.Add("sinh", 4);
        priorities.Add("cosh", 4);
        priorities.Add("tgh", 4);
        priorities.Add("ctgh", 4);

        priorities.Add("sqrt", 3);
        priorities.Add("ln", 4);
        priorities.Add("pow", 3);
        priorities.Add("log", 4);
        priorities.Add("ceil", 5);
        priorities.Add("flor", 4);
        
    
    }
    
    public double calculate(string text)
    {
        List<string> input_list = new List<string>(convert_to_rpn(token(text)));
        Stack<double> stack = new Stack<double>();
        double value = 0;
      
        double a, b; //zmienne pomocnicze do zapamiętywania wartosci ze stosu

        for (int i = 0; i < input_list.Count; i++)
        {
            if (double.TryParse(input_list[i], out value))
            {
                stack.Push(value);
            }
            else
            {
                if (parameters(input_list[i]) == 2)
                {
                    a = stack.Pop(); //zapamiętywanie i ściąganie ze stosu liczb
                    b = stack.Pop();

                    switch (input_list[i])
                    {
                        case addition:
                            stack.Push(b + a);
                            break;
                        case substraction:
                            stack.Push(b - a);
                            break;
                        case multiplication:
                            stack.Push(b * a);
                            break;
                        case division:
                            stack.Push(b / a);
                            break;
                        case exponentiation:
                            stack.Push(Math.Pow(b, a));
                            break;
                        case log:
                            stack.Push(Math.Log(a, b));
                            break;
                        case pow:
                            stack.Push(Math.Pow(b, a));
                            break;
                    }
                }
                else
                {
                    a = stack.Pop(); //zapamiętywanie i ściąganie ze stosu liczb

                    switch (input_list[i])
                    {
                        case sin:
                            stack.Push(Math.Sin(a*(Math.PI/180)));
                            break;
                        case cos:
                            stack.Push(Math.Cos(a * (Math.PI / 180)));
                            break;
                        case tg:
                            stack.Push(Math.Tan(a * (Math.PI / 180)));
                            break;
                        case ctg:
                            stack.Push(1/(Math.Tan(a * (Math.PI / 180))));
                            break;
                        case sqrt:
                            stack.Push(Math.Sqrt(a));
                            break;
                        case ln:
                            stack.Push(Math.Log(a));
                            break;
                        case sinh:
                            stack.Push(Math.Sinh(a));
                            break;
                        case cosh:
                            stack.Push(Math.Cosh(a));
                            break;
                        case tgh:
                            stack.Push(Math.Tanh(a));
                            break;
                        case ctgh:
                            stack.Push((Math.Cosh(a)) / (Math.Sinh(a)));
                            break;
                        case flor:
                            stack.Push(Math.Floor(a));
                            break;
                        case ceil:
                            stack.Push(Math.Ceiling(a));
                            break;
                    }
                }
            }
        }
        return stack.Peek();
    }


    private List<string> convert_to_rpn(String[] input)
    {
        var to_return = new List<string>();
        var stack = new Stack<string>();
        double value;

        for (int i = 0; i < input.Length; i++)
        {

            if (input[i] == pi || input[i] == e)
            {
                switch (input[i])
                {
                    case e:
                        input[i] = Math.E.ToString();
                        break;
                    case pi:
                        input[i] = Math.PI.ToString();
                        break;
                }
            }
            //jest liczbą
            if (double.TryParse(input[i], out value))
            {
                to_return.Add(input[i]);
            }
            else
            {  
                if (input[i] == "(")
                {
                    stack.Push(input[i]);
                }//jeśli aktualnego element jest prawym nawiasem wszstko do lewego nawiasu wyślij na wyjście i lewy nawias zrzuć
                if (input[i] == ")") 
                {
                    while (stack.Peek() != "(")
                    {
                        to_return.Add(stack.Pop()); 
                    }
                    stack.Pop();
                }
                //aktualnego element jest operatorem
                if (priorities.ContainsKey(input[i]) && input[i]!="(")
                {
                    if (stack.Count == 0)
                    {
                        stack.Push(input[i]);
                    }//stos nie jest pusty
                    else
                    {
                        if (priorities[input[i]] > priorities[stack.Peek()])
                        {
                            stack.Push(input[i]);
                        }
                        //jeśli priorytet aktualnego elementu jest mniejszy lub równy priorytetowi elementu ze stosu
                        else
                        {
                            while (stack.Count != 0 && priorities[input[i]] <= priorities[stack.Peek()])
                            {
                                to_return.Add(stack.Pop());
                            }
                            stack.Push(input[i]);
                        }
                    }
                }
            }
        }
        while (stack.Count != 0)
        {
            to_return.Add(stack.Pop());
        }
        return to_return;
    }

    //dzielenie wejściowego tekstu na liczby/operatory
    public String[] token(string input_text)
    {
        input_text = input_text.Replace(" ", "");
        input_text = input_text.Replace("+", " + ").Replace("-", " - ").Replace("/", " / ");
        input_text = input_text.Replace("*", " * ").Replace("^", " ^ ");

        input_text = input_text.Replace("sin", " sin");
        input_text = input_text.Replace("cos", " cos");
        input_text = input_text.Replace("tg", " tg");
        input_text = input_text.Replace("ctg", " tg");

        input_text = input_text.Replace("sinh", " sinh");
        input_text = input_text.Replace("cosh", " cosh");
        input_text = input_text.Replace("tgh", " tgh");
        input_text = input_text.Replace("ctgh", " ctgh");

        input_text = input_text.Replace("sqrt", " sqrt");
        input_text = input_text.Replace("ln", " ln");
        input_text = input_text.Replace("log", " log");
        input_text = input_text.Replace("pow", " pow");
        input_text = input_text.Replace("flor", " flor");
        input_text = input_text.Replace("ceil", " ceil");

        input_text = input_text.Replace(";", " ");

        input_text = input_text.Replace("(", " ( ").Replace(")", " ) ").Trim();

        String[] to_return = input_text.Split(' ');
       
        return to_return;
    }
}
