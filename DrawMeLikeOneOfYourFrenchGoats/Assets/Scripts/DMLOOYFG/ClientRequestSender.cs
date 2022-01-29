using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading;
using UnityEngine;

public class ClientRequestSender
{
    //ngrok http http://localhost:3000 -host-header="localhost:3000" 
    public static string SendRequest(NetworkMsg toSend) {
       /* Thread t = new Thread(() => AsyncActionRequest(toSend));
        Thread timeout = new Thread(() => AsyncThreadTimeout(t, 10));
        timeout.Start();*/

        return AsyncActionRequest(toSend);

        /*NetworkMsg resToSend = new NetworkMsg();
        resToSend.msgType = "test1212";

        return JsonUtility.ToJson(resToSend);*/
    }

    private static void AsyncThreadTimeout(Thread toTimeout, double timeout) {
        toTimeout.Start();
        if (!toTimeout.Join(TimeSpan.FromSeconds(timeout)))
            toTimeout.Abort();
    }


    //use as a thread only
    private static string AsyncActionRequest(NetworkMsg request) {
        string result = "";

        Debug.Log("Begin request: " + request.msgType);
        var httpWebRequest = (HttpWebRequest)WebRequest.Create(StaticInfoHolder.serverAddr);
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
