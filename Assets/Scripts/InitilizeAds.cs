using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class InitilizeAds : MonoBehaviour
{
    string gameId = "3869419";
    bool testMode = false;

    private void Start ()
    {
        Advertisement.Initialize (gameId, testMode);
    }
    
    public void ShowInterstitialAd() 
    {
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
