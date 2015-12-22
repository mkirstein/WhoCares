using UnityEngine;
using System.Collections;

namespace UnityStandardAssets._2D
{
    public class AudioEffects : MonoBehaviour
    {

        public AudioClip player1jump, player2jump, wasted, robbe, playerwin;
        private AudioSource source;

        void Awake()
        {
            this.source = GetComponent<AudioSource>();
        }
        public void p1jump()
        {
            this.source = GetComponent<AudioSource>();
            source.PlayOneShot(player1jump, 1);
            Debug.Log("Player 1 Jump Sound");
        }
    }
}
