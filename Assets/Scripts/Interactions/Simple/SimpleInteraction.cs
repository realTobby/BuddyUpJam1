using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Interactable))]
public class SimpleInteraction : MonoBehaviour, IInteractable
{
    public Vector3[] CameraPositions;

    public string[] myDialog;

    public void Awake()
    {
        this.GetComponent<Interactable>().SetInteraction(this);
    }

    public void ExecuteInteraction()
    {
        if(CameraPositions.Length > 0)
        {
            EventSystem.Instance.StartConversation(myDialog, CameraPositions[0], this.transform);
        }
        else
        {
           EventSystem.Instance.StartConversation(myDialog);
        }

       
    }
}
