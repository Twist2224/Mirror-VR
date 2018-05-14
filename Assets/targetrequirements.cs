﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class targetrequirements : MonoBehaviour {

    public int minHits = 1;
    public int hits = 0;
    public Material colr;
    public Color color;

    private float intensity = 0f;
    private Renderer rend1;
    private bool done = false;
    private Renderer rend2;


    // Use this for initialization
    void Start () {
        rend1 = gameObject.transform.parent.GetChild(0).gameObject.GetComponent<Renderer>();
        rend2 = gameObject.transform.parent.GetChild(2).gameObject.GetComponent<Renderer>();
        color = rend1.sharedMaterial.color;
    }
	
	// Update is called once per frame
	void Update () {
        if (minHits <= hits && !done)
        {
            if (intensity >= 100f)
            {
                //Debug.Log("++++++you win!!!!");
                done = true;
                GameObject[] hold = GameObject.FindGameObjectsWithTag("LevelControl");
                hold[0].gameObject.GetComponent<levels>().currComplete++;

            }
            else
            {
                intensity += Time.time * 0.1f;
                //Debug.Log(intensity);
                Color emission = color * Mathf.LinearToGammaSpace(intensity*0.01f);
                rend1.material.SetColor("_EmissionColor", emission);
                rend2.material.SetColor("_EmissionColor", emission);
                //RendererExtensions.UpdateGIMaterials(rend1);
                //DynamicGI.UpdateEnvironment();
                //DynamicGI.SetEmissive(rend1, colr * intensity);
                //RendererExtensions.UpdateGIMaterials(rend1);
                //DynamicGI.SetEmissive(rend2, colr * intensity);
                //RendererExtensions.UpdateGIMaterials(rend2);
            }
            
        }
	}
}
