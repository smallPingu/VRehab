using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.Interaction.Toolkit;

public class ControlCuerda : MonoBehaviour
{
    [SerializeField]
    private CuerdaArco cuerdaRenderer;

    private UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable interactableVR;

    [SerializeField]
    private Transform medioCuerdaAgarrar, medioCuerdaVisual, puntoMedio;

    [SerializeField]
    private float limiteEstirarCuerda = 0.3f;

    private Transform interactuador;

    private float fuerza;

    public UnityEvent OnTirarCuerda;
    public UnityEvent<float> OnSoltarCuerda;

    private void Awake()
    {
        interactableVR = medioCuerdaAgarrar.GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable>();
    }

    private void Start()
    {
        interactableVR.selectEntered.AddListener(PrepararCuerda);
        interactableVR.selectExited.AddListener(ResetearCuerda);
    }

    private void ResetearCuerda(SelectExitEventArgs arg0)
    {
        OnSoltarCuerda?.Invoke(fuerza);
        fuerza = 0;

        interactuador = null;
        medioCuerdaAgarrar.localPosition = Vector3.zero;
        medioCuerdaVisual.localPosition = Vector3.zero;
        cuerdaRenderer.InicializarCuerda(null);

    }

    private void PrepararCuerda(SelectEnterEventArgs arg0)
    {
        interactuador = arg0.interactorObject.transform;
        OnTirarCuerda?.Invoke();
    }

    private void Update()
    {
        if (interactuador != null)
        {
            Vector3 puntoMedioLocal = puntoMedio.InverseTransformPoint(medioCuerdaAgarrar.position); // localPosition

            //get the offset
            float puntoMedioLocalZAbs = Mathf.Abs(puntoMedioLocal.z);

            // Cuerda Hasta Inicio
            if (puntoMedioLocal.z >= 0)
            {
                medioCuerdaVisual.localPosition = Vector3.zero;
            }

            // Cuerda Hasta Limite
            if (puntoMedioLocal.z < 0 && puntoMedioLocalZAbs >= limiteEstirarCuerda)
            {
                fuerza = 1;
                //Vector3 direction = puntoMedio.TransformDirection(new Vector3(0, 0, puntoMedioLocal.z));
                medioCuerdaVisual.localPosition = new Vector3(0, 0, -limiteEstirarCuerda);
            }

            // Estirar Cuerda
            if (puntoMedioLocal.z < 0 && puntoMedioLocalZAbs < limiteEstirarCuerda)
            {
                fuerza = Remap(puntoMedioLocalZAbs, 0, limiteEstirarCuerda, 0, 1);
                medioCuerdaVisual.localPosition = new Vector3(0, 0, puntoMedioLocal.z);
            }

            cuerdaRenderer.InicializarCuerda(medioCuerdaVisual.position);
        }
    }

    // Valor de fuerza nuevo [0 a 1], si está entre los límites
    private float Remap(float valor, int desdeMin, float desdeMax, int hastaMin, int hastaMax)
    {
        return (valor - desdeMin) / (desdeMax - desdeMin) * (hastaMax - hastaMin) + hastaMin;
    }
}