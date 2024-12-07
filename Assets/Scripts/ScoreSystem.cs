using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreSystem : MonoBehaviour
{
    [SerializeField] private TMP_Text scoreTxt;
    [SerializeField] private int scoreMultipliyer;

    public float Score { get; private set; }
    private bool isPaused;
    
    public void PauseGame()
    {
        isPaused = !isPaused;
        scoreTxt.text = string.Empty;
    }
    private void Update()
    {
        if (isPaused) return;
        Score += Time.deltaTime * scoreMultipliyer;
        scoreTxt.text = Mathf.FloorToInt(Score).ToString();
    }
}
