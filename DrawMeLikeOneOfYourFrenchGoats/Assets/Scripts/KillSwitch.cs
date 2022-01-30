using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class KillSwitch : MonoBehaviour
{
    public GameObject buttonOn;
    public GameObject buttonOff;
    public AudioSource winAudio;
    public string nextScene;
    public int thisLvlIndex;

    public RawImage overlay;

    private Color overlayColor;
    private float fadeOut = 3;
    private bool hasWon = false;

    // Start is called before the first frame update
    void Start()
    {
        overlayColor = overlay.color;
    }

    private void Update() {
        if (hasWon) {
            if (fadeOut > 0) {
                fadeOut -= Time.deltaTime;
                overlayColor.a = 255;
                overlay.color = overlayColor;
            }

        }
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
            hasWon = true;
            //GameObject.Find("bg music").GetComponent<AudioSource>().volume = 0.1f;

            //add progress... 
            if (thisLvlIndex < 10)
                File.AppendAllText(Application.dataPath + "/SaveProgress.txt", (thisLvlIndex + 1).ToString(), Encoding.ASCII);
            yield return new WaitForSeconds(3.4f);

            SceneManager.LoadScene(nextScene);
        }
    }
}
