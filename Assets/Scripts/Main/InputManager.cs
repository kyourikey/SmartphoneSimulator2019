using UnityEngine;

public class InputManager : MonoBehaviour
{
    public Quaternion GyroQuaternion;

    void Start()
    {
        Input.gyro.enabled = true;
    }

    void Update()
    {
        RotationUpdate();
    }

    void RotationUpdate()
    {
        var gyroQ = Input.gyro.attitude;
        //座標系そろえる
        GyroQuaternion = new Quaternion(-gyroQ.x, -gyroQ.z, -gyroQ.y, gyroQ.w);
    }
}
