using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UILifecounterSpawn : MonoBehaviour {

    public GameObject LifeCounterUI;
    private GameObject spawnedObj;
    private Text lifesP1Text;
    public string LifeCounter
    {
        set
        {
            lifesP1Text.text = value;
        }
    }
    private Vector3 spawnLocation = new Vector3(0, 0, -1);
    private int counter = 0;

	// Use this for initialization
	void Start () {

        spawnedObj = Instantiate(LifeCounterUI, spawnLocation, Quaternion.Euler(0, 0, 0)) as GameObject;

        lifesP1Text = spawnedObj.GetComponentInChildren<Transform>().Find("LifesP1TextSingleplayer")
            .GetComponent<Transform>().Find("LifesP1AmountText")
            .GetComponent<Text>();
        lifesP1Text.text = "";

    }
	
	// Update is called once per frame
	void Update () {
	}
}
