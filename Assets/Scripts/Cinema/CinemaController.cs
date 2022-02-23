using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CinemaController : MonoBehaviour
{
    public bool IsCinematic = false;


    #region Singleton
    private static CinemaController _instance = null;

    public static CinemaController Instance
    {
        get
        {
            return _instance;
        }
    }

    private void Awake()
    {
        _instance = this;

        DialogSystem.OnDialogClosed += EndCinematicMode;

    }
    #endregion

    #region CinemaController

    public delegate void EndCinematic();
    public static event EndCinematic OnCinematicEnd;

    public delegate void OpenCinematic(string[] dialogLinesToPass);
    public static event OpenCinematic OnCinematicOpen;

    public GameObject CinemaBorders;

    public GameObject FadeOverlay;

    public Animator FadeAnimator;

    public GameObject UI_CROSSHAIR;

    public Vector3 OriginalCameraPosOnPlayer;

    public void DisableCrosshair()
    {
        UI_CROSSHAIR.SetActive(false);
    }

    public void EnableCrosshair()
    {
        UI_CROSSHAIR.SetActive(true);
    }

    public void ActivateCinemaBorders()
    {
        CinemaBorders.SetActive(true);
    }

    public void DeactivateCinamaBorders()
    {
        CinemaBorders.SetActive(false);
    }


    public void StartCinematic(string[] dialog)
    {
        StartCoroutine(InitCinema(dialog));
    }

    public void StartCinematic(string[] dialog, Vector3 cameraPos, Transform lookAtTarget)
    {
        StartCoroutine(InitCinema(dialog, cameraPos, lookAtTarget));
    }

    private IEnumerator InitCinema(string[] dialog, Vector3 cameraPos, Transform lookAtTarget)
    {
        IsCinematic = true;
        FadeOut();
        yield return new WaitForSeconds(1.2f);
        DisableCrosshair();
        DetachCameraFromPlayer();
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().IsInControl = false;
        MoveCamera(cameraPos, lookAtTarget);
        CinemaBorders.SetActive(true);
        DialogSystem.Instance.OpenTextbox();
        yield return new WaitForSeconds(0.5f);

        FadeIn();
        yield return new WaitForSeconds(0.5f);
        if (OnCinematicOpen != null)
            OnCinematicOpen(dialog);
        yield break;
    }

    private IEnumerator InitCinema(string[] dialog)
    {
        Debug.Log("I will fade out!");
        FadeOut();
        yield return new WaitForSeconds(1.2f);
        IsCinematic = true;
        DisableCrosshair();
        DetachCameraFromPlayer();
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().IsInControl = false;
        CinemaBorders.SetActive(true);
        DialogSystem.Instance.OpenTextbox();
        Debug.Log("I stay on black screen");
        yield return new WaitForSeconds(0.5f);

        Debug.Log("I will fade in!");
        FadeIn();
        yield return new WaitForSeconds(0.5f);
        Debug.Log("I waited for fade in");
        if (OnCinematicOpen != null)
        {
            Debug.Log("I fired the OnCinematicOpen event");
            OnCinematicOpen(dialog);
        }
        yield break;
    }


    public void EndCinematicMode()
    {
        StartCoroutine(CloseCinema());

        if (OnCinematicEnd != null)
            OnCinematicEnd();
    }

    private IEnumerator CloseCinema()
    {
        FadeOut();
        yield return new WaitForSeconds(0.5f);
        IsCinematic = false;
        CinemaBorders.SetActive(false);
        AttachCameraToPlayer();
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().IsInControl = true;
        FadeIn();
        EnableCrosshair();
    }

    public void FadeOut()
    {
        FadeAnimator.Play("FadeOut");
    }

    public void FadeIn()
    {
        FadeAnimator.Play("FadeIn");

    }

    public void MoveCamera(Vector3 positon, Transform target)
    {
        this.transform.position = positon;
        this.transform.LookAt(target);
    }

    public void DetachCameraFromPlayer()
    {
        this.gameObject.transform.parent = null;
        OriginalCameraPosOnPlayer = this.gameObject.transform.position;
    }

    public void AttachCameraToPlayer()
    {
        this.gameObject.transform.position = OriginalCameraPosOnPlayer;
        this.gameObject.transform.parent = GameObject.FindGameObjectWithTag("Player").transform;
    }

    #endregion


}
