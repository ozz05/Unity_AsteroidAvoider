using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class GameOverHandler : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private Button continueButton;
    [SerializeField] private TMP_Text gameOverTxt;
    [SerializeField] private GameObject gameOverDisplay;
    [SerializeField] private AsteroidSpawner asteroidSpawner;
    [SerializeField] private ScoreSystem scoreSystem;
    
    public void EndGame()
    {
        gameOverDisplay.SetActive(true);
        asteroidSpawner.enabled = false;
        scoreSystem.PauseGame();
        DisplayScore();
    }
    public void PlayAgain()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void ContinueButton()
    {
        continueButton.interactable = false;
        AdManager.Instance.ShowAd(this);
    }
    public void ResumeGame()
    {
        player.transform.position = Vector3.zero;
        player.SetActive(true);
        player.GetComponent<Rigidbody>().velocity = Vector3.zero;
        gameOverDisplay.SetActive(false);
        asteroidSpawner.enabled = true;
        scoreSystem.PauseGame();
    }
    private void DisplayScore()
    {
        gameOverTxt.text = $"Game Over \n Score: {Mathf.FloorToInt(scoreSystem.Score)}";
    }
}
