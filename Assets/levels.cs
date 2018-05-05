using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class levels : MonoBehaviour {

    private int currReq;
    public int currComplete = 0;
    public int currlvl = 1;

    private int[] lvlsReq = { 1, 1, 1, 1, 2 };
    private int[] lvls = { 1, 2, 3, 4, 5 };
    private GameObject[][] levelObjects = new GameObject[5][];
    private GameObject[] currLevelObjects = { };
    private GameObject[] prevLevelObjects;

    private GameObject floorMirror;
    private GameObject wallMirror;

    // Use this for initialization
    void Start () {
        currReq = lvlsReq[currlvl - 1];
        floorMirror = GameObject.FindGameObjectWithTag("floorMirrorBase");
        wallMirror = GameObject.FindGameObjectWithTag("wallMirror");

        if(currlvl < 4)
        {
            floorMirror.SetActive(false);

        }
        if (currlvl < 3)
        {
            wallMirror.SetActive(false);

        }
        

        foreach (int lvl in lvls)
        {
            if (lvl != currlvl)
            {
                GameObject[] hold = GameObject.FindGameObjectsWithTag("Level" + lvl);
                levelObjects[lvl - 1] = hold;
                foreach (GameObject levlObject in hold)
                {
                    levlObject.SetActive(false);
                }
            }
            else
            {
                GameObject[] hold = GameObject.FindGameObjectsWithTag("Level" + lvl);
                levelObjects[lvl - 1] = hold;
            }
        }
    }
	
	// Update is called once per frame
	void Update () {
		if(currReq == currComplete)
        {
            currlvl++;
            currReq= lvlsReq[currlvl - 1];
            if(currLevelObjects != null)
            {
                prevLevelObjects = currLevelObjects;
            }
            
            currComplete = 0;

            for (int i=0;i<levelObjects.Length;++i)
            {
                
                foreach (GameObject objct in levelObjects[i])
                {
                    if (currlvl == i + 1)
                    {
                        objct.SetActive(true);
                    }
                    else
                    {
                        objct.SetActive(false);
                    }
                }

                
            }

            GameObject[] stray = GameObject.FindGameObjectsWithTag("Level" + (currlvl - 1));
            foreach (GameObject objct in stray)
            {
                
                objct.SetActive(false);
            }

            if (currlvl == 4)
            {
                floorMirror.SetActive(true);

            }
            if (currlvl == 3)
            {
                wallMirror.SetActive(true);

            }
        }
	}
}
