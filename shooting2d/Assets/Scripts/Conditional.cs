//#define �̸��ο����� //���⼭ �ο��� �̸��� Conditional �����ؼ� ��밡��.

public static class Debug
{
    [System.Diagnostics.Conditional("UNITY_EDITOR")]
    public static void Log(object msg) { UnityEngine.Debug.Log(msg); }

    [System.Diagnostics.Conditional("UNITY_EDITOR")]
    public static void LogError(object msg) { UnityEngine.Debug.LogError(msg); }
    [System.Diagnostics.Conditional("UNITY_EDITOR")]
    public static void LogWarning(object msg) { UnityEngine.Debug.LogWarning(msg); }
}
