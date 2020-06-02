using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using TMPro;
using System.Data;
using Mono.Data.Sqlite;
using System.IO;
using UnityEngine.SceneManagement;

public class Login : MonoBehaviour
{
    public GameObject username;
    public GameObject password;
    public GameObject popupText;
    private string Username;
    private string Password;
    private string storedName;
    private string savedPassword;
    private string[] Lines;
    public static string userRef;
    public static string firstName;
    public static string lastName;

    public void LoginButton()
    {
        userRef = Username;
        string conn = "URI=file:" + Application.dataPath + "/Plugins/DMVIE.db";  //location of database
        IDbConnection dbconn; //object to connect to database
        dbconn = (IDbConnection)new SqliteConnection(conn); //database connection created 
        dbconn.Open(); //database connection made
        IDbCommand dbcmd = dbconn.CreateCommand(); //object to create commands for database

        bool UN = false;
        bool PW = false;
        if (Username != "")
        {
            string userNameQuery = "SELECT username, f_name, l_name FROM User_tb WHERE username = \"" + Username + "\"";  //query to pull current username entered in text input from database
            dbcmd.CommandText = userNameQuery; //load query into command object 
            IDataReader reader = dbcmd.ExecuteReader(); //executing command
            while (reader.Read()) //read through query results
            {
                storedName = reader.GetString(0); //sets variable to the username that exists in the database which will have to match what was inputted 
                firstName = reader.GetString(1);
                lastName = reader.GetString(2);

                Debug.Log("Saved User Name is: " + storedName);
            }
            reader.Close();
            if (Username == storedName)
            {
                UN = true;
                Debug.Log("set to true");
            }
            else
            {
                UnityEngine.Debug.LogWarning("Username Invalid!");
                popupText.GetComponent<TMPro.TextMeshProUGUI>().text = "Invalid Username or Password";
                popupText.SetActive(true);
            }
        }
        else
        {
            UnityEngine.Debug.LogWarning("Username field is empty!");
            popupText.GetComponent<TMPro.TextMeshProUGUI>().text = "Enter Username and Password";
            popupText.SetActive(true);
        }

        if (Password != "")
        {
            string passwordQuery = "SELECT pword FROM User_tb WHERE username = \"" + Username + "\"";
            dbcmd.CommandText = passwordQuery;
            IDataReader reader = dbcmd.ExecuteReader();
            while (reader.Read())
            {
                savedPassword = reader.GetString(0);

                Debug.Log("Saved Password is: " + savedPassword);
            }
            reader.Close();
            if (Password == savedPassword)
            {
                PW = true;
            }
            else
            {
                UnityEngine.Debug.LogWarning("Password Invalid");
                popupText.GetComponent<TMPro.TextMeshProUGUI>().text = "Invalid Username or Password";
                popupText.SetActive(true);
            }
        }
        else
        {
            UnityEngine.Debug.LogWarning("Password field is empty");
            popupText.GetComponent<TMPro.TextMeshProUGUI>().text = "Enter Username and Password";
            popupText.SetActive(true);
        }

        if (UN == true && PW == true)
        {
            dbconn.Close();
            print("Login Successful");
            username.GetComponent<TMP_InputField>().text = "";
            password.GetComponent<TMP_InputField>().text = "";
            SceneManager.LoadScene(2);
        }

    }

    public void RegisterButton()
    {
        SceneManager.LoadScene(1);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (username.GetComponent<TMP_InputField>().isFocused)
            {
                password.GetComponent<TMP_InputField>().Select();
            }
            if (password.GetComponent<TMP_InputField>().isFocused)
            {
                username.GetComponent<TMP_InputField>().Select();
            }
        }

        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (Username != "" && Password != "")
            {
                LoginButton();
            }
            else
            {
                popupText.GetComponent<TMPro.TextMeshProUGUI>().text = "Enter Username and Password";
                popupText.SetActive(true);
            }
        }

        Username = username.GetComponent<TMP_InputField>().text;
        Password = password.GetComponent<TMP_InputField>().text;
    }
}