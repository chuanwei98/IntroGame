using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class gameController : MonoBehaviour
{
    //Audio Variables
    public AudioSource staticAudio;
    public AudioSource chaseMusic;

    public Text prompt;

    //Player Variables
    
    playerController playerScript;
    public GameObject player;
    Rect rect = new Rect(Screen.width / 2, Screen.height / 2, 200, 25);


    //Enemy Variables
    public GameObject enemy;
    public bool isAngry;
    bool toggle;

    //Game states
    public bool hidden;
    public bool dead;
    public bool generator = false;

    public float maxVolume = 1;

    public Vector3 checkpoint;

    public int collectible;

    public int keypad;

    // Start is called before the first frame update
    void Start()
    {
        //staticAudio.Play();
        toggle = false;
        playerScript = player.GetComponent<playerController>();
    }

    // Update is called once per frame
    void Update()
    {

        StartCoroutine(FadeTrack());

    }
    IEnumerator FadeTrack()
    {
        isAngry = enemy.GetComponent<EnemyAi>().angry;

        float timeToFade = 3f;
        float timeElapsed = 0f;

        staticAudio.loop = isAngry;
        if (isAngry && !toggle)
        {
            staticAudio.Play();
            chaseMusic.Play();
            toggle = true;
            while (timeElapsed < timeToFade)
            {
                staticAudio.volume = Mathf.Lerp(0, maxVolume, timeElapsed / timeToFade);
                chaseMusic.volume = Mathf.Lerp(0, maxVolume, timeElapsed / timeToFade);

                timeElapsed += Time.deltaTime;
                yield return null;
            }
        }
        if (toggle && !isAngry)
        {
            while(timeElapsed < timeToFade)
            {
                staticAudio.volume = Mathf.Lerp(1, 0, timeElapsed / timeToFade);
                chaseMusic.volume = Mathf.Lerp(1, 0, timeElapsed / timeToFade);
                timeElapsed += Time.deltaTime;
                yield return null;
            }
            staticAudio.Stop();
            chaseMusic.Stop();
            toggle = false;
            
        }
        
    }
    IEnumerator deathScene()
    {
        if (dead)
        {
            playerScript.lockPlayerMovement();
            playerScript.mouseSensitivity = 0f;

            prompt.text = "you are dead";

            yield return new WaitForSeconds(2);

            player.GetComponent<Transform>().position = checkpoint;

            playerScript.ResetWalkspeed();
            playerScript.ResetMouse();

            dead = false;
            prompt.text = "";
        }
        yield return null;
    }
    public void deathSceneRunner()
    {
        StartCoroutine(deathScene());
    }
}
