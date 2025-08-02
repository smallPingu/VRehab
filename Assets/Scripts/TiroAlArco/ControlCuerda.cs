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
        interactuador = null;
        medioCuerdaAgarrar.localPosition = Vector3.zero;
        medioCuerdaVisual.localPosition = Vector3.zero;
        cuerdaRenderer.InicializarCuerda(null);

    }

    private void PrepararCuerda(SelectEnterEventArgs arg0)
    {
        interactuador = arg0.interactorObject.transform;
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
                //Vector3 direction = puntoMedio.TransformDirection(new Vector3(0, 0, puntoMedioLocal.z));
                medioCuerdaVisual.localPosition = new Vector3(0, 0, -limiteEstirarCuerda);
            }

            // EstirarCuerda(puntoMedioLocalZAbs, puntoMedioLocal);
            if (puntoMedioLocal.z < 0 && puntoMedioLocalZAbs < limiteEstirarCuerda)
            {
                medioCuerdaVisual.localPosition = new Vector3(0, 0, puntoMedioLocal.z);
            }

            cuerdaRenderer.InicializarCuerda(medioCuerdaVisual.position);
        }
    }
}