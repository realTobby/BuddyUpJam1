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

    public static int MaxLines = 3;
    public static int MaxCharacters = 18;


    public void StartDialogSystem(string[] lines)
    {
        UIDialogOverlay.SetActive(true);
        IsCurrentlyDialogOpen = true;
        DialogToPrint = lines;
        LinePointer = 0;
        DialogText.text = string.Empty;
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
            characterIndex = 0;

            if(LinePointer < 3 && LinePointer > 0)
            {
                DialogText.text = DialogText.text + System.Environment.NewLine;
            }
            else
            {
                DialogText.text = string.Empty;
            }

            
            StartCoroutine(nameof(PrintEachLetter));
            
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

    private int characterIndex = 0;

    private IEnumerator PrintEachLetter()
    {
        while(LinePointer < DialogToPrint.Length)
        {
            while (characterIndex < DialogToPrint[LinePointer].Length)
            {
                DialogText.text = DialogText.text + DialogToPrint[LinePointer][characterIndex];
                characterIndex++;
                yield return new WaitForSeconds(0.05f);
            }
            LinePointer++;
            yield break;
        }
    }
        

    #endregion


}
