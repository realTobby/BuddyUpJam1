using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Interactable))]
public class MushroomCollectable : MonoBehaviour, IInteractable
{
    public void Awake()
    {
        this.GetComponent<Interactable>().SetInteraction(this);
    }

    public void ExecuteInteraction()
    {
        ObjectiveController.Instance.IncreaseCollection();
        this.gameObject.SetActive(false);
    }
}
