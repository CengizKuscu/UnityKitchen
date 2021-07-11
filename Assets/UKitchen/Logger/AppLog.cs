using System.Text;
using UnityEngine;

namespace UKitchen.Logger
{
    public static class AppLog
    {
        public static void Info(object e, params object[] args)
        {
#if APP_LOG
            Debug.Log(GetLog(e.GetType().Name, args));
#endif
        }

        public static void Info(string e, params object[] args)
        {
#if APP_LOG
            Debug.Log(GetLog(e, args));
#endif
        }
        
        public static void Warning(object obj, params object[] args)
        {
#if APP_LOG
            Debug.LogWarning(string.Format("<color=orange>{0}</color>", GetLog(obj.GetType().Name, args)));
#endif
        }

        public static void Warning(string obj, params object[] args)
        {
#if APP_LOG
            Debug.LogWarning(string.Format("<color=orange>{0}</color>", GetLog(obj, args)));
#endif
        }

        public static void Error(object obj, params object[] args)
        {
#if APP_LOG
            Debug.LogError(string.Format("<color=red>{0}</color>", GetLog(obj.GetType().Name, args)));
#endif
        }

        public static void Error(string obj, params object[] args)
        {
#if APP_LOG
            Debug.LogError(string.Format("<color=red>{0}</color>", GetLog(obj, args)));
#endif
        }

        private static string GetLog(string obj, params object[] args)
        {
            string param = string.Format("[{0}]", obj);

            StringBuilder sb = new StringBuilder(param);

            for (int i = 0; i < args.Length; i++)
            {
                sb.AppendFormat(" {0}", args[i] == null ? "null" : args[i].ToString());
            }
            
            return sb.ToString();
        }
    }
}