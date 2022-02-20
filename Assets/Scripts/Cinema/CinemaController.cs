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


    public void StartCinematic()
    {
        StartCoroutine(InitCinema());
    }

    public void StartCinematic(Vector3 cameraPos, Transform lookAtTarget)
    {
        StartCoroutine(InitCinema(cameraPos, lookAtTarget));
    }

    private IEnumerator InitCinema(Vector3 cameraPos, Transform lookAtTarget)
    {
        DisableCrosshair();
        DetachCameraFromPlayer();
        FadeOut();
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().IsInControl = false;
        yield return new WaitForSeconds(0.5f);
        CinemaBorders.SetActive(true);
        MoveCamera(cameraPos, lookAtTarget);
        FadeIn();
    }

    private IEnumerator InitCinema()
    {
        DisableCrosshair();
        DetachCameraFromPlayer();
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().IsInControl = false;
        FadeOut();
        yield return new WaitForSeconds(0.5f);
        CinemaBorders.SetActive(true);
        FadeIn();
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
