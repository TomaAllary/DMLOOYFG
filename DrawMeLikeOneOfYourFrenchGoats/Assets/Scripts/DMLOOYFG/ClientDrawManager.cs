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

    public string SendRequest(NetworkMsg toSend) {
        /*Thread t = new Thread(() => AsyncActionRequest(toSend));
        t.IsBackground = true;
        t.Start();*/
        return AsyncActionRequest(toSend);
    }


    //use as a thread only
    private string AsyncActionRequest(NetworkMsg request) {
        string result = "";

        Debug.Log("Begin request: " + request.msgType);
        var httpWebRequest = (HttpWebRequest)WebRequest.Create(serverAdrr);
        httpWebRequest.ContentType = "application/json";
        httpWebRequest.Method = "POST";

        using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream())) {
            string json = JsonUtility.ToJson(request);
            Debug.Log("json: " + json);
            streamWriter.Write(json);
        }

        var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
        using (var streamReader = new StreamReader(httpResponse.GetResponseStream())) {
            result = streamReader.ReadToEnd();
            Debug.Log("request result: " + result);
        }

        return result;
    }
}
