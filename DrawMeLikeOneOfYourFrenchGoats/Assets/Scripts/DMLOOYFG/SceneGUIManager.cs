using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneGUIManager : MonoBehaviour
{

    public GameObject[] clientGUIList;
    public GameObject[] serverGUIList;
    public GameObject goat;


    // Start is called before the first frame update
    void Start() {
        if (StaticInfoHolder.isClient) {
            foreach (GameObject obj in clientGUIList) {
                obj.SetActive(true);
            }
            goat.GetComponent<PlayerMovement>().enabled = false;
        }
        else {
            foreach (GameObject obj in serverGUIList) {
                obj.SetActive(true);
            }
            goat.GetComponent<PlayerMovement>().enabled = true;
        }
    }

}
