using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class DebugSign : MonoBehaviour, IInteractable
{
    public Interactable myInteractionParent;

    public string[] dialog;

    private IEnumerator CinematicDialog()
    {
        CinemaController.Instance.StartCinematic();
        yield return new WaitForSeconds(0.5f);
        DialogSystem.Instance.StartDialogSystem(dialog);
        //Process.Start("https://itch.io/jam/buddy-up-jam-2022");
        yield break;
    }

    public void ExecuteInteraction()
    {
        StartCoroutine(CinematicDialog());
    }

    public string GetInteractionKey()
    {
        return "F";
    }

    public string GetInteractionTitle()
    {
        return "Buddy Up Jam: Winter 2022";
    }

    public string GetInteractionType()
    {
        return "inspect";
    }

    public void InitInteractions(Interactable parent)
    {
        myInteractionParent = parent;
    }

    public void SetDialog(string[] lines)
    {
        dialog = lines;
    }
}