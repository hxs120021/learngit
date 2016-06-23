package Boub;
import java.util.ArrayList;

/**
 * Created by 战地记者 on 2016/6/4.
 */
public class Actionev {
    ArrayList<Float> arrayListFloat;
    ArrayList<String> arrayListString;
    public Actionev(ArrayList<Float> arrayListFloat, ArrayList<String> arrayListString)
    {
        this.arrayListFloat = arrayListFloat;
        this.arrayListString = arrayListString;
    }
    public Actionev(){}
    public void RemoveAll()
    {
        this.arrayListString.clear();
        this.arrayListFloat.clear();
    }
    public float XResult()
    {
        int k = 0;
        boolean isEnd = true;
        do
        {
            String string = arrayListString.get(k);
            if (string == "=")
                break;
            if (string == "×" || string == "÷")
                switch (string) {
                    case "×":
                        arrayListFloat.set(k, arrayListFloat.get(k) * arrayListFloat.get(k + 1));
                        arrayListFloat.remove(k + 1);
                        arrayListString.remove(k);
                        break;
                    case "÷":
                        arrayListFloat.set(k, arrayListFloat.get(k) / arrayListFloat.get(k + 1));
                        arrayListFloat.remove(k + 1);
                        arrayListString.remove(k);
                        break;
                }
            else k++;

        } while (isEnd) ;
        return HResult();
    }
    private float HResult()
    {
        float result = 0;
        for(int i = 0; i < arrayListString.size() - 1; i++)
        {
            String string = arrayListString.get(i);
            switch (string)
            {
                case "+":
                    break;
                case "-":
                    arrayListFloat.set(i + 1, arrayListFloat.get(i + 1) * (-1));
                    break;
            }
        }
        for(int i = 0; i < arrayListFloat.size(); i++)
        {
            result += arrayListFloat.get(i);
        }
        return result;
    }
}
