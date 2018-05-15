using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class swaphandItem : MonoBehaviour {

	public GameObject gripper;
	public GameObject handMirrorprefab;
	public GameObject currentHandheld;
	private bool inBackPack = false;
    public bool activeController;
	private SteamVR_TrackedController controller;
	private bool handMActive = false;

	// Use this for initialization
	void Awake () {
		Vector3 localPos = Vector3.zero;
		localPos.z = -0.05f;
		controller = GetComponent<SteamVR_TrackedController>();
		currentHandheld = Instantiate(gripper, Vector3.zero, Quaternion.LookRotation(Vector3.zero));
		currentHandheld.transform.parent = gameObject.transform;
		currentHandheld.transform.localPosition = localPos;
		currentHandheld.transform.localRotation = Quaternion.Euler (180,-90,-90);
	}
	
	// Update is called once per frame
	void FixedUpdate () {

        //Debug.Log(inBackPack);


    }

	private void OnEnable()
	{
		controller.TriggerClicked += Triggerpull;
	}

	private void OnDisable()
	{
		controller.TriggerClicked -= Triggerpull;

	}

	private void Triggerpull(object sender, ClickedEventArgs e)
	{
        GameObject[] hold = GameObject.FindGameObjectsWithTag("LevelControl");
        if (hold[0].gameObject.GetComponent<levels>().currlvl == 0)
        {
            hold[0].gameObject.GetComponent<levels>().currComplete++;
        }

        if (inBackPack && activeController)
		{
			if (gameObject.transform.GetChild(0).tag == "handMirrorL" || gameObject.transform.GetChild(0).tag == "handMirrorR") {
				Destroy (currentHandheld);
				Vector3 localPos = Vector3.zero;
				localPos.z = -0.05f;
				
				currentHandheld = Instantiate(gripper, Vector3.zero, Quaternion.identity);
				currentHandheld.transform.parent = gameObject.transform;
				currentHandheld.transform.localPosition = localPos;
				currentHandheld.transform.localRotation = Quaternion.Euler (180,-90,-90);

                Collider m_ObjectCollider;
                m_ObjectCollider = GetComponent<Collider>();
                m_ObjectCollider.enabled = true;
            }
			else if (gameObject.transform.GetChild(0).tag == "grip") {
				Destroy (currentHandheld);
				Vector3 localPos = Vector3.zero;
				localPos.z = -0.05f;
				//Destroy (gripper);
				currentHandheld = Instantiate(handMirrorprefab, Vector3.zero, Quaternion.identity);
				currentHandheld.transform.parent = gameObject.transform;
				currentHandheld.transform.localPosition = localPos;
				currentHandheld.transform.localRotation = Quaternion.Euler (180,-90,-90);

                Collider m_ObjectCollider;
                m_ObjectCollider = GetComponent<Collider>();
                //m_ObjectCollider.enabled = false;
            }

		}


	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.transform.gameObject.tag == "backpack")
		{
            //Debug.Log(transform.gameObject.tag);
            inBackPack = true;
		}

	}
	private void OnTriggerExit(Collider other)
	{
		if (other.transform.gameObject.tag == "backpack")
		{
            
			inBackPack = false;
		}
	}


}
