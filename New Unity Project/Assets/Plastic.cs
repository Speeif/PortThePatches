using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plastic : MonoBehaviour
{

    Vector2 MoveDir = new Vector2(0, 0);
    float floatingCounter = 0;
    public bool inactive = true;
    float startRotation;
    Vector3 Target;
    public GameHandler gm;
    void OnTriggerEnter(Collider coll)
    {
        if (!inactive && coll.gameObject.tag == "plastic" && coll.gameObject.GetComponent<Plastic>().inactive)
        {
            inactive = true;
            gm.totalGarbage++;
            gm.plastics.Add(gameObject);
        }
    }

    void OnEnable()
    {
        startRotation = transform.rotation.x;
    }

    void Update()
    {
        if (!inactive)
        {
            floatingCounter += Time.deltaTime;
            transform.position += (Vector3)MoveDir * Time.deltaTime * 2;
            transform.localRotation = Quaternion.Euler(Mathf.Cos(floatingCounter * 10) * 12 + 180, 0, 0);
            if (Vector3.Distance(transform.position, Target) <= 0.1f)
            {
                inactive = true;
                gm.totalGarbage++;
                gm.plastics.Add(gameObject);
            }
        }
    }
    public void FloatTowards(Vector3 dir)
    {
        Target = dir;
        MoveDir = Target - transform.position;
        inactive = false;
    }
}
