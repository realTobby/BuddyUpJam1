using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectiveController : MonoBehaviour
{
    #region Singleton

    private static ObjectiveController _instance = null;

    public static ObjectiveController Instance
    {
        get
        {
            return _instance;
        }
    }

    private void Awake()
    {
        _instance = this;
    }

    #endregion

    #region ObjectiveController

    IObjective CurrentObjective = null;

    public GameObject OBJECTIVE_UI;

    public TMPro.TextMeshProUGUI objectiveText;

    public void FinishObjective()
    {
        CurrentObjective = null;
        objectiveText.text = string.Empty;
        OBJECTIVE_UI.SetActive(false);
    }

    public bool HasObjective()
    {
        if (CurrentObjective == null)
            return false;
        return true;
    }

    public bool IsObjectiveDone()
    {
        return CurrentObjective.CheckForQuestCompletion();
    }

    public void SetObjective(IObjective quest)
    {
        CurrentObjective = quest;
        CurrentObjective.StartQuest();
    }

    public void IncreaseCollection()
    {
        CurrentObjective.IncreaseCollection();
    }

    public int GetQuestID() => CurrentObjective.GetQuestID();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(DialogSystem.Instance.IsCurrentlyDialogOpen == false)
        {
            if(CurrentObjective != null)
            {
                OBJECTIVE_UI.SetActive(true);

                if(CurrentObjective.CheckForQuestCompletion() == false)
                {
                    objectiveText.color = Color.white;
                    objectiveText.text = CurrentObjective.GetObjectiveTitle() + System.Environment.NewLine + CurrentObjective.GetObjectiveText() + System.Environment.NewLine + CurrentObjective.GetCollectionText();
                }
                else if(CurrentObjective.CheckForQuestCompletion() == true)
                {
                    objectiveText.color = Color.green;
                    objectiveText.text = CurrentObjective.GetObjectiveTitle() + System.Environment.NewLine + CurrentObjective.GetCompletionText();
                }

                
            }

        }
        else
        {
            OBJECTIVE_UI.SetActive(false);
        }
    }

    #endregion
}
