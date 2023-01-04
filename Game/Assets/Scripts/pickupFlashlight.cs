using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class pickupFlashlight : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject flashLight;
    public GameObject flashLightPickup;
    public Text prompt;


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerStay(Collider other)
    {
        if (flashLightPickup.gameObject.activeSelf)
        {
            prompt.text = "Press E to pickup flashlight";
        }
 
        if (Input.GetKeyDown(KeyCode.E))
        {
            StartCoroutine(UseFlashlight());

            flashLight.SetActive(true);
            flashLightPickup.gameObject.SetActive(false);


        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (flashLightPickup.gameObject.activeSelf)
        {
            prompt.text = "";
        }
            
    }
    IEnumerator UseFlashlight() //prompt to teach player how to use the flashlight (prompt disappears after 2 seconds)
    {
        prompt.text = " F to toggle flashlight";
        yield return new WaitForSeconds(2);
        prompt.text = "";
        this.gameObject.SetActive(false);

    }
}
