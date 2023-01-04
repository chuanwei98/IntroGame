using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour
{
    [SerializeField] Transform playerCamera = null;
    public float mouseSensitivity = 2f;
    public float walkSpeed = 3.0f;
    public float gravity = -20.0f;
    [Range(0.0f, 0.5f)] float moveSmoothTime = 0.13f;
    [Range(0.0f, 0.5f)] float mouseSmoothTime = 0.03f;
    [SerializeField] float jumpHeight = 1.0f;
    float normalWalkSpeed = 0f;
    float normalMouseSpeed = 0f;
    [SerializeField] bool lockCursor = true;
    float bhop;

    float cameraPitch = 0.0f;
    float velocityY = 0.0f;
    CharacterController controller = null;

    Vector2 currentDir = Vector2.zero;
    Vector2 currentDirVelocity = Vector2.zero;

    Vector2 currentMouseDelta = Vector2.zero;
    Vector2 currentMouseDeltaVelocity = Vector2.zero;

    bool canMove = true;

    public Light flashLight;
    public bool toggleFlashlight;
    public flashlight flashLightScript;

    public AudioSource footstep;

    private float timer;


    void Start()
    {
        normalWalkSpeed = walkSpeed;
        normalMouseSpeed = mouseSensitivity;
        controller = GetComponent<CharacterController>();
        if (lockCursor)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }

    void Update()
    {
        UpdateMouseLook();
        if (canMove)
        {
            UpdateMovement();
        }
        if (!flashLightScript.spotted)
        {
            FlashLight();
        }
        debugButton();

        if (controller.isGrounded)
        {
            walkingAudio();
        }
        else
        {
            footstep.enabled = false;
        }
        
    


        
    }

    void UpdateMouseLook()
    {
        Vector2 targetMouseDelta = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));

        currentMouseDelta = Vector2.SmoothDamp(currentMouseDelta, targetMouseDelta, ref currentMouseDeltaVelocity, mouseSmoothTime);

        cameraPitch -= currentMouseDelta.y * mouseSensitivity;
        cameraPitch = Mathf.Clamp(cameraPitch, -90.0f, 90.0f);

        playerCamera.localEulerAngles = Vector3.right * cameraPitch;
        transform.Rotate(Vector3.up * currentMouseDelta.x * mouseSensitivity);
    }

    void UpdateMovement()
    {
        Vector2 targetDir = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        targetDir.Normalize();

        currentDir = Vector2.SmoothDamp(currentDir, targetDir, ref currentDirVelocity, moveSmoothTime);

        if (controller.isGrounded){ //check's if the player is on the ground

            velocityY = 0.0f;
            bhop -= Time.deltaTime;

            if (bhop <= 0)//resets the walk speed back to normal
            {
                ResetWalkspeed();
            }
        }
        if (Input.GetKeyDown(KeyCode.Space) && controller.isGrounded){//jumping code

            walkSpeed += 0.5f;
            bhop = 0.1f;
            velocityY += Mathf.Sqrt(jumpHeight * -3.0f * gravity);
            
        }
        if (Input.GetKey(KeyCode.LeftShift))
        {
            walkSpeed = 6;
        }
        velocityY += gravity * Time.deltaTime;

        Vector3 velocity = (transform.forward * currentDir.y + transform.right * currentDir.x) * walkSpeed + Vector3.up * velocityY;

        controller.Move(velocity * Time.deltaTime);



    }
    void FlashLight()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            toggleFlashlight = !toggleFlashlight;
        }
        if (toggleFlashlight)
        {
            flashLight.intensity = flashLightScript.normalIntensity;
        }
        else
        {
            flashLight.intensity = 0;
        }
    }
    public void ResetWalkspeed()
    {
        walkSpeed = normalWalkSpeed;
        canMove = true;
    }
    public void ResetMouse()
    {
        mouseSensitivity = normalMouseSpeed;
    }
    public void lockPlayerMovement()
    {
        canMove = !canMove;
    }

    public void debugButton()
    {
        if (Input.GetKeyDown(KeyCode.Backslash))
        {
            Debug.Log("pushed!");
            this.transform.position = new Vector3(-22f,1f, -1f);
        }
    }
    public void walkingAudio()
    {
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
        {
            timer += Time.deltaTime;
            if (timer > 0.5f)
            {
                footstep.enabled = true;
            }
        }
        else
        {
            footstep.enabled = false;
            timer = 0;
        }
    }

}