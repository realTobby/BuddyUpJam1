using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IObjective
{
    int GetQuestID();

    string GetCollectionText();
    string GetObjectiveTitle();
    string GetObjectiveText();
    void StartQuest();
    void EndQuest();
    void IncreaseCollection();
    bool CheckForQuestCompletion();
    string GetCompletionText();



}
