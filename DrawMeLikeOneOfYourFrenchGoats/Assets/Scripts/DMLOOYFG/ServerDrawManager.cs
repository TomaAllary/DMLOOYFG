using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Threading;
using UnityEngine;

public class ServerDrawManager : MonoBehaviour
{

    private List<NetworkMsg> requests = new List<NetworkMsg>();
    private Mutex requestMutex = new Mutex();
    private Thread listen;

    private string dataPath;
    // Start is called before the first frame update
    void Start()
    {
        listen = new Thread(ListenToRequest);
        listen.Start();

        dataPath = Application.dataPath;
    }

    // Update is called once per frame
    void Update()
    {
        if(requests.Count > 0) {
            foreach(NetworkMsg req in requests) {

            }
        }
    }

    private void OnDestroy() {
        listen.Abort();
    }




    public void ListenToRequest() {
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

                requestMutex.WaitOne();
                requests.Add(msg);
                Debug.Log("msg from: " + msg.senderUUID);

                byte[] backToBytes = Convert.FromBase64String(msg.imageByteArray);
                File.WriteAllBytes(dataPath + "/ServerSideImg.png", backToBytes);

                requestMutex.ReleaseMutex();
            }





            // Obtain a response object.
            HttpListenerResponse response = context.Response;
            // Construct a response.
            string responseString = "action done";
            byte[] buffer = System.Text.Encoding.UTF8.GetBytes(responseString);
            // Get a response stream and write the response to it.
            response.ContentLength64 = buffer.Length;
            System.IO.Stream output = response.OutputStream;
            output.Write(buffer, 0, buffer.Length);
            // You must close the output stream.
            output.Close();
        }


        listener.Stop();

    }
}
