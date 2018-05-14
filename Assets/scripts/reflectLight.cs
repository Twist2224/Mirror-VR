using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class reflectLight : MonoBehaviour {

    public GameObject lightSource;
    public Vector3 hitPoint;
    public Vector3 hitNormal;
	public Vector3 reflectAngle;
	public Vector3 hitAngle;
	public bool reflect = false;
	public LayerMask thisLayerMask;
    private GameObject reflection;
    private ArrayList groupsReflected = new ArrayList();


    // Use this for initialization
    void Start () {

    }
	
	// Update is called once per frame
	void Update () {

        if (reflect)
        {
            
            if (reflection != null)
            {
                //Debug.Log("in mirr "+ reflectAngle);
                //Debug.DrawLine(hitPoint, reflectAngle, Color.red);
                //reflection.gameObject.GetComponentInChildren<lightContact>().myLayerMask -= thisLayerMask.value;
                reflection.gameObject.transform.LookAt(reflectAngle);
                reflection.gameObject.transform.localRotation = Quaternion.Euler(reflection.gameObject.transform.eulerAngles.x-90, reflection.gameObject.transform.eulerAngles.y, reflection.gameObject.transform.eulerAngles.z);
                reflection.gameObject.transform.localPosition = hitPoint;

                reflection.transform.GetChild(1).gameObject.GetComponent<Renderer>().enabled = false;
            } else
            {
				reflection = Instantiate(lightSource, hitPoint, Quaternion.LookRotation(reflectAngle));
                //reflection.gameObject.GetComponentInChildren<lightContact>().myLayerMask -= thisLayerMask.value;
                reflection.gameObject.transform.LookAt(reflectAngle);
                reflection.gameObject.transform.localRotation = Quaternion.Euler(reflection.gameObject.transform.eulerAngles.x-90, reflection.gameObject.transform.eulerAngles.y, reflection.gameObject.transform.eulerAngles.z);
                reflection.gameObject.transform.localPosition = hitPoint;

                groupsReflected.Add(reflection.gameObject.GetComponentInChildren<generateLightBeam>().group);

            }
            
            //reflection.gameObject.transform.localScale = new Vector3(reflection.gameObject.transform.localScale.x, 12f, reflection.gameObject.transform.localScale.z);
            //Debug.Log(hit.point*1.01f);
        } else
        {
            Destroy(reflection);
        }
	}

    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "light-beam" && (reflection == null || !groupsReflected.Contains(reflection.gameObject.GetComponentInChildren<generateLightBeam>().group)))
        {
            //reflect = true;
            
            
        }

    }
}
