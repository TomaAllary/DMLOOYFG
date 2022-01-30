using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LvlSelector : MonoBehaviour
{
    private string progress;
    public TextMeshProUGUI waitingLb;
    public Button startBtn;
    public List<GameObject> lvlButtons = new List<GameObject>();
    private string selectedLvl = "lvl1";

    private void Awake() {
        Server.startServer();
        if (File.Exists(Application.dataPath + "/SaveProgress.txt"))
            progress = File.ReadAllText(Application.dataPath + "/SaveProgress.txt", Encoding.ASCII);
        else
            progress = "";
        if (!progress.Contains("1"))
            progress += "1";

        startBtn.enabled = false;
        StaticInfoHolder.onClientConnect += handleClientConnect;
    }

    private void handleClientConnect() {
        startBtn.enabled = true; 
        waitingLb.text = "A player is connected !";
    }
    // Start is called before the first frame update
    void Start()
    {
        
        foreach(GameObject b in lvlButtons) {
            Button btn = b.GetComponent<Button>();
            if (b.tag == "1") {
                btn.enabled = true;
                Image img = b.GetComponent<Image>();
                img.color = Color.white;

                btn.Select();
            }
            else if (progress.Contains(b.tag)) {
                btn.enabled = true;
                Image img = b.GetComponent<Image>();
                img.color = Color.white;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDestroy() {
        StaticInfoHolder.onClientConnect -= handleClientConnect;
        File.WriteAllText(Application.dataPath + "/SaveProgress.txt", progress, Encoding.ASCII);
    }

    public void setLevel(string scene) {
        selectedLvl = scene;
        startBtn.GetComponentInChildren<TextMeshProUGUI>().text = "Start " + scene;
    }

    public void startLevel() {
        if (!StaticInfoHolder.isClient) {
            Server.startServer();
            SceneManager.LoadScene(selectedLvl);
        }

    }


}
