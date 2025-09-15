using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using System.Collections;
using UnityEngine.Networking;
using System.Text;

public class CajaGuardaCoche : MonoBehaviour
{
    private UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable grabInteractable;
    private bool guardado;

    [System.Serializable]
    private class DatosPuntuCoche
    {
        public string juego;
        public float duracion;
        public string platforma;
        public string versionJuego;
        public float points;
    }

    void Awake()
    {
        guardado = false;
        grabInteractable = GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable>();
        grabInteractable.selectEntered.AddListener(OnGrab);
        grabInteractable.selectExited.AddListener(OnRelease);
    }

    private void OnGrab(SelectEnterEventArgs args)
    {
        Debug.Log($"{gameObject.name} was grabbed by {args.interactorObject.transform.name}");
    }

    private void OnRelease(SelectExitEventArgs args)
    {
        Debug.Log($"{gameObject.name} was released by {args.interactorObject.transform.name}");
        if (!guardado)
        {
            StartCoroutine(SubirPuntuacion());
            guardado = true;
        }
    }

    void OnDestroy()
    {
        grabInteractable.selectEntered.RemoveListener(OnGrab);
        grabInteractable.selectExited.RemoveListener(OnRelease);
    }

    IEnumerator SubirPuntuacion()
    {
        string token = PlayerPrefs.GetString("jwt_token", null);
        if (string.IsNullOrEmpty(token))
        {
            Debug.LogError("No se encontró token de autenticación. El usuario debe iniciar sesión.");
            yield break;
        }

        DatosPuntuCoche dataPuntu = new()
        {
            juego = "RotacionCoches",
            duracion = GuardarPuntuacion.Instance.GetTiempoCoches(),
            platforma = Application.platform.ToString(),
            versionJuego = "1.0.0",
            points = GuardarPuntuacion.Instance.GetPuntCoches()
        };

        string jsonData = JsonUtility.ToJson(dataPuntu);
        Debug.Log("Enviando JSON: " + jsonData);

        string url = "http://localhost:3000/api/puntuaciones";

        using UnityWebRequest www = new UnityWebRequest(url, "POST");
        byte[] bodyRaw = Encoding.UTF8.GetBytes(jsonData);
        www.uploadHandler = new UploadHandlerRaw(bodyRaw);
        www.downloadHandler = new DownloadHandlerBuffer();

        www.SetRequestHeader("Content-Type", "application/json");
        www.SetRequestHeader("Authorization", "Bearer " + token);

        www.certificateHandler = new AcceptAllCertificates();

        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError($"Error al subir puntuación: {www.error}");
            Debug.LogError($"Respuesta del servidor: {www.downloadHandler.text}");
        }
        else
        {
            Debug.Log("Puntuación de coches subida con éxito!");
            Debug.Log($"Respuesta del servidor: {www.downloadHandler.text}");
        }
    }

    // Clase interna para ignorar la validación SSL
    private class AcceptAllCertificates : CertificateHandler
    {
        protected override bool ValidateCertificate(byte[] certificateData)
        {
            return true;
        }
    }
}