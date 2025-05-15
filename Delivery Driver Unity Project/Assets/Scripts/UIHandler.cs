using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIHandler : MonoBehaviour
{
    [Header("Player Values")]
    [SerializeField]
    float initialPlayerTime = 10f;

    [SerializeField]
    int initialPlayerScore = 0;

    [SerializeField]
    PlayerHealth playerHealth;


    [Header("Upper Border Overlay")]
    [SerializeField]
    TextMeshProUGUI deliveryText;

    [SerializeField]
    TextMeshProUGUI timerText;

    [SerializeField]
    TextMeshProUGUI eventMessageText;

    [Header("Death Overlay")]
    [SerializeField]
    GameObject parentDeathPanelObj;

    [SerializeField]
    TextMeshProUGUI deliveryDrivingPerformanceText;

    [SerializeField]
    TextMeshProUGUI gameDurationBonusText;

    [SerializeField]
    TextMeshProUGUI finalScoreText;

    [SerializeField]
    float fadeInDuration = 3f;

    Image[] deathOverlayImages;

    int currentDeliveries;
    public int CurrentDeliveries
    {
        get { return currentDeliveries; }
        set
        {
            currentDeliveries = value;
            SetDeliveryText(currentDeliveries);
        }
    }

    float currentTime;
    public float CurrentTime
    {
        get { return currentTime; }
        set 
        {
            currentTime = value; 
            SetTimeText(currentTime);
            if (currentTime <= 0)
            {
                Die();
            }
        }
    }

    string currentEventMessage;
    public string CurrentEventMessage
    {
        set 
        { 
            currentEventMessage = value;
            SetEventMessageText(currentEventMessage);
        }

    }

    bool isDead = false;
    float gameStartTime;

    const string MAIN_MENU_SCENE_STRING = "MainMenu";


    private void Die()
    {
        if (isDead) return;
        isDead = true;
        playerHealth.CurrentHealth = 0;
    }

    private void Awake()
    {
        InitializeUI();

        deathOverlayImages = parentDeathPanelObj.GetComponentsInChildren<Image>();

        gameStartTime = Time.time;
    }

    private void LateUpdate()
    {
        if (!isDead)
        {
            UpdateTimer();
        }
        else
        {
            SetTimeText(0);
        }
    }

    private void OnDestroy()
    {
        StopAllCoroutines();
    }

    private void InitializeUI()
    {
        CurrentDeliveries = initialPlayerScore;
        CurrentTime = initialPlayerTime;
        CurrentEventMessage = "Collect a delivery!";
    }

    private void SetEventMessageText(string newMessage)
    {
        eventMessageText.text = newMessage;
    }

    private void SetTimeText(float newTime)
    {
        timerText.text = newTime.ToString("0.00");
    }

    private void SetDeliveryText(float newDeliveries)
    {
        deliveryText.text = "Deliveries: " + newDeliveries.ToString();
    }

    void UpdateTimer()
    {
        CurrentTime -= Time.deltaTime;
    }

    public float GetGameDuration()
    {
        return Time.time - gameStartTime;
    }

    //Death Panel Logic
    public void ShowDeathPanel(Score playerScore)
    {
        deliveryDrivingPerformanceText.text = "Delivery-Driving Performance: " +
            $"{playerScore.GetDrivingPerformance()}";

        gameDurationBonusText.text = "Game Duration Bonus: " +
            $"{playerScore.GetGameDurationFactor()}";

        finalScoreText.text = "Final Score: " +
            $"{playerScore.GetFinalScore()}";

        StartCoroutine(ShowDeathPanel());
    }

    IEnumerator ShowDeathPanel()
    {
        parentDeathPanelObj.SetActive(true);
        float remainingFadeInDuration = 0;

        //Initial hiding
        foreach (Image image in deathOverlayImages)
        {
            image.color = new Color(
                image.color.r,
                image.color.g,
                image.color.b,
                0
                );
        }

        //Fade in death panel
        while (remainingFadeInDuration <= fadeInDuration)
        {
            foreach (Image image in deathOverlayImages)
            {
                image.color = new Color(
                    image.color.r,
                    image.color.g,
                    image.color.b,
                    remainingFadeInDuration / fadeInDuration
                    );
            }

            remainingFadeInDuration += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene(MAIN_MENU_SCENE_STRING);
    }
}
