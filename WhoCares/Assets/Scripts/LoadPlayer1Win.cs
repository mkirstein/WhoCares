using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LoadPlayer1Win : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
        SceneManager.LoadScene("Player1Win");
    }
}