using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class generateLightBeam : MonoBehaviour {

    public int group;

    public GameObject light; 


    private bool expand = true;
    private float speed = 0.05f;
    private Vector3 holdsize;
    private float holdPosY;

    // Use this for initialization
    void Start () {
        //GetComponentInChildren<lightContact>();
        //holdsize = light.transform.localScale;
        //holdsize.y = 0f;

        //transform.localScale = holdsize;



        //holdPosY = transform.localPosition.y - holdsize.y;

    }
	
	// Update is called once per frame
	void Update () {
        
        //expand = !light.GetComponent<lightContact>().contact;
        //Debug.Log("expand: "+expand+" size "+ holdsize.y);
        //if (expand)
        //{
        //    holdsize.y += speed;
        //    light.transform.localScale = holdsize;

        //    light.transform.localPosition = new Vector3(light.transform.localPosition.x, holdPosY - holdsize.y, light.transform.localPosition.z );
        //}
    }
}
