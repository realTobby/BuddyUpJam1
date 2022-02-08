using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum InteractableType
{
    Sign,
    MushroomCollectable,
    NPC
}

public class Interactable : MonoBehaviour
{
    public InteractableType MyInteractableType;

    private IInteractable MyInteractions;

    [SerializeField] string[] dialogLines;

    public void Awake()
    {
        switch(MyInteractableType)
        {
            case InteractableType.MushroomCollectable:
                MyInteractions = this.gameObject.AddComponent<MushroomCollectable>();
                break;
            case InteractableType.NPC:
                MyInteractions = this.gameObject.AddComponent<NPCInteraction>();
                NPCInteraction npc = MyInteractions as NPCInteraction;
                npc.InitNPCLines(dialogLines);
                break;
        }
        MyInteractions.InitInteractions(this);
    }

    public string GetInteractionKey()
    {
        return MyInteractions.GetInteractionKey();
    }

    public string GetInteractionTitle()
    {
        return MyInteractions.GetInteractionTitle();
    }

    public string GetInteractionType()
    {
        return MyInteractions.GetInteractionType();
    }

    public void ExecuteInteraction()
    {
        MyInteractions.ExecuteInteraction();
    }

}
