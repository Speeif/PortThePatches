using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class clickedBehaviour : MonoBehaviour
{

    public Color defaultColor;
    public Color mousedColor;
    public Color clickedColor;
    // Start is called before the first frame update
    [HideInInspector]
    public Collider coll;
    [HideInInspector]
    public SpriteRenderer sr;

    void Start()
    {
        coll = GetComponent<Collider>();
        sr = GetComponent<SpriteRenderer>();

        defaultColor.a = 1;
        mousedColor.a = 1;
        clickedColor.a = 1;

    }


    void OnMouseDown()
    {
        sr.color = clickedColor;
        Debug.Log("I'm pushed!");
    }

    void OnMouseEnter()
    {
        sr.color = mousedColor;
    }

    void OnMouseExit()
    {
        sr.color = defaultColor;
    }
}
