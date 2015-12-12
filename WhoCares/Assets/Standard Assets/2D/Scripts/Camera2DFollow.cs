using UnityEngine;

namespace UnityStandardAssets._2D
{
    public class Camera2DFollow : MonoBehaviour
    {
        public Transform target;

        // Use this for initialization
        private void Start()
        {
            transform.parent = null;
        }


        // Update is called once per frame
        private void Update()
        {
            transform.position = new Vector3(transform.position.x + 0.1f, target.position.y, transform.position.z);
        }
    }
}
