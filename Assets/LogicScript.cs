using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Linq;
using System.Collections.Generic;

public class LogicScript : MonoBehaviour
{

    public int playerScore;
    public Text scoreText;
    public GameObject gameOverScreen;
    public GameObject startScreen;
    public bool gameRunning = false;

    void Start()
    {
        Screen.orientation = ScreenOrientation.LandscapeLeft;
    }

    [ContextMenu("Increase Score")]
    public void addScore(int scoreToAdd)
    {
        playerScore += scoreToAdd;
        scoreText.text = playerScore.ToString();
    }
    [ContextMenu("Restart Game")]
    public void restartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void startGame()
    {
        var playEntities = new List<GameObject>();
        foreach (GameObject go in Resources.FindObjectsOfTypeAll(typeof(GameObject)) as GameObject[])
        {
            Debug.Log($"Found object: {go.name}");
            if (go.name == "Bird")
            {
                Debug.Log(go.tag);
            }
            if(go.tag == "Play Entities")
            {
                Debug.Log($"Add item {go.name}");
                playEntities.Add(go);
            } else {
                Debug.Log($"Skip item {go.name} with tag {go.tag}");
            }
        }
        Debug.Log($"Found play entities: {playEntities.Count}");
        startScreen.SetActive(false);
        gameRunning = true;
        foreach (GameObject g in playEntities)
        {
            Debug.Log($"Set Active: {g.name}");
            g.SetActive(true);
        }
    }

    public void gameOver()
    {
        gameOverScreen.SetActive(true);
    }
}
