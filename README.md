# DatonomySDK Documentation

The `DatonomySDK` provides seamless and rapid integration with native iOS services for Unity. Below, you'll find a brief description of how to set up and use the main features of the SDK.

## Initial Setup

1. Ensure that the `IOSNativeBridgeManager` is attached to a `GameObject` in your scene named `IOSBridgeManager`.
2. In the `Start` method of your script, the SDK will automatically initialize with the provided API key. If you need to use one provided by Datonomy, replace this string with the desired value.
3. All the functions have their own callback

### Sample Code for Initial Setup:
```csharp
using System;
using System.Runtime.InteropServices;
using UnityEngine;

public class IOSNativeBridge : IIOSNativeBridge
{
#if UNITY_IOS
    [DllImport("__Internal")]
    private static extern void datonomyKit_initialize(string apiKey, Action<int> completionHandler);
#endif
    public void Initialize(string apiKey)
    {
#if UNITY_IOS
        datonomyKit_initialize(apiKey,completionHandler);
        Debug.LogWarning("Running on iOS!");
#else
        Debug.LogWarning("Not running on iOS!");
#endif
    }


}
```
## Available Methods
### InitializeSDK
Initializes the SDK with a specific API key.

Parameters:

apiKey (string): The API key for authentication.
Usage Example:

```csharp
InitializeSDK("YourAPIKeyHere");
```
## RequestLTVScore
### Requests the user's LTV (Lifetime Value) score. The response is natively processed by the SDK.

Usage Example:

```csharp
GetLTVScore();
```
## ReportEvent
### Reports a specific event to the platform.

Parameters:

eventName (Object): The name of the event you wish to report.
Usage Example:

```csharp
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

Event(EventObject);
```

# Callbacks Documentation for DatonomySDK

In the `DatonomySDK`, several callbacks are used to handle responses and events natively. This documentation provides insights into these callbacks, offering a clear understanding of their functionalities and use cases.

## 1. Initialization Completion Handler

The `completionHandler` is primarily invoked once the SDK initialization is finished. It's used to handle the result of the initialization and to manage events that follow.

### Signature:
```csharp
[AOT.MonoPInvokeCallback(typeof(Action<int>))]
private static void completionHandler(int result)
```
### Behavior:
Creates an AdEvent instance with defined attributes.
Converts the AdEvent to a Base64 string and logs it.
Converts the Base64 string back to its JSON format and logs the JSON.
Logs the state of SDK initialization.
Makes calls to fetch the LTV score and reports an event.
Usage Notes:
Ensure you're using the TypeAdsString instead of typeAds when setting the impression type.

2. LTV Score Completion Handler
This callback, completionHandlerLTVScore, is triggered to handle the response after requesting the LTV (Lifetime Value) score for a user.

Signature:
```csharp
Copy code
[AOT.MonoPInvokeCallback(typeof(Action<double>))]
private static void completionHandlerLTVScore(double score)
```
### Behavior:
Logs the received LTV score.

3. Event Completion Handler
The completionHandlerEvent handles the result after reporting a specific event to the platform.

Signature:
```csharp
Copy code
[AOT.MonoPInvokeCallback(typeof(Action<int>))]
private static void completionHandlerEvent(int result)
```
### Behavior:
Logs the state of the event reporting process.
### Additional Considerations

Ensure that all dependencies and native libraries required for the DatonomySDK are correctly imported and set up in your Unity project. To make sure the SDK operates correctly, refrain from invoking its methods before it has been initialized. Keep the SDK updated to the latest versions to ensure optimal compatibility and security. We hope this documentation aids you in effectively integrating and utilizing the DatonomySDK. Should you have further questions, please reach out to our support team.