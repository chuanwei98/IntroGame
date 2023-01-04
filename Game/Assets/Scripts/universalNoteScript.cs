using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class universalNoteScript : MonoBehaviour
{
    // Start is called before the first frame update
    public Text prompt;
    public GameObject image;

    private GameObject player;
    private playerController playerScript;


    void Start()
    {
        player = GameObject.Find("Player");
        playerScript = player.GetComponent<playerController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {

            prompt.text = "Press E to read Note";
            if (Input.GetKeyDown(KeyCode.E))
            {
                image.SetActive(!image.activeSelf);

                playerScript.lockPlayerMovement();
            }
        }

    }
    private void OnTriggerExit(Collider other)
    {
        prompt.text = "";
    }
}
