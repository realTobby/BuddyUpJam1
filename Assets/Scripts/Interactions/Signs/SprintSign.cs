using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SprintSign : MonoBehaviour, IInteractable
{
    public Interactable myInteractionParent;

    public string[] cannotSprintDialog;

    public string[] canSprintDialog;

    public string[] dialog;

    private IEnumerator CannotSprint()
    {
        CinemaController.Instance.StartCinematic();
        yield return new WaitForSeconds(0.5f);
        DialogSystem.Instance.StartDialogSystem(cannotSprintDialog);

        yield break;
    }

    private IEnumerator CanSprint()
    {
        CinemaController.Instance.StartCinematic();
        yield return new WaitForSeconds(0.5f);
        DialogSystem.Instance.StartDialogSystem(canSprintDialog);

        yield break;
    }

    public void ExecuteInteraction()
    {
        if(PlayerAbilities.Instance.AbilityRun == true)
        {
            StartCoroutine(CanSprint());
        }
        else
        {
            StartCoroutine(CannotSprint());
        }
    }

    public string GetInteractionKey()
    {
        return "F";
    }

    public string GetInteractionTitle()
    {
        return "Hole Turtle";
    }

    public string GetInteractionType()
    {
        return "talk";
    }

    public void InitInteractions(Interactable parent)
    {
        myInteractionParent = parent;
    }

    public void SetDialog(string[] lines)
    {
        dialog = lines;


        cannotSprintDialog = new string[] {"You cannot cross the hole!", "You have to sprint to get over it", "Help the Squirrel first!", "Come back when you know how to sprint!" };
        canSprintDialog = new string[] { "You did it! You can SPRINT!","You should try to get over the hole then!", "Wish you luck :)"};

    }
}
