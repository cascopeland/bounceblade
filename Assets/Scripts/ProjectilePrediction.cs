using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


[RequireComponent(typeof(LineRenderer))]
public class ProjectilePrediction : MonoBehaviour
{
    public LayerMask layerMask;
    public float defaultLength = 50;
    public int numOfReflections = 2;

    private LineRenderer lineRenderer;
    private Camera cam;
    private RaycastHit hit;
    private bool spacePressed = false;

    private Ray ray;
    private Vector3 direction;

    // Start is called before the first frame update
    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        cam = Camera.main;
        
    }

    // Update is called once per frame
    void Update()
    {
        //Normalaser();
        
        if (Input.GetKeyDown(KeyCode.Space))
        {
            spacePressed = true;
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            spacePressed = false;
            lineRenderer.enabled = false;
        }
        if (spacePressed == true)
        {
            lineRenderer.enabled = true;
            Reflectlaser();
        }

    }


    void Reflectlaser()
    {
        ray = new Ray(transform.position, transform.forward);
        lineRenderer.positionCount = 1;
        lineRenderer.SetPosition(0, transform.position);

        float remainLength = defaultLength;

        for (int i = 0; i < numOfReflections; i++)
        {
            if (Physics.Raycast(ray.origin, ray.direction, out hit, remainLength, layerMask))
            {
                lineRenderer.positionCount += 1;
                lineRenderer.SetPosition(lineRenderer.positionCount - 1, hit.point);

                remainLength -= Vector3.Distance(ray.origin, hit.point);
                ray = new Ray(hit.point, Vector3.Reflect(ray.direction, hit.normal));
            }
            else
            {
                lineRenderer.positionCount += 1;
                lineRenderer.SetPosition(lineRenderer.positionCount - 1, ray.origin + (ray.direction * remainLength)); ;
            }
        }


        
    }

    void Normalaser()
    {
        lineRenderer.SetPosition(0, transform.position);

        if (Physics.Raycast(transform.position, transform.forward, out hit, defaultLength, layerMask))
        {
            lineRenderer.SetPosition(1, hit.point);
        } else
        {
            lineRenderer.SetPosition(1, transform.position + (transform.forward * defaultLength));
        }
    }
}
