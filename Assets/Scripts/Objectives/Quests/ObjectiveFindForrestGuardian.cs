using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectiveFindForrestGuardian : MonoBehaviour, IObjective
{
    public QuestDataModel myQuestData;

    public void Awake()
    {
        myQuestData = new QuestDataModel();
        myQuestData.ID = 0;
        myQuestData.Title = "Big World";
        myQuestData.Description = "Find a way out!";
        myQuestData.MaxCollected = 2;
        myQuestData.CurrentCollected = 0;
        myQuestData.CompletionText = "Talk to the forrest guardian!";

    }

    public int GetQuestID() => myQuestData.ID;

    public bool CheckForQuestCompletion() => myQuestData.CurrentCollected >= myQuestData.MaxCollected;

    public string GetCollectionText() => "Checkpoints: " + myQuestData.CurrentCollected + " / " + myQuestData.MaxCollected;

    public string GetCompletionText()=> myQuestData.CompletionText;

    public string GetObjectiveText() => myQuestData.Description;

    public string GetObjectiveTitle() => myQuestData.Title;

    public void IncreaseCollection() => myQuestData.CurrentCollected += 1;

    public void StartQuest()
    {
        var questList = GameObject.FindGameObjectWithTag("QUESTS");
        questList.transform.GetChild(0).gameObject.SetActive(true);
    }
}
