using UnityEngine;

[ExecuteInEditMode]
public class CameraZoom : MonoBehaviour // Đổi tên class thành CameraZoom
{
    public Camera camera; // Sử dụng UnityEngine.Camera
    public float defaultFOV = 60;
    public float maxZoomFOV = 15;
    [Range(0, 1)]
    public float currentZoom;
    public float sensitivity = 1;

    void Awake()
    {
        // Get the camera on this gameObject and the defaultZoom.
        if (camera == null) // Kiểm tra xem camera đã được gán chưa
        {
            camera = GetComponent<Camera>();
        }

        if (camera)
        {
            defaultFOV = camera.fieldOfView;
        }
    }

    void Update()
    {
        // Update the currentZoom and the camera's fieldOfView.
        currentZoom += Input.mouseScrollDelta.y * sensitivity * .05f;
        currentZoom = Mathf.Clamp01(currentZoom);
        camera.fieldOfView = Mathf.Lerp(defaultFOV, maxZoomFOV, currentZoom);
    }
}