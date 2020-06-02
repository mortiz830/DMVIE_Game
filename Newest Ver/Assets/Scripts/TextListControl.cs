using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Data;
using Mono.Data.Sqlite;

public class TextListControl : MonoBehaviour
{
    [SerializeField]
    private GameObject textFieldTemplate;

    private List<string> usrProgressReport = new List<string>();

    private void Start()
    {
        string conn = "URI=file:" + Application.dataPath + "/Plugins/DMVIE.db";  //location of database
        IDbConnection dbconn; //object to connect to database
        dbconn = (IDbConnection)new SqliteConnection(conn); //database connection created 
        dbconn.Open(); //database connection made
        IDbCommand dbcmd = dbconn.CreateCommand(); //object to create commands for database

        string userProgQuery = "SELECT quizcat, quizdt, numCorrect, numIncorrect FROM usr_prog WHERE username = \"" + Login.userRef + "\"" + "ORDER BY quizdt DESC";
        dbcmd.CommandText = userProgQuery;
        IDataReader reader = dbcmd.ExecuteReader();
        while (reader.Read())
        {
            string currentCat = reader.GetString(0);
            string currentDate = reader.GetString(1);
            string curCrct = reader.GetInt32(2).ToString();
            string curInc = reader.GetInt32(3).ToString();
            if (currentCat == "CDL")
            {
                usrProgressReport.Add(" " + currentCat + "    " + currentDate + " " + curCrct + " " + curInc);
                Debug.Log(currentCat + "   " + currentDate + " " + curCrct + " " + curInc);
            }
            else
            {
                usrProgressReport.Add(" " + currentCat + " " + currentDate + " " + curCrct + " " + curInc);
                Debug.Log(currentCat + " " + currentDate + " " + curCrct + " " + curInc);
            }
        }
        reader.Close();
        dbconn.Close();

        for(int i = 0; i < usrProgressReport.Count; i++)
        {
            GameObject text = Instantiate(textFieldTemplate) as GameObject;
            text.SetActive(true);

            text.GetComponent<TextListText>().SetText(usrProgressReport[i]);

            text.transform.SetParent(textFieldTemplate.transform.parent, false);
        }
    }

}
