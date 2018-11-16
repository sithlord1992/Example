using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class SessionHandler : MonoBehaviour
{
    #region Variables
    public string current_date_time;
    public string last_logged_date_time;
    public string app_version;
    
    #endregion

    #region Server Links for the Data
    string _liveServerLink = "";
    string _dummyServerLink = "http://nukeboxstudios.com/nukebox/foodtruckchefgame/services/index1.php";
    #endregion

    [SerializeField]
    Dictionary<string, object> _responseStr = new Dictionary<string, object>();

    private void Start()
    {
        StartCoroutine(GetServerResponse());
    }

    /// <summary>
    /// Get server data and load to local instance
    /// </summary>
    /// <returns></returns>
    IEnumerator GetServerResponse()
    {
        var link = _dummyServerLink;
        UnityWebRequest serverResponse = UnityWebRequest.Get(link);

        yield return serverResponse.SendWebRequest();
        
        if (serverResponse.isNetworkError || serverResponse.isHttpError)
        {
            Debug.Log(serverResponse.error);
        }
        else
        {
            string str = File.ReadAllText(Application.dataPath + "/Resources/ServerData.json");
            JsonUtility.FromJsonOverwrite(str, this);

            //JsonUtility.FromJsonOverwrite(serverResponse.downloadHandler.text, this);

            SessionLogger();

            //Checking Application Version here
            if(VersionCheck(this.app_version, Application.version.ToString()).Equals(true))
            {

                //Download the latest version here or corresponding update
                Debug.Log("Download the latest available version here!");
            }

            //Check for every other update and download here
        }
    }

    /// <summary>
    /// Compares the versions of the feature on server and installed 
    /// </summary>
    /// <param name="serverVersion"></param>
    /// <param name="currentVersion"></param>
    /// <returns></returns>
    bool VersionCheck(string serverVersion, string currentVersion)
    {
        try
        {
            var app_ver = new System.Version(serverVersion);
            var curr_app_ver = new System.Version(currentVersion);

            var result = app_ver.CompareTo(curr_app_ver);
            if (result > 0)
                return true;
            else
                return false;
        }
        catch (Exception e) {
            Debug.LogException(e);
            return false;
        }
    }

    void SessionLogger() {
        if (_responseStr != null)
        {
            //Current server time
            //_dateTime = Convert.ToDateTime(_responseStr["current_date_time"].ToString());
        }
    }
}