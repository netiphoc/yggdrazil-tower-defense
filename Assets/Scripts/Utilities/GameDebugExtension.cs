using UnityEngine;

namespace Utilities
{
    public static class GameDebugExtension
    {
        public static bool EnableGameLog = true;

        public static void Log(this object obj, string log)
        {
            if (!EnableGameLog) return;
            Debug.Log($"[{obj.GetType().FullName}] <color=yellow>{log}</color>");
        }

        public static void ErrorLog(this object obj, string log)
        {
            if (!EnableGameLog) return;
            Debug.LogError($"[{obj.GetType().FullName}] <color=red>{log}</color>");
        }

        public static void DebugAssert(this Object obj)
        {
            if (!EnableGameLog) return;
            Debug.Assert(obj != null);
        }
    }
}