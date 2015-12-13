using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LoadGame : MonoBehaviour {

	// Update is called once per frame
	public void Update () {
        SceneManager.LoadScene("Mainscene");
	}
}
