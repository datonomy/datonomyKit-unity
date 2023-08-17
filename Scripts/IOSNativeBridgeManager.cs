using UnityEngine;

public class IOSNativeBridgeManager : MonoBehaviour
{
    private IIOSNativeBridge _bridge;

    private void Awake()
    {
        Debug.Log("---> entrando no awake");
#if UNITY_IOS
        _bridge = new IOSNativeBridge();
        Debug.Log("inicializando a bridge");
#else
        Debug.LogWarning("Not running on iOS, the bridge won't be instantiated.");
#endif
    }

    void Start()
    {
        Debug.Log("-------> Datonomy SDK Inicializando 〄");

#if UNITY_IOS
    Invoke("DelayedInitialization", 2.0f);  // Adia a inicialização por 2 segundos.
#else
        Debug.LogWarning("Not running on iOS, SDK initialization skipped.");
#endif
    }

    private void DelayedInitialization()
    {
        InitializeSDK("bvjI2tBTpU8Wd5Id1lP037e0ccTuZWqu91Y4Icdp");
        Debug.Log("-------> Datonomy SDK Inicializado ✅");
        // RequestLTVScore();
    }


    public void InitializeSDK(string apiKey)
    {
        _bridge?.Initialize(apiKey);
    }

    // public void RequestLTVScore()
    // {
    //     _bridge?.GetLTVScore();
    // }

    // public void ReportEvent(AdEvent adEvent)
    // {
    //     _bridge?.Event(adEvent);
    // }
}
