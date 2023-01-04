using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class sinwave : MonoBehaviour
{
    public LineRenderer myLineRenderer;
    public int points;
    public float amplitude = 0.5f;
    public float frequency = 2;
    public Vector2 xLimits = new Vector2(1, 1);
    public float movementSpeed = 1;
    [Range(0, 2 * Mathf.PI)]
    public float radians;

    public GameObject Enemy;
    public Transform player;
    public bool isAngry;

    void Start()
    {
        myLineRenderer = GetComponent<LineRenderer>();
        
        //player = GameObject.Find("Player").transform;
    }

    void Draw()
    {
        //movementSpeed = 3/Mathf.Log((Enemy.position - player.position).magnitude, 5) + 0.5f;
        //frequency = 3/ Mathf.Log((Enemy.position - player.position).magnitude, 5) + 0.5f;
        if (isAngry)
        {
            amplitude = 0.7f;
            movementSpeed = 3;
            frequency = 2;
        }
        else
        {
            movementSpeed = 1;
            amplitude = 0.3f;
            frequency = 2;
        }
        float xStart = xLimits.x;
        float Tau = 2 * Mathf.PI;
        float xFinish = xLimits.y;
        myLineRenderer.positionCount = points;
        for (int currentPoint = 0; currentPoint < points; currentPoint++)
        {
            float progress = (float)currentPoint / (points - 1);
            float x = Mathf.Lerp(xStart, xFinish, progress);
            float y = amplitude * Mathf.Sin((Tau * frequency * x) + (Time.timeSinceLevelLoad * movementSpeed));
            myLineRenderer.SetPosition(currentPoint, new Vector3(x, y, 0));
        }
    }

    void Update()
    {
        Draw();
        isAngry = Enemy.GetComponent<EnemyAi>().angry;
    }
}
