﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class InitilizeAds : MonoBehaviour
{
    string gameId = "3869419";
    bool testMode = true;

    private void Start ()
    {
        Advertisement.Initialize (gameId, testMode);
    }
    
    public void ShowInterstitialAd() {
        // Check if UnityAds ready before calling Show method:
        if (Advertisement.IsReady()) 
        {
            Advertisement.Show();
        } 
        else
        {
            Debug.Log("Interstitial ad not ready at the moment! Please try again later!");
        }
    }
}
