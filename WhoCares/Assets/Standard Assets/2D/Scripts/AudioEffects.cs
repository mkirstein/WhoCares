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
        public void seal()
        {
            source.PlayOneShot(robbe, 1);
        }
        public void win()
        {
            source.PlayOneShot(playerwin, 1);
        }
    }
}
