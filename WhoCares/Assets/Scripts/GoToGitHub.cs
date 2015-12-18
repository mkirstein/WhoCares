using UnityEngine;
using System.Collections;

public class GoToGitHub : MonoBehaviour {

    public void Start()
    {
        Application.OpenURL("https://github.com/nkristek/awesome");
    }
}
