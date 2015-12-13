using UnityEngine;
using System.Collections;

public class MoveBackground : MonoBehaviour {

    public float movespeed = 0.05f;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        //movespeed++;
        transform.position = new Vector3(transform.position.x + movespeed, transform.position.y);
    }
}
