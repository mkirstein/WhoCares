using System;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

namespace UnityStandardAssets._2D
{
    [RequireComponent(typeof(PlatformerCharacter2D))]
    public class Platformer2DUserControlPlayer2 : MonoBehaviour
    {
        private PlatformerCharacter2D m_Character;
        private bool m_Jump;


        private void Awake()
        {
            m_Character = GetComponent<PlatformerCharacter2D>();
        }

        private float direction
        {
            get
            {
                if (Input.GetKey(KeyCode.LeftArrow))
                {
                    return -1f;
                }
                else if (Input.GetKey(KeyCode.RightArrow))
                {
                    return 1f;
                }
                else
                {
                    return 0f;
                }
            }
        }

        private void Update()
        {
            if (!m_Jump)
            {
                // Read the jump input in Update so button presses aren't missed.
                //m_Jump = CrossPlatformInputManager.GetButtonDown("Jump");
                m_Jump = Input.GetKeyDown(KeyCode.UpArrow);
            }
        }


        private void FixedUpdate()
        {
            // Read the inputs.
            bool crouch = Input.GetKey(KeyCode.LeftShift);
            //float h = CrossPlatformInputManager.GetAxis("Horizontal");
            // Pass all parameters to the character control script.
            m_Character.Move(direction, crouch, m_Jump);
            m_Jump = false;
        }
    }
}
