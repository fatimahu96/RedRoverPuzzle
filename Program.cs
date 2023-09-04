using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Globalization;
using System.Linq;

namespace RedRoverPuzzle
{
    class Program
    {
        static void Main(string[] args)
        {
            string input =
                "(id, name, email, type(id, name, customFields(c1, c2, c3)), externalId)";
            Console.WriteLine("Input: " + input);
            Console.WriteLine();

            Console.WriteLine("Output (Sorted): ");
            //Parse first level
            String[] sortedlevel = ParseLevel(input.Substring(1, input.Length - 2), true);
            //Print first level
            PrintIndentedString(sortedlevel, 0, true);

            Console.WriteLine();
            Console.WriteLine("Output (Not Sorted): ");
            //Parse first level
            String[] sortedlevel_notsorted = ParseLevel(
                input.Substring(1, input.Length - 2),
                false
            );
            //Print first level
            PrintIndentedString(sortedlevel_notsorted, 0, false);
        }

        public static String[] ParseLevel(string s, Boolean sort)
        {
            String result = "";
            int count = 0;
            for (int i = 0; i < s.Length; i++)
            {
                char c = s[i];
                if (c == '(')
                {
                    count++;
                    result = result + c;
                }
                else if (c == ')')
                {
                    count--;
                    result = result + c;
                }
                else if (c == ',')
                {
                    if (count == 0)
                    {
                        result = result + "\n";
                    }
                    else
                    {
                        result = result + c;
                    }
                }
                else if (!Char.IsWhiteSpace(c))
                {
                    result = result + c;
                }
            }

            string[] arr = result.Split("\n");
            if (sort)
            {
                Array.Sort(arr);
            }
            return arr;
        }

        public static void PrintIndentedString(string[] sortedlevel, int level, Boolean sort)
        {
            //print results
            for (int i = 0; i < sortedlevel.Length; i++)
            {
                if (
                    sortedlevel[i].Contains('(')
                    || sortedlevel[i].Contains(')')
                    || sortedlevel[i].Contains(',')
                )
                {
                    //get the index of first parantheses
                    int charPosition = sortedlevel[i].IndexOf("(");

                    //Print with level
                    PrintWithLevel(sortedlevel[i].Substring(0, charPosition), level);
                    //Increase level
                    level++;

                    //Parse the sub-input
                    String sub_input = sortedlevel[i].Substring(
                        charPosition + 1,
                        sortedlevel[i].Length
                            - (2 + sortedlevel[i].Substring(0, charPosition).Length)
                    );
                    string[] arr = ParseLevel(sub_input, sort);
                    PrintIndentedString(arr, level, sort);
                    level--;
                }
                else
                {
                    PrintWithLevel(sortedlevel[i], level);
                }
            }
        }

        public static void PrintWithLevel(string s, int level)
        {
            if (level > 0)
            {
                for (int i = 0; i < level; i++)
                {
                    Console.Write("  ");
                }
                Console.Write(s + "\n");
            }
            else
            {
                Console.WriteLine(s);
            }
        }
    }
}
