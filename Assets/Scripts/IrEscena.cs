using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.XR.Interaction.Toolkit;

public class IrEscena : MonoBehaviour
{
    private UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable grabInteractable;
    private bool wasGrabbed = false;
    public string escenaObjetivo = "MenuPrincipal";

    void Awake()
    {
        grabInteractable = GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable>();

        grabInteractable.selectEntered.AddListener(OnGrab);
        grabInteractable.selectExited.AddListener(OnRelease);
    }

    private void OnGrab(SelectEnterEventArgs args)
    {
        wasGrabbed = true;
        Debug.Log("Objeto agarrado");
    }

    private void OnRelease(SelectExitEventArgs args)
    {
        if (wasGrabbed)
        {
            Debug.Log("Objeto soltado. Cargando escena...");
            SceneManager.LoadScene(escenaObjetivo);
        }
    }

    void OnDestroy()
    {
        grabInteractable.selectEntered.RemoveListener(OnGrab);
        grabInteractable.selectExited.RemoveListener(OnRelease);
    }
}
