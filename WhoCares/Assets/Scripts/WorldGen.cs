using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WorldGen : MonoBehaviour {

    private float xAxisCoordLast;
    private float yAxisCoordLast;
    private float zAxisCoordLast;

    private List<GameObject> allPrefabs;
    private int prefabsCount;

    public GameObject[] spawnPrefab;
    
    private float currPos;
    private int lastrnd;

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
        xAxisCoordLast = 0;
        yAxisCoordLast = 0;
        zAxisCoordLast = 0;
        allPrefabs = new List<GameObject>();
        prefabsCount = 0;

        int rnd = GetRandomNumber(0, 4);
        while (rnd == lastrnd)
        {
            rnd = GetRandomNumber(0, 4);
        }
        this.lastrnd = rnd;
        GameObject chunk = Instantiate(spawnPrefab[rnd], new Vector3(0,0,0), Quaternion.Euler(0, 0, 0)) as GameObject;
        allPrefabs.Add(chunk);
        prefabsCount++;
    }

    private Vector3 spawnLocation
    {
        get
        {
            xAxisCoordLast += prefabWidth(allPrefabs[prefabsCount-1]);
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

    private float prefabWidth(GameObject gObj)
    {
        return gObj.GetComponentInChildren<Transform>().Find("Ground").GetComponent<BoxCollider2D>().bounds.size.x;
    }
    
    // Update is called sonce per frame
    public void Update()
    {
        if (cameraLocation.x + prefabWidth(allPrefabs[prefabsCount - 1]) >= currPos)
        {
            int rnd = GetRandomNumber(0, 4);
            while (rnd == lastrnd)
            {
                rnd = GetRandomNumber(0, 4);
            }
            this.lastrnd = rnd;
            GameObject chunk = Instantiate(spawnPrefab[rnd], spawnLocation, Quaternion.Euler(0, 0, 0)) as GameObject;
            allPrefabs.Add(chunk);
            prefabsCount++;

            Debug.Log("Aktuelle Breite des Level-Chunk: "+chunk.GetComponentInChildren<Transform>().Find("Ground").GetComponent<BoxCollider2D>().bounds.size.x);
            
            currPos = chunk.transform.position.x + prefabWidth(chunk);
        }
    }
}
