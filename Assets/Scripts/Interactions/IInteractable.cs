using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteractable
{
    void SetDialog(string[] lines);
    void InitInteractions(Interactable parent);
    string GetInteractionKey();
    string GetInteractionTitle();
    string GetInteractionType();
    void ExecuteInteraction();
}
