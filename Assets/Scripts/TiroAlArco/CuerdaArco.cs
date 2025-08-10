using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class CuerdaArco : MonoBehaviour
{
    [SerializeField]
    private Transform extremo1, extremo2;

    private LineRenderer lineRenderer;

    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }

    public void InicializarCuerda(Vector3? posicionMedia) //? significa que puede ser "null"
    {
        Vector3[] linePuntos = new Vector3[posicionMedia == null ? 2 : 3];
        linePuntos[0] = extremo1.localPosition;
        if (posicionMedia != null)
        {
            linePuntos[1] = transform.InverseTransformPoint(posicionMedia.Value);
        }
        linePuntos[^1] = extremo2.localPosition; // ^1 significa último elemento (^2 sería penúltimo)

        lineRenderer.positionCount = linePuntos.Length;
        lineRenderer.SetPositions(linePuntos);
    }

    private void Start()
    {
        InicializarCuerda(null);
    }
}