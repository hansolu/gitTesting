//#define 이름부여가능 //여기서 부여한 이름은 Conditional 관련해서 사용가능.

public static class Debug
{
    [System.Diagnostics.Conditional("UNITY_EDITOR")]
    public static void Log(object msg) { UnityEngine.Debug.Log(msg); }

    [System.Diagnostics.Conditional("UNITY_EDITOR")]
    public static void LogError(object msg) { UnityEngine.Debug.LogError(msg); }
    [System.Diagnostics.Conditional("UNITY_EDITOR")]
    public static void LogWarning(object msg) { UnityEngine.Debug.LogWarning(msg); }
}
