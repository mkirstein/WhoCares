using UnityEngine;

namespace UnityStandardAssets._2D
{
    public class Camera2DFollow : MonoBehaviour
    {
        public Transform targetPlayer1;
        public Transform targetPlayer2;

        private Transform cameraTarget;

        private float scrollSpeed = 0.1f;

        private float timeLeft = 10;

        public float ScrollSpeed
        {
            get
            {
                return scrollSpeed;
            }
        }

        // Use this for initialization
        private void Start()
        {
            transform.parent = null;
            cameraTarget = targetPlayer1;
        }


        // Update is called once per frame
        private void Update()
        {
            transform.position = new Vector3(transform.position.x + ScrollSpeed, cameraTarget.position.y, transform.position.z);

            timeLeft -= Time.deltaTime;
            if (timeLeft < 0)
            {
                cameraTarget = (cameraTarget == targetPlayer1) ? targetPlayer2 : targetPlayer1;
                timeLeft = 10;
            }
        }
    }
}
