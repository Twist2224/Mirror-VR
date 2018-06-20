using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class levels : MonoBehaviour {

    private int currReq;
    public int currComplete = 0;
    public int currlvl = 0;

    public AudioClip[] dialog;

    public int[] lvlsReq = {1, 1, 1, 1, 1, 1,1 };
    public int[] lvls = {0, 1, 2, 3, 4, 5, 6 };
    private GameObject[][] levelObjects = new GameObject[6][];
    private GameObject[] currLevelObjects = { };
    private GameObject[] prevLevelObjects;
    private AudioSource audioEmitter;

    private GameObject floorMirror;
    private GameObject wallMirror;

    public Texture2D fadeTexture ;
    public float fadeSpeed = 0.05f;
    public int drawDepth = -1000;

    private float alpha = 0.0f;
    private int fadeDir = 0;

    private bool faded = false;

    void OnGUI()
    {
        if (fadeDir != 0)
            {
            alpha += fadeDir * fadeSpeed * Time.deltaTime;
            alpha = Mathf.Clamp01(alpha);
            Color fadeCol = GUI.color;

            fadeCol.a = alpha;

            GUI.color = fadeCol;

            GUI.depth = drawDepth;

            GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), Texture2D.whiteTexture);

        }

        if (faded && alpha == 0)
        {
            faded = false;
            fadeDir = 0;
        }
         else if (alpha == 1)
        {
            faded = true;
        }
    }

    // Use this for initialization
    void Start () {
        audioEmitter = GameObject.FindGameObjectWithTag("speaker").gameObject.GetComponent<AudioSource>();
        audioEmitter.clip = dialog[currlvl];
        currReq = lvlsReq[currlvl];
        floorMirror = GameObject.FindGameObjectWithTag("floorMirrorBase");
        wallMirror = GameObject.FindGameObjectWithTag("wallMirror");

        if(currlvl != 4)
        {
            floorMirror.SetActive(false);

        }
        if (currlvl != 3)
        {
            wallMirror.SetActive(false);

        }
        

        foreach (int lvl in lvls)
        {
            if (lvl != currlvl)
            {
                GameObject[] hold = GameObject.FindGameObjectsWithTag("Level" + lvl);
                levelObjects[lvl] = hold;
                foreach (GameObject levlObject in hold)
                {
                    levlObject.SetActive(false);
                }
            }
            else
            {
                GameObject[] hold = GameObject.FindGameObjectsWithTag("Level" + lvl);
                levelObjects[lvl] = hold;
            }
        }
    }
	
	// Update is called once per frame
	void Update () {
        if (currReq == currComplete)
        {
            fadeDir = 1;
        }

   

        if (currReq == currComplete && faded)
        {
            
            currlvl++;
            currReq= lvlsReq[currlvl];
            if(currLevelObjects != null)
            {
                prevLevelObjects = currLevelObjects;
            }
            
            currComplete = 0;

            for (int i=0;i<levelObjects.Length;++i)
            {
                
                foreach (GameObject objct in levelObjects[i])
                {
                    if (currlvl == i)
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
            audioEmitter.Stop();
            audioEmitter.PlayOneShot(dialog[currlvl]);

            fadeDir = -1;
        }

        
	}
}
