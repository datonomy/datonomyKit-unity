/*
    Datonomy 2023 unity plugin for datonomy kit sdk
*/

using UnityEngine;
/// Impression class is used to store the data for ad impressions
[System.Serializable]
public class Impression
{

    [SerializeField]
    private string typeAds;

    public AdsType TypeAdsString
    {
        get
        {
            return (AdsType)System.Enum.Parse(typeof(AdsType), typeAds, true);
        }
        set
        {
            typeAds = value.ToString().ToLower();
        }
    }
    [SerializeField]
    private double revenue;
    public double Revenue
    {
        get { return revenue; }
        set { revenue = value; }
    }

    [SerializeField]
    private string networkName;
    public string NetworkName
    {
        get { return networkName; }
        set { networkName = value; }
    }

    [SerializeField]
    private string currency;
    public string Currency
    {
        get { return currency; }
        set { currency = value; }
    }

}

public enum AdsType
{
    banner,
    video,
    interstitial,
    rewarded,
    mrec
}
