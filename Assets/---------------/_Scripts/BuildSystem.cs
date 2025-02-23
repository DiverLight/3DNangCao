using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildSystem : MonoBehaviour
{
    [SerializeField] Transform CamChild;
    [SerializeField] Transform FloorBuild;
    [SerializeField] Transform WallBuild;
    RaycastHit Hit;

    [SerializeField] Transform FloorPrefab;
    [SerializeField] Transform WallPrefab;

    private enum BuildType { Floor, Wall }
    private BuildType currentBuild = BuildType.Floor;

    private int rotationAngle = 0; // Góc xoay của công trình

    void Update()
    {
        if (Physics.Raycast(CamChild.position, CamChild.forward, out Hit, 7f))
        {
            if (currentBuild == BuildType.Floor)
            {
                FloorBuild.gameObject.SetActive(true);
                WallBuild.gameObject.SetActive(false);

                // Nhấn phím Q/E để tăng hoặc giảm độ cao của nền
                if (Input.GetKeyDown(KeyCode.Q)) FloorBuild.position += Vector3.up * 3;
                if (Input.GetKeyDown(KeyCode.E)) FloorBuild.position -= Vector3.up * 3;

                // Đặt nền theo lưới 3x3, bỏ qua kiểm tra mặt đất
                FloorBuild.position = new Vector3(
                    Mathf.RoundToInt(Hit.point.x / 3) * 3,
                    Mathf.RoundToInt(FloorBuild.position.y / 3) * 3, // Giữ nguyên độ cao
                    Mathf.RoundToInt(Hit.point.z / 3) * 3
                );

                FloorBuild.eulerAngles = new Vector3(0, rotationAngle, 0);
            }
            else if (currentBuild == BuildType.Wall)
            {
                FloorBuild.gameObject.SetActive(false);
                WallBuild.gameObject.SetActive(true);

                Vector3 snappedPosition = new Vector3(
                    Mathf.RoundToInt(Hit.point.x / 3) * 3,
                    (Mathf.RoundToInt(Hit.point.y / 3) * 3) + 1.5f, // Tường cao 3m -> đặt ở giữa
                    Mathf.RoundToInt(Hit.point.z / 3) * 3
                );

                float offset = 1.5f;
                bool placeOnX = Mathf.Abs(Hit.point.x % 3) > Mathf.Abs(Hit.point.z % 3);

                if (placeOnX)
                {
                    snappedPosition.x += Hit.point.x % 3 > 0 ? offset : -offset;
                    WallBuild.eulerAngles = new Vector3(0, rotationAngle, 0);
                }
                else
                {
                    snappedPosition.z += Hit.point.z % 3 > 0 ? offset : -offset;
                    WallBuild.eulerAngles = new Vector3(0, rotationAngle, 0);
                }

                WallBuild.position = snappedPosition;
            }

            // Đặt công trình khi nhấn chuột trái
            if (Input.GetMouseButtonDown(0))
            {
                if (currentBuild == BuildType.Floor)
                {
                    Instantiate(FloorPrefab, FloorBuild.position, Quaternion.Euler(0, rotationAngle, 0));
                }
                else if (currentBuild == BuildType.Wall)
                {
                    Instantiate(WallPrefab, WallBuild.position, Quaternion.Euler(0, rotationAngle, 0));
                }
            }

            // Phá công trình khi nhấn C
            if (Input.GetKeyDown(KeyCode.C))
            {
                DestroyStructure();
            }

            // Xoay công trình khi nhấn chuột phải
            if (Input.GetMouseButtonDown(1))
            {
                RotateStructure();
            }
        }

        // Chuyển đổi chế độ xây dựng
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            currentBuild = BuildType.Floor;
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            currentBuild = BuildType.Wall;
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            Cursor.lockState = CursorLockMode.None;
        }
    }

    void DestroyStructure()
    {
        if (Physics.Raycast(CamChild.position, CamChild.forward, out Hit, 7f))
        {
            if (Hit.collider.gameObject.CompareTag("Buildable"))
            {
                Destroy(Hit.collider.gameObject);
            }
        }
    }

    void RotateStructure()
    {
        rotationAngle += 90;
        if (rotationAngle >= 360) rotationAngle = 0;
    }
}
