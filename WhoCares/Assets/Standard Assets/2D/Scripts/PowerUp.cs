using UnityEngine;

namespace UnityStandardAssets._2D
{
    public class PowerUp : MonoBehaviour
    {

        public enum PowerUps { TripleJump, Sonic };

        private bool active = true;

        // Use this for initialization
        void Start()
        {
        }

        // Update is called once per frame
        void Update()
        {

        }

        void OnCollisionEnter2D(Collision2D coll)
        {
            if (active)
            {
                gameObject.GetComponent<SpriteRenderer>().enabled = false;
                PlatformerCharacter2D char2D = (PlatformerCharacter2D)coll.gameObject.GetComponent(typeof(PlatformerCharacter2D));

                char2D.PowerUp(PowerUps.TripleJump);
                active = false;
            }
        }
    }
}