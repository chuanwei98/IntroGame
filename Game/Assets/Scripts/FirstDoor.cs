using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstDoor : MonoBehaviour
{
    Animator _doorAnim;
    private bool keycard = false;
    private bool showGUI = false;

    Rect rect = new Rect(Screen.width / 2, Screen.height / 2, 150, 25);
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        if (keycard == true)
        {
            showGUI = false;

            _doorAnim.SetBool("character_nearby", true);
        }
        if(keycard == false)
        {
            showGUI = true;
        }

    }
    private void OnTriggerExit(Collider other)
    {
        _doorAnim.SetBool("character_nearby", false);
        showGUI = false;
    }

    void Start()
    {
        _doorAnim = this.transform.parent.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void setCard()
    { 
        keycard = true;
    }
    void OnGUI()
    {
        if (showGUI)
        {
            GUI.Box(rect, "requires keycard");
        }
    }
}

