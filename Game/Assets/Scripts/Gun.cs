using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
 
public class Gun : MonoBehaviour
{
    public Transform player;
    public GameObject enemy;
    public EnemyAi enemyScript;

    public AudioSource pewSFX;

    public float timer;

    public bool hasBullet;

    public Text prompt;

    private void Start()
    {
        enemyScript = enemy.GetComponent<EnemyAi>();
        hasBullet = true;
    }
    void Update()
    {
        FireGun();
    }

    void FireGun()
    {

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            RaycastHit hitData;
            if (hasBullet)
            {
                if (Physics.Raycast(player.position, player.forward, out hitData, 30f))
                {

                    Debug.Log(hitData.collider.tag);

                    if (hitData.collider.tag == "Enemy")
                    {
                        enemy.GetComponent<CapsuleCollider>().enabled = false;
                        enemyScript.agent.isStopped = true;

                        pewSFX.enabled = true;

                        hasBullet = false;

                        



                    }
                }
            }
            else
            {
                StartCoroutine(noBullet());

            }

        }
        
        if(enemyScript.agent.isStopped)
        {
            timer += Time.deltaTime;
        }
        if (timer > 5f)
        {
            enemy.GetComponent<CapsuleCollider>().enabled = true;
            enemyScript.agent.isStopped = false;

            pewSFX.enabled = false;

            timer = 0;
        }
    }
    IEnumerator noBullet()
    {
        prompt.text = "No bullets";
        yield return new WaitForSeconds(2f);
        prompt.text = "";
    }

}