using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class keypadTrigger : MonoBehaviour
{

    private gameController gameControllerScript;
    public Text prompt;

    // Start is called before the first frame update
    void Start()
    {
        gameControllerScript = GameObject.Find("GameController").GetComponent<gameController>();
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {

            prompt.text = "Press E to activate keypad";

            if (Input.GetKeyDown(KeyCode.E))
            {
                gameControllerScript.keypad += 1;
                this.transform.position = new Vector3(10000, 0, 0);
                StartCoroutine(activated());
                

            }
        }

    }
    private void OnTriggerExit(Collider other)
    {
        prompt.text = "";
    }
    IEnumerator activated()
    {
        yield return new WaitForSeconds(0.1f);
        prompt.text = "keypad activated";
        yield return new WaitForSeconds(2);
        prompt.text = "";
        Destroy(this);

    }
}

