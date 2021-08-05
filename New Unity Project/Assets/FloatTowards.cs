using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatTowards : MonoBehaviour
{
    public GameHandler gm;
    //Spawnable Objects
    public List<GameObject> Spawnables;

    //Counter
    public float timer, spawnTime, lastTime;

    public Vector3 spawndir = new Vector3(1, 0, 0);
    public float spawnScale = 1;
    public enum MyStates
    {
        beforeRunning,
        running,
        paused
    };
    public MyStates state = MyStates.paused;



    void Start()
    {
        timer = 0;
        gm = FindObjectOfType<GameHandler>();
        if (gm.turnTime != 0)
        {
            spawnTime = gm.turnTime / gm.garbageAcc;
        }
        else
        {
            spawnTime = gm.turnTime / 0.000001f;
        }
        lastTime = Time.realtimeSinceStartup;
        state = MyStates.paused;
    }

    void Update()
    {
        switch (state)
        {
            case MyStates.beforeRunning:
                if (gm.turnTime != 0)
                {
                    spawnTime = gm.turnTime / gm.garbageAcc;
                }
                else
                {
                    spawnTime = gm.turnTime / 0.000001f;
                }
                state = MyStates.running;
                break;
            case MyStates.running:
                //Update Usable Variables
                timer = Time.realtimeSinceStartup - lastTime;
                spawndir = rotateVector2(spawndir, Random.Range(0, 180.0f));
                if (timer >= spawnTime)
                {
                    //Create 3D object, and add the script(component) Plastic to it
                    Debug.Log("plastics: " + FindObjectsOfType<Plastic>().Length);
                    lastTime = Time.realtimeSinceStartup - (timer % spawnTime);
                    GameObject temp = Instantiate(Spawnables[Random.Range(0, Spawnables.Count)], new Vector3(transform.position.x, transform.position.y, transform.position.z) + new Vector3(spawndir.x, spawndir.y, 0) * spawnScale, transform.rotation);
                    temp.transform.localScale = new Vector3(0.055f, 0.055f, 0.055f);
                    Plastic a = temp.AddComponent<Plastic>();
                    //Make the plastic float towards this game object
                    a.FloatTowards(transform.position);
                    a.gm = gm;
                }
                break;
            case MyStates.paused:
                break;
        }
    }

    Vector2 rotateVector2(Vector2 vec, float degreese)
    {
        float radians = degreese * Mathf.Deg2Rad;
        float sin = Mathf.Sin(radians);
        float cos = Mathf.Cos(radians);

        return new Vector2(cos * vec.x - sin * vec.y, sin * vec.x + cos * vec.y);
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, new Vector3(transform.position.x, transform.position.y, transform.position.z) + new Vector3(spawndir.x, spawndir.y, 0) * spawnScale);
    }
}






