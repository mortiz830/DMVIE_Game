using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using System.Data;
using Mono.Data.Sqlite;
using UnityEngine.SceneManagement;

public class QuizControl : MonoBehaviour
{
    [SerializeField]
    public RaceBar raceBar;

    public GameObject questionText;
    public GameObject op3_Button;
    public GameObject op4_Button;
    public GameObject op1;
    public GameObject op2;
    public GameObject op3;
    public GameObject op4;
    public GameObject questionPanel;
    public GameObject answerMessage;
    public GameObject cardButton;
    public AudioSource correct;
    public AudioSource incorrect;
    public AudioSource cardFlip;
    public int score = 0;
    public int numCorrect;
    public int numIncorrect;
    private string questionQuery;

    List<int> duplicateCheck = new List<int>();//list to check duplicate questions

    string activeQuestion;
    string[,] answerOptions = new string[4, 2];

    Random rnd = new Random();
    int rand;

    public void CardButton()
    {
        cardButton.SetActive(false);
        Debug.Log(Main_Menu.quizType);
        cardFlip.Play();
        answerMessage.SetActive(false);
        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 2; j++)
                answerOptions[i, j] = "";
        }

        List<string> quest = new List<string>();//List of questions available from database, 87 string long
        string conn = "URI=file:" + Application.dataPath + "/Plugins/DMVIE.db";  //location of database
        IDbConnection dbconn; //object to connect to database
        dbconn = (IDbConnection)new SqliteConnection(conn); //database connection created 
        dbconn.Open(); //database connection made
        IDbCommand dbcmd = dbconn.CreateCommand(); //object to create commands for database
        if (Main_Menu.quizType == "CDL")
        {
            //Debug.Log("Quiz type set to:" + quizType);
            questionQuery = "SELECT question FROM DMVIE_QUESTION WHERE Category = \"CDL\"";
        }
        else if(Main_Menu.quizType == "Permit")
        {
            //Debug.Log("Quiz type set to:" + quizType);
            questionQuery = "SELECT question FROM DMVIE_QUESTION WHERE Category = \"Permit\"";
        }
        //string questionQuery = "SELECT question FROM DMVIE_QUESTION";
        dbcmd.CommandText = questionQuery;
        IDataReader reader = dbcmd.ExecuteReader();
        while (reader.Read())
        {
            string currentQuestion = reader.GetString(0);

            quest.Add(currentQuestion);

            Debug.Log("Quest: " + currentQuestion);
        }
        reader.Close();
        do
        {
            rand = Random.Range(0, quest.Count);
            Debug.Log(rand);
            Debug.Log("inloop");
            Debug.Log(duplicateCheck.Contains(rand));

        } while (duplicateCheck.Contains(rand));

        //Debug.Log(quest.Count);
        duplicateCheck.Add(rand);

        activeQuestion = quest[rand]; //Active question
        questionText.GetComponent<TMPro.TextMeshProUGUI>().text = activeQuestion;

        string optionsQuery = "SELECT Answer, isCorrect FROM QUEST_OPTION WHERE Question = \"" + quest[rand] + "\" ORDER BY Answer DESC";
        dbcmd.CommandText = optionsQuery;
        reader = dbcmd.ExecuteReader();
        Debug.Log(quest[rand]);


        int row = 0;
        string correctString;

        while (reader.Read())
        {
            int col = 0;
            string currentOption = reader.GetString(0);
            bool isCorrect = reader.GetBoolean(1);
            if (isCorrect)
            {
                correctString = "Correct";
            }
            else { correctString = "Incorrect"; }

            answerOptions[row, col] = currentOption;
            col = 1;
            answerOptions[row, col] = correctString;
            row++;

            Debug.Log("Option: " + currentOption + " Correct? " + isCorrect);
        }
        Debug.Log(answerOptions[0, 0] + " " + answerOptions[0, 1] + " " + answerOptions[1, 0] + " " + answerOptions[1, 1] + " " + answerOptions[2, 0] + " " + answerOptions[2, 1] + " " + answerOptions[3, 0] + " " + answerOptions[3, 1]);
        op1.GetComponent<TMPro.TextMeshProUGUI>().text = answerOptions[0, 0];
        op2.GetComponent<TMPro.TextMeshProUGUI>().text = answerOptions[1, 0];
        op3.GetComponent<TMPro.TextMeshProUGUI>().text = answerOptions[2, 0];
        op4.GetComponent<TMPro.TextMeshProUGUI>().text = answerOptions[3, 0];
        questionPanel.SetActive(true);

        if (answerOptions[2, 0].Equals("") && answerOptions[3, 0].Equals(""))
        {
            op3_Button.SetActive(false);
            op4_Button.SetActive(false);
        }
        else
        {
            op3_Button.SetActive(true);
            op4_Button.SetActive(true);
        }
    }


    public void answerButton()
    {
        if (EventSystem.current.currentSelectedGameObject.name == op1.name)
        {
            if (answerOptions[0, 1] == "Correct")
            {
                correct.Play();
                score++;
                numCorrect++;
                answerMessage.GetComponent<TMPro.TextMeshProUGUI>().color = new Color(255, 255, 255);
                answerMessage.GetComponent<TMPro.TextMeshProUGUI>().text = "CORRECT!";
                raceBar.UpdateProgress(1.0f);
            }

            else
            {
                incorrect.Play();
                numIncorrect++;
                answerMessage.GetComponent<TMPro.TextMeshProUGUI>().color = new Color(255, 255, 255);
                answerMessage.GetComponent<TMPro.TextMeshProUGUI>().text = "INCORRECT!";
            }
        }
        if (EventSystem.current.currentSelectedGameObject.name == op2.name)
        {
            if (answerOptions[1, 1] == "Correct")
            {
                correct.Play();
                score++;
                numCorrect++;
                answerMessage.GetComponent<TMPro.TextMeshProUGUI>().color = new Color(255, 255, 255);
                answerMessage.GetComponent<TMPro.TextMeshProUGUI>().text = "CORRECT!";
                raceBar.UpdateProgress(1.0f);
            }

            else
            {
                incorrect.Play();
                numIncorrect++;
                answerMessage.GetComponent<TMPro.TextMeshProUGUI>().color = new Color(255, 255, 255);
                answerMessage.GetComponent<TMPro.TextMeshProUGUI>().text = "INCORRECT!";
            }
        }
        if (EventSystem.current.currentSelectedGameObject.name == op3.name)
        {
            if (answerOptions[2, 1] == "Correct")
            {
                correct.Play();
                score++;
                numCorrect++;
                answerMessage.GetComponent<TMPro.TextMeshProUGUI>().color = new Color(255, 255, 255);
                answerMessage.GetComponent<TMPro.TextMeshProUGUI>().text = "CORRECT!";
                raceBar.UpdateProgress(1.0f);
            }

            else
            {
                incorrect.Play();
                numIncorrect++;
                answerMessage.GetComponent<TMPro.TextMeshProUGUI>().color = new Color(255, 255, 255);
                answerMessage.GetComponent<TMPro.TextMeshProUGUI>().text = "INCORRECT!";
            }
        }
        if (EventSystem.current.currentSelectedGameObject.name == op4.name)
        {
            if (answerOptions[3, 1] == "Correct")
            {
                correct.Play();
                score++;
                numCorrect++;
                answerMessage.GetComponent<TMPro.TextMeshProUGUI>().color = new Color(255, 255, 255);
                answerMessage.GetComponent<TMPro.TextMeshProUGUI>().text = "CORRECT!";
                raceBar.UpdateProgress(1.0f);
            }

            else
            {
                incorrect.Play();
                numIncorrect++;
                answerMessage.GetComponent<TMPro.TextMeshProUGUI>().color = new Color(255, 255, 255);
                answerMessage.GetComponent<TMPro.TextMeshProUGUI>().text = "INCORRECT!";
            }
        }

        if (score == 5)
        {
            SceneManager.LoadScene(4);

            string conn = "URI=file:" + Application.dataPath + "/Plugins/DMVIE.db";  //location of database
            IDbConnection dbconn; //object to connect to database
            dbconn = (IDbConnection)new SqliteConnection(conn); //database connection created 
            dbconn.Open(); //database connection made
            IDbCommand dbcmd = dbconn.CreateCommand(); //object to create commands for database

            string updateProgressQuery = "INSERT INTO usr_prog (username, quizcat, numIncorrect, numCorrect) VALUES (\"" + Login.userRef + "\", \"" + Main_Menu.quizType + "\", \"" + numIncorrect + "\", \"" + numCorrect + "\")";
            dbcmd.CommandText = updateProgressQuery;
            IDataReader reader = dbcmd.ExecuteReader();
            reader.Close();
        }

        if (numIncorrect == 5)
        {
            SceneManager.LoadScene(5);

            string conn = "URI=file:" + Application.dataPath + "/Plugins/DMVIE.db";  //location of database
            IDbConnection dbconn; //object to connect to database
            dbconn = (IDbConnection)new SqliteConnection(conn); //database connection created 
            dbconn.Open(); //database connection made
            IDbCommand dbcmd = dbconn.CreateCommand(); //object to create commands for database

            string updateProgressQuery = "INSERT INTO usr_prog (username, quizcat, numIncorrect, numCorrect) VALUES (\"" + Login.userRef + "\", \"" + Main_Menu.quizType + "\", \"" + numIncorrect + "\", \"" + numCorrect + "\")";
            dbcmd.CommandText = updateProgressQuery;
            IDataReader reader = dbcmd.ExecuteReader();
            reader.Close();
        }

        questionPanel.SetActive(false);
        answerMessage.SetActive(true);
        cardButton.SetActive(true);
    }

}
