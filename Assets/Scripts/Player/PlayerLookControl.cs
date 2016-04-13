using UnityEngine;
using System.Collections;

public class PlayerLookControl : MonoBehaviour {

    public Camera head = null;
    private Vector2 lookDir;
    public Vector2 LookDir
    {
        get
        {
            return lookDir;
        }
        set
        {
            lookDir = value;
            updateLookDir();
        }
    }
    public float sensitivityX = 0.5f;
    public float sensitivityY = 0.5f;
    public float maxXAngle = 80;
    public float minXAngle = -80;
    public float maxYAngle = 360;
    public float minYAngle = -360;

    [SerializeField] Vector2 lookRotation = Vector2.zero;
    Quaternion originalRotation;

    void Start()
    {
        originalRotation = head.transform.localRotation;
    }

    void updateLookDir()
    {
        if(Cursor.lockState != CursorLockMode.Locked)
        {
            Cursor.lockState = CursorLockMode.Locked;
        }

        lookRotation.x += lookDir.x * sensitivityX;
        lookRotation.y += lookDir.y * sensitivityY;

        lookRotation.x = clampAngle(lookRotation.x, minXAngle, maxXAngle);
        lookRotation.y = clampAngle(lookRotation.y, minYAngle, maxYAngle);

        Quaternion xQuaternion = Quaternion.AngleAxis(lookRotation.x, Vector3.right);
        Quaternion yQuaternion = Quaternion.AngleAxis(lookRotation.y, Vector3.up);

        Vector3 eulerRotation = (originalRotation * xQuaternion).eulerAngles;
        eulerRotation += (originalRotation * yQuaternion).eulerAngles;
        eulerRotation.z = 0f;

        head.transform.localRotation = Quaternion.Euler(eulerRotation);
    }

    float clampAngle(float angle, float min, float max)
    {
        if (angle < -360F)
            angle += 360F;
        if (angle > 360F)
            angle -= 360F;

        return Mathf.Clamp(angle, min, max);
    }
}
