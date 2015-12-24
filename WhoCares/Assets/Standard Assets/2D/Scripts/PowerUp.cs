using UnityEngine;
using System;

namespace UnityStandardAssets._2D
{
    public class PowerUp : MonoBehaviour
    {

        public enum PowerUps { TripleJump, Sonic };
        private PowerUps[] allPowerUps = (PowerUps[])Enum.GetValues(typeof(PowerUps));
        private int count;
        private System.Random rnd = new System.Random();


        private bool active = true;

        // Use this for initialization
        void Start()
        {
            count = allPowerUps.Length;
        }

        // Update is called once per frame
        void Update()
        {

        }

        void OnCollisionEnter2D(Collision2D coll)
        {
            if (active)
            {
                gameObject.GetComponent<CircleCollider2D>().enabled = false;
                gameObject.GetComponent<SpriteRenderer>().enabled = false;
                PlatformerCharacter2D char2D = (PlatformerCharacter2D)coll.gameObject.GetComponent(typeof(PlatformerCharacter2D));
                int rand = rnd.Next(0, count);
                char2D.PowerUp(allPowerUps[rand]);
                active = false;
            }
        }
    }
}