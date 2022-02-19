using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC_ForrestGurdian : MonoBehaviour, IInteractable
{
    private Interactable myParentInteractable;

    private Vector3 PlayerOriginalPos;

    string[] DialogBeforeQuest;
    string[] DialogWhileQuest;
    string[] DialogAfterQuest;

    public bool CanGiveQuest = true;

    public GameObject demoobject;

    private IEnumerator GetQuest()
    {
        CanGiveQuest = false;
        CinemaController.Instance.StartCinematic(new Vector3(3.5f, 15, -16f), this.transform);
        yield return new WaitForSeconds(0.5f);
        DialogSystem.Instance.StartDialogSystem(DialogBeforeQuest);
        ObjectiveController.Instance.SetObjective(ObjectiveController.Instance.gameObject.AddComponent<ObjectiveCollectMushrooms>());
        yield break;
    }

    private IEnumerator TurnInQuest()
    {
        CinemaController.Instance.StartCinematic(new Vector3(3.5f, 15, -16f), this.transform);
        yield return new WaitForSeconds(0.5f);
        DialogSystem.Instance.StartDialogSystem(DialogAfterQuest);
        ObjectiveController.Instance.FinishObjective();
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerAbilities>().AbilityRun = true;
        yield break;
    }

    private IEnumerator TalkWhileQuest()
    {
        CinemaController.Instance.StartCinematic(new Vector3(3.5f, 15, -16f), this.transform);
        yield return new WaitForSeconds(0.5f);
        DialogSystem.Instance.StartDialogSystem(DialogWhileQuest);
        yield break;
    }

    private IEnumerator ThankDemoText()
    {
        CinemaController.Instance.StartCinematic(new Vector3(3.5f, 15, -16f), this.transform);
        yield return new WaitForSeconds(0.5f);
        DialogSystem.Instance.StartDialogSystem(new string[] {"You reached the true ending!", "(Try talking to the red wall)"});

        // destroy this npc
        Destroy(this.gameObject);




        yield break;
    }

    public void ExecuteInteraction()
    {
        // now heres the deal...this guy needs multiple dialogs

        // 1 => First Meet, gives you the quest
        // 2 => Maybe you talk to him when you already have the quest but its not completed yet
        // 3 => You talk to him when you completed his quests

        if(CanGiveQuest == true)
        {
            StartCoroutine(GetQuest());
        }
        else if(ObjectiveController.Instance.HasObjective() && ObjectiveController.Instance.IsObjectiveDone())
        {
            StartCoroutine(TurnInQuest());
        }else if(ObjectiveController.Instance.HasObjective() && ObjectiveController.Instance.IsObjectiveDone() == false)
        {
            StartCoroutine(TalkWhileQuest());
        }else if(ObjectiveController.Instance.HasObjective() == false)
        {
            StartCoroutine(ThankDemoText());
        }

    }

  

    public string GetInteractionKey()
    {
        return "F";
    }
    public string GetInteractionType()
    {
        return "talk";
    }

    public string GetInteractionTitle()
    {
        return "to Forrest Guardian";
    }

    public void InitInteractions(Interactable parent)
    {
        myParentInteractable = parent;
    }

    public void SetDialog(string[] lines)
    {
        //DialogBeforeQuest = lines;
        DialogBeforeQuest = new string[] { "Welcome to our Forrest!", "I need your help!", "Please bring me some mushrooms!" };
        DialogWhileQuest = new string[] { "The Mushrooms have a red head!", "You should be able to spot them!", "They are almost as big as you!" };
        DialogAfterQuest = new string[] { "You found the mushrooms! Thanks!", "You can now SPRINT by using SHIFT", "Thank you for playing! (talk to me again)"};
    }
}
