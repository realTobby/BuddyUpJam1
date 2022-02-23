using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Interactable))]
public class NPC_ForrestGurdian : MonoBehaviour, IInteractable
{
    #region NPC_ForrestGuardian
    public bool CanGiveQuest = true;

    private Vector3 PlayerOriginalPos;

    private string[] DialogBeforeQuest;
    private string[] DialogWhileQuest;
    private string[] DialogAfterQuest;

    public void Awake()
    {
        DialogBeforeQuest = new string[] { "Welcome to our Forrest!", "I need your help!", "Please bring me some red mushrooms!", "I dont like the brown ones!" };
        DialogWhileQuest = new string[] { "The Mushrooms have a red head!", "You should be able to spot them!", "They are almost as big as you!" };
        DialogAfterQuest = new string[] { "You found the mushrooms! Thanks!", "You can now SPRINT by using SHIFT", "Talk to me when you are ready!" };

        this.GetComponent<Interactable>().SetInteraction(this);
    }
    #endregion

    #region Conversations

    private IEnumerator GiveQuestOut()
    {
        CanGiveQuest = false;

        EventSystem.Instance.StartConversation(DialogBeforeQuest);

        //CinemaController.Instance.StartCinematic(new Vector3(3.5f, 15, -16f), this.transform);
        //yield return new WaitForSeconds(0.5f);
        //DialogSystem.Instance.StartDialogSystem(DialogBeforeQuest);
        EventSystem.Instance.StartConversation(DialogBeforeQuest, new Vector3(3.5f, 15, -16f), this.transform);
        ObjectiveController.Instance.SetObjective(ObjectiveController.Instance.gameObject.AddComponent<ObjectiveCollectMushrooms>());
        yield break;
    }

    private IEnumerator GetQuestBack()
    {
        //CinemaController.Instance.StartCinematic(new Vector3(3.5f, 15, -16f), this.transform);
        //yield return new WaitForSeconds(0.5f);
        //DialogSystem.Instance.StartDialogSystem(DialogAfterQuest);
        EventSystem.Instance.StartConversation(DialogAfterQuest, new Vector3(3.5f, 15, -16f), this.transform);
        ObjectiveController.Instance.FinishObjective();
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerAbilities>().AbilityRun = true;
        yield break;
    }

    private IEnumerator TalkWhileQuest()
    {
        //CinemaController.Instance.StartCinematic(new Vector3(3.5f, 15, -16f), this.transform);
        //yield return new WaitForSeconds(0.5f);
        //DialogSystem.Instance.StartDialogSystem(DialogWhileQuest);
        EventSystem.Instance.StartConversation(DialogWhileQuest, new Vector3(3.5f, 15, -16f), this.transform);
        yield break;
    }

    private IEnumerator TalkAfterQuest()
    {
        //CinemaController.Instance.StartCinematic(new Vector3(3.5f, 15, -16f), this.transform);
        //yield return new WaitForSeconds(0.5f);
        //DialogSystem.Instance.StartDialogSystem();
        EventSystem.Instance.StartConversation(new string[] { "Now that you know how to SPRINT", "Why not use it?", "Maybe to get over a hole?", "Seek the Turtle!" }, new Vector3(3.5f, 15, -16f), this.transform);
        yield break;
    }
    #endregion

    #region IInteractable Interface Implementation

    public void ExecuteInteraction()
    {
        // now heres the deal...this guy needs multiple dialogs

        // 1 => First Meet, gives you the quest
        // 2 => Maybe you talk to him when you already have the quest but its not completed yet
        // 3 => You talk to him when you completed his quests

        if (CanGiveQuest == true)
        {
            StartCoroutine(GiveQuestOut());
        }
        else if(ObjectiveController.Instance.HasObjective() && ObjectiveController.Instance.IsObjectiveDone())
        {
            StartCoroutine(GetQuestBack());
        }
        else if(ObjectiveController.Instance.HasObjective() && ObjectiveController.Instance.IsObjectiveDone() == false)
        {
            StartCoroutine(TalkWhileQuest());
        }
        else if(ObjectiveController.Instance.HasObjective() == false)
        {
            StartCoroutine(TalkAfterQuest());
        }
    }
    #endregion
}
