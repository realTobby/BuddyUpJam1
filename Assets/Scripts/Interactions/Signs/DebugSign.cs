using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DebugSign : MonoBehaviour, IInteractable
{
    public Interactable myInteractionParent;

    public string[] dialog;

    private IEnumerator DEMOEND()
    {
        yield return new WaitForSeconds(1);
        CinemaController.Instance.FadeOut();
        yield return new WaitForSeconds(3);
    }

    public void CloseGame()
    {
        StartCoroutine(DEMOEND());
    }

    private IEnumerator CinematicDialog()
    {
        CinemaController.Instance.StartCinematic(new Vector3(23.5f, 18, 61.3f), this.transform);
        yield return new WaitForSeconds(0.5f);
        DialogSystem.Instance.StartDialogSystem(dialog);
        yield break;
    }

    public void ExecuteInteraction()
    {
        CinemaController.OnCinematicEnd += CloseGame;
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
