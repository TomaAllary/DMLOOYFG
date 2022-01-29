using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;

public class KillSwitch : MonoBehaviour
{
    public GameObject buttonOn;
    public GameObject buttonOff;
    public AudioSource winAudio;
    public string nextScene;
    public int thisLvlIndex;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        buttonOff.SetActive(false);
        buttonOn.SetActive(true);

        winAudio.Play();
        StartCoroutine(NextScene());
    }

    IEnumerator NextScene() {
        if (nextScene != "") {
            //GameObject.Find("bg music").GetComponent<AudioSource>().volume = 0.1f;

            //add progress... 
            if (thisLvlIndex < 10)
                File.AppendAllText(Application.dataPath + "/SaveProgress.txt", (thisLvlIndex + 1).ToString(), Encoding.ASCII);
            yield return new WaitForSeconds(3);

            SceneManager.LoadScene(nextScene);
        }
    }
}
