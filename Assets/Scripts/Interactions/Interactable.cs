using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Interactable : MonoBehaviour
{
    private IInteractable _interaction;

    public string InteractionKey = "F";
    public string InteractionTitle = "Empty";
    public string InteractionVerb = "Inspect";

    public void SetInteraction(IInteractable interaction)
    {
        _interaction = interaction;
    }

    public string GetInteractionKey()
    {
        return InteractionKey;
    }

    public string GetInteractionTitle()
    {
        return InteractionTitle;
    }

    public string GetInteractionType()
    {
        return InteractionVerb;
    }

    public void ExecuteInteraction()
    {
        _interaction.ExecuteInteraction();
    }

}
