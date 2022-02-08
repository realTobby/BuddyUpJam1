using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogSystem : MonoBehaviour
{
    #region Singleton
    public static DialogSystem _instance = null;

    public static DialogSystem Instance
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
    #endregion

    #region DialogSystem

    [SerializeField]
    public GameObject UIDialogOverlay;

   
    public TMPro.TextMeshProUGUI DialogText;

    public string[] DialogToPrint;
    public int LinePointer = 0;

    public bool IsCurrentlyDialogOpen = false;

    public void StartDialogSystem(string[] lines)
    {
        UIDialogOverlay.SetActive(true);
        IsCurrentlyDialogOpen = true;
        DialogToPrint = lines;
        LinePointer = 0;
        PrintNextLine();
    }

    public void EndDialogSystem()
    {
        StartCoroutine(nameof(CinematicEnd));
        UIDialogOverlay.SetActive(false);
        IsCurrentlyDialogOpen = false;
    }

    public IEnumerator CinematicEnd()
    {
        CinemaController.Instance.FadeOut();

        yield return new WaitForSeconds(1.2f);

        CinemaController.Instance.EndCinematic();

        CinemaController.Instance.FadeIn();
    }

    private void PrintNextLine()
    {
        if(LinePointer >= DialogToPrint.Length)
        {
            EndDialogSystem();
            return;
        }
        else
        {
            DialogText.text = DialogToPrint[LinePointer];
            LinePointer++;
        }
    }

    public void Update()
    {
        if(IsCurrentlyDialogOpen == true)
        {
            if (Input.GetKeyUp(KeyCode.Space))
            {
                PrintNextLine();
            }
        }
    }


    #endregion


}
