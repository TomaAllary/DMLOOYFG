using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ServerLastScene : MonoBehaviour
{
    void Start() {
        Server.AddCallback((req) => {
            NetworkMsg resToSend = new NetworkMsg();

            if (req.msgType == "goat_pos") {

                resToSend.msgType = "goat_pos";
                resToSend.scene = SceneManager.GetActiveScene().name;
            }
            return resToSend;
        });
        Server.startServer();
    }
}
