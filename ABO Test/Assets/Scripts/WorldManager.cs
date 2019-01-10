using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WorldManager : MonoBehaviour
{
    public GameObject mainCamParent;
    public Camera mainCam;
    private CameraController camController;
    private List<GameObject> objList;
    private List<GameObject> visList;
    public Text ballCountTxt;
    public float startAnimTime = 5.0f;

    // Start is called before the first frame update
    void Start()
    {
        objList = new List<GameObject>();
        visList = new List<GameObject>();
        if (mainCamParent == null)
        {
            mainCamParent = GameObject.Find("MainCamParent");
        }
        if (mainCam == null)
        {
            mainCam = Camera.main;
        }   
        camController = mainCamParent.GetComponent<CameraController>();
        foreach (GameObject obj in GameObject.FindGameObjectsWithTag("Cube"))
        {
            objList.Add(obj);
        }
        foreach (GameObject obj in GameObject.FindGameObjectsWithTag("Ball"))
        {
            objList.Add(obj);
        }
        if (ballCountTxt == null)
        {
            ballCountTxt = GameObject.Find("Ball Count Text").GetComponent<Text>();
        }
        StartCoroutine("StartingAnim");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator StartingAnim()
    {
        float curAnimTime = 0f;
        Vector3 camOriginPos = mainCamParent.transform.position;
        Vector3 camStartPos = new Vector3(21, 20, -22.5f);
        float camOriginSize = 0f;
        float camStartSize = 0f;
        if (mainCam.orthographic)
        {
            camOriginSize = mainCam.orthographicSize;
            camStartSize = 6.0f;
        }
        while (curAnimTime < startAnimTime)
        {
            curAnimTime += Time.deltaTime;
            mainCamParent.transform.position = Vector3.Lerp(camOriginPos, camStartPos, curAnimTime / startAnimTime);
            if (mainCam.orthographic)
            {
                mainCam.orthographicSize = Mathf.Lerp(camOriginSize, camStartSize, curAnimTime / startAnimTime);
            }
            yield return null;
        }
        camController.contState = CameraController.controllerState.IDLE;
        StartCoroutine("CalculateVisible");
    }

    IEnumerator CalculateVisible()
    {
        while (true)
        {
            visList.Clear();
            foreach (GameObject obj in objList)
            {
                ObjectVisible objVisScript = obj.GetComponent<ObjectVisible>();
                objVisScript.CalculateVisible();
                if (objVisScript.objVisible && obj.tag == "Ball")
                {
                    visList.Add(obj);
                }
            }
            ballCountTxt.text = visList.Count.ToString();
            yield return new WaitForSeconds(1.0f);
        }
    }
}
