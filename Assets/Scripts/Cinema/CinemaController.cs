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
    }
    #endregion

    #region CinemaController

    public GameObject CinemaBorders;

    public GameObject FadeOverlay;

    public Animator FadeAnimator;

    public Vector3 OriginalCameraPosOnPlayer;

    public void StartCinematic()
    {
        GameObject.FindGameObjectWithTag("Player").GetComponent<FirstPersonController>().IsInControl = false;
        OriginalCameraPosOnPlayer = this.transform.position;
        CinemaBorders.SetActive(true);
    }

    public void EndCinematic()
    {
        GameObject.FindGameObjectWithTag("Player").GetComponent<FirstPersonController>().IsInControl = true;
        this.transform.position = OriginalCameraPosOnPlayer;
        CinemaBorders.SetActive(false);
        AttachCameraToPlayer();
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
    }

    public void AttachCameraToPlayer()
    {
        this.gameObject.transform.parent = GameObject.FindGameObjectWithTag("Player").transform;
    }

    #endregion


}
