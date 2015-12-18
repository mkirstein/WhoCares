using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LoadTutorial : MonoBehaviour
{
    public bool isSingleplayer;
    // Update is called once per frame
    public void Update()
    {
        if (isSingleplayer)
        {
            SceneManager.LoadScene("TutorialSingleplayer");
        } else
        {
            SceneManager.LoadScene("Tutorial");
        }
    }
}
