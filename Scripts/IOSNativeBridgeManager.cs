using UnityEngine;

public class IOSNativeBridgeManager : MonoBehaviour
{
    private IIOSNativeBridge _bridge;

    private void Awake()
    {
        _bridge = new IOSNativeBridge();
    }
  
    public void InitializeSDK(string apiKey)
    {
        _bridge.Initialize(apiKey);
    }

    public void RequestLTVScore()
    {
        _bridge.GetLTVScore();
    }

    public void ReportEvent(AdEvent adEvent)
    {
        _bridge.Event(adEvent);
    }
}
