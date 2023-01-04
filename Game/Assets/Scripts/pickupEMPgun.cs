using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class pickupEMPgun : MonoBehaviour
{
    // Start is called before the first frame update
    public Text prompt;

    public GameObject gun;
    
    private GameObject player;
    private playerController playerScript;


    void Start()
    {
        player = GameObject.Find("Player");
        playerScript = player.GetComponent<playerController>();
    }

    // Update is called once per frame

    private void OnTriggerStay(Collider other)
    {
        prompt.text = "Press E to pickup gun";
        if (Input.GetKeyDown(KeyCode.E))
        {
            gun.SetActive(true);
        }

    }
    private void OnTriggerExit(Collider other)
    {
        prompt.text = "";
    }
}
