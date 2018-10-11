using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;

public class DataManager : MonoBehaviour {

    private static readonly string PostURL = "https://us-central1-huddle-team.cloudfunctions.net/api/memory/caiomarciocc@gmail.com";
    public bool createJSONFile;

    public void CreateJSON ()
    {
        string _playerName = MenuManager.instance.playerNameText.text;
        int _playerScore = GameManager.instance.playerScore;

        PlayerData playerData = new PlayerData();
        playerData.playerName = _playerName;
        playerData.playerScore = _playerScore;

        string json = JsonUtility.ToJson(playerData);

        if (createJSONFile)
        {
            var file = File.CreateText(Application.dataPath + "/" + "playerData.json");
            file.WriteLine(json);
            file.Close();
        }

        StartCoroutine(Post(PostURL,json));
    }

    IEnumerator Post(string url, string bodyJsonString)
    {
        var request = new UnityWebRequest(url, "POST");
        byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(bodyJsonString);
        request.uploadHandler = (UploadHandler) new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = (DownloadHandler) new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");

        yield return request.Send();

        Debug.Log ("Status Code: " + request.responseCode);

        string _response = "" + request.responseCode + "";

        if (_response == "200")
        {
            MenuManager.instance.SentResult();
        }

    }
}
