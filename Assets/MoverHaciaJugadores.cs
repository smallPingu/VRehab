using UnityEngine;

public class MoverHaciaJugadores : MonoBehaviour
{
    public float velocidad = 2f;

    // Update is called once per frame
    void Update()
    {
        transform.position += new Vector3(0, 0, velocidad) * Time.deltaTime;
    }
}
