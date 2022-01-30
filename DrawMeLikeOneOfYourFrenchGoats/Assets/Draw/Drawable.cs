using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Drawable : MonoBehaviour
{
    public Camera m_camera;
    public Transform goatPos;
    public GameObject brush;
    public GameObject eraserBrush;

    private GameObject currentBrush;
    public RenderTexture rt;
    public GameObject drawPanelObj;

    LineRenderer currentLineRenderer;
    Vector2 lastPos;

    //saving frequence when drawing
    private float saveFrequence = 2.5f;

    //saving frequence when drawing
    private float posUpdateFrequence = 0.5f;

    private void Start() {
        currentBrush = brush;
    }

    private void Update() {
        if(drawPanelObj.activeSelf)
            Drawing();

        if (posUpdateFrequence < 0) {

            RefreshGoatPos();
            posUpdateFrequence = 0.5f;
        }
        else {
            posUpdateFrequence -= Time.deltaTime;
        }
    }

    private void OnDestroy() {
        ResetTexture();
    }


    void Drawing() {
        if (Input.GetKeyDown(KeyCode.Mouse0)) {
            CreateBrush();
        }
        else if (Input.GetKey(KeyCode.Mouse0)) {
            if (currentLineRenderer == null)
                CreateBrush();
            else
                PointToMousePos();

            if(saveFrequence < 0) {
                Save();
                saveFrequence = 2.5f;
            }
            else {
                saveFrequence -= Time.deltaTime;
            }
        }
        else if(Input.GetKeyUp(KeyCode.Mouse0)){
            Save();
        }
        else {
            currentLineRenderer = null;
        }
    }

    public void toggleEraserPencil() {
        if (currentBrush == brush)
            currentBrush = eraserBrush;
        else
            currentBrush = brush;
    }

    void CreateBrush() {
        Vector2 mousePos = m_camera.ScreenToWorldPoint(Input.mousePosition);
        //hardcode point limit:
        if (mousePos.x < -7.8 || mousePos.x > 17.12 || mousePos.y < -9.1 || mousePos.y > 4.8) {
            currentLineRenderer = null;
            return;
        }

        GameObject brushInstance = Instantiate(currentBrush);
        currentLineRenderer = brushInstance.GetComponent<LineRenderer>();

        if (currentBrush == eraserBrush) {
            Vector3 pos = new Vector3(mousePos.x, mousePos.y, -1);

            //because you gotta have 2 points to start a line renderer, 
            currentLineRenderer.SetPosition(0, pos);
            currentLineRenderer.SetPosition(1, pos);
        }
        else {
            //because you gotta have 2 points to start a line renderer, 
            currentLineRenderer.SetPosition(0, mousePos);
            currentLineRenderer.SetPosition(1, mousePos);
        }
        

    }

    void AddAPoint(Vector3 pointPos) {
        currentLineRenderer.positionCount++;
        int positionIndex = currentLineRenderer.positionCount - 1;
        currentLineRenderer.SetPosition(positionIndex, pointPos);
    }

    void PointToMousePos() {
        Vector2 mousePos = m_camera.ScreenToWorldPoint(Input.mousePosition);

        //hardcode point limit:
        if (mousePos.x < -7.8 || mousePos.x > 17.12 || mousePos.y < -9.1 || mousePos.y > 4.8) {
            currentLineRenderer = null;
            return;
        }

        if (lastPos != mousePos) {
            if (currentBrush == eraserBrush) {
                Vector3 pos = new Vector3(mousePos.x, mousePos.y, -1);
                AddAPoint(pos);
                lastPos = mousePos;
            }
            else {
                AddAPoint(mousePos);
                lastPos = mousePos;
            }

        }
    }

    public void Clear() {
        foreach(GameObject line in GameObject.FindGameObjectsWithTag("Ink")) {
            Destroy(line);
        }

        Save();
    }

    public void Save() {
        StartCoroutine(CoSave());
    }

    public void RefreshGoatPos() {
        StartCoroutine(CoRefreshPos());
    }

    private IEnumerator CoSave() {
        //wait for rendering
        yield return new WaitForEndOfFrame();
        Debug.Log(Application.dataPath + "/ActualDraw.png");

        //set active texture
        RenderTexture.active = rt;

        //convert rendering texture to texture2D
        var texture2D = new Texture2D(rt.width, rt.height);
        texture2D.ReadPixels(new Rect(0, 0, rt.width, rt.height), 0, 0);
        texture2D.Apply();

        //write data to file
        byte[] data = texture2D.EncodeToPNG();
        File.WriteAllBytes(Application.dataPath + "/ActualDraw.png", data);


        //update drawing with http request..

        string base64String = Convert.ToBase64String(data);
        NetworkMsg toSend = new NetworkMsg();
        toSend.msgType = "image_update";
        toSend.imageByteArray = base64String;

        ClientRequestSender.SendRequest(toSend);

    }

    private IEnumerator CoRefreshPos() {
        NetworkMsg req = new NetworkMsg();
        req.msgType = "goat_pos";

        string rawResponse = ClientRequestSender.SendRequest(req);
        if (rawResponse != "error") {

            NetworkMsg response = JsonUtility.FromJson<NetworkMsg>(rawResponse);
            if (response.msgType == "goat_pos") {
                if (response.scene != SceneManager.GetActiveScene().name) {
                    SceneManager.LoadScene(response.scene);
                }
                else {
                    float x = float.Parse(response.goatX);
                    float y = float.Parse(response.goatY);
                    goatPos.position = new Vector3(x, y, goatPos.position.z);
                }
                
            }
        }
        yield return 0;
    }

    public void ResetTexture() {
        var texture2D = new Texture2D(rt.width, rt.height);

        Color32 resetColor = new Color32(255, 255, 255, 255);
        Color32[] resetColorArray = texture2D.GetPixels32();

        for (int i = 0; i < resetColorArray.Length; i++) {
            resetColorArray[i] = resetColor;
        }

        texture2D.SetPixels32(resetColorArray);
        texture2D.Apply();

        var data = texture2D.EncodeToPNG();
        File.WriteAllBytes(Application.dataPath + "/ActualDraw.png", data);
    }

}
