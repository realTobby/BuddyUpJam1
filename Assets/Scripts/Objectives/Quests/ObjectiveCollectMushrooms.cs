using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectiveCollectMushrooms : MonoBehaviour, IObjective
{
    public int MaxMushrooms = 6;
    public int CurrentMushrooms = 0;

   
    public string GetObjectiveTitle()
    {
        return "Forrest Guardian needs your help!";
    }

    public string GetObjectiveText()
    {
        return "Collect 6 Mushrooms!";
    }

    public string GetCollectionText()
    {
        return CurrentMushrooms + " / " + MaxMushrooms;
    }

    public void IncreaseCollection()
    {
        CurrentMushrooms++;
    }

    public string GetObjectiveNPC()
    {
        return "Forrest Guardian";
    }

    public bool CheckForQuestCompletion()
    {
        if(CurrentMushrooms >= MaxMushrooms)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void StartQuest()
    {
        var questList = GameObject.FindGameObjectWithTag("QUESTS");

        questList.transform.GetChild(0).gameObject.SetActive(true);
    }
}
