using System;
using System.Runtime.InteropServices;
using UnityEngine;

public class IOSNativeBridge : IIOSNativeBridge
{
#if UNITY_IOS
    [DllImport("__Internal")]
    private static extern void datonomyKit_initialize(string apiKey, Action<int> completionHandler);

    [DllImport("__Internal")]
    private static extern void DatonomyKit_getLTVScore(Action<double> callback);

    [DllImport("__Internal")]
    private static extern void DatonomyKit_event(string eventName, Action<int> completionHandlerEvent);
#endif
    public void Initialize(string apiKey)
    {
#if UNITY_IOS
        datonomyKit_initialize(apiKey,completionHandler);
        Debug.LogWarning("running on iOS!");
#else
        Debug.LogWarning("Not running on iOS!");
#endif
    }

    public void GetLTVScore()
    {
#if UNITY_IOS
            DatonomyKit_getLTVScore(completionHandlerLTVScore);
#else
        Debug.LogWarning("Not running on iOS!");
#endif
    }

    public void Event(string eventName)
    {
#if UNITY_IOS
            DatonomyKit_event(eventName, completionHandlerEvent);
#else
        Debug.LogWarning("Not running on iOS!");
#endif
    }

    [AOT.MonoPInvokeCallback(typeof(Action<int>))]
    private static void completionHandler(int result)
    {
        AdEvent adEventStatic = new AdEvent
        {
            type = 0,
            impression = new Impression
            {
                TypeAdsString = AdsType.banner, // Use TypeAdsString em vez de typeAds
                Revenue = 5.5,
                NetworkName = "SampleNetwork",
                Currency = "BRL"
            }
        };
        string adEventBase64 = adEventStatic.ToBase64();
        Debug.Log($"AdEvent b64: {adEventBase64}");
        string jsonString = adEventStatic.FromBase64ToString(adEventBase64);
        Debug.Log($"AdEvent J: {jsonString}");

        Debug.Log($"Initialization Completed with State: {result}");
        DatonomyKit_getLTVScore(completionHandlerLTVScore);
        DatonomyKit_event(adEventBase64, completionHandlerEvent);

    }
    [AOT.MonoPInvokeCallback(typeof(Action<double>))]
    private static void completionHandlerLTVScore(double score)
    {
        Debug.Log($"LTV Score: {score}");
    }

    [AOT.MonoPInvokeCallback(typeof(Action<int>))]
    private static void completionHandlerEvent(int result)
    {
        Debug.Log($"Event Completed with State: {result}");
    }

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void LTVScoreCallback(double ltvScore, DatonomySDKError error);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void EventCallback(double result, DatonomySDKError error);

    [AOT.MonoPInvokeCallback(typeof(LTVScoreCallback))]
    private static void OnLTVScoreReceived(double ltvScore, DatonomySDKError error)
    {
        Debug.Log($"LTV Score: {ltvScore}, Error: {error}");
    }

    [AOT.MonoPInvokeCallback(typeof(EventCallback))]
    private static void OnEventCompleted(double result, DatonomySDKError error)
    {
        Debug.Log($"Event Result: {result}, Error: {error}");
    }
}


