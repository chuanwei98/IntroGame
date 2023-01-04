using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class flashlight : MonoBehaviour
{
    float timer;
    [SerializeField] Light myLight;
    public float normalIntensity = 10;

    public bool spotted = false;

    public playerController playerScript;

    public AudioSource lightBuzz;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        

        if (playerScript.toggleFlashlight)
        {
            FireRay();
        }
        
    }
    void FireRay()
    {

        spotted = false;

        


        RaycastHit hitData;
        if(Physics.Raycast(transform.position, transform.forward, out hitData, 30f))
        {
            if (hitData.collider.tag == "Enemy")
            {
                spotted = true;

                lightBuzz.enabled = true;

                timer += Time.deltaTime;
                if (timer < 0.2)
                {
                    myLight.intensity = 0;
                }
                else
                {
                    myLight.intensity = normalIntensity;
                    if (timer > 0.4)
                    {
                        timer = 0;
                    }
                }
            }
            else
            {
                myLight.intensity = normalIntensity;

                lightBuzz.enabled = false;
            }
        }

    }
}
