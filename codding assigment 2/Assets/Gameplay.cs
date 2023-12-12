using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Gameplay : MonoBehaviour
{
    public TMP_Text timerText;
    public GameObject finishPanel;
    public TMP_Text itemsCollectedText;
    public float totalTimeInSeconds = 30f;

    private float currentTime;
    private bool isGameFinished = false;
    private float elapsedTime = 0f;


    private List<GameObject> collectedItems = new List<GameObject>();
    public int totalItems = 5;

    private int score = 0;
    public int coinScoreValue = 1;
    public int bombScoreValue = -1;

    private void Start()
    {
        currentTime = totalTimeInSeconds;
        UpdateTimerDisplay();
        finishPanel.SetActive(false);
    }

    private void Update()
    {
        if (!isGameFinished)
        {
            elapsedTime += Time.deltaTime;

            if (elapsedTime >= totalTimeInSeconds)
            {
                elapsedTime = totalTimeInSeconds;
                FinishGame();
            }
            else
            {
                currentTime = totalTimeInSeconds - elapsedTime;
                UpdateTimerDisplay();
            }
        }
    }

    private void UpdateTimerDisplay()
    {
        if (timerText != null)
        {
            timerText.text = "Time Remaining: " + currentTime.ToString("0");
        }

    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Coin"))
        {
            Debug.Log("Collected a coin!");
            CollectItem(collision.gameObject);
            Destroy(collision.gameObject);
        }
        else if (collision.CompareTag("Bomb"))
        {
            Debug.Log("Boommmmb!!!");
            CollectItem(collision.gameObject);
            Destroy(collision.gameObject);
        }
        else if (collision.CompareTag("Exit"))
        {
            FinishGame();
        }

    }

    private void UpdateItemsCollectedDisplay()
    {
        if (itemsCollectedText != null)
        {
            itemsCollectedText.text = "Items Collected: " + collectedItems.Count.ToString() + " / " + totalItems.ToString();
        }
    }

    public void CollectItem(GameObject item)
    {
        collectedItems.Add(item);
        if (item.CompareTag("Coin"))
        {
            UpdateScore(coinScoreValue);
        }
        else if (item.CompareTag("Bomb"))
        {
            UpdateScore(bombScoreValue);
        }

        if (collectedItems.Count >= totalItems && !isGameFinished)
        {
            isGameFinished = true;
            FinishGame();
        }

        UpdateItemsCollectedDisplay();
    }

    private void UpdateScore(int scoreChange)
    {
        score += scoreChange;
    }

    private void FinishGame()
    {
        if (finishPanel != null)
        {
            TMP_Text textComponent = finishPanel.GetComponentInChildren<TMP_Text>();
            if (textComponent != null)
            {
                textComponent.text = "Time Remaining: " + currentTime.ToString("0") +
                    "\nItems Collected: " + collectedItems.Count.ToString() + " / " + totalItems.ToString() +
                    "\nFinal Score: " + score.ToString();
            }
            finishPanel.SetActive(true);
        }
    }

}

