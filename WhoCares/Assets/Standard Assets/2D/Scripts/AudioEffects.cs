using UnityEngine;
using System.Collections;

namespace UnityStandardAssets._2D
{
    public class AudioEffects : MonoBehaviour
    {
        // Simple Class to play sound effects

        public AudioClip player1jump, player2jump, wasted;
        private AudioSource source;

        void Awake()
        {
            this.source = GetComponent<AudioSource>();
        }
        public void p1jump()
        {
            source.PlayOneShot(player1jump, 0.5f);
        }
        public void p2jump()
        {
            source.PlayOneShot(player2jump, 0.5f);
        }
        public void die()
        {
            source.PlayOneShot(wasted, 1);
        }
    }
}
