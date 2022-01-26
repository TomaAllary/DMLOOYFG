using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkMsg
{
    public string senderUUID;
    public string imageByteArray;

    public NetworkMsg() {
        senderUUID = System.Guid.NewGuid().ToString();
    }

    public NetworkMsg(string uuid) {
        senderUUID = uuid;
    }

}
