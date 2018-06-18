using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class show_grip : MonoBehaviour
{
    private SteamVR_TrackedController controller;

    // Use this for initialization
    void Awake () {
        controller = GetComponent<SteamVR_TrackedController>();

    }
	
	// Update is called once per frame
	void FixedUpdate () {

        Renderer renderer = GetComponent<Renderer>();
        if (controller.gripped)
        {
            Debug.Log("gripped");
            //Color myColor = new Color();
            //ColorUtility.TryParseHtmlString("#F00", out myColor);
            //renderer.material.color = myColor;
        }else
        {
            //Color myColor = new Color();
            //ColorUtility.TryParseHtmlString("#60000A", out myColor);
            //renderer.material.color = myColor;

        }
    }
}
