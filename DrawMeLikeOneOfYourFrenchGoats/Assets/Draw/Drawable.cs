using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class Drawable : MonoBehaviour
{
    public Camera m_camera;
    public GameObject brush;

    public RenderTexture rt;

    public GameObject drawPanelObj;

    LineRenderer currentLineRenderer;

    Vector2 lastPos;

    //saving frequence when drawing
    private float saveFrequence = 2.5f;

    private ClientDrawManager client = new ClientDrawManager("http://localhost:3000");
    private string myUUId = "test";

    private void Update() {
        if(drawPanelObj.activeSelf)
            Drawing();
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

    void CreateBrush() {
        Vector2 mousePos = m_camera.ScreenToWorldPoint(Input.mousePosition);
        //hardcode point limit:
        if (mousePos.x < -7.8 || mousePos.x > 17.12 || mousePos.y < -9.1 || mousePos.y > 4.8) {
            currentLineRenderer = null;
            return;
        }

        GameObject brushInstance = Instantiate(brush);
        currentLineRenderer = brushInstance.GetComponent<LineRenderer>();

        //because you gotta have 2 points to start a line renderer, 
        currentLineRenderer.SetPosition(0, mousePos);
        currentLineRenderer.SetPosition(1, mousePos);

    }

    void AddAPoint(Vector2 pointPos) {
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
            AddAPoint(mousePos);
            lastPos = mousePos;
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
        //byte[] backToBytes = Base64.decodeBase64(base64String);
        NetworkMsg toSend = new NetworkMsg(myUUId);
        toSend.imageByteArray = base64String;

        client.SendRequest(toSend);

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
