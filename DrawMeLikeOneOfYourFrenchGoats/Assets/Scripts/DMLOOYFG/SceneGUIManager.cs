using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneGUIManager : MonoBehaviour
{

    public GameObject[] hostGUIList;
    public GameObject[] serverGUIList;


    // Start is called before the first frame update
    void Start()
    {
        if (StaticInfoHolder.isHost) {
            foreach(GameObject obj in hostGUIList) {
                obj.SetActive(true);
            }
        }
        else {
            foreach (GameObject obj in serverGUIList) {
                obj.SetActive(true);
            }
        }
    }

}
