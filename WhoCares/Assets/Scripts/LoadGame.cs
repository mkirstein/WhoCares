using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LoadGame : MonoBehaviour {

    public bool isSingleplayer;
	// Update is called once per frame
	public void Update () {
        if (!isSingleplayer)
        {
            SceneManager.LoadScene("Mainscene");
        } else
        {
            SceneManager.LoadScene("MainsceneSingleplayer");
        }
	}
}
