using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WolfAttack : BaseAttack
{
    // Nếu cần thêm logic riêng cho sói, bạn có thể override các hàm
    protected override void PerformAttack()
    {
        base.PerformAttack(); // Gọi hành vi tấn công cơ bản từ BaseAttack

        // Thêm hiệu ứng hoặc hành vi riêng cho sói
        Debug.Log("Wolf is attacking!");
    }

    protected override void StopAttack()
    {
        base.StopAttack(); // Dừng hành vi tấn công cơ bản
        Debug.Log("Wolf stopped attacking!");
    }
}
