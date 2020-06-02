using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Data;
using Mono.Data.Sqlite;

public class Main_Menu : MonoBehaviour {

    public static string quizType;
    public GameObject firstLastNameText;
    public GameObject userNameText;
    private List<string> usrProgressReport = new List<string>();

    public void Start()
    {
        userNameText.GetComponent<TMPro.TextMeshProUGUI>().text = Login.userRef;
        firstLastNameText.GetComponent<TMPro.TextMeshProUGUI>().text = Login.firstName + " " + Login.lastName;
    }

    public void PlayCDLGame ()
    {
        quizType = "CDL";
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void PlayPermitGame()
    {
        quizType = "Permit";
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void QuitGame()
    {
        Debug.Log ("Quit");
        Application.Quit();
    }
    public void Logout()
    {
        SceneManager.LoadScene(0);
        Debug.Log("UserName Was: " + Login.userRef);
    }

}
