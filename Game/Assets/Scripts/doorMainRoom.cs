using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class doorMainRoom : MonoBehaviour
{
    //calls the the animation handler
    Animator _doorAnim;
    public GameObject flashlightPickup;
    public Text prompt;

    private void OnTriggerEnter(Collider other)
    {
        if (flashlightPickup.activeSelf)
        {
            _doorAnim.SetBool("character_nearby", true);
        }
        else
        {
            prompt.text = "I should take my flashlight";
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
