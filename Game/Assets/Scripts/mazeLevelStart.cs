using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class mazeLevelStart : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject enemy;

    public Text prompt;

    public AudioSource ambient;

    public gameController gameControllerScript;

    void Start()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            enemy.GetComponent<EnemyAi>().agent.Warp(new Vector3(54, 1, 24));
            enemy.GetComponent<EnemyAi>().walkPointSet = false;
            gameControllerScript.checkpoint = this.transform.position;
            ambient.enabled = true;

            StartCoroutine(objectiveText());
            
        }
            
    }
    IEnumerator objectiveText()
    {
        yield return new WaitForSeconds(0.2f);
        this.transform.position = new Vector3(1000, 0, 0);
        prompt.text = "Find a way out";
        yield return new WaitForSeconds(3f);
        prompt.text = "";
        Destroy(this);

    }
}
