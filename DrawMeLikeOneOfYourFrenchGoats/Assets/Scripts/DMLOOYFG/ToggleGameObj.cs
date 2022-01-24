using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleGameObj : MonoBehaviour
{

    public GameObject obj;
    
    public void ToogleObj() {
        obj.SetActive(!obj.activeSelf);
    }

    public bool GetActive() {
        return obj.activeSelf;
    }
}
