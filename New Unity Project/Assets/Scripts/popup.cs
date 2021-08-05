using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class popup : MonoBehaviour
{
    [TextArea]
    public string title, text;

    public Text poText, budgetText, garbageaccText, garbageText, titleText, textText;
    public SpriteRenderer poRend, budgetRend, garbageaccRend, garbageRend;
    public option effect;
    public GameHandler gm;
    public EventParent parent;
    public Event myParent;

    public bool inUse = false;
    // Start is called before the first frame update
    void Start()
    {
        effect = new option();
        gm = FindObjectOfType<GameHandler>();
        parent = FindObjectOfType<EventParent>();
        inUse = false;
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

        titleText.text = title;
        textText.text = effect.text;
        poText.text = effect.publicOpinion.ToString() + "%";
        budgetText.text = "$" + effect.budget;
        garbageaccText.text = effect.GarbageAccumelation.ToString();
        garbageText.text = effect.totalGarbage.ToString();

        if (effect.publicOpinion < 0)
        {
            poText.color = Color.red;
            poRend.color = Color.red;
        }
        else if (effect.publicOpinion > 0)
        {
            poText.color = Color.green;
            poRend.color = Color.green;
        }
        else
        {
            poText.color = Color.black;
            poRend.color = Color.gray;
        }

        if (effect.budget < 0)
        {
            budgetText.color = Color.red;
            budgetRend.color = Color.red;
        }
        else if (effect.budget > 0)
        {
            budgetText.color = Color.green;
            budgetRend.color = Color.green;
        }
        else
        {
            budgetText.color = Color.black;
            budgetRend.color = Color.gray;
        }

        if (effect.totalGarbage > 0)
        {
            garbageText.color = Color.red;
            garbageRend.color = Color.red;
        }
        else if (effect.totalGarbage < 0)
        {
            garbageText.color = Color.green;
            garbageRend.color = Color.green;
        }
        else
        {
            garbageText.color = Color.black;
            garbageRend.color = Color.gray;
        }

        if (effect.GarbageAccumelation > 0)
        {
            garbageaccText.color = Color.red;
            garbageaccRend.color = Color.red;
        }
        else if (effect.GarbageAccumelation < 0)
        {
            garbageaccText.color = Color.green;
            garbageaccRend.color = Color.green;
        }
        else
        {
            garbageaccText.color = Color.black;
            garbageaccRend.color = Color.gray;
        }
    }

    public void SendOptionData()
    {
        gm.garbageAcc += effect.GarbageAccumelation;
        gm.totalGarbage += effect.totalGarbage;
        gm.Budget += effect.budget;
        gm.PO += effect.publicOpinion;
        if (effect.totalGarbage < 0)
        {
            gm.deletePlastics((int)(Mathf.Sqrt(Mathf.Pow(effect.totalGarbage, 2))));
        }
        parent.RemoveEvent(myParent);
        Destroy(myParent.gameObject);
        inUse = false;
        gameObject.SetActive(false);
    }
}
