using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventSystem : MonoBehaviour
{
    #region Singleton
    private static EventSystem _instance;

    public static EventSystem Instance
    {
        get
        {
            return _instance;
        }
    }

    public void Awake()
    {
        _instance = this;
    }
    #endregion

    #region EventSystem

    public void StartConversation(string[] dialog)
    {
        CinemaController.Instance.StartCinematic(dialog);
        //DialogSystem.Instance.StartDialogSystem(dialog);
    }

    public void StartConversation(string[] dialog, Vector3 cameraPos, Transform lookAtTarget)
    {
        CinemaController.Instance.StartCinematic(dialog, cameraPos, lookAtTarget);
        //DialogSystem.Instance.StartDialogSystem(dialog);
    }

    #endregion
}
