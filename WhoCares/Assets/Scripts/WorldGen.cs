using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WorldGen : MonoBehaviour {

    // Das sind die Koordinaten von dem neustem Prefab was erstellt wurde
    private float xAxisCoordLast;
    private float yAxisCoordLast;
    private float zAxisCoordLast;

    // Hier werden alle Weltabschnitte gespeichert
    private List<GameObject> allPrefabs;
    //Liste aller Hintergründe
    private List<GameObject> allBackgrounds;

    // Hier werden die zu spawnenden Prefabs gespeichert
    // Man zieht diese dazu im Inspektor da rein
    public GameObject[] spawnPrefab;
    //Array aller Hintergründe
    public GameObject[] spawnBackground;
    
    // aktuelle x Koordinate des neuesten Prefabs
    private float currPos;

    // die letzte Zufallszahl die berechnet wurde, damit wir nicht 2 gleiche Chunks hintereinander erzeugen
    private int lastrnd;

    // Berechnung von Zufallszahlen mit geeigneter Sperrsynchronisation
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
        // Am Anfang soll das erste Levelchunk auf dem Vectord3(0,0,0) gespawned werden
        xAxisCoordLast = 0;
        yAxisCoordLast = 0;
        zAxisCoordLast = 0;

        // Initialisiere die Liste der Levelchunks
        allPrefabs = new List<GameObject>();
        allBackgrounds = new List<GameObject>();

        // Bekomme eine "zufällige" Zahl
        int rndPrefab = GetRandomNumber(0, spawnPrefab.Length);
        while (rndPrefab == lastrnd)
        {
            rndPrefab = GetRandomNumber(0, spawnPrefab.Length);
        }
        this.lastrnd = rndPrefab;

        // Berechne eine andere Zahl um die Hintergründe anzusprechen
        int rndBackground = GetRandomNumber(0, spawnBackground.Length);

        // Spawne das erste Levelchunk
        GameObject chunk = Instantiate(spawnPrefab[rndPrefab], new Vector3(0,0,0), Quaternion.Euler(0, 0, 0)) as GameObject;
        //Spawne den ersten Hintergrund
        GameObject background = Instantiate(spawnBackground[rndBackground],spawnBackground[rndBackground].transform.position, Quaternion.Euler(0, 0, 0)) as GameObject;
        //Spawne den zweiten Hintergrund
        GameObject background1 = Instantiate(spawnBackground[spawnBackground.Length - 1 - rndBackground], spawnBackground[spawnPrefab.Length -1 - rndBackground].transform.position, Quaternion.Euler(0, 0, 0)) as GameObject;
        // Füge das erste Levelchunk der Liste aller Chunks hinzu
        allPrefabs.Add(chunk);
        //Füge die Hintergründe in die Liste der Hintergründe
        allBackgrounds.Add(background);
        allBackgrounds.Add(background1);
    }

    // Berechnet eine neue spawnLocation welche hinter dem letzten Prefab ist
    private Vector3 spawnLocation
    {
        get
        {
            xAxisCoordLast += prefabWidth(allPrefabs[allPrefabs.Count-1]);
            return new Vector3(xAxisCoordLast, yAxisCoordLast, zAxisCoordLast);
        } 
    }

    // Gibt einfach nur die aktuelle Kameraposition zurück
    private Vector3 cameraLocation
    {
        get
        {
            return Camera.main.gameObject.transform.position;
        }
    }

    // Berechnet die Breite des GameObjects auf der x Achse indem wir die Breite des Colliders des Elements Ground nehmen
    private float prefabWidth(GameObject gObj)
    {
        return gObj.GetComponentInChildren<Transform>().Find("Ground").GetComponent<BoxCollider2D>().bounds.size.x;
    }
    
    // Update is called sonce per frame
    public void Update()
    {
        // Wenn die x Koordnate meiner Kamera + Breite des aktuellen Prefabs größer oder gleich meiner aktuellen x Koordinate des aktuellen Prefabs ist, dann erstelle eine neue Kopie dahinter
        if (cameraLocation.x + prefabWidth(allPrefabs[allPrefabs.Count - 1]) >= currPos)
        {
            // Wenn die Liste aller Kopien die gespawned wurde zu groß wird, lösche die älteste Kopie
            if (allPrefabs.Count >= 5)
            {
                // Erst zerstören, danach die Referenz löschen -> perfect crime
                Destroy(allPrefabs[0]);
                allPrefabs.RemoveAt(0);
            }

            if (allBackgrounds.Count >=10)
            {
                Destroy(allBackgrounds[0]);
                allBackgrounds.RemoveAt(0);
            }

            // erstellt eine "Zufall"szahl und stellt danach sicher, dass diese unterschiedlich von der Zufallszahl davor ist, um etwas Abwechslung in die Level-Generierung zu bringen
            int rnd = GetRandomNumber(0, 4);
            while (rnd == lastrnd)
            {
                rnd = GetRandomNumber(0, 4);
            }
            this.lastrnd = rnd;

            // Wir holen uns die neue Spawnlocation
            Vector3 spLoc = spawnLocation;

            // Spawne eine Kopie von dem "zufällig" ausgewählten Prefab
            GameObject chunk = Instantiate(spawnPrefab[rnd], spLoc, Quaternion.Euler(0, 0, 0)) as GameObject;
            // Füge sie meiner Liste aller Kopien hinzu
            allPrefabs.Add(chunk);

            // Spawne eine Kopie von dem "zufällig" ausgewählten Background an neuer x-Position
            GameObject background = Instantiate(spawnBackground[rnd], new Vector3(spLoc.x, spawnBackground[rnd].transform.position.y, spawnBackground[rnd].transform.position.z), Quaternion.Euler(0, 0, 0)) as GameObject;
            //// Füge sie meiner Liste aller Kopien hinzu
            allBackgrounds.Add(background);

            // Debug Meldung dass ein Chunk erstellt wurde
            //Debug.Log("Aktuelle Breite des Level-Chunk: "+ prefabWidth(chunk));
            
            // Speichert die x Koordinate der aktuellen Position um Sie dann abzufragen ob ein neuer Chunk instanziert werden soll
            currPos = chunk.transform.position.x + prefabWidth(chunk);
        }
    }
}
