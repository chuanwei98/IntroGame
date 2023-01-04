using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class proximityLight : MonoBehaviour
{
    
    //[SerializeField] float detectionRange = 10f;
    [SerializeField] float timer = 0f;
    Light myLight;
    bool isClose;
    bool flickerClose;
    Transform enemyVar;
    gameController gameScript;

    float normalIntensity = 0.75f;
    // Start is called before the first frame update
    void Start()
    {
        myLight = GetComponent<Light>();
        enemyVar = GameObject.FindWithTag("Enemy").GetComponent<Transform>();
        gameScript = GameObject.FindWithTag("GameController").GetComponent<gameController>();
        
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!gameScript.generator)
        {
            isClose = false;
            flickerClose = false;


            if (Vector3.Distance(enemyVar.position, transform.position) <= 3f)
            {
                isClose = true;
            }
            else if (Vector3.Distance(enemyVar.position, transform.position) <= 7f)
            {
                flickerClose = true;
            }
            TurnOff();
            Flicker();
        }
        else
        {
            myLight.intensity = 0;
        }

    }
    void TurnOff()
    {
        if (flickerClose)
        {
            if (isClose)
            {

                myLight.intensity = 0;
            }
        }
    }
    void Flicker()
    {
        if (!isClose)
        {

            if (flickerClose)
            {

                timer += Time.deltaTime;
                if (timer < 0.5)
                {
                    myLight.intensity = 0;
                }
                else
                {
                    myLight.intensity = normalIntensity;
                    if (timer > 1)
                    {
                        timer = 0;
                    }
                }
            }
            else
            {
                myLight.intensity = normalIntensity;
            }
        }
    }
}
