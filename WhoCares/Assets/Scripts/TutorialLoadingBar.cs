using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TutorialLoadingBar : MonoBehaviour {

    public Image progress;
    public float waitTime = 5.0f;

    // Update is called once per frame
    void Update()
    {
        progress.fillAmount += 1.0f / waitTime * Time.deltaTime;
    }
}
