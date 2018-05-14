using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lightContact : MonoBehaviour {

    public Light lightSource;
    public LayerMask myLayerMask;
    public bool contact = false;
    public Vector3 shear = new Vector3(0f,0.1f,0f);
    public GameObject source = null;
    private Mesh mesh;
    private Vector3[] startVertices;
    private Vector3[] vertices;
    private RaycastHit centerHit;
    private GameObject currHit;
    private float lightBeamLength;

    // Use this for initialization
    void Start()
    {
        startVertices = GetComponent<MeshFilter>().mesh.vertices;


    }

    // Update is called once per frame
    void Update () {
        // beam length
        //transform.localEulerAngles = new Vector3(0, 0, 0);
        RaycastHit hit;
        //Debug.DrawRay(transform.position + transform.localScale.y * transform.up, -transform.up, Color.green);
        if (Physics.Raycast(transform.position + transform.localScale.y * transform.up, -transform.up, out hit, 100f, myLayerMask))
        {
			Debug.Log(hit.collider.name + " "+ gameObject.transform.parent.gameObject.name);
            if (hit.collider.tag == "light-barrier" || hit.collider.tag == "mirror")
            {
                float magn = Vector3.Distance(hit.point, transform.parent.transform.position);
                Vector3 halfway = transform.position;
                halfway.x = 0;
                halfway.y = -magn / 2;
                halfway.z = 0;
                transform.localPosition = halfway;
                transform.localScale = new Vector3(transform.localScale.x, magn/2, transform.localScale.z);
                lightBeamLength = magn;
                centerHit = hit;
                //Debug.Log(magn/2 + " " + gameObject.transform.parent.gameObject.name);
            }
            // 
            if (hit.transform.gameObject.tag == "mirror")
            {
                //Debug.DrawRay(hit.point, hit.normal, Color.blue);
                //Debug.DrawRay(hit.point, Vector3.Reflect((hit.point - transform.position), hit.normal), Color.cyan);
                //Debug.DrawLine(hit.point, hit.point + Vector3.Reflect((hit.point - transform.position), hit.normal).normalized, Color.magenta);
                //Debug.Log("2 "+hit.normal+" "+ transform.position+" "+ Vector3.Reflect(hit.point - transform.position, hit.normal) + " " + (hit.normal+hit.point));
                hit.transform.gameObject.GetComponent<reflectLight>().hitPoint = centerHit.point;
                hit.transform.gameObject.GetComponent<reflectLight>().hitNormal = centerHit.normal;
				hit.transform.gameObject.GetComponent<reflectLight>().reflectAngle = hit.point + Vector3.Reflect((hit.point - transform.position), hit.normal).normalized;
				hit.transform.gameObject.GetComponent<reflectLight>().hitAngle = transform.position;
                hit.transform.gameObject.GetComponent<reflectLight>().reflect = true; 
                hit.transform.gameObject.GetComponent<reflectLight>().lightSource = transform.parent.gameObject; 
                 currHit = hit.collider.gameObject;

            }
            else if (currHit != null && currHit.transform.gameObject.tag == "mirror")
            {
                //Debug.Log("off "+ gameObject.transform.parent.gameObject.name);
                currHit.transform.gameObject.GetComponent<reflectLight>().reflect = false;
                currHit = null;
            }
            //calculateShear();


            if (hit.transform.gameObject.tag == "target")
            {
                hit.collider.transform.gameObject.GetComponent<targetrequirements>().hits++;
                currHit = hit.collider.gameObject;
            } else if(currHit != null && currHit.transform.gameObject.tag == "target")
            {
                hit.collider.transform.gameObject.GetComponent<targetrequirements>().hits--;
                currHit = null;
            }

        }
        else if (currHit != null && currHit.transform.gameObject.tag == "mirror")
        {
            //Debug.Log("off "+ gameObject.transform.parent.gameObject.name);
            currHit.transform.gameObject.GetComponent<reflectLight>().reflect = false;
            currHit = null;
        } else if(currHit != null && currHit.transform.gameObject.tag == "mirror")
        {
            hit.collider.transform.gameObject.GetComponent<targetrequirements>().hits--;
            currHit = null;
        }
        else if (currHit != null && currHit.transform.gameObject.tag == "target")
        {
            hit.collider.transform.gameObject.GetComponent<targetrequirements>().hits--;
            currHit = null;
        }
    }
    void OnTriggerEnter(Collider collider)
    {
        //if (collider.transform.gameObject.tag == "mirror")
        //{
        //    collider.transform.gameObject.GetComponent<reflectLight>().hit = centerHit;
        //    //collider.transform.gameObject.GetComponent<reflectLight>().reflect = true;
        //    //collider.transform.gameObject.GetComponent<reflectLight>().reflectAngle = Vector3.Reflect(gameObject.transform.parent.transform.position, centerHit.normal);
        //}
    }

    //    if(collider.gameObject.tag == "light-barrier")
    //    {
    //        //Debug.Log("collide");
    //        contact = true;
    //        RaycastHit hit;
    //        if (Physics.Raycast(transform.position, -Vector3.up, out hit))
    //        {

    //            lightSource.spotAngle = 180 - 2*(Mathf.Rad2Deg * Mathf.Atan(2 * Vector3.Distance(hit.point, lightSource.transform.position) / transform.localScale.x));
    //            //float raylen = Vector3.Magnitude(Vector3.)
    //            Vector3 collidedSurfaceVect = collider.gameObject.transform.TransformVector(Vector3.up).normalized;
    //            Vector3 beamvector = transform.TransformVector(-Vector3.up).normalized;
    //            float shearAngle = Vector3.Angle(collidedSurfaceVect, beamvector);
    //            float shearDot = Vector3.Dot(collidedSurfaceVect, beamvector);

    //                //shearAngle = Mathf.PI - shearAngle;
    //                shear =  new Vector3(0, -beamvector.y, 0);


    //            Debug.Log(transform.TransformVector(-Vector3.up).normalized);
    //            Debug.Log(collidedSurfaceVect);
    //            Debug.Log(shearAngle);
    //            Debug.Log(shearDot);
    //            Debug.Log(shear);
    //            Debug.Log(hit.collider.name);

    //            shear_beam(GetComponent<MeshFilter>().mesh, shear/2);
    //            //shear_beam(GetComponentInChildren<MeshFilter>().mesh, shear);
    //        }
    //    }
    //}


    void calculateShear()
    {
        float shearVal;
        RaycastHit hitN, hitS, hitW, hitE;
        Vector3 rayNorth = new Vector3(transform.position.x + transform.localScale.x / 2, transform.position.y, transform.position.z);
        Vector3 raySouth = new Vector3(transform.position.x - transform.localScale.x / 2, transform.position.y, transform.position.z);
        Vector3 rayWest = new Vector3(transform.position.x, transform.position.y, transform.position.z + transform.localScale.z / 2);
        Vector3 rayEast = new Vector3(transform.position.x, transform.position.y, transform.position.z - transform.localScale.z / 2);
        if (Physics.Raycast(rayNorth, -Vector3.up, out hitN) && Physics.Raycast(raySouth, -Vector3.up, out hitS)
            && Physics.Raycast(rayWest, -Vector3.up, out hitW) && Physics.Raycast(rayEast, -Vector3.up, out hitE))
        {


            float magnN = Vector3.Magnitude(hitN.point - rayNorth);
            float magnS = Vector3.Magnitude(hitS.point - raySouth);
            float magnW = Vector3.Magnitude(hitW.point - rayWest);
            float magnE = Vector3.Magnitude(hitE.point - rayEast);


            if (magnN <= magnE && magnN <= magnS && magnN <= magnW)// North longest
            {
                shearVal = magnN - lightBeamLength;
            }
            else if (magnS <= magnE && magnS <= magnN && magnS <= magnW)// South longest
            {
                shearVal = magnS - lightBeamLength;
            }
            else if (magnE <= magnS && magnE <= magnN && magnE <= magnW)// East longest
            {
                shearVal = magnE - lightBeamLength;
            }
            else// West longest
            {
                shearVal = magnW - lightBeamLength;
            }
            shear = new Vector3(0f, shearVal, 0f);
            //Debug.Log(shear);
            //Debug.Log("------------------");
            //Debug.Log(magnW);
            //Debug.Log(magnE);
            //Debug.Log(magnN);
            //Debug.Log(magnS);
            //Debug.Log("------------------");
            //Debug.Log(lightBeamLength);
            //shear_beam(GetComponent<MeshFilter>().mesh, shear);
            //transform.localPosition = new Vector3(transform.localPosition.x, (transform.localPosition.y - transform.localScale.y) - transform.localScale.y, transform.localPosition.z);
        }

    }

    void shear_beam(Mesh mesh, Vector3 shear)
    {
        
        vertices = startVertices;
        //shear  = new Vector3(0f, - 0.1f, 0f);
        for (int i = 0; i < vertices.Length; i++)
        {
            vertices[i] = vertices[i] + shear * vertices[i].z;
            mesh.vertices = vertices;
            mesh.RecalculateBounds();
            mesh.RecalculateNormals();
        }
    }
}
