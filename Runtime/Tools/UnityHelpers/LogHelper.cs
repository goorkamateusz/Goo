using UnityEngine;

namespace Goo.Tools.UnityHelpers
{
    public static class LogHelper
    {
        public static void Log(this Object obj, string msg) => Debug.Log(LogMessage(obj, msg));
        public static void LogWarning(this Object obj, string msg) => Debug.LogWarning(LogMessage(obj, msg));
        public static void LogError(this Object obj, string msg) => Debug.LogError(LogMessage(obj, msg));

        public static void Log(this GameObject obj, string msg) => Debug.Log(LogMessage(obj, msg));
        public static void LogWarning(this GameObject obj, string msg) => Debug.LogWarning(LogMessage(obj, msg));
        public static void LogError(this GameObject obj, string msg) => Debug.LogError(LogMessage(obj, msg));

        public static string LogMessage(Object obj, string msg)
        {
            return $"[{obj.name}] {msg}";
        }

        public static string LogMessage(GameObject obj, string msg)
        {
            return $"{msg}\n| {obj.PathToGameObject()}\n";
        }

        public static string PathToGameObject(this GameObject obj)
        {
            return obj.transform.PathToGameObject();
        }

        public static string PathToGameObject(this Transform obj)
        {
            string path = obj.name;
            while (obj != null)
            {
                // idea optimise with string builder
                path = $"{obj.name}/{path}";
                obj = obj.parent;
            }
            return path;
        }
    }
}
