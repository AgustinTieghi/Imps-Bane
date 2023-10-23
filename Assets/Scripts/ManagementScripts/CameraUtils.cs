
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CameraUtils : MonoBehaviour
{
    [Header("Camera Settings")]
    public float offset = 0.5f;
    public float speedCamera = 5f;
    public float margen = 1;
    public float xLimit;
    public float zLimit;
    public float minusZLimit;
    public float minusXLimit;

    void Update()
    {
        MoveCamera();
    }
 
    private void MoveCamera()
    {
        float x = Input.mousePosition.x / Screen.width;
        float y = Input.mousePosition.y / Screen.height;

        if (x <= 0 + margen)
        {
            if (transform.position.x >= -minusXLimit)
            {
                this.transform.Translate(-1 * Time.deltaTime * speedCamera, 0, 0);
            }
        }
        if (x >= 1 - margen)
        {
            if (transform.position.x <= xLimit)
            {
                this.transform.Translate(1 * Time.deltaTime * speedCamera, 0, 0);
            }
        }
        if (y <= margen)
        {
            if (transform.position.z >= -minusZLimit)
            {

                this.transform.Translate(0, -1 * Time.deltaTime * speedCamera, 0);
            }

        }
        if (y >= 1 - margen)
        {
            if (transform.position.z <= zLimit)
            {
                this.transform.Translate(0, 1 * Time.deltaTime * speedCamera, 0);
            }

        }
    }
}
