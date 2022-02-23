using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Interactable))]
public class NPC_HoleTurtle : MonoBehaviour, IInteractable
{
    public Vector3[] CinematicCameraPositions;

    private string[] cannotSprintDialog;

    private string[] canSprintDialog;


    public void Awake()
    {
        cannotSprintDialog = new string[] { "You cannot cross the hole!", "You have to sprint to get over it", "Help the Squirrel first!", "Come back when you know how to sprint!" };
        canSprintDialog = new string[] { "You did it! You can SPRINT!", "You should try to get over the hole then!", "Wish you luck :)" };

        this.GetComponent<Interactable>().SetInteraction(this);
    }

    public void ExecuteInteraction()
    {
        if(PlayerAbilities.Instance.AbilityRun == true)
        {
            EventSystem.Instance.StartConversation(canSprintDialog, CinematicCameraPositions[1], this.transform);
        }
        else
        {
            EventSystem.Instance.StartConversation(cannotSprintDialog, CinematicCameraPositions[0], this.transform);
        }
    }

}
