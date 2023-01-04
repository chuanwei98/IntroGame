using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hide_script : MonoBehaviour
{
    //The player object
    GameObject player;
    Transform player_Transform;
    playerController playerScript = null;

    //Cameras
    [SerializeField] Camera hideCamera;
    Camera playerCamera;

    //GetEvent Controller
    GameObject gameController;

    float raylength = 2f;

    //object variables
    [SerializeField] bool isHiding;
    Rect rect = new Rect(Screen.width / 2, Screen.height / 2, 200, 25);
    public LayerMask theLocker;

    bool showGui = false;

    void Start()
    {
        //Player Variables
        player = GameObject.FindWithTag("Player");
        playerCamera = player.GetComponentInChildren<Camera>();
        player_Transform = player.GetComponent<Transform>();
        playerScript = player.GetComponent<playerController>();

        hideCamera.enabled = false;

        isHiding = false;
        gameController = GameObject.FindWithTag("GameController");

    }
    private void OnTriggerStay(Collider other)
    {
        RaycastHit hit;
        //OnGUI();
        showGui = false;

        if (Physics.Raycast(player_Transform.position, player_Transform.TransformDirection(Vector3.forward), out hit, raylength))
        {
            //Debug.Log(hit.collider.gameObject.tag);
            
            showGui = true;
            if (Input.GetKeyUp("e"))
            {
                //Debug.Log("yes?");
                hideCamera.enabled = true;
                playerCamera.enabled = false;

                isHiding = true;
                gameController.GetComponent<gameController>().hidden = true;
                playerScript.lockPlayerMovement();
                playerScript.mouseSensitivity = 0;

            }
            if (isHiding)
            {
                if (Input.GetKeyUp(KeyCode.LeftShift))
                {
                    //Debug.Log("this isnt happening");
                    hideCamera.enabled = false;
                    playerCamera.enabled = true;

                    isHiding = false;
                    gameController.GetComponent<gameController>().hidden = false;

                    playerScript.ResetWalkspeed();
                    playerScript.ResetMouse();
                }
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        showGui = false;
    }
    void OnGUI()
    {
        if (showGui && !isHiding)
        {
            GUI.Box(rect, "Press E to Hide?");
        }
        if(showGui && isHiding)
        {
            GUI.Box(rect, "press shift to leave");
        }
    }
}
