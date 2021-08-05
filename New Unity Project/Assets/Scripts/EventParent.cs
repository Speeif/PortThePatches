using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventParent : MonoBehaviour
{
    public GameHandler gm;
    public List<Event> currentEvents;
    public List<popup> currentPopups;
    public GameObject spawnable;

    public void CreateEvent(Event e)
    {
        GameObject temp = Instantiate(spawnable, new Vector2(0, 0), new Quaternion(0, 0, 0, 1));
        Event eve = temp.GetComponent<Event>();
        //Update Text
        eve.land = e.land;
        eve.head = e.head;
        eve.text = e.text;
        eve.researchText = e.researchText;
        eve.yes = e.yes;
        eve.no = e.no;
        eve.research = e.research;
        eve.ignore = e.ignore;
        eve.transform.SetParent(this.gameObject.transform);
        eve.GetComponent<RectTransform>().localPosition += new Vector3(0, -50 * currentEvents.Count + 35, 0);
        currentEvents.Add(eve);
    }
    public void RemoveEvent(Event e)
    {
        if (currentEvents.IndexOf(e) < currentEvents.Count)
        {
            for (int i = currentEvents.IndexOf(e) + 1; i < currentEvents.Count; i++)
            {
                currentEvents[i].GetComponent<RectTransform>().localPosition += new Vector3(0, e.InforPanel.rect.height, 0);
            }
        }
        currentEvents.Remove(e);
    }
}
