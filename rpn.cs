using System;

public class Class1
{
	public Class1()
	{
	}

    private List<string> division(string input_text)
    {
        var to_return = new List<string>(); 

        string part = String.Empty; //to stoarge value or operand
        int k = 0; // it's need in while loop

        for (int i = 0; i < input_text.Length; i++)
        {
            if (((int)input_text[i] >= 48 && (int)input_text[i] <= 57) || (int)input_text[i] == 46)
            {
                k = i;
                part = String.Empty;
                while ((((int)input_text[k] >= 48 && (int)input_text[k] <= 57) || (int)input_text[k] == 46))
                {
                    part += input_text[k];
                    ++k;
                    if (k == input_text.Length)
                    {
                        break;
                    }
                }
                --k;
                to_return.Add(part);
                i = k;
                part = String.Empty;
            }
            else
            {
                part = String.Empty;
                part += input_text[i];
                to_return.Add(part);
                part = String.Empty;
            }
            return to_return;
        }
    }
}
