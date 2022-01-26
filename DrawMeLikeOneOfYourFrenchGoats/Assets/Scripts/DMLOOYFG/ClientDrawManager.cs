using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading;
using UnityEngine;

public class ClientDrawManager
{
    private string myUUID;
    private string serverAdrr;

    public ClientDrawManager(string serverAdrr) {
        this.serverAdrr = serverAdrr;
        myUUID = "";
    }

    public void SendRequest(NetworkMsg toSend) {
        Thread t = new Thread(() => AsyncActionRequest(toSend));
        t.IsBackground = true;
        t.Start();
    }


    //use as a thread only
    private void AsyncActionRequest(NetworkMsg request) {
        var httpWebRequest = (HttpWebRequest)WebRequest.Create(serverAdrr);
        httpWebRequest.ContentType = "application/json";
        httpWebRequest.Method = "POST";

        using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream())) {
            string json = JsonUtility.ToJson(request);

            streamWriter.Write(json);
        }

        var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
        using (var streamReader = new StreamReader(httpResponse.GetResponseStream())) {
            var result = streamReader.ReadToEnd();
            Debug.Log("request result: " + result);
        }
    }
}
