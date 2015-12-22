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

        public float speed = 1f;

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
                            return -1f * speed;
                        }
                        else if (Input.GetKey(KeyCode.D))
                        {
                            return 1f * speed;
                        }
                        else
                        {
                            return 0f;
                        }
                    } else if (playerId == 1)
                    {
                        if (Input.GetKey(KeyCode.LeftArrow))
                        {
                            return -1f * speed;
                        }
                        else if (Input.GetKey(KeyCode.RightArrow))
                        {
                            return 1f * speed;
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
                    //return CrossPlatformInputManager.GetAxis("Horizontal");
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

        private bool crouch
        {
            get
            {
                if (!isSingleplayer)
                {
                    if (playerId == 0)
                    {
                        return Input.GetKey(KeyCode.LeftShift);
                    }
                    else if (playerId == 1)
                    {
                        return Input.GetKey(KeyCode.RightShift);
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift);
                }
            }
        }

        private void FixedUpdate()
        {
            // Pass all parameters to the character control script.
            m_Character.Move(direction, crouch, m_Jump);
            m_Jump = false;
        }
    }
}
