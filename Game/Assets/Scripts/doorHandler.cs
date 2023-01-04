using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class doorHandler : MonoBehaviour
{
    //calls the the animation handler
    Animator _doorAnim;
    public bool rebooted;
    public Text prompt;

    private void OnTriggerEnter(Collider other)
    {
        if (rebooted)
        {
            _doorAnim.SetBool("character_nearby", true);
        }
        else
        {
            prompt.text = "The door isn't working, i need to reboot the system";
        }

    }
    private void OnTriggerExit(Collider other)
    {
        _doorAnim.SetBool("character_nearby", false);

        prompt.text = "";
    }

    void Start()
    {
        _doorAnim = this.transform.parent.GetComponent<Animator>();
    }

    // Update is called once per frame
}
