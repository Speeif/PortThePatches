using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameHandler : MonoBehaviour
{
    public FloatTowards GarbageSpawner;
    public EventParent eventsParent;
    public GameObject eventList;
    [HideInInspector]
    public List<Event> events;
    // Start is called before the first frame update
    //Variables for stat adjustment

    public float garbageAcc, totalGarbage, Budget, PO;
    public List<GameObject> plastics = new List<GameObject>();

    public enum GameStates

    {
        opening,
        pausing,
        beforePausing,
        beforeRunning,
        running,
        ending,
        empty
    };
    public GameStates state = GameStates.opening;

    public float currentTime = 0;
    float timeBegin;
    public float turnTime = 10;

    public Text publicOpinionText;
    public Text budgetText;
    public Text garbageAccumulationText;
    public Text totalGarbageText;

    float finalScore = 0;
    public Text finalScoreText;
    public GameObject endscreen, startscreen;

    void Start()
    {
        //Find and add all the written events into the game
        if (eventList != null)
        {
            for (int i = 0; i < eventList.transform.childCount; i++)
            {
                Event a = eventList.transform.GetChild(i).GetComponent<Event>();
                events.Add(a);
            }
        }
        //Find other dependency modifiers
        if (!(GarbageSpawner != null))
        {
            GarbageSpawner = FindObjectOfType<FloatTowards>();
        }
        eventsParent = FindObjectOfType<EventParent>();
        //Set variable for initialization
        state = GameStates.opening;
        startscreen.SetActive(true);
        currentTime = 0;
    }

    // Update is called once per frame
    void Update()
    {
        switch (state)
        {
            case GameStates.opening:
                break;
            case GameStates.beforeRunning:
                //Set variables
                GarbageSpawner.state = FloatTowards.MyStates.beforeRunning;
                timeBegin = Time.realtimeSinceStartup;
                currentTime = 0;
                state = GameStates.running;
                break;
            case GameStates.running:
                //Time since the day cyclus began. 
                currentTime = Time.realtimeSinceStartup - timeBegin;
                //Check if real time exceeds turnTime threshold.
                if (currentTime >= turnTime)
                {
                    state = GameStates.beforePausing;
                    currentTime = 0;
                }
                //Scale the time by a cosine function using realtime, so that the game may spand over turntime
                Time.timeScale = (Mathf.Cos(1 * Mathf.PI + 2 * Mathf.PI * currentTime / turnTime) + 1);
                break;
            case GameStates.beforePausing:
                //Set gain and variable for current day
                Budget += PO / 100 * 1000;
                Time.timeScale = 0;
                //Spawn a destined amount of events
                for (int i = 0; i < 2; i++)
                {
                    if (events.Count == 0) break;
                    eventsParent.CreateEvent(events[0]);
                    events.Remove(events[0]);
                }
                GarbageSpawner.state = FloatTowards.MyStates.paused;
                state = GameStates.pausing;
                break;
            case GameStates.pausing:
                //No current events in session? Begin running once again
                if (eventsParent.currentEvents.Count == 0)
                {
                    state = GameStates.beforeRunning;
                }
                break;
            case GameStates.ending:
                //Display end screen & score. Wait for user input.
                endscreen.SetActive(true);
                finalScore = (PO / 100 * Budget) - (garbageAcc / 100 * totalGarbage);
                finalScoreText.text = "Your score became:\n" + finalScore;
                state = GameStates.empty;
                break;
            case GameStates.empty: break;
        }

        if (eventsParent.currentEvents.Count == 0 && events.Count == 0)
        {
            //End game if there are no more events
            state = GameStates.ending;
        }

        //Update resources
        publicOpinionText.text = PO.ToString() + "%";
        //update budget
        budgetText.text = "$" + Budget.ToString();
        //update garbage accumulation
        garbageAccumulationText.text = garbageAcc.ToString() + " pr.dag";
        //udate amount of garbage
        totalGarbageText.text = totalGarbage.ToString();
    }

    public void deletePlastics(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            //Place exception
            if (plastics.Count == 0)
            {
                break;
            }
            plastics.RemoveAt(plastics.Count - 1);
            Destroy(plastics[plastics.Count - 1].gameObject);
        }
    }

    public void startGame()
    {
        state = GameStates.beforeRunning;
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
