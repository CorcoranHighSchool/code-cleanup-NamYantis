using UnityEngine;

public class RotateCamera : MonoBehaviour
{
    [Serialize Field] private float rotationSpeed;
    private const string Horizontal = "Horizontal";
    // Update is called once per frame
    void Update()
    {
        float horizontalInput = Input.GetAxis(Horizontal);
        transform.Rotate(Vector3.up, horizontalInput * (rotationSpeed * Time.deltaTime));
    }
}
