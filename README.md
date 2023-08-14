# DatonomySDK Documentation

The `DatonomySDK` offers seamless and quick integration with native iOS services for Unity. Below you will find a brief description of how to set up and utilize the main features of the SDK.

## Initial Setup

1. Ensure that the `IOSNativeBridgeManager` is attached to a `GameObject` in your scene, named `IOSBridgeManager`.
2. In the `Start` method of the `IOSNativeBridgeManager` script, the SDK will be automatically initialized with the provided API key. If you need to use one provided by datonomy, replace this string with the desired value.

## Available Methods

### InitializeSDK

Initializes the SDK with a specific API key.

**Parameters**:
- `apiKey` (string): The API key for authentication.

**Usage Example**:
```csharp
InitializeSDK("YourAPIKeyHere");
```
### RequestLTVScore

Requests the user's LTV (Lifetime Value) Score. The response is processed natively by the SDK.

***Usage Example***:

```csharp
RequestLTVScore();
```
### ReportEvent
Reports a specific event to the platform.

Parameters:

adEvent (AdEvent): The event you wish to report.

***Usage Example***:

```csharp

AdEvent myEvent = new AdEvent() {
    // Define event attributes here
};
ReportEvent(myEvent);
```
### Additional Considerations:

Ensure all dependencies and native libraries required for DatonomySDK are correctly imported and configured in your Unity project. To ensure the SDK operates correctly, avoid invoking its methods before the SDK has been initialized. Keep the SDK updated with the latest versions to ensure optimal compatibility and security. We hope this documentation assists you in effectively integrating and using the DatonomySDK. Should you have any further queries, please get in touch with our support team.
 
