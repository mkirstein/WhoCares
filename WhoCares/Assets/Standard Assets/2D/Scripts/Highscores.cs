using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

namespace UnityStandardAssets._2D
{
    public class Highscores : MonoBehaviour
    {
        private static Highscores reference;

        private int highscore = 0;
        private int totalHighscore = 0;
        public int Highscore
        {
            get
            {
                return this.highscore;
            }

            set
            {
                this.highscore = (int) value;
                if (this.totalHighscore < (int) value)
                {
                    totalHighscore = (int) value;
                }
            }
        }

        public int TotalHighscore
        {
            get
            {
                return this.totalHighscore;
            }
        }

        public void Awake()
        {
            if (reference == null)
            {
                reference = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                DestroyImmediate(gameObject);
            }
        }
    }
}
