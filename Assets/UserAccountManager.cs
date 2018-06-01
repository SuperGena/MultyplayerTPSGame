using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DatabaseControl;

public class UserAccountManager : MonoBehaviour {

    public static UserAccountManager instance;

    void Awake()
    {
        if(instance != null)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(this);
    }

    public static string LoggedIn_Username { get; protected set; }
    private static string LoggedIn_Password = "";
    public static string LoggedIn_Data { get; protected set; }

    public static bool IsLoggedIn { get; protected set; }

    public string loggedInSceneName = "Lobby";
    public string loggedOutSceneName = "LoginMenu";

    public delegate void OnDataReceivedCallback(string data);

    public void LogOut()
    {
        LoggedIn_Username = "";
        LoggedIn_Password = "";

        IsLoggedIn = false;

        Debug.Log("Log Out");

        SceneManager.LoadScene(loggedOutSceneName);
    }

    public void LogIn(string username, string password)
    {
        LoggedIn_Username = username;
        LoggedIn_Password = password;

        IsLoggedIn = true;

        Debug.Log("Log in as" + username);

        SceneManager.LoadScene(loggedInSceneName);
    }

    IEnumerator sendGetData(string username, string password, OnDataReceivedCallback OnDataReceived)
    {
        string data = "ERROR";

        IEnumerator e = DCF.GetUserData(username, password); // << Send request to get the player's data string. Provides the username and password
        while (e.MoveNext())
        {
            yield return e.Current;
        }
        string response = e.Current as string; // << The returned string from the request

        if (response == "Error")
        {
            Debug.Log("Error");
        }
        else
        {
            if (response == "ContainsUnsupportedSymbol")
            {
                Debug.Log("ContainsUnsupportedSymbol");

            }
            else
            {
                string DataRecieved = response;
                data = DataRecieved;
            }
        }

        if(OnDataReceived != null)
           OnDataReceived.Invoke(data);
    }

    IEnumerator sendSendData(string useername, string password, string data)
    {
        IEnumerator e = DCF.SetUserData(useername, password, data); // << Send request to set the player's data string. Provides the username, password and new data string
        while (e.MoveNext())
        {
            yield return e.Current;
        }
        string response = e.Current as string; // << The returned string from the request
        
    }

    public void SendData(string data)
    {
        if (IsLoggedIn)
        {
            StartCoroutine(sendSendData(LoggedIn_Username, LoggedIn_Password, data));
        }
    }

    public void GetData(OnDataReceivedCallback OnDataReceived)
    {
        if (IsLoggedIn)
        {
            StartCoroutine(sendGetData(LoggedIn_Username, LoggedIn_Password, OnDataReceived));
        }
    }

}
