using UnityEngine;
using Datonomy.DatonomySDK;

public class IOSNativeBridgeManager : MonoBehaviour
{
    private IIOSNativeBridge _bridge;

    private void Awake()
    {

#if UNITY_IOS
        Datonomy.DatonomySDK.IOSNativeBridge _bridge = new Datonomy.DatonomySDK.IOSNativeBridge();
#else
        Debug.LogWarning("Not running on iOS, the bridge won't be instantiated.");
#endif
    }

    void Start()
    {


#if UNITY_IOS
    InitializeSDK("your api key");
#else
        Debug.LogWarning("Not running on iOS, SDK initialization skipped.");
#endif
    }


    public void InitializeSDK(string apiKey)
    {
        _bridge?.Initialize(apiKey);
    }

    public void RequestLTVScore()
    {
        _bridge?.GetLTVScore();
    }

    public void ReportEvent(AdEvent adEvent)
    {
        _bridge?.Event(adEvent);
    }
}
