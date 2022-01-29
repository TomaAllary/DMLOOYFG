using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LvlSelector : MonoBehaviour
{
    private string progress;
    public List<GameObject> lvlButtons = new List<GameObject>();

    private void Awake() {
        progress = File.ReadAllText(Application.dataPath + "/SaveProgress.txt", Encoding.ASCII);
    }
    // Start is called before the first frame update
    void Start()
    {
        
        foreach(GameObject b in lvlButtons) {
            if (progress.Contains(b.tag)) {
                Button btn = b.GetComponent<Button>();
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

        File.WriteAllText(Application.dataPath + "/SaveProgress.txt", progress, Encoding.ASCII);
    }

    public void startLVL(int lvl) {
       if (StaticInfoHolder.isClient) {
            NetworkMsg toSend = new NetworkMsg();
            toSend.msgType = "lvl";
            toSend.lvl = lvl.ToString();

            string response = ClientRequestSender.SendRequest(toSend);
            if(response == "lvl loaded") {
                //SceneManager.LoadScene("");
            }
        }
    }


}
