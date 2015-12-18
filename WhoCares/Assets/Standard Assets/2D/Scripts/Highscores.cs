using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

namespace UnityStandardAssets._2D
{
    public class Highscores : MonoBehaviour
    {
        private static Highscores reference;

        private float highscore = 0f;
        private float totalHighscore = 0f;
        public float Highscore
        {
            get
            {
                return this.highscore;
            }

            set
            {
                Debug.Log("Highscore set: "+value);
                this.highscore = 0f;
                this.highscore = value;
                if (this.totalHighscore < value)
                {
                    totalHighscore = value;
                }
            }
        }

        public float TotalHighscore
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
