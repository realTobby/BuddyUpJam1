using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAbilities : MonoBehaviour
{
    #region Singleton
    private static PlayerAbilities _instance = null;

    public static PlayerAbilities Instance
    {
        get
        {
            return _instance;
        }
    }

    private void Awake()
    {
        _instance = this;

        

    }

    private void Start()
    {
        ObjectiveController.Instance.SetObjective(ObjectiveController.Instance.gameObject.AddComponent<ObjectiveFindForrestGuardian>());
    }

    #endregion

    #region PlayerAbilies

    public bool AbilityJump = true;

    public bool AbilityRun = false;

    


    #endregion

}
