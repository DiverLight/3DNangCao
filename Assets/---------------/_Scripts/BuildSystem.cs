
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildSystem : MonoBehaviour
{
    //Neww
    [SerializeField] private int wood = 500; // Số lượng gỗ ban đầu
    [SerializeField] private int stone = 500; // Số lượng đá ban đầu
    [SerializeField] private int floorCostWood = 2;
    [SerializeField] private int floorCostStone = 1;
    [SerializeField] private int wallCostWood = 3;
    [SerializeField] private int wallCostStone = 2;
    [SerializeField] private int doorCostWood = 4;
    [SerializeField] private int doorCostStone = 3;




    [SerializeField] Transform CamChild;
    [SerializeField] Transform FloorBuild;
    [SerializeField] Transform WallBuild;
    [SerializeField] Transform DoorBuild; // Ghost Preview của cửa

    RaycastHit Hit;

    [SerializeField] Transform FloorPrefab;
    [SerializeField] Transform WallPrefab;
    [SerializeField] Transform DoorPrefab; // Prefab cửa

    private bool isBuildingMode = true; // Mặc định bật chế độ xây dựng

    public enum BuildType { Floor, Wall, Door }
    private BuildType currentBuild = BuildType.Floor;
    private int rotationAngle = 0; // Góc xoay của công trình

    void Update()
    {
        // Khi nhấn P, bật/tắt chế độ xây dựng
        if (Input.GetKeyDown(KeyCode.P))
        {
            ToggleBuildMode();
        }
        // Nếu không trong chế độ xây dựng, không xử lý gì nữa
        if (!isBuildingMode) return;
       




        if (Physics.Raycast(CamChild.position, CamChild.forward, out Hit, 7f))
        {
            if (currentBuild == BuildType.Floor)
            {
                FloorBuild.gameObject.SetActive(true);
                WallBuild.gameObject.SetActive(false);
                DoorBuild.gameObject.SetActive(false);

                if (Input.GetKeyDown(KeyCode.Q)) FloorBuild.position += Vector3.up * 3;
                if (Input.GetKeyDown(KeyCode.E)) FloorBuild.position -= Vector3.up * 3;

                FloorBuild.position = new Vector3(
                    Mathf.RoundToInt(Hit.point.x / 3) * 3,
                    Mathf.RoundToInt(FloorBuild.position.y / 3) * 3,
                    Mathf.RoundToInt(Hit.point.z / 3) * 3
                );

                FloorBuild.eulerAngles = new Vector3(0, rotationAngle, 0);
            }
            else if (currentBuild == BuildType.Wall)
            {
                FloorBuild.gameObject.SetActive(false);
                WallBuild.gameObject.SetActive(true);
                DoorBuild.gameObject.SetActive(false);

                Vector3 snappedPosition = new Vector3(
                    Mathf.RoundToInt(Hit.point.x / 3) * 3,
                    (Mathf.RoundToInt(Hit.point.y / 3) * 3) + 1.5f,
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
            else if (currentBuild == BuildType.Door)
            {
                FloorBuild.gameObject.SetActive(false);
                WallBuild.gameObject.SetActive(false);
                DoorBuild.gameObject.SetActive(true);

                Vector3 snappedPosition = new Vector3(
                    Mathf.RoundToInt(Hit.point.x / 3) * 3,
                    Mathf.RoundToInt(Hit.point.y / 3) * 3 + 1.5f,
                    Mathf.RoundToInt(Hit.point.z / 3) * 3
                );

                float offset = 1.5f;
                bool placeOnX = Mathf.Abs(Hit.point.x % 3) > Mathf.Abs(Hit.point.z % 3);

                if (placeOnX)
                {
                    snappedPosition.x += Hit.point.x % 3 > 0 ? offset : -offset;
                    DoorBuild.eulerAngles = new Vector3(0, rotationAngle, 0);
                }
                else
                {
                    snappedPosition.z += Hit.point.z % 3 > 0 ? offset : -offset;
                    DoorBuild.eulerAngles = new Vector3(0, rotationAngle, 0);
                }

                DoorBuild.position = snappedPosition;
            }

          

            if (Input.GetMouseButtonDown(0))
            {
                if (currentBuild == BuildType.Floor && CanBuild(floorCostWood, floorCostStone))
                {
                    Instantiate(FloorPrefab, FloorBuild.position, Quaternion.Euler(0, rotationAngle, 0));
                    UseResources(floorCostWood, floorCostStone);
                }
                else if (currentBuild == BuildType.Wall && CanBuild(wallCostWood, wallCostStone))
                {
                    Instantiate(WallPrefab, WallBuild.position, Quaternion.Euler(0, rotationAngle, 0));
                    UseResources(wallCostWood, wallCostStone);
                }
                else if (currentBuild == BuildType.Door && CanBuild(doorCostWood, doorCostStone))
                {
                    Instantiate(DoorPrefab, DoorBuild.position, Quaternion.Euler(0, rotationAngle, 0));
                    UseResources(doorCostWood, doorCostStone);
                }
            }
            //new
            bool CanBuild(int requiredWood, int requiredStone)
            {
                return wood >= requiredWood && stone >= requiredStone;
            }
            void UseResources(int woodCost, int stoneCost)
            {
                wood -= woodCost;
                stone -= stoneCost;
                Debug.Log($"Tài nguyên còn lại - Gỗ: {wood}, Đá: {stone}");
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
        if (Input.GetKeyDown(KeyCode.Alpha1)) currentBuild = BuildType.Floor;
        if (Input.GetKeyDown(KeyCode.Alpha2)) currentBuild = BuildType.Wall;
        if (Input.GetKeyDown(KeyCode.Alpha3)) currentBuild = BuildType.Door;
    }

    // Bật/tắt chế độ xây dựng
    void ToggleBuildMode()
    {
        isBuildingMode = !isBuildingMode;

        // Khi tắt chế độ xây dựng, ẩn Ghost Preview
        if (!isBuildingMode)
        {
            FloorBuild.gameObject.SetActive(false);
            WallBuild.gameObject.SetActive(false);
            DoorBuild.gameObject.SetActive(false);
            Cursor.lockState = CursorLockMode.Locked; // Ẩn con trỏ chuột
        }
       
    }

    // Hàm này sẽ được gọi từ UI
    public void SetBuildMode(BuildType type)
    {
        currentBuild = type;
        Debug.Log("Chế độ xây dựng: " + type);
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
