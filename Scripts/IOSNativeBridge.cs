using System;
using System.Runtime.InteropServices;
using UnityEngine;

namespace Datonomy.DatonomySDK
{
    public class IOSNativeBridge : IIOSNativeBridge
    {
        #region DllImports

#if UNITY_IOS
        [DllImport("__Internal")]
        private static extern void datonomyKit_initialize(string apiKey, Action<int> completionHandler);

        [DllImport("__Internal")]
        private static extern void DatonomyKit_getLTVScore(Action<double> callback);

        [DllImport("__Internal")]
        private static extern void DatonomyKit_event(string eventName, Action<int> completionHandlerEvent);
#endif

        #endregion

        #region Public Methods

        public void Initialize(string apiKey)
        {
#if UNITY_IOS
            datonomyKit_initialize(apiKey, completionHandler);
            Debug.LogWarning("Running on iOS!");
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

        public void Event(AdEvent adEvent)
        {
            string adEventBase64 = adEvent.ToBase64();
#if UNITY_IOS
            DatonomyKit_event(adEventBase64, completionHandlerEvent);
#else
            Debug.LogWarning("Not running on iOS!");
#endif
        }

        #endregion

        #region Callback Handlers

        [AOT.MonoPInvokeCallback(typeof(Action<int>))]
        private static void completionHandler(int result)
        {
            Debug.Log($"Initialization Completed with State: {result}");
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

        #endregion

        #region Delegates

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void LTVScoreCallback(double ltvScore, DatonomySDKError error);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void EventCallback(double result, DatonomySDKError error);

        #endregion
    }
}
