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
    [SerializeField] private string apiBaseUrl = "https://react-web-tfg.vercel.app";

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

        string url = apiBaseUrl + "/api/parametros";
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
        
        string url = apiBaseUrl + "/api/parametros";
        string jsonBody = JsonUtility.ToJson(data);

        using (UnityWebRequest request = UnityWebRequest.PostWwwForm(url, "POST"))
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
                onError?.Invoke(request.error);
            }
        }
    }
}