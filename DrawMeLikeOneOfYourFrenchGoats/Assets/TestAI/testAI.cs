using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testAI : MonoBehaviour
{
    List<GameObject> surroundingWeapons = new List<GameObject>();
    GameObject[] closestSurroundingWeapons;
    private GameObject player;

    public GameObject targetPrefab;

    private GameObject[] targets = new GameObject[5];

    // Start is called before the first frame update
    void Start() {
        targets[0] = Instantiate(targetPrefab);
        targets[1] = Instantiate(targetPrefab);
        targets[2] = Instantiate(targetPrefab);
        targets[3] = Instantiate(targetPrefab);
        targets[4] = Instantiate(targetPrefab);
        player = GameObject.FindGameObjectWithTag("Player");

    }

    void LateUpdate()
    {
        surroundingWeapons.Clear();
        surroundingWeapons.AddRange(GameObject.FindGameObjectsWithTag("weapon"));


        surroundingWeapons.Sort((GameObject c1, GameObject c2) => {
            float c1Dist = Mathf.Abs((c1.transform.position - player.transform.position).magnitude);
            float c2Dist = Mathf.Abs((c1.transform.position - player.transform.position).magnitude);
            if (c1Dist < c2Dist)
                return 1;
            else
                return -1;
        });

        for (int i = 0; i < targets.Length; i++) {
            if (i < surroundingWeapons.Count) {
                if (surroundingWeapons[i]) {
                    targets[i].transform.position = surroundingWeapons[i].transform.position;
                    targets[i].SetActive(true);
                }

            }
            else {
                targets[i].SetActive(false);
            }
        }
    }

}
