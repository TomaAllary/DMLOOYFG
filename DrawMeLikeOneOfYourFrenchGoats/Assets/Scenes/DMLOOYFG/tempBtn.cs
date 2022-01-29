using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class tempBtn : MonoBehaviour
{
    public InputField input;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setServerAddr() {
        StaticInfoHolder.serverAddr = input.text;
    }

    public void setIsClient(bool client) {
        StaticInfoHolder.isClient = client;

        SceneManager.LoadScene("BaseScene");
    }
}
