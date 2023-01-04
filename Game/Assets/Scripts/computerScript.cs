using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class computerScript : MonoBehaviour
{
    // Start is called before the first frame update

    public Text prompt;
    public doorHandler doorScript;
    public Light spookyLight;

    public AudioSource lightbreak;

    // Update is called once per frame
    private void OnTriggerStay(Collider other)
    {
        

        if (doorScript.rebooted)
        {
            prompt.text = "";
        }
        else
        {
            prompt.text = "Press E to use the reboot the door systems";

            if (Input.GetKeyDown(KeyCode.E))
            {
                doorScript.rebooted = true;

                StartCoroutine(Spooky());
            }
        }  
    }
    private void OnTriggerExit(Collider other)
    {
        prompt.text = "";
    }
    IEnumerator Spooky()
    {

        yield return new WaitForSeconds(3);
        spookyLight.intensity = 0;
        lightbreak.enabled = true;
        
    }
}
