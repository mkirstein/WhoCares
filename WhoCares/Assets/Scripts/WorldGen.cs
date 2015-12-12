using UnityEngine;
using System.Collections;

public class WorldGen : MonoBehaviour {

    private float xAxisCoordLast;
    private float yAxisCoordLast;
    private float zAxisCoordLast;

    public GameObject[] spawnPrefab;
    public GameObject[] spawnClone;

    private int prefabsRendered;
    private Vector3 currPos;

    private static readonly System.Random getrandom = new System.Random();
    private static readonly object syncLock = new object();
    public static int GetRandomNumber(int min, int max)
    {
        lock (syncLock)
        { // synchronize
            return getrandom.Next(min, max);
        }
    }

    // Use this for initialization
    void Start () {
        xAxisCoordLast = -3f;
        yAxisCoordLast = 0;
        zAxisCoordLast = 0;
        prefabsRendered = 1;
	}

    private Vector3 spawnLocation
    {
        get
        {
            xAxisCoordLast += 35f;
            return new Vector3(xAxisCoordLast, yAxisCoordLast, zAxisCoordLast);
        } 
    }

    private Vector3 cameraLocation
    {
        get
        {
            return Camera.main.gameObject.transform.position;
        }
    }

    private bool renderPrefab
    {
        get
        {
            if (cameraLocation[0] + 35 > currPos[0])
            {
                return true;
            } 
            else
            {
                return false;
            } 
        }
    }
    // Update is called sonce per frame
    public void Update()
    {
        if (renderPrefab)
        {
            int rnd = GetRandomNumber(0, 3);
            spawnClone[rnd] = Instantiate(spawnPrefab[rnd], spawnLocation, Quaternion.Euler(0, 0, 0)) as GameObject;
            currPos = spawnClone[rnd].transform.position;
            prefabsRendered += 1;
        }
    }
}
