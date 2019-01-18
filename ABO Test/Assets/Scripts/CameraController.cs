using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;

public class CameraController : MonoBehaviour
{
    public enum controllerState
    {
        INACTIVE,
        IDLE,
        DRAGGING
    }

    public controllerState contState;
    public int playerId = 0;
    private Vector3 mousePos;
    public Camera mainCamera;
    public float panSpeed = 1f;
    public float mouseSensitivity = 0.1f;
    private Player player;

    void Awake()
    {
        player = ReInput.players.GetPlayer(playerId);
    }

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
                    Vector3 posMoved = (mousePos - Input.mousePosition) * mouseSensitivity;
                    MoveCamera(posMoved);
                    mousePos = Input.mousePosition;
                }

                if (Input.GetMouseButtonUp(0))//end of drag
                {
                    contState = controllerState.IDLE;
                    mousePos = new Vector3(0, 0, 0);
                }
                break;
        }
        if (contState != controllerState.INACTIVE)
        {
            Vector2 controllerDir = new Vector3(0, 0, 0);
            controllerDir.x = player.GetAxis("MoveX");
            controllerDir.y = player.GetAxis("MoveY");
            if (controllerDir.x != 0.0f || controllerDir.y != 0.0f)
            {
                Vector3 posMoved = new Vector3(0, 0, 0);
                posMoved.x = controllerDir.x;
                posMoved.y = controllerDir.y;
                posMoved = posMoved * panSpeed * Time.deltaTime;
                MoveCamera(posMoved);
            }
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    void MoveCamera(Vector3 posMoved)
    {
        Vector3 curPos = transform.position;
        Vector3 newPos = curPos + (transform.right * posMoved.x);
        newPos = newPos + (transform.forward * posMoved.y);
        transform.position = newPos;
    }
}
