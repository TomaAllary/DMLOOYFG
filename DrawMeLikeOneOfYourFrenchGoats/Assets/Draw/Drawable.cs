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
    public Object updatableDrawImgAsset;

    LineRenderer currentLineRenderer;

    Vector2 lastPos;



    private void Update() {
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
            PointToMousePos();
        }
        else if(Input.GetKeyUp(KeyCode.Mouse0)){
            Save();
        }
        else {
            currentLineRenderer = null;
        }
    }

    void CreateBrush() {
        GameObject brushInstance = Instantiate(brush);
        currentLineRenderer = brushInstance.GetComponent<LineRenderer>();

        //because you gotta have 2 points to start a line renderer, 
        Vector2 mousePos = m_camera.ScreenToWorldPoint(Input.mousePosition);

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
        if (lastPos != mousePos) {
            AddAPoint(mousePos);
            lastPos = mousePos;
        }
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
        var data = texture2D.EncodeToPNG();
        File.WriteAllBytes(Application.dataPath + "/ActualDraw.png", data);

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
