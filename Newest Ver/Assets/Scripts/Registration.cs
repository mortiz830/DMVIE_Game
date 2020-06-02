using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Diagnostics;
using System.Data;
using Mono.Data.Sqlite;
using System.IO;
using UnityEngine.SceneManagement;

public class Registration : MonoBehaviour
{
    public GameObject firstname;
    public GameObject lastname;
    public GameObject username;
    public GameObject password;
    public GameObject regPopUp;
    private string Firstname;
    private string Lastname;
    private string Username;
    private string Password;
    private string form;

    public void RegistrationButton()
    {
        bool FN = false;
        bool LN = false;
        bool UN = false;
        bool PW = false;

        if (Firstname != "")
        {
            FN = true;
        }
        else
        {
            UnityEngine.Debug.LogWarning("Firstname field is empty!");
            regPopUp.GetComponent<TMPro.TextMeshProUGUI>().text = "Please fill out all fields";
            regPopUp.SetActive(true);
        }

        if (Lastname != "")
        {
            LN = true;
        }
        else
        {
            UnityEngine.Debug.LogWarning("Lastname field is empty!");
            regPopUp.GetComponent<TMPro.TextMeshProUGUI>().text = "Please fill out all fields";
            regPopUp.SetActive(true);
        }
        if (Username != "")
        {
            List<string> userCheck = new List<string>();//this string list will hold usernames to check for duplicates

            string conn = "URI=file:" + Application.dataPath + "/Plugins/DMVIE.db";  //location of database
            IDbConnection dbconn; //object to connect to database
            dbconn = (IDbConnection)new SqliteConnection(conn); //database connection created 
            dbconn.Open(); //database connection made
            IDbCommand dbcmd = dbconn.CreateCommand(); //object to create commands for database
            string readUsersQuery = "SELECT username FROM User_tb"; //pull username list from database
            dbcmd.CommandText = readUsersQuery;
            IDataReader reader = dbcmd.ExecuteReader();
            while (reader.Read())
            {
                string curUser = reader.GetString(0);
                userCheck.Add(curUser);
            }

            if (userCheck.Contains(Username))
            {
                UnityEngine.Debug.LogWarning("This username already exists, enter a different username");
                regPopUp.GetComponent<TMPro.TextMeshProUGUI>().text = "Duplicate Username, enter a different Username";
                regPopUp.SetActive(true);
            }
            else
            {
                UN = true;
            }
        }
        else
        {
            UnityEngine.Debug.LogWarning("Username field is empty!");
            regPopUp.GetComponent<TMPro.TextMeshProUGUI>().text = "Please fill out all fields";
            regPopUp.SetActive(true);
        }

        if (Password != "")
        {
            if (Password.Length > 5)
            {
                PW = true;
            }
            else
            {
                UnityEngine.Debug.LogWarning("Password must be greater than 6 characters!");
                regPopUp.GetComponent<TMPro.TextMeshProUGUI>().text = "Password must be greater that 6 characters";
                regPopUp.SetActive(true);
            }
        }
        else
        {
            UnityEngine.Debug.LogWarning("Password field is empty!");
            regPopUp.GetComponent<TMPro.TextMeshProUGUI>().text = "Please fill out all fields";
            regPopUp.SetActive(true);
        }




        if (FN == true && LN == true && UN == true && PW == true)
        {
            string conn = "URI=file:" + Application.dataPath + "/Plugins/DMVIE.db";  //location of database
            IDbConnection dbconn; //object to connect to database
            dbconn = (IDbConnection)new SqliteConnection(conn); //database connection created 
            dbconn.Open(); //database connection made
            IDbCommand dbcmd = dbconn.CreateCommand(); //object to create commands for database

            string insertQuery = "INSERT INTO User_tb (f_name, l_name, username, pword) VALUES (\"" + Firstname + "\",\"" + Lastname + "\",\"" + Username + "\",\"" + Password + "\")";
            dbcmd.CommandText = insertQuery;
            IDataReader reader = dbcmd.ExecuteReader();
            reader.Close();

            print("Registration Successful");
            SceneManager.LoadScene(0);
        }
    }

    public void BackButton()
    {
        SceneManager.LoadScene(0);
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (firstname.GetComponent<TMP_InputField>().isFocused)
            {
                lastname.GetComponent<TMP_InputField>().Select();
            }
            if (lastname.GetComponent<TMP_InputField>().isFocused)
            {
                username.GetComponent<TMP_InputField>().Select();
            }
            if (username.GetComponent<TMP_InputField>().isFocused)
            {
                password.GetComponent<TMP_InputField>().Select();
            }
            if (password.GetComponent<TMP_InputField>().isFocused)
            {
                firstname.GetComponent<TMP_InputField>().Select();
            }
        }

        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (Firstname != "" && Lastname != "" && Username != "" && Password != "")
            {
                RegistrationButton();
            }
            else
            {
                regPopUp.GetComponent<TMPro.TextMeshProUGUI>().text = "Please fill out all fields";
                regPopUp.SetActive(true);
            }
        }

        Firstname = firstname.GetComponent<TMP_InputField>().text;
        Lastname = lastname.GetComponent<TMP_InputField>().text;
        Username = username.GetComponent<TMP_InputField>().text;
        Password = password.GetComponent<TMP_InputField>().text;
    }
}
