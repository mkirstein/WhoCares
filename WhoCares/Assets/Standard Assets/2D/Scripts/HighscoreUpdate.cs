using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

namespace UnityStandardAssets._2D
{
    public class HighscoreUpdate : MonoBehaviour
    {
        Highscores hs;

        public Text fieldhs;
        public Text fieldths;

        // Use this for initialization
        void Start()
        {
            // Set Text-Fields
            hs = GameObject.Find("Scores").GetComponent("Highscores") as Highscores;
            fieldhs.text = "Your score was: " + hs.Highscore;
            fieldths.text = "The highscore was: " + hs.TotalHighscore;
        }
    }
}
