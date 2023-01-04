using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class exitDoor : MonoBehaviour
{
    Animator _doorAnim;
    public gameController gameScript;
    bool showGUI = false;
    Rect rect = new Rect(Screen.width / 2, Screen.height / 2, 400, 25);
    // Start is called before the first frame update
    void Start()
    {
        _doorAnim = this.transform.parent.GetComponent<Animator>();
    }

    // Update is called once per frame
    private void OnTriggerStay(Collider other)
    {
        if (!gameScript.generator)
        {
            showGUI = true;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (gameScript.generator)
        {
            _doorAnim.SetBool("character_nearby", true);
        }
            
    }
    private void OnTriggerExit(Collider other)
    {
        showGUI = false;
    }
    private void OnGUI()
    {
        if (showGUI)
        {
            GUI.Box(rect, "Find Generator to reroute power to door");
        }
    }
}
