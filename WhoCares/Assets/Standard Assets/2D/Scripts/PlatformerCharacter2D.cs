using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using System;

namespace UnityStandardAssets._2D
{
    public class PlatformerCharacter2D : MonoBehaviour
    {
        [SerializeField]
        private float m_MaxSpeed = 10f;                    // The fastest the player can travel in the x axis.
        [SerializeField]
        private float m_JumpForce = 400f;                  // Amount of force added when the player jumps.
        [Range(0, 1)]
        [SerializeField]
        private float m_CrouchSpeed = .36f;  // Amount of maxSpeed applied to crouching movement. 1 = 100%
        [SerializeField]
        private bool m_AirControl = false;                 // Whether or not a player can steer while jumping;
        [SerializeField]
        private LayerMask m_WhatIsGround;                  // A mask determining what is ground to the character

        private Transform m_GroundCheck;    // A position marking where to check if the player is grounded.
        const float k_GroundedRadius = .2f; // Radius of the overlap circle to determine if grounded
        private bool m_Grounded;            // Whether or not the player is grounded.
        private Transform m_CeilingCheck;   // A position marking where to check for ceilings
        const float k_CeilingRadius = .01f; // Radius of the overlap circle to determine if the player can stand up
        private Animator m_Anim;            // Reference to the player's animator component.
        private Rigidbody2D m_Rigidbody2D;
        private bool m_FacingRight = true;  // For determining which way the player is currently facing.
        public GameObject character;        // Der eigene Character
        private bool dead = false;

        public bool isSingleplayer;
        // Custom
        private int maxJumps = 2;
        public int MaxJumps
        {
            get
            {
                return maxJumps;
            }

            set
            {
                maxJumps = value;
            }
        }
        private volatile int jumpCount = 0;         // JumpCounter for double-jump

        [SerializeField]
        private int playerID;
        [SerializeField]
        private Text lifeCounterText;
        private int lifeCounter = 5;
        private float highscore = 0f;
        private AudioEffects ae;
        

        private void Awake()
        {
            // Setting up references.
            m_GroundCheck = transform.Find("GroundCheck");
            m_CeilingCheck = transform.Find("CeilingCheck");
            m_Anim = GetComponent<Animator>();
            m_Rigidbody2D = GetComponent<Rigidbody2D>();

            //Um die Audio-Effekte abzuspielen
            ae = GameObject.Find("Audio").GetComponent("AudioEffects") as AudioEffects;


            jumpCount = this.MaxJumps;
        }


        private void FixedUpdate()
        {
            m_Grounded = false;

            if (highscore < character.transform.position.x)
            {
                highscore = character.transform.position.x;
            }

            // The player is grounded if a circlecast to the groundcheck position hits anything designated as ground
            // This can be done using layers instead but Sample Assets will not overwrite your project settings.
            Collider2D[] colliders = Physics2D.OverlapCircleAll(m_GroundCheck.position, k_GroundedRadius, m_WhatIsGround);
            for (int i = 0; i < colliders.Length; i++)
            {
                if (colliders[i].gameObject != gameObject)
                {
                    m_Grounded = true;
                }
            }
            m_Anim.SetBool("Ground", m_Grounded);

            // Set the vertical animation
            m_Anim.SetFloat("vSpeed", m_Rigidbody2D.velocity.y);
        }

        public void Move(float move, bool crouch, bool jump)
        {
            // If crouching, check to see if the character can stand up
            if (!crouch && m_Anim.GetBool("Crouch"))
            {
                // If the character has a ceiling preventing them from standing up, keep them crouching
                if (Physics2D.OverlapCircle(m_CeilingCheck.position, k_CeilingRadius, m_WhatIsGround))
                {
                    crouch = true;
                }
            }

            // Set whether or not the character is crouching in the animator
            m_Anim.SetBool("Crouch", crouch);

            //only control the player if grounded or airControl is turned on
            if (m_Grounded || m_AirControl)
            {
                // Reduce the speed if crouching by the crouchSpeed multiplier
                move = (crouch ? move * m_CrouchSpeed : move);

                // The Speed animator parameter is set to the absolute value of the horizontal input.
                m_Anim.SetFloat("Speed", Mathf.Abs(move));

                // Move the character
                m_Rigidbody2D.velocity = new Vector2(move * m_MaxSpeed, m_Rigidbody2D.velocity.y);

                // If the input is moving the player right and the player is facing left...
                if (move > 0 && !m_FacingRight)
                {
                    // ... flip the player.
                    Flip();
                }
                // Otherwise if the input is moving the player left and the player is facing right...
                else if (move < 0 && m_FacingRight)
                {
                    // ... flip the player.
                    Flip();
                }
            }

            if (jump)
            {
                // If the player should jump...
                if (m_Grounded && m_Anim.GetBool("Ground"))
                {
                    // Add a vertical force to the player.
                    m_Grounded = false;
                    m_Anim.SetBool("Ground", false);
                    jumpCount = this.MaxJumps;
                }
                if (jumpCount > 0)
                {
                    m_Rigidbody2D.velocity = new Vector2(m_Rigidbody2D.velocity.x, 0);
                    m_Rigidbody2D.AddForce(new Vector2(0f, m_JumpForce));
                    // Reduce max jumps in the air (double jump)
                    jumpCount = jumpCount - 1;
                    if ((this.playerID == 0)&&(jumpCount == 0))
                    {
                        // Audio abspielen bei Sprung
                        ae.p1jump();
                    }
                    if ((this.playerID == 1)&& (jumpCount == 0))
                    {
                        // Audio abspielen bei Sprung
                        ae.p2jump();
                    }
                    
                }
            }

        }

        private void Flip()
        {
            // Switch the way the player is labelled as facing.
            m_FacingRight = !m_FacingRight;

            // Multiply the player's x local scale by -1.
            Vector3 theScale = transform.localScale;
            theScale.x *= -1;
            transform.localScale = theScale;
        }

        void OnBecameInvisible()
        {
            UpdateLifeCounter();
            Respawn();
        }

        private void Respawn()
        {
            if ((dead == false)&&(character.activeInHierarchy == true)&&(GameObject.Find("Main Camera") != null))
            {
                ae.die();
                Vector3 camPos = GameObject.Find("Main Camera").transform.position;
                Vector3 respawnPos = new Vector3(camPos.x + 5, camPos.y + 3, gameObject.transform.position.z);
                gameObject.transform.position = respawnPos;
                jumpCount = this.MaxJumps;
            }
        }

        private void UpdateLifeCounter()
        {
            lifeCounter--;
            if(lifeCounter < 0)
            {
                //Damit das nachfolgende nur 1 Mal ausgeführt wird
                if (!dead)
                {
                    dead = true;
                    if (!isSingleplayer)
                    {

                        switch (playerID)
                        {
                            case 0:
                                SceneManager.LoadScene("Player2Win");
                                break;
                            case 1:
                                SceneManager.LoadScene("Player1Win");
                                break;
                        }
                        return;
                    }
                    else
                    {
                        // Highscore update
                        Highscores hs = GameObject.Find("Scores").GetComponent("Highscores") as Highscores;
                        hs.Highscore = (int) highscore;
                        SceneManager.LoadScene("SingleplayerLost");
                    }
                }
            }
            lifeCounterText.text = lifeCounter.ToString();
        }

        public void PowerUp(PowerUp.PowerUps powUp)
        {
            Debug.Log("PowerUp!!!");
        }

    }
}
