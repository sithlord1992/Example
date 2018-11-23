using System.IO;
using System.Collections;
using UnityEngine;
using System;

#region Profile Layout
[Serializable]
public class Profile
{
    public data data = new data();
    public meta meta = new meta();
}
[Serializable]
public class data
{
    public string id;
    public string username;
    public string profile_picture;
    public string full_name;
    public string bio;
    public string website;
    public bool is_business;
    public counts counts;
}
[Serializable]
public class counts
{
    public int media;
    public int follows;
    public int followed_by;
}
[Serializable]
public class meta
{
    public int code;
}
#endregion

public class ImportData : MonoBehaviour
{
    public Profile _profile = new Profile();

    private void Start()
    {
        StartCoroutine(GetData());
    }

    IEnumerator GetData()
    {
        try
        {
            _profile.data.id = "0978986897";
            _profile.data.username = "goodquote";
            _profile.data.profile_picture = "12345123";
            _profile.data.full_name = "Good Quote";
            _profile.data.bio = "";
            _profile.data.website = "";
            _profile.data.is_business = false;
            _profile.data.counts.media = 24;
            _profile.data.counts.follows = 2454;
            _profile.data.counts.followed_by = 234;

            _profile.meta.code = 502;
            string jsonString = JsonUtility.ToJson(_profile);

            File.WriteAllText(Application.dataPath + "/Resources/DataNew.json", jsonString);
            Debug.Log("Json String : " + jsonString);
        }
        catch (Exception ex)
        {
            Debug.LogException(ex);
        }
        yield return new WaitForEndOfFrame();
    }
}
