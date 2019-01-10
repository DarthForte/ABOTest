using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public enum controllerState
    {
        INACTIVE,
        IDLE,
        DRAGGING
    }

    public controllerState contState;
    private Vector3 mousePos;
    public Camera mainCamera;
    public float panSpeed = 1f;

    // Start is called before the first frame update
    void Start()
    {
        contState = controllerState.INACTIVE;
        mousePos = new Vector3(0,0,0);
    }

    // Update is called once per frame
    void Update()
    {
        switch (contState)
        {
            case controllerState.INACTIVE:
            case controllerState.IDLE:
                if (Input.GetMouseButtonDown(0))
                {
                    contState = controllerState.DRAGGING;
                    mousePos = Input.mousePosition;
                }
                break;
            case controllerState.DRAGGING:
                if (Input.GetMouseButton(0))//currently dragging
                {
                    Vector3 posMoved = (mousePos - Input.mousePosition) * panSpeed;
                    Vector3 curPos = transform.position;
                    Vector3 newPos = curPos + (transform.right * posMoved.x);
                    newPos = newPos + (transform.forward * posMoved.y);
                    transform.position = newPos;
                    //transform.position = new Vector3(curPos.x + posMoved.x, curPos.y, curPos.z + posMoved.y);
                    mousePos = Input.mousePosition;
                }

                if (Input.GetMouseButtonUp(0))//end of drag
                {
                    contState = controllerState.IDLE;
                    mousePos = new Vector3(0, 0, 0);
                }
                break;
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }
}
