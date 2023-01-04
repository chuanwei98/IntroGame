using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class reroutePower : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] gameController gameScript;
    [SerializeField] EnemyAi enemyScript;


    public Text prompt;
    public AudioSource ambient;

    void Start()
    {
        
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            prompt.text = "Reroute power?";

            if (Input.GetKeyUp("e"))
            {

                gameScript.generator = true;
                //enemyScript.sightRange = 10f;
                enemyScript.agent.Warp(new Vector3(30, 1, 0));


                enemyScript.walkPoint = new Vector3(28, 0, 0);

                ambient.enabled = false;

                prompt.text = "";
                gameObject.SetActive(false);
            }
        }

        
    }

    private void OnTriggerExit(Collider other)
    {
        prompt.text = "";
    }
}
