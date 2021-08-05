using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

[System.Serializable]
public class option
{
    public float totalGarbage;
    public float GarbageAccumelation;
    public float budget;
    public float publicOpinion;
    [TextArea]
    public string text;

    //Rewrite default constructor to please me
    public option()
    {
        totalGarbage = 0;
        GarbageAccumelation = 0;
        budget = 0;
        publicOpinion = 0;
        text = "";
    }
}

public class Event : MonoBehaviour
{
    public GameHandler gm;

    public EventParent parent;

    public string land;
    public string head;

    [TextArea]
    public string text;
    [TextArea]
    public string researchText;
    public string researchReference;


    public option yes;
    public option no;
    public option research;
    public option ignore;

    public Text Description;
    public Text Header;
    public Text researchResult;
    public Text ResearchReferenceText;

    //Variables for maximization
    public RectTransform InforPanel;

    public RectTransform test;
    public Text plusMinus;

    bool minimized = true;
    bool researched = false;
    public popup[] pop;
    public bool clicked = false;

    void Start()
    {
        minimized = true;
        researched = false;
        clicked = false;
        parent = FindObjectOfType<EventParent>();
        gm = FindObjectOfType<GameHandler>();
        Description.text = text;
        Header.text = head;
        researchResult.text = researchText;
        ResearchReferenceText.text = researchReference;
        pop = Resources.FindObjectsOfTypeAll<popup>();
    }

    public void clickYes()
    {
        pop = Resources.FindObjectsOfTypeAll<popup>();
        if (!clicked && !pop[0].inUse)
        {
            Debug.Log(pop[0].inUse);
            pop[0].gameObject.SetActive(true);
            pop[0].effect = yes;
            pop[0].myParent = this;
            pop[0].title = head;
            clicked = true;
            pop[0].inUse = true;
            Debug.Log(pop[0].inUse);
        }
    }

    public void clickNo()
    {
        pop = Resources.FindObjectsOfTypeAll<popup>();
        if (!clicked && !pop[0].inUse)
        {
            pop[0].gameObject.SetActive(true);
            pop[0].effect = no;
            pop[0].myParent = this;
            pop[0].title = head;
            clicked = true;
            pop[0].inUse = true;
        }
    }

    public void clickResearch()
    {
        //If not researched, take money.
        if (!researched)
        {
            researched = true;
            gm.Budget += research.budget;
            gm.PO += research.publicOpinion;
            gm.garbageAcc += research.GarbageAccumelation;
            gm.totalGarbage += research.totalGarbage;
        }
    }

    public void clickIgnore()
    {
        gm.Budget += ignore.budget;
        gm.PO += ignore.publicOpinion;
        gm.garbageAcc += ignore.GarbageAccumelation;
        gm.totalGarbage += ignore.totalGarbage;
        parent.RemoveEvent(this);
        Destroy(gameObject);
    }

    public void ToggleMaximizeMinimize()
    {
        //Find position in parents.currentEvents
        int index = parent.currentEvents.IndexOf(this);
        //Move all objects below this one by the amount of parents.currentEvents[#].posY - inforPanel.height;
        if (minimized)
        {
            minimized = false;
            plusMinus.text = "-";
            for (int i = index + 1; i < parent.currentEvents.Count; i++)
            {
                parent.currentEvents[i].gameObject.GetComponent<RectTransform>().localPosition += -new Vector3(0, InforPanel.rect.height - 45, 0);
            }
        }
        else
        {
            plusMinus.text = "+";
            for (int i = index + 1; i < parent.currentEvents.Count; i++)
            {
                parent.currentEvents[i].gameObject.GetComponent<RectTransform>().localPosition += new Vector3(0, InforPanel.rect.height - 45, 0);
            }
            minimized = true;
        }
    }

}
