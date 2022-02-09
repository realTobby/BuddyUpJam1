using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MushroomCollectable : MonoBehaviour, IInteractable
{
    private Interactable myParentInteractable;
    public void ExecuteInteraction()
    {
        ObjectiveController.Instance.IncreaseCollection();
        Destroy(myParentInteractable.gameObject);
    }

    public string GetInteractionKey()
    {
        return "F";
    }

    public string GetInteractionTitle()
    {
        return "Mushroom";
    }

    public string GetInteractionType()
    {
        return "collect";
    }

    public void InitInteractions(Interactable parent)
    {
        myParentInteractable = parent;
    }

}
