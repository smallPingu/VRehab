using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System;

[System.Serializable]
public class ParametrosData
{
    public int cuentaAtras;
    public float velocidadJuego;
    public float anguloMinimo;
    public int nivelInicial;
}

public class EnvioRecibeParam : MonoBehaviour
{
    [SerializeField] private string url = "https://react-web-tfg.vercel.app/api/parametros";
    [System.Serializable]
    private class ParametrosPostBody
    {
        public ParametrosData parametros;
    }

    public void ObtenerParametros(Action<ParametrosData> onSuccess, Action<string> onError)
    {
        StartCoroutine(GetParametersCoroutine(onSuccess, onError));
    }

    private IEnumerator GetParametersCoroutine(Action<ParametrosData> onSuccess, Action<string> onError)
    {
        string token = PlayerPrefs.GetString("jwt_token", null);
        if (string.IsNullOrEmpty(token))
        {
            onError?.Invoke("Token no encontrado en PlayerPrefs.");
            yield break;
        }

        using (UnityWebRequest request = UnityWebRequest.Get(url))
        {
            request.SetRequestHeader("Authorization", "Bearer " + token);
            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.Success)
            {
                string json = request.downloadHandler.text;
                ParametrosData data = JsonUtility.FromJson<ParametrosData>(json);
                onSuccess?.Invoke(data);
            }
            else
            {
                onError?.Invoke(request.error);
            }
        }
    }

    public void GuardarParametros(ParametrosData data, Action onSuccess, Action<string> onError)
    {
        Debug.Log("Token actual: " + PlayerPrefs.GetString("jwt_token", "NO TOKEN"));
        StartCoroutine(SaveParametersCoroutine(data, onSuccess, onError));
    }

    private IEnumerator SaveParametersCoroutine(ParametrosData data, Action onSuccess, Action<string> onError)
    {
        string token = PlayerPrefs.GetString("jwt_token", null);
        if (string.IsNullOrEmpty(token))
        {
            onError?.Invoke("Token no encontrado.");
            yield break;
        }

        ParametrosPostBody postBody = new ParametrosPostBody
        {
            parametros = data
        };
        string jsonBody = JsonUtility.ToJson(postBody);

        using (UnityWebRequest request = new UnityWebRequest(url, "POST"))
        {
            byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(jsonBody);
            request.uploadHandler = new UploadHandlerRaw(bodyRaw);
            request.downloadHandler = new DownloadHandlerBuffer();

            request.SetRequestHeader("Content-Type", "application/json");
            request.SetRequestHeader("Authorization", "Bearer " + token);

            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.Success)
            {
                onSuccess?.Invoke();
            }
            else
            {
                Debug.LogError("Error al guardar: " + request.error + " - " + request.downloadHandler.text);
                onError?.Invoke(request.error);
            }
        }
    }
}