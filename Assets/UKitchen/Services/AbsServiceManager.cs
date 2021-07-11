using System;
using System.Threading.Tasks;
using UKitchen.Logger;
using UnityEngine;
using UnityEngine.Networking;

namespace UKitchen.Services
{
    public abstract class AbsServiceManager
    {
        #region Connection Test

        protected const string CONNECTION_TEST_URL = "https://google.com";

        protected async Task<bool> ConnectionTestByUrl(string url)
        {
            using (UnityWebRequest uwr = UnityWebRequest.Head(url))
            {
                await uwr.SendWebRequest();

                return uwr.result == UnityWebRequest.Result.Success;
            }
        }

        #endregion

        #region Service Connections

        protected async Task<T> ResponseWithJson<T>(string url, string key) where T : AbsServiceResponse, new()
        {
            AppLog.Warning("<SERVICE>", key, url);

            if (string.IsNullOrEmpty(url))
                return new T {error = ServiceError.EmptyUrl};

            using (UnityWebRequest uwr = UnityWebRequest.Get(url))
            {
                await uwr.SendWebRequest();

                if (uwr.result == UnityWebRequest.Result.Success)
                {
                    if (string.IsNullOrEmpty(uwr.downloadHandler.text))
                        return new T {error = ServiceError.NotResponse};

                    try
                    {
                        return JsonUtility.FromJson<T>(uwr.downloadHandler.text);
                    }
                    catch (Exception )
                    {
                        return new T {error = ServiceError.ConvertJSONError};
                    }
                }
                else
                {
                    if (uwr.result == UnityWebRequest.Result.ConnectionError)
                        return new T {error = ServiceError.ConnectionError};
                    else if (uwr.result == UnityWebRequest.Result.ProtocolError)
                        return new T {error = ServiceError.ProtocolError};
                    else if (uwr.result == UnityWebRequest.Result.DataProcessingError)
                        return new T {error = ServiceError.DataProcessingError};
                }
            }
            return new T {error = ServiceError.Error};
        }

        #endregion
    }
}