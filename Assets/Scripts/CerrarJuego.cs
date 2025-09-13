using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class CerrarJuego : MonoBehaviour
{
    private UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable interactable;

    void Awake()
    {
        interactable = GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable>();

        interactable.selectExited.AddListener(CerrarLaAplicacion);
    }

    private void CerrarLaAplicacion(SelectExitEventArgs args)
    {
        Debug.Log("Se ha activado el objeto para cerrar el juego.");

        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }

    void OnDestroy() => interactable.selectExited.RemoveListener(CerrarLaAplicacion);
}