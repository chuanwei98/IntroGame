using UnityEngine.SceneManagement;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI deathMsg;
    [SerializeField] private TextMeshProUGUI winMsg;
    float respawnTimer = 2.5f;
    private void Start()
    {
        deathMsg.enabled = false;
        winMsg.enabled = false;
    }
    public void GameOver()
    {
        deathMsg.enabled = true;
        Invoke("Respawn", respawnTimer);
    }
    public void GameWon()
    {
        winMsg.enabled = true;
        Invoke("Respawn", respawnTimer);
    }
    void Respawn()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

}
