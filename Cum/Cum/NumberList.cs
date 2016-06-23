using System.Collections.Generic;

namespace Cum
{
    public class NumberList
    {
        public List<double> listdouble;
        public List<string> listString;
        public NumberList(List<double> listdouble, List<string> listString)
        {
            this.listdouble = listdouble;
            this.listString = listString;
        }
        public double XResurt()
        {
            int i = 0;
            bool isStop = true;
            do
            {
                if (listString[i] == "×" || listString[i] == "÷")
                {
                    switch (listString[i])
                    {
                        case "×":
                            listdouble[i] = listdouble[i] * listdouble[i + 1];
                            listdouble.RemoveAt(i + 1);
                            listString.RemoveAt(i);
                            break;
                        case "÷":
                            listdouble[i] = listdouble[i] / listdouble[i + 1];
                            listdouble.RemoveAt(i + 1);
                            listString.RemoveAt(i);
                            break;
                    }
                }
                else {
                    if (listString[i] == "=")
                        isStop = false;
                    else i++;
                }
            } while (isStop);
            return HResurt();
        }
        private  double HResurt()
        {
            bool isStop = true;
            int i = 0;
            do
            {
                if (listString[i] == "-" || listString[i] == "+")
                {
                    switch (listString[i])
                    {
                        case "+":
                            listdouble[i] = listdouble[i] + listdouble[i + 1];
                            listdouble.RemoveAt(i + 1);
                            listString.RemoveAt(i);
                            break;
                        case "-":
                            listdouble[i] = listdouble[i] - listdouble[i + 1];
                            listdouble.RemoveAt(i + 1);
                            listString.RemoveAt(i);
                            break;
                    }
                }
                else
                {
                    if (listString[i] == "=")
                        isStop = false;
                    else i++;
                }
            } while (isStop);
            return listdouble[0];
        }
    }

    public static class BuildString
    {
        public static bool Subsist(this string str, string sub)
        {
            int i = str.IndexOf(sub);
            if (i > -1 )
                return true;
            else return false;
        }
        public static void Fact(ref double d)
        {
            if (d > 1)
            {
                double m = 1, n = d;
                for (int i = 1; i <= n; i++)
                {
                    d = m * i;
                    m = m * i;
                }
            }
            else d = 0;
        }
    }
}
