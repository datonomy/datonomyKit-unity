
// IOSNativeBridge.cs
using System;
using System.Runtime.InteropServices;
using UnityEngine;

public class IOSNativeBridge : IIOSNativeBridge
{
#if UNITY_IOS
    [DllImport("__Internal")]
    private static extern void DatonomyKit_initialize(string apiKey, InitializeCallback callback);

    [DllImport("__Internal")]
    private static extern void DatonomyKit_getLTVScore(LTVScoreCallback callback);

    [DllImport("__Internal")]
    private static extern void DatonomyKit_event(AdEvent adEvent, EventCallback callback);
#endif

    public void Initialize(string apiKey)
    {
#if UNITY_IOS
        DatonomyKit_initialize(apiKey, OnInitializeCompleted);
#else
        Debug.Log("Not running on iOS!");
#endif
    }

    public void GetLTVScore()
    {
#if UNITY_IOS
        DatonomyKit_getLTVScore(OnLTVScoreReceived);
#else
        Debug.Log("Not running on iOS!");
#endif
    }

    public void Event(AdEvent adEvent)
    {
#if UNITY_IOS
        DatonomyKit_event(adEvent, OnEventCompleted);
#else
        Debug.Log("Not running on iOS!");
#endif
    }

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void InitializeCallback(SdkState state, DatonomySDKError error);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void LTVScoreCallback(double ltvScore, DatonomySDKError error);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void EventCallback(double result, DatonomySDKError error);

    [AOT.MonoPInvokeCallback(typeof(InitializeCallback))]
    private static void OnInitializeCompleted(SdkState state, DatonomySDKError error)
    {
        Debug.Log($"Initialization Completed with State: {state}, Error: {error}");
    }

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
