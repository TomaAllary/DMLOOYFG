using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using UnityEngine;

public class IntroScript : MonoBehaviour
{
    public GameObject panel1;
    public GameObject panel2;
    public GameObject panel3;

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

    }
    public void hostButton()
    {
        List<string> codes = new List<string>();
        foreach (string line in System.IO.File.ReadLines(@".\ngrokCodes.txt"))
        {
            codes.Add(line.Substring(2));
        }
        System.Random random = new System.Random();
        int num = random.Next(50);
        System.Threading.Thread.Sleep(1000);
        Process console = new Process();
        List<string> consoleReturn = new List<string>();
        //string test = Application.dataPath;
        console.StartInfo.FileName = Application.dataPath + "/ngrok.exe";
        console.StartInfo.UseShellExecute = false;
        //console.StartInfo.WindowStyle = ProcessWindowStyle.Normal;
        console.StartInfo.RedirectStandardInput = true;
        console.StartInfo.RedirectStandardOutput = true;
        //console.StartInfo.RedirectStandardError = true;
        //console.StartInfo.Arguments = codes[num].Substring(6);
        console.StartInfo.Arguments = "http http://localhost:3000 -host-header=\"localhost:3000\"";
        //console.OutputDataReceived += (s, e) => consoleReturn.Add(e.Data);
        console.Start();
        //console.ErrorDataReceived += (s, e) => consoleReturn.Add(e.Data);
        //console.BeginOutputReadLine();
        string test = console.StandardOutput.ReadToEnd();
        //console.BeginErrorReadLine();
        //StreamWriter consoleWriter = console.StandardInput;
        //consoleWriter.WriteLine(codes[num]);
        //consoleWriter.WriteLine("ngrok http http://localhost:3000 -host-header=\"localhost:3000\"");
        //console.WaitForExit();
        System.Threading.Thread.Sleep(2000);
        int i = 1;


        /*Process console2 = new Process();
        console2.StartInfo.FileName = "cmd.exe";
        console2.StartInfo.UseShellExecute = false;
        //console.StartInfo.WindowStyle = ProcessWindowStyle.Normal;
        console2.StartInfo.RedirectStandardInput = true;
        console2.StartInfo.RedirectStandardOutput = true;
        console2.Start();
        while (!console2.StandardOutput.EndOfStream)
        {
            string line = console2.StandardOutput.ReadLine();
            ;
        }
            System.Threading.Thread.Sleep(1000);
        StreamWriter consoleWriter2 = console2.StandardInput;
        consoleWriter2.WriteLine("ngrok http http://localhost:3000 -host-header=\"localhost:3000\"");

        List<string> mylist = new List<string>();
        while (!console2.StandardOutput.EndOfStream)
        {
            string line = console2.StandardOutput.ReadLine();
            mylist.Add(line);
            // do something with line
        }
        console2.CloseMainWindow();
        console2.Close();
        int i = 1;*/
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
