using System.Collections;

using System.Collections.Generic;

using UnityEngine;



[RequireComponent(typeof(CharacterController))]

public class PlayerMovement : MonoBehaviour

{

    public Camera playerCamera;

    public float defaultWalkSpeed = 4f;

    public float defaultRunSpeed = 6f;

    private float walkSpeed;

    private float runSpeed;

    public float jumpPower = 7f;

    public float gravity = 10f;

    public float lookSpeed = 2f;

    public float lookXLimit = 45f;

    public float defaultHeight = 2f;

    public float crouchHeight = 1f;

    public float crouchSpeed = 3f;

    // For a smooth rising after crouching
    private bool raiseFromCrouch = false;
    public float cameraRaiseSpeed = 0.01f;



    private Vector3 moveDirection = Vector3.zero;

    private float rotationX = 0;

    private CharacterController characterController;



    private bool canMove = true;



    void Start()

    {
        walkSpeed = defaultWalkSpeed;
        runSpeed = defaultRunSpeed;

        characterController = GetComponent<CharacterController>();

        Cursor.lockState = CursorLockMode.Locked;

        Cursor.visible = false;

    }



    void Update()

    {

        Vector3 forward = transform.TransformDirection(Vector3.forward);

        Vector3 right = transform.TransformDirection(Vector3.right);



        bool isRunning = Input.GetKey(KeyCode.LeftShift);

        float curSpeedX = canMove ? (isRunning ? runSpeed : walkSpeed) * Input.GetAxis("Vertical") : 0;

        float curSpeedY = canMove ? (isRunning ? runSpeed : walkSpeed) * Input.GetAxis("Horizontal") : 0;

        float movementDirectionY = moveDirection.y;

        moveDirection = (forward * curSpeedX) + (right * curSpeedY);



        if (Input.GetButton("Jump") && canMove && characterController.isGrounded)

        {

            moveDirection.y = jumpPower;

        }

        else

        {

            moveDirection.y = movementDirectionY;

        }



        if (!characterController.isGrounded)

        {

            moveDirection.y -= gravity * Time.deltaTime;

        }



        if (Input.GetKey(KeyCode.LeftControl) && canMove)

        {

            characterController.height = crouchHeight;

            walkSpeed = crouchSpeed;

            runSpeed = crouchSpeed;

            if (!raiseFromCrouch) {
                raiseFromCrouch = true;
            }

        }

        else

        {
            if(raiseFromCrouch) {
                if(characterController.height < defaultHeight) {
                    characterController.height += cameraRaiseSpeed;
                }
                if (characterController.height >= defaultHeight) {
                    raiseFromCrouch = false;
                    characterController.height = defaultHeight;
                }
            }

            walkSpeed = defaultWalkSpeed;

            runSpeed = defaultRunSpeed;

        }



        characterController.Move(moveDirection * Time.deltaTime);



        if (canMove)

        {

            rotationX += -Input.GetAxis("Mouse Y") * lookSpeed;

            rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);

            playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);

            transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookSpeed, 0);

        }

    }

}