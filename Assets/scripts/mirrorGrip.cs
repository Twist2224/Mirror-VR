﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mirrorGrip : MonoBehaviour {
    private SteamVR_TrackedController controller;
    private bool atGrip = false;
    private GameObject currgrip;
	private bool moving = false;

	// Use this for initialization
	void Awake () {
        controller = GetComponent<SteamVR_TrackedController>();
        
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if(atGrip && controller.gripped)
        {
			moving = true;
            Vector3 gripPos = controller.transform.position;
			GameObject mirror = currgrip.transform.parent.gameObject;

			if (mirror.tag == "wallMirror") { // Gripping wallMirror
				if ((mirror.transform.eulerAngles.y <= 360 && mirror.transform.eulerAngles.y >= 180) || mirror.transform.eulerAngles.y == 0) {
					mirror.transform.LookAt (gripPos);
					Quaternion rotateGoal = Quaternion.Euler (0, mirror.transform.eulerAngles.y, 0);
					mirror.transform.rotation = rotateGoal * Quaternion.AngleAxis (-180, Vector3.up);
					if (mirror.transform.eulerAngles.y < 180 && mirror.transform.eulerAngles.y > 90) {
						mirror.transform.rotation = Quaternion.Euler (0, 180, 0);
					} else if (mirror.transform.eulerAngles.y < 90 && mirror.transform.eulerAngles.y > 0) {
						mirror.transform.rotation = Quaternion.Euler (0, 0, 0);
					}
				}
			}

			if (mirror.tag == "floorMirror") { // Gripping floorMirror
					GameObject baseOfMirror = mirror.transform.parent.gameObject;
					baseOfMirror.transform.LookAt (gripPos);
					Quaternion baseRotateGoal = Quaternion.Euler (0, baseOfMirror.transform.eulerAngles.y, 0);
					baseOfMirror.transform.rotation = baseRotateGoal * Quaternion.AngleAxis (-180, Vector3.up);
				
				//if ((mirror.transform.eulerAngles.x <= 360 && mirror.transform.eulerAngles.x >= 270) || mirror.transform.eulerAngles.x == 0) {
					mirror.transform.LookAt (gripPos);
					mirror.transform.localRotation = Quaternion.Euler (-mirror.transform.eulerAngles.x, 0, 0);
					//mirror.transform.rotation = rotateGoal * Quaternion.AngleAxis (-180, Vector3.up);


				//}
			}

		}
		else if(moving) {
			GameObject mirror = currgrip.transform.parent.gameObject;
			if (mirror.transform.eulerAngles.y > -180 && mirror.transform.eulerAngles.y < -90) {
				
				mirror.transform.eulerAngles = new Vector3(0,-180,0);
			} else if (mirror.transform.eulerAngles.y > 90 && mirror.transform.eulerAngles.y < 180) {
				mirror.transform.rotation = Quaternion.Euler(0,180,0);
			}
			moving = false;
			if (!atGrip) {
				currgrip = null;
			}
		}
	}

    private void OnEnable()
    {
        controller.Gripped += HandleGripped;
    }

    private void OnDisable()
    {
        controller.Gripped -= HandleGripped;
        
    }

    private void HandleGripped(object sender, ClickedEventArgs e)
    {
        //if(touchingGrip)
        //{

        //}
        
        
    }
    

    private void OnTriggerEnter(Collider other)
    {
		if (other.transform.gameObject.tag == "grip" && gameObject.transform.GetChild(0).tag == "grip")
		{
			currgrip = other.gameObject;
			atGrip = true;
			//other.gameObject.GetComponent<floorMirrorMovable> ().numIteractions++;
        }
    
    }
    private void OnTriggerExit(Collider other)
    {
		if (other.transform.gameObject.tag == "grip" && gameObject.transform.GetChild(0).tag == "grip")
		{
			atGrip = false;
			//other.gameObject.GetComponent<floorMirrorMovable> ().numIteractions--;

        }
    }

    //start game

    private void Triggerpull(object sender, ClickedEventArgs e)
    {

        GameObject[] hold = GameObject.FindGameObjectsWithTag("LevelControl");
        if(hold[0].gameObject.GetComponent<levels>().currlvl == 0)
        {
            hold[0].gameObject.GetComponent<levels>().currComplete++;
        }
        
    }
   }
