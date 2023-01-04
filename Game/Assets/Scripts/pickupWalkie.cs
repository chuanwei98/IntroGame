using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class pickupWalkie : MonoBehaviour
{
    [SerializeField] Transform player;
    [SerializeField] GameObject walkie;
    [SerializeField] GameObject itself;
    [SerializeField] GameObject enemy;
    [SerializeField] GameObject script;
    [SerializeField] GameObject stuff; //Stuff is the card and the walkie talkie 

    public Text prompt;

    public bool inTrigger;


    // Start is called before the first frame update
    void Start()
    {
        

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            prompt.text = "pickup keycard and walkie talkie?";
            inTrigger = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {

        prompt.text = "";
        inTrigger = false;
    }

    // Update is called once per frame
    void Update()
    {
        
        var enemyAi = enemy.GetComponent<EnemyAi>();

        if (Input.GetKeyDown("e") && inTrigger == true)
        {
            prompt.text = "";
            walkie.SetActive(true);
            
            //Teleports the enemy to the end of the hallway
            enemyAi.agent.Warp(new Vector3(12, 1, -26));
            enemyAi.walkPointSet = false;
            //Triggers the flag for allowing the player to open the door
            script.GetComponent<FirstDoor>().setCard();

            //makes the card and walkie talkie disappear from the floor
            stuff.SetActive(false);

            prompt.text = "press shift to sprint";

            StartCoroutine(Wait());

        }
        //OnGUI();
    }
    IEnumerator Wait() //function to deactive the sprint UI after 2 seconds
    {
        yield return new WaitForSeconds(2);
        prompt.text = "";
        
        
    }

}
