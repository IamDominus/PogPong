using GoogleMobileAds.Api;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AdManager : MonoBehaviour
{
    private const string BANNERID = "ca-app-pub-2135290352531087/3918493627";
    private BannerView _bannerView;
    public bool IsBannerLoaded { get; private set; }
    public bool IsBannerShown { get; private set; }

    private void ChangedActiveScene(Scene current, Scene next)
    {
        if (IsBannerLoaded)
        {
            ShowBanner();
        }
    }
    private void Awake()
    {
        Debug.Log("AdManager Awake");
        SceneManager.activeSceneChanged += ChangedActiveScene;

        MobileAds.Initialize(initStatus => { });

        AdSize adaptiveSize = AdSize.GetCurrentOrientationAnchoredAdaptiveBannerAdSizeWithWidth(AdSize.FullWidth);
        _bannerView = new BannerView(BANNERID, adaptiveSize, AdPosition.Top);
        _bannerView.OnAdLoaded += HandleBannerAdLoaded;
        AdRequest bannerRequest = new AdRequest.Builder().Build();
        _bannerView.LoadAd(bannerRequest);
        if (IsBannerLoaded)
        {
            ShowBanner();
        }
        DontDestroyOnLoad(transform.gameObject);
    }
    private void HandleBannerAdLoaded(object sender, EventArgs e)
    {
        IsBannerLoaded = true;
    }

    public void ShowBanner()
    {
        _bannerView.Show();
        IsBannerShown = true;
    }

    public void HideBanner()
    {
        _bannerView.Hide();
        IsBannerShown = false;
    }
}
