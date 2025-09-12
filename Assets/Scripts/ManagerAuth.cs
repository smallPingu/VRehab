using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using TMPro;

public class ManagerAuth : MonoBehaviour
{
    public TMP_InputField emailInputField;
    public TMP_InputField passwordInputField;

    public TextoFade controladorMensaje; 

    private string loginUrl = "http://localhost:3000/api/auth/login";

    [System.Serializable]
    private class LoginData
    {
        public string email;
        public string password;
    }

    [System.Serializable]
    private class UserData
    {
        public string name;
    }

    [System.Serializable]
    private class TokenResponse
    {
        public string token;
        public UserData user;
    }

    public void IniciarLogin()
    {

        string email = emailInputField.text;
        string password = passwordInputField.text;

        StartCoroutine(LoginCoroutine(email, password));

    }

    private IEnumerator LoginCoroutine(string email, string password)
    {
        LoginData data = new() { email = email, password = password };
        string jsonData = JsonUtility.ToJson(data);

        using UnityWebRequest www = new UnityWebRequest(loginUrl, "POST");
        byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(jsonData);
        www.uploadHandler = new UploadHandlerRaw(bodyRaw);
        www.downloadHandler = new DownloadHandlerBuffer();
        www.SetRequestHeader("Content-Type", "application/json");

        Debug.Log("Enviando petición de login a: " + loginUrl);

        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError("Error en el login: " + www.error);
            controladorMensaje?.MostrarMensaje("Error de conexión");
        }
        else
        {
            string jsonResponse = www.downloadHandler.text;
            TokenResponse tokenResponse = JsonUtility.FromJson<TokenResponse>(jsonResponse);

            PlayerPrefs.SetString("jwt_token", tokenResponse.token);
            PlayerPrefs.Save();
            
            string nombreUsuario = tokenResponse.user.name;

            Debug.Log($"¡Login exitoso para {nombreUsuario}! Token guardado.");

            controladorMensaje?.MostrarMensaje($"Bienvenido, {nombreUsuario}");
        }
    }

    public void CerrarSesion()
    {
        string tokenKey = "jwt_token";

        if (PlayerPrefs.HasKey(tokenKey))
        {
            PlayerPrefs.DeleteKey(tokenKey);
            PlayerPrefs.Save();

            Debug.Log("Token eliminado y sesión cerrada.");
        }
        else
        {
            Debug.LogWarning("No se encontró ningún token para borrar.");
        }
    }
}