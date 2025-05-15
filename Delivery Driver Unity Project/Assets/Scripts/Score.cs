using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Store score values for the player
public class Score
{
    //base score attributes
    int baseScore;
    int numOfCrashes;
    int numOfDeliveries;
    float gameDuration;
    int finalScore;

    //constants
    const float GOOD_PERFORMANCE_INDEX = 1.5f;
    const float BAD_PERFORMANCE_INDEX = 0.25f;
    const int GOOD_TIME_FACTOR_INDEX = 5;

    public Score(int baseScore)
    {
        numOfCrashes = 0;
        numOfDeliveries = 0;
        this.baseScore = baseScore;
    }

    public void IncrementCrash()
    {
        numOfCrashes++;
    }
    public void IncrementDeliveries()
    {
        numOfDeliveries++;
    }

    public void AddGameDuration(float gameDuration)
    {
        this.gameDuration = gameDuration;
    }
    public void AddBaseScore(int givenScore)
    {
        baseScore = givenScore;
    }

    public void CalculateFinalScore()
    {
        //Calculate delivery-driving performance
        float deliveryDrivingPerformanceFactor = GetDrivingPerformance();
        Debug.Log($"delivery driving performance: {deliveryDrivingPerformanceFactor}");

        //Calculate game duration factor
        float gameDurationFactor = GetGameDurationFactor();

        Debug.Log($"Game Duration factor: {gameDurationFactor}");

        //calculate final score
        finalScore = (int)(baseScore * deliveryDrivingPerformanceFactor * gameDurationFactor);

    }

    public float GetGameDurationFactor()
    {
        float gameDurationFactor = Mathf.Floor(gameDuration / GOOD_TIME_FACTOR_INDEX);
        gameDurationFactor *= GOOD_PERFORMANCE_INDEX;
        return gameDurationFactor;
    }

    public int GetFinalScore()
    {
        CalculateFinalScore();
        return finalScore;
    }

    public float GetDrivingPerformance()
    {
        float deliveryDrivingPerformanceFactor = (numOfDeliveries - numOfCrashes);
        if (deliveryDrivingPerformanceFactor >= 0)
        {
            deliveryDrivingPerformanceFactor = (deliveryDrivingPerformanceFactor * GOOD_PERFORMANCE_INDEX);
        }
        else
        {
            deliveryDrivingPerformanceFactor = BAD_PERFORMANCE_INDEX;
        }

        return deliveryDrivingPerformanceFactor;
    }
}
