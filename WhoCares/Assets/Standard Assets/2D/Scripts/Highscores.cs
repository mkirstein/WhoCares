﻿using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

namespace UnityStandardAssets._2D
{
    public class Highscores : MonoBehaviour
    {
        private static Highscores reference;

        private float highscore = 0f;
        public float Highscore
        {
            get { return highscore; }
            set { highscore = value; }
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
