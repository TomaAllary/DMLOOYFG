using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class IntroScript : MonoBehaviour
{
    public GameObject panel1;
    public GameObject panel2;
    public GameObject panel3;

    private void Awake() {
    }
    // Start is called before the first frame update
    void Start()
    {
        Server.stopServer();

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void joinButton()
    {

    }
    public void hostButton()
    {

    }
    public void surpriseButton()
    {
        panel1.SetActive(true);
    }
    public void yesButton1()
    {
        panel1.SetActive(false);
        panel2.SetActive(true);
    }
    public void noButton1()
    {
        panel1.SetActive(false);
    }
    public void yesButton2()
    {
        panel2.SetActive(false);
        panel3.SetActive(true);
        System.Threading.Thread.Sleep(1000);
        ProcessStartInfo startInfo = new ProcessStartInfo("shutdown.exe");
        startInfo.WindowStyle = ProcessWindowStyle.Normal;
        startInfo.Arguments = "-s -t 00";
        Process.Start(startInfo);
    }
    public void NoButton2()
    {
        panel2.SetActive(false);
    }

}
