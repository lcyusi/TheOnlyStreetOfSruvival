using UnityEngine;

public class CameraOrbit : MonoBehaviour
{
    public Transform pivot; // 被跟随的对象
    public Vector3 pivotOffset = Vector3.zero; // 目标偏离值
    //public Transform target; // 像选中的对象（用于检查对象在相机和目标之间）

    public float distance = 10.0f; // 距离目标（与变焦一起使用）
    public float minDistance = 2f; //最小距离
    public float maxDistance = 15f;//最大距离
    public float zoomSpeed = 1f; //变焦速度

    public float xSpeed = 250.0f; //X轴速度
    public float ySpeed = 120.0f; //Y轴速度

    public bool allowYTilt = true;
    public float yMinLimit = 30f;//Y轴最小角度
    public float yMaxLimit = 80f;//Y轴最大角度

    private float x = 0.0f;
    private float y = 0.0f;

    private float targetX = 0f;
    private float targetY = 0f;
    private float targetDistance = 0f;
    private float xVelocity = 1f;
    private float yVelocity = 1f;
    private float zoomVelocity = 1f;

    void Start()
    {
        var angles = transform.eulerAngles;
        targetX = x = angles.x;
        targetY = y = ClampAngle(angles.y, yMinLimit, yMaxLimit);
        targetDistance = distance;
    }

    void LateUpdate()
    {
        if (pivot)
        {
            float scroll = Input.GetAxis("Mouse ScrollWheel");

            if (scroll > 0.0f) targetDistance -= zoomSpeed;
            else if (scroll < 0.0f) targetDistance += zoomSpeed;
            targetDistance = Mathf.Clamp(targetDistance, minDistance, maxDistance);

            // -=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-
            // right mouse button must be held down to tilt/rotate cam
            // 鼠标右键必须按住倾斜/旋转凸轮
            // or player can use the left mouse button while holding Ctr
            // 或者玩家可以在保持CTR的同时使用鼠标左键
            if (Input.GetMouseButton(1) || (Input.GetMouseButton(0) && (Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl))))
            {
                targetX += Input.GetAxis("Mouse X") * xSpeed * 0.02f;
                if (allowYTilt)
                {
                    targetY -= Input.GetAxis("Mouse Y") * ySpeed * 0.02f;
                    targetY = ClampAngle(targetY, yMinLimit, yMaxLimit);
                }
            }
            x = Mathf.SmoothDampAngle(x, targetX, ref xVelocity, 0.3f);
            if (allowYTilt) y = Mathf.SmoothDampAngle(y, targetY, ref yVelocity, 0.3f);
            else y = targetY;
            Quaternion rotation = Quaternion.Euler(y, x, 0);
            distance = Mathf.SmoothDamp(distance, targetDistance, ref zoomVelocity, 0.5f);

            // -=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-
            // apply
            Vector3 position = rotation * new Vector3(0.0f, 0.0f, -distance) + pivot.position + pivotOffset;
            transform.rotation = rotation;
            transform.position = position;

        }
    }

    /// <summary>
    /// 夹角
    /// </summary>
    /// <param name="angle">角度</param>
    /// <param name="min">最小角度</param>
    /// <param name="max">最大角度</param>
    /// <returns></returns>
    private float ClampAngle(float angle, float min, float max)
    {
        if (angle < -360) angle += 360;
        if (angle > 360) angle -= 360;
        return Mathf.Clamp(angle, min, max);
    }
}