using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectiveCollectMushrooms : MonoBehaviour, IObjective
{
    public QuestDataModel myQuestData;

    public int MaxMushrooms = 6;
    public int CurrentMushrooms = 0;

    public void Awake()
    {
        myQuestData = new QuestDataModel();
        myQuestData.ID = 1;
        myQuestData.Title = "The Forrest Guardian needs your help!";
        myQuestData.Description = "Collect 6 Mushrooms!";
        myQuestData.MaxCollected = 6;
        myQuestData.CurrentCollected = 0;
        myQuestData.CompletionText = "Return to the Forrest Guardian!";

    }

    public int GetQuestID() => myQuestData.ID;

    public string GetObjectiveTitle() => myQuestData.Title;

    public string GetObjectiveText() => myQuestData.Description;

    public string GetCollectionText() => myQuestData.CurrentCollected + " / " + myQuestData.MaxCollected;

    public void IncreaseCollection() => myQuestData.CurrentCollected += 1;

    public string GetCompletionText() => myQuestData.CompletionText;

    public bool CheckForQuestCompletion() => myQuestData.CurrentCollected >= myQuestData.MaxCollected;

    public void StartQuest()
    {
        var questList = GameObject.FindGameObjectWithTag("QUESTS");
        questList.transform.GetChild(1).gameObject.SetActive(true);
    }
}
