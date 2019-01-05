using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectVisible : MonoBehaviour
{
    private Collider coll;
    public Camera mainCam;
    private Plane[] planes;
    public bool objVisible = false;
    private CameraController camController;

    // Start is called before the first frame update
    void Start()
    {
    }

    void Awake()
    {
        mainCam = Camera.main;
        coll = GetComponent<Collider>();
        camController = mainCam.GetComponent<CameraController>();
    }

    // Update is called once per frame
    void Update()
    {
        /*switch (camController.contState)
        {
            case CameraController.controllerState.INACTIVE:
                break;
            default:
                CalculateVisible();
                break;
            
        }*/
    }

    public void CalculateVisible()
    {
        planes = GeometryUtility.CalculateFrustumPlanes(mainCam);
        if (GeometryUtility.TestPlanesAABB(planes, coll.bounds))//object within frustum
        {
            //bool anyVisible = false;
            for (float x = 0; x < 1; x += 0.1f)
            {
                for (float y = 0; y < 1; y += 0.1f)
                {
                    Vector3 rayOrigin = mainCam.ViewportToWorldPoint(new Vector3(x, y, mainCam.nearClipPlane));
                    Vector3 rayDir = (transform.position - rayOrigin).normalized;
                    RaycastHit hit;
                    if (Physics.Raycast(rayOrigin, rayDir, out hit))
                    {
                        if (hit.transform.gameObject == this.gameObject)
                        {
                            objVisible = true;
                            //Fetch the Renderer from the GameObject
                            Renderer rend = GetComponent<Renderer>();

                            //set the color to blue
                            rend.material.color = Color.blue;
                            break;
                        }
                        else if(objVisible != true)
                        {
                            objVisible = false;
                            //Fetch the Renderer from the GameObject
                            Renderer rend = GetComponent<Renderer>();

                            rend.material.color = Color.red;
                        }
                    }
                }
            }
        }
        else//object not within camera frustum
        {
            objVisible = false;
            //Fetch the Renderer from the GameObject
            Renderer rend = GetComponent<Renderer>();

            rend.material.color = Color.red;
        }
    }
}
