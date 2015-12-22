using UnityEngine;
using System.Collections;
using System;
using UnityEngine.SceneManagement;

public class TutorialText : MonoBehaviour {

    public bool isSingleplayer;
    public DateTime startTime;

	// Use this for initialization
	void Start () {
        // Nach 5 Sekunden die Szene ändern
        Invoke("ChangeLevel", 5.0f);
    }
	
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
