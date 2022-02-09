using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IObjective
{
    string GetCollectionText();

    string GetObjectiveTitle();

    string GetObjectiveText();

    void StartQuest();

    void IncreaseCollection();

    string GetObjectiveNPC();
    bool CheckForQuestCompletion();

}
