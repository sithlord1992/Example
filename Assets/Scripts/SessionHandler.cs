using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using MiniJSON;

public class SessionHandler : MonoBehaviour
{
    #region Server Data Variables

    public string current_date_time;
    public string last_logged_date_time;
    public string app_version;
    public string s_audio_package_ver;
    public string s_graphic_package_ver;
    public string s_source_package_ver;
    #endregion

    #region Module Info Variables

    public string l_audio_package_ver;
    public string l_graphic_package_ver;
    public string l_source_package_ver;

    #endregion

    #region Server Links for the Data
    string _liveServerLink = "";
    string _dummyServerLink = "http://nukeboxstudios.com/nukebox/foodtruckchefgame/services/index1.php";
    #endregion

    [SerializeField]
    Dictionary<string, object> s_response = new Dictionary<string, object>();
    [SerializeField]
    Dictionary<string, object> l_response = new Dictionary<string, object>();

    string s_path = "/Resources/ServerData.json";
    string l_path = "/Resources/ModulesInf.json";

    private void Start()
    {
        StartCoroutine(GetServerResponse(s_path));
    }

    /// <summary>
    /// Get server data and load to local instance
    /// </summary>
    /// <returns></returns>
    IEnumerator GetServerResponse(string path)
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
            string s_str = File.ReadAllText(Application.dataPath + path);
            JsonUtility.FromJsonOverwrite( s_str, this);

            string l_str = File.ReadAllText(Application.dataPath + l_path);
            JsonUtility.FromJsonOverwrite(l_str, this);
            
            //JsonUtility.FromJsonOverwrite(serverResponse.downloadHandler.text, this);

            SessionLogger();

           

            //Check for every other update and download here
            StartCoroutine(ModuleObserver());
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
        if (s_response != null)
        {
            //Current server time
            //_dateTime = Convert.ToDateTime(_response["current_date_time"].ToString());
        }
    }

    IEnumerator ModuleObserver() {
        //Checking Application Version here
        if (VersionCheck(this.app_version, Application.version.ToString()).Equals(true))
        {
            //Download the latest version here or corresponding update
            Debug.Log("Download the latest package available here!");
        }
        yield return new WaitForEndOfFrame();

        //Demo packages
        if (VersionCheck(this.s_audio_package_ver, this.l_audio_package_ver).Equals(true))
        {
            //Download the latest version here or corresponding update
            //Updating the local inventory also
            Debug.Log("Download the latest audio package ver "+ this.s_audio_package_ver + " available here!");
        }
        yield return new WaitForEndOfFrame();
        if (VersionCheck(this.s_graphic_package_ver, this.l_graphic_package_ver).Equals(true))
        {
            //Download the latest version here or corresponding update
            //Updating the local inventory also
            Debug.Log("Download the latest graphics package ver "+ this.s_graphic_package_ver + " available here!");
        }
        yield return new WaitForEndOfFrame();
        if (VersionCheck(this.s_source_package_ver, this.l_source_package_ver).Equals(true))
        {
            //Download the latest version here or corresponding update
            //Updating the local inventory also
            Debug.Log("Download the latest source package ver " + this.s_source_package_ver + " available here!");
        }
        yield return new WaitForEndOfFrame();
    }
}