using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Data;
using Mono.Data.Sqlite;
using System.IO;

public class SQLiteUser : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        string conn = "URI=file:" + Application.dataPath + "/Plugins/DMVIE.db";  //connection to database
        IDbConnection dbconn;
        dbconn = (IDbConnection)new SqliteConnection(conn);
        dbconn.Open();
        IDbCommand dbcmd = dbconn.CreateCommand();
        string sqlQuery = "SELECT * " + "FROM User_tb";
        dbcmd.CommandText = sqlQuery;
        IDataReader reader = dbcmd.ExecuteReader();
        while (reader.Read())
        {
            int id = reader.GetInt32(0);
            string fName = reader.GetString(1);
            string lName = reader.GetString(2);
            string userName = reader.GetString(3);
            string pword = reader.GetString(4);

            Debug.Log("id = " + id + " First Name = " + fName + " Last Name = " + lName + " User Name = " + userName + "password = " + pword);
        }
        reader.Close();
        string questQuery = "SELECT * FROM DMVIE_QUESTION";
        dbcmd.CommandText = questQuery;
        reader = dbcmd.ExecuteReader();
        while(reader.Read())
        {
            string question = reader.GetString(0);
            string category = reader.GetString(1);

            Debug.Log("question: " + question + " cat: " + category);
        }

        dbconn.Close();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
