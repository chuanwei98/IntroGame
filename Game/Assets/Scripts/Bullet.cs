using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bullet : MonoBehaviour
{
    private Text prompt;
    private Gun gunScript;

    private bool activated = true;


    private void Start()
    {
        gunScript = GameObject.Find("GunParent").GetComponent<Gun>();
        prompt = GameObject.Find("prompt").GetComponent<Text>();
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (activated)
            {
                prompt.text = "Pickup Ammo?";
            }
        }
            
        
        if (other.CompareTag("Player"))
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (gunScript.hasBullet)
                {
                    StartCoroutine(maxBullets());
                }
                else
                {
                    gunScript.hasBullet = true;
                    prompt.text = "";
                    Destroy(this.gameObject);
                }
            }
        }
            
        
            

    }
    private void OnTriggerExit(Collider other)
    {
        prompt.text = "";
    }
    IEnumerator maxBullets()
    {
        activated = false;
        prompt.text = "you already have max bullets";
        yield return new WaitForSeconds(2);
        prompt.text = "";
        activated = true;
    }

}
