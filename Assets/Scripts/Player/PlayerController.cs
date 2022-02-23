using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    public Cinemachine.CinemachineVirtualCamera startCam;

    public bool IsInControl = true;

    public float walkingSpeed = 7.5f;
    public float runningSpeed = 11.5f;
    public float jumpSpeed = 8.0f;
    public float gravity = 20.0f;
    public Camera playerCamera;
    public float lookSpeed = 2.0f;
    public float lookXLimit = 45.0f;

    CharacterController characterController;
    Vector3 moveDirection = Vector3.zero;
    float rotationX = 0;

    [HideInInspector]
    public bool canMove = true;

    public bool fistFall = false;
    public bool isFirstCutscene = true;

    public Vector3 lastGroundedPosition;

    void Start()
    {
        CinemaController.Instance.FadeIn();

        characterController = GetComponent<CharacterController>();

        // Lock cursor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;


        // play the game starting event
        CinemaController.Instance.DisableCrosshair();

        IsInControl = false;

        // detach camera from player and move player to sky position
        CinemaController.Instance.DetachCameraFromPlayer();
        this.transform.position = new Vector3(this.transform.position.x, 100, this.transform.position.z);

        // move camera to view position
        Camera.main.transform.position = new Vector3(2.43f, 3, 8.6f);
        Camera.main.transform.localRotation = Quaternion.Euler(new Vector3(-50, -50, 0));

        // now we wait til player hits the ground
    }

    IEnumerator FistSkyFall()
    {
        // fade out
        CinemaController.Instance.FadeOut();

        yield return new WaitForSeconds(1.2f);

        CinemaController.Instance.AttachCameraToPlayer();

        EventSystem.Instance.StartConversation(new string[] { "Test of the EventSystem :)" });

        yield break;

    }

    Vector3 warpPosition = Vector3.zero;
    public void WarpToPosition(Vector3 newPosition)
    {
        warpPosition = newPosition;
    }

    void LateUpdate()
    {
        if (warpPosition != Vector3.zero)
        {
            transform.position = warpPosition;
            warpPosition = Vector3.zero;
        }
    }

    IEnumerator OutOfBounds()
    {
        CinemaController.Instance.FadeOut();

        yield return new WaitForSeconds(2);

        WarpToPosition(lastGroundedPosition);

        CinemaController.Instance.FadeIn();

        yield return new WaitForSeconds(0.5f);

        IsInControl = true;
        isOOB = false;

        yield break;
    }

    bool isOOB = false;

    void Update()
    {
        if(isOOB == false)
        {
            if (this.transform.position.y <= -5)
            {
                IsInControl = false;
                isOOB = true;
                StartCoroutine(OutOfBounds());
            }
        }

        

        if (isFirstCutscene == true)
        {
            characterController.Move(new Vector3(0,-50,0) * Time.deltaTime);
            Camera.main.transform.LookAt(this.transform);
        }

        if (!characterController.isGrounded)
        {
            moveDirection.y -= gravity * Time.deltaTime;
        }
        else
        {
            lastGroundedPosition = this.transform.position;
            if(fistFall == false)
            {
                fistFall = true;
                isFirstCutscene = false;
                StartCoroutine(FistSkyFall());
            }
        }

        if (IsInControl == true)
        {
            // We are grounded, so recalculate move direction based on axes
            Vector3 forward = transform.TransformDirection(Vector3.forward);
            Vector3 right = transform.TransformDirection(Vector3.right);
            // Press Left Shift to run
            bool isRunning = Input.GetKey(KeyCode.LeftShift) && PlayerAbilities.Instance.AbilityRun;
            float curSpeedX = canMove ? (isRunning ? runningSpeed : walkingSpeed) * Input.GetAxis("Vertical") : 0;
            float curSpeedY = canMove ? (isRunning ? runningSpeed : walkingSpeed) * Input.GetAxis("Horizontal") : 0;
            float movementDirectionY = moveDirection.y;
            moveDirection = (forward * curSpeedX) + (right * curSpeedY);

            if (Input.GetButton("Jump") && canMove && characterController.isGrounded && PlayerAbilities.Instance.AbilityJump)
            {
                moveDirection.y = jumpSpeed;
            }
            else
            {
                moveDirection.y = movementDirectionY;
            }

            
            // Player and Camera rotation
            if (canMove)
            {
                rotationX += -Input.GetAxis("Mouse Y") * lookSpeed;
                rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);
                playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
                transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookSpeed, 0);
            }

            // Move the controller
            characterController.Move(moveDirection * Time.deltaTime);
        }
        else
        {
            moveDirection.x = 0;
            moveDirection.z = 0;
        }

        

    }
}
