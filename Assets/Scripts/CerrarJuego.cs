using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class CerrarJuego : MonoBehaviour
{
    private UnityEngine.XR.Interaction.Toolkit.Interactables.XRBaseInteractable interactable;

    void Awake()
    {
        interactable = GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRBaseInteractable>();

        interactable.activated.AddListener(CerrarLaAplicacion);
    }

    private void CerrarLaAplicacion(ActivateEventArgs args)
    {
        Debug.Log("Se ha activado el objeto para cerrar el juego.");

        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }

    void OnDestroy() => interactable.activated.RemoveListener(CerrarLaAplicacion);
}