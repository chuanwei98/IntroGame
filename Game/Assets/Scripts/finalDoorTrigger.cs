using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class finalDoorTrigger : MonoBehaviour
{
    Animator _doorAnim;
    public gameController gameScript;

    public Text prompt;

    // Start is called before the first frame update
    void Start()
    {
        _doorAnim = this.transform.parent.GetComponent<Animator>();
    }

    // Update is called once per frame
    private void OnTriggerStay(Collider other)
    {
        if (gameScript.keypad < 2)
        {
            prompt.text = "find the 2 keypads to escape";
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (gameScript.keypad > 1)
        {
            _doorAnim.SetBool("character_nearby", true);
        }

    }
    private void OnTriggerExit(Collider other)
    {
        prompt.text = "";
    }
}
