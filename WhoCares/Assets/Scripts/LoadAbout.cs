using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LoadAbout : MonoBehaviour
{
    // Update is called once per frame
    public void Update()
    {
        SceneManager.LoadScene("About");
    }
}
