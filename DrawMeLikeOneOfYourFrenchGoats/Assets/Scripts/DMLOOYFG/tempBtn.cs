using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class tempBtn : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void MainMenu() {
        SceneManager.LoadScene("Intro");
    }

    public void host() {
        StaticInfoHolder.isClient = false;

        SceneManager.LoadScene("LvlScene");
    }

    public void client(TMP_InputField input) {
        StaticInfoHolder.serverAddr = input.text;
        StaticInfoHolder.isClient = true;

        if(ClientRequestSender.ConnectionTest() == System.Net.HttpStatusCode.OK) {
            SceneManager.LoadScene("lvl1");
        }
        else {
            input.text = "";
            input.placeholder.GetComponent<TextMeshProUGUI>().text = "Connection failed...";
        }
    }
}
