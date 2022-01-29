using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Threading;
using UnityEngine;

public class Server
{
    private static bool isListening = false;
    private static Thread listen;
    private static Func<NetworkMsg, NetworkMsg> handler; 

    public static void startServer() {
        if (!isListening) {
            listen = new Thread(ListenToRequest);
            listen.Start();

            isListening = true;
        }
    }

    public static void AddCallback(Func<NetworkMsg, NetworkMsg> handleRequests ) {
        handler = handleRequests;
    }

    public static void RemoveCallback() {
        handler = null;
    }

    public static void stopServer() {
        if (isListening) {
            listen.Abort();
            isListening = false;
        }
    }

    public static void ListenToRequest() {
        // Create a listener.
        HttpListener listener = new HttpListener();

        // Add the prefixes. //change for NGROK here
        listener.Prefixes.Add("http://localhost:3000/");

        listener.Start();
        Debug.Log("Listening...");


        while (true) {
            // Note: The GetContext method blocks while waiting for a request.
            HttpListenerContext context = listener.GetContext();
            HttpListenerRequest request = context.Request;

            NetworkMsg msg;

            using (StreamReader readStream = new StreamReader(request.InputStream, Encoding.UTF8)) {
                string received = readStream.ReadToEnd();
                msg = JsonUtility.FromJson<NetworkMsg>(received);
                NetworkMsg resToSend = new NetworkMsg(); ;

                if (handler != null && msg.msgType != "conn") {
                    resToSend = handler.Invoke(msg);
                }
                else if (msg.msgType == "conn" && StaticInfoHolder.onClientConnect != null) {
                    StaticInfoHolder.onClientConnect.Invoke();
                }

                // Obtain a response object.
                HttpListenerResponse response = context.Response;
                // Construct a response.
                string responseString = JsonUtility.ToJson(resToSend);


                byte[] buffer = System.Text.Encoding.UTF8.GetBytes(responseString);
                // Get a response stream and write the response to it.
                response.ContentLength64 = buffer.Length;
                System.IO.Stream output = response.OutputStream;
                output.Write(buffer, 0, buffer.Length);
                // You must close the output stream.
                output.Close();
            }
        }


        listener.Stop();

    }
}
