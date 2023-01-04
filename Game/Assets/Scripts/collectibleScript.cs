using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class collectibleScript : MonoBehaviour
{
    // Start is called before the first frame update
    private Text prompt;

    private GameObject player;
    private playerController playerScript;
    private gameController gameControllerScript;

    void Start()
    {
        player = GameObject.Find("Player");
        prompt = GameObject.Find("prompt").GetComponent<Text>();

        gameControllerScript = GameObject.Find("GameController").GetComponent<gameController>();

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

            prompt.text = "Press E to collect CD album";

            if (Input.GetKeyDown(KeyCode.E))
            {
                gameControllerScript.collectible += 1;
                StartCoroutine(collected());
                this.transform.position = new Vector3(10000, 0, 0);

            }
        }

    }
    private void OnTriggerExit(Collider other)
    {
        prompt.text = "";
    }
    IEnumerator collected()
    {
        yield return new WaitForSeconds(0.1f);
        prompt.text = "collected " + gameControllerScript.collectible + " album covers";
        yield return new WaitForSeconds(2);
        prompt.text = "";
        Destroy(this);

    }
}
