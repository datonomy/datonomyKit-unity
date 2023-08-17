/*
    Datonomy 2023 unity plugin for datonomy kit sdk
*/
using System;
using System.Text;
using UnityEngine;
/// AdEvent class is used to store the data for ad events
[System.Serializable]
public class AdEvent
{
    public int type;
    public Impression impression;
    public TransactionIAP transactionIAP;

    public static AdEvent FromJson(string json)
    {
        return JsonUtility.FromJson<AdEvent>(json);
    }

    public string ToJson()
    {
        return JsonUtility.ToJson(this);
    }

    public string ToBase64()
    {
        string stringRepresentation = this.ToJson();
        return Convert.ToBase64String(Encoding.UTF8.GetBytes(stringRepresentation));
    }
    public string FromBase64ToString(string base64)
    {
        byte[] bytes = Convert.FromBase64String(base64);
        return Encoding.UTF8.GetString(bytes);
    }
}
