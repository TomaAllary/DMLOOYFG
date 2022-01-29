using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class ServerDrawManager : MonoBehaviour
{

    public RawImage boardToUpdate;
    public Transform goat;

    private Mutex posMutex = new Mutex();
    private float xPos;
    private float yPos;

    private List<NetworkMsg> requests = new List<NetworkMsg>();
    private Mutex requestMutex = new Mutex();
    // Start is called before the first frame update
    void Start()
    {
        Server.AddCallback((req) => {
            NetworkMsg resToSend = new NetworkMsg();

            if (req.msgType == "goat_pos") {

                resToSend.msgType = "goat_pos";
                posMutex.WaitOne();
                resToSend.goatX = (xPos).ToString();
                resToSend.goatY = (yPos).ToString();
                posMutex.ReleaseMutex();

            }
            else if (req.msgType == "lvl") {
                resToSend.msgType = "lvl loaded";

                requestMutex.WaitOne();
                requests.Add(req);
                requestMutex.ReleaseMutex();
            }
            else {
                resToSend.msgType = "image updated";

                requestMutex.WaitOne();
                requests.Add(req);
                requestMutex.ReleaseMutex();
            }

            return resToSend;
        });
        Server.startServer();
    }

    // Update is called once per frame
    void Update()
    {
        if(requests.Count > 0) {
            requestMutex.WaitOne();

            foreach (NetworkMsg req in requests) {
                if (req.msgType == "image_update") {
                    byte[] backToBytes = Convert.FromBase64String(req.imageByteArray);

                    Texture2D tex = new Texture2D(1, 1);
                    tex.LoadImage(backToBytes);

                    boardToUpdate.texture = tex;
                }
                else if(req.msgType == "lvl") {

                }
            }

            requests.Clear();
            requestMutex.ReleaseMutex();
        }
    }

    private void FixedUpdate() {
        posMutex.WaitOne();
        xPos = goat.position.x;
        yPos = goat.position.y;
        posMutex.ReleaseMutex();
    }

    private void OnDestroy() {
        //listen.Abort();
        Server.RemoveCallback();
    }
}
