using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Question
{
    private string question;
    private string ans_A;
    private string ans_B;
    private string ans_C;
    private string ans_D;
    private string correct;

    public Question()
    {
        question = "The rules governing covering loads, securing cargo, where you can drive, and how much your load can weigh:";
        ans_A = "Are determined by the federal government.";
        ans_B = "Are determined by local government.";
        ans_C = "Are determined by local, state, and the federal government.";
        ans_D = "Are determined by state government.";
        correct = "Are determined by local, state, and the federal government.";
    }

    public Question(string ques, string a, string b, string c, string d, string cor)
    {
        question = ques;
        ans_A = a;
        ans_B = b;
        ans_C = c;
        ans_D = d;
        correct = cor;
    }

    public void setQuestion(string ques)
    {
        question = ques;
    }

    public string getQuestion()
    {
        return question;
    }

    public void setA(string a)
    {
        ans_A = a;
    }

    public string getA()
    {
        return ans_A;
    }

    public void setB(string b)
    {
        ans_B = b;
    }

    public string getB()
    {
        return ans_B;
    }
    public void setC(string c)
    {
        ans_C = c;
    }

    public string getC()
    {
        return ans_C;
    }
    public void setD(string d)
    {
        ans_D = d;
    }

    public string getD()
    {
        return ans_D;
    }

    public void setCor(string cor)
    {
        correct = cor;
    }

    bool isEqual(string ans)
    {
        if (ans.Equals(correct))
        {
            return true;
        }
        else
            return false;
    }
};
