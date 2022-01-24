using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextScene : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(nextScene());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator nextScene() {
        yield return new WaitForSeconds(26f);

        SceneManager.LoadScene("menu");
    }
}
