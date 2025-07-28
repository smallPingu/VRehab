using UnityEngine;

public class GirarManecilla : MonoBehaviour
{    
    void Update()
    {
        // GIRAR 1 VUELTA POR MINUTO
        transform.eulerAngles = new Vector3(transform.eulerAngles.x + 20 * Time.deltaTime, 90, 0);
    }
}
