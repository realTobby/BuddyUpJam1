using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCInteraction : MonoBehaviour, IInteractable
{
    private Interactable myParentInteractable;

    private Vector3 PlayerOriginalPos;

    string[] myNPCLines;
    
    public void ExecuteInteraction()
    {
        // init cutscene with dialog system

        StartCoroutine(nameof(InitDialog));


    }


    public void InitNPCLines(string[] lines)
    {
        myNPCLines = lines;
    }

    public IEnumerator InitDialog()
    {
        CinemaController.Instance.FadeOut();

        yield return new WaitForSeconds(1f);

        // DO MAGIC

        PlayerOriginalPos = GameObject.FindGameObjectWithTag("Player").transform.position;

        CinemaController.Instance.StartCinematic();

        CinemaController.Instance.DetachCameraFromPlayer();

        CinemaController.Instance.MoveCamera(new Vector3(3.5f, 15, -16f), this.transform);

        yield return new WaitForSeconds(2f);

        CinemaController.Instance.FadeIn();



        DialogSystem.Instance.StartDialogSystem(myNPCLines);

        yield break;
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
}
