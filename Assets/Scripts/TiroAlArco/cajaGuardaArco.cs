using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using System.Collections;
using UnityEngine.Networking;

public class cajaGuardaArco : MonoBehaviour
{
    private UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable grabInteractable;
    private bool guardado;

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
            SubirPuntuacion();
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
        WWWForm form = new WWWForm();
        form.AddField("usuario_id", 5);
        form.AddField("tabla", "TiroAlArco");
        form.AddField("puntos", 3);
        form.AddField("tiempo", 10);

        UnityWebRequest www = UnityWebRequest.Post("https://192.168.1.134/guardar_puntuacion.php", form);

        www.certificateHandler = new AcceptAllCertificates();
        yield return www.SendWebRequest();

        Debug.Log(www.downloadHandler.text);
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
