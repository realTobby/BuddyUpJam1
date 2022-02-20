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

    public AudioSource nextLetter;
    public AudioSource nextLine;

    public delegate void CloseDialogSystem();
    public static event CloseDialogSystem OnDialogClosed;

    [SerializeField]
    public GameObject UIDialogOverlay;

   
    public TMPro.TextMeshProUGUI DialogText;
    public GameObject cursor;

    public string[] DialogToPrint;
    public int LinePointer = 0;

    public bool IsCurrentlyDialogOpen = false;

    public static int MaxLines = 3;
    public static int MaxCharacters = 18;


    public void StartDialogSystem(string[] lines)
    {
        IsDialogAtEndOfLine = false;

        UIDialogOverlay.SetActive(true);
        IsCurrentlyDialogOpen = true;
        DialogToPrint = lines;
        LinePointer = 0;
        DialogText.text = string.Empty;
        PrintNextLine();
    }

    public void EndDialogSystem()
    {
        // now here we need the event
        // fire event that the Dialog has been closed
        // and whoever wants to know that, gets notified
        IsDialogAtEndOfLine = false;

        UIDialogOverlay.SetActive(false);
        IsCurrentlyDialogOpen = false;

        OnDialogClosed();
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

    public bool IsDialogAtEndOfLine = false;

    public void Update()
    {
        if(IsDialogAtEndOfLine == true)
        {
            cursor.SetActive(true);
        }
        else
        {
            cursor.SetActive(false);
        }

        if(IsCurrentlyDialogOpen == true)
        {
            if(IsDialogAtEndOfLine == true)
            {
                if (Input.GetKeyUp(KeyCode.Space))
                {
                    IsDialogAtEndOfLine = false;
                    nextLine.Play();
                    PrintNextLine();
                }
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
                yield return new WaitForSeconds(0.09f);
                nextLetter.Play();
                yield return new WaitForSeconds(0.01f);
            }
            LinePointer++;
            IsDialogAtEndOfLine = true;
            yield break;
        }
    }
        

    #endregion


}
