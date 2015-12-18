using UnityEngine;
using System.Collections;
using System;
using UnityEngine.SceneManagement;

public class TutorialText : MonoBehaviour {

    public bool isSingleplayer;
    public DateTime startTime;

	// Use this for initialization
	void Start () {
        Invoke("ChangeLevel", 5.0f);
    }
	
	// Update is called once per frame
	void ChangeLevel () {
	    if (isSingleplayer)
        {
            SceneManager.LoadScene("MainsceneSingleplayer");
        } else
        {
            SceneManager.LoadScene("Mainscene");
        }
	}
}
