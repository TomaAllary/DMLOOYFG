using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneGUIManager : MonoBehaviour
{

    public GameObject[] clientGUIList;
    public GameObject[] serverGUIList;
    public Behaviour[] serverComponents;
    public Behaviour[] clientComponents;


    // Start is called before the first frame update
    void Start() {
        if (StaticInfoHolder.isClient) {
            foreach (GameObject obj in clientGUIList) {
                obj.SetActive(true);
            }
            foreach (Behaviour c in clientComponents) {
                c.enabled = true;
            }
        }
        else {
            foreach (GameObject obj in serverGUIList) {
                obj.SetActive(true);
                foreach (Behaviour c in serverComponents) {
                    c.enabled = true;
                }
            }
        }
    }

}
