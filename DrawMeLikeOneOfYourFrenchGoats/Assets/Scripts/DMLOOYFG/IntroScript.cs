using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class IntroScript : MonoBehaviour
{
    public GameObject panel1;
    public GameObject panel2;
    public GameObject panel3;
    public InputField tb;
    public GameObject launchButton;
    public List<string> dataReceived = new List<string>();

    private void Awake() {
        Server.stopServer();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void joinButton()
    {
        StaticInfoHolder.serverAddr = tb.text;
        StaticInfoHolder.isClient = true;

        if (ClientRequestSender.ConnectionTest() == System.Net.HttpStatusCode.OK)
        {
            SceneManager.LoadScene("lvl1");
        }
        else
        {
            tb.text = "";
            tb.placeholder.GetComponent<TextMeshProUGUI>().text = "Connection failed...";
        }
    }
    public void hostButton()
    {
        List<string> codes = new List<string>();
        foreach (string line in System.IO.File.ReadLines(@".\ngrokCodes.txt"))
        {
            codes.Add(line.Substring(1));
        }
        System.Random random = new System.Random();
        int num = random.Next(50);
        System.Threading.Thread.Sleep(1000);

        List<string> consoleReturn = new List<string>();
        string path = Application.dataPath;

        var t = new Thread(() => {
            Process console = new Process();
            console.StartInfo.FileName = "cmd.exe";
            console.StartInfo.UseShellExecute = false;
            //console.StartInfo.WindowStyle = ProcessWindowStyle.Normal;
            console.StartInfo.RedirectStandardInput = true;
            console.StartInfo.WindowStyle = ProcessWindowStyle.Minimized;
            console.StartInfo.RedirectStandardOutput = true;
            console.Start();
            console.StandardInput.WriteLine(path + codes[num]);
            console.StandardInput.WriteLine(path + "/ngrok.exe http http://localhost:3000 -host-header=\"localhost:3000\"");

        });
        t.Start();

        //console.WaitForExit();
        System.Threading.Thread.Sleep(1000);
        Process console2 = new Process();
        console2.StartInfo.FileName = "cmd.exe";
        console2.StartInfo.UseShellExecute = false;
        console2.StartInfo.RedirectStandardInput = true;
        console2.StartInfo.RedirectStandardOutput = true;
        console2.StartInfo.WindowStyle = ProcessWindowStyle.Minimized;
        console2.Start();
        console2.StandardInput.WriteLine("curl http://localhost:4040/api/tunnels");
        Thread t2 = new Thread(() =>
        {
            while (!console2.StandardOutput.EndOfStream)
            {
                dataReceived.Add(console2.StandardOutput.ReadLine());

            }
        });
        t2.Start();


        int k = 1;

        System.Threading.Thread.Sleep(1000);
        string wip = dataReceived[4];
        string[] wip2 = wip.Split('\"');
        int counter = 0;
        string tunnelAddress = "";
        foreach (string s in wip2)
        {
            if(wip2[counter] == "public_url")
            {
                tunnelAddress = wip2[counter + 2];
                break;
            }
            counter++;
        }
        tb.text = tunnelAddress;
        launchButton.SetActive(true);
        int j = 1;


    }

    public void launchClick()
    {
        StaticInfoHolder.isClient = false;
        SceneManager.LoadScene("LvlScene");
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
        System.Threading.Thread.Sleep(3000);
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
