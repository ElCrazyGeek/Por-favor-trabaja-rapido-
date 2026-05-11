using UnityEngine;

public class Logger : MonoBehaviour
{
    public static Logger Instance {get; set;}


    [Header("Settings")]
    public bool showLogs;

    void Awake()
    {
        if (Instance != this && Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void Log(object message, Object sender)
    {
        if (showLogs)
        {
            Debug.Log(message, sender);
        }
    }
}
