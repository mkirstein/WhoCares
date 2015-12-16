using System;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

namespace UnityStandardAssets._2D
{
    [RequireComponent(typeof (PlatformerCharacter2D))]
    public class Platformer2DUserControl : MonoBehaviour
    {
        private PlatformerCharacter2D m_Character;
        private bool m_Jump;
        public bool isSingleplayer;
        public int playerId;

        private void Awake()
        {
            m_Character = GetComponent<PlatformerCharacter2D>();
        }


        private void Update()
        {
            if (!m_Jump)
            {
                // Read the jump input in Update so button presses aren't missed.
                if (!isSingleplayer)
                {
                    if (playerId == 0)
                    {
                        m_Jump = Input.GetKeyDown(KeyCode.W);
                    } else if (playerId == 1)
                    {
                        m_Jump = Input.GetKeyDown(KeyCode.UpArrow);
                    }
                } else
                {
                    m_Jump = Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow);
                }
            }
        }

        private float direction
        {
            get
            {
                if (!isSingleplayer)
                {
                    if (playerId == 0)
                    {
                        if (Input.GetKey(KeyCode.A))
                        {
                            return -1f;
                        }
                        else if (Input.GetKey(KeyCode.D))
                        {
                            return 1f;
                        }
                        else
                        {
                            return 0f;
                        }
                    } else if (playerId == 1)
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
                    } else
                    {
                        return 0f;
                    }
                } else
                {
                    if ((Input.GetKey(KeyCode.A)) || (Input.GetKey(KeyCode.LeftArrow)))
                    {
                        return -1f;
                    }
                    else if ((Input.GetKey(KeyCode.D)) || (Input.GetKey(KeyCode.RightArrow)))
                    {
                        return 1f;
                    }
                    else
                    {
                        return 0f;
                    }
                }
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
