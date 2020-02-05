using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSCamera : MonoBehaviour
{
    [SerializeField, Header("Angle")]
    public float horizontalSpeed = 2.0F;
    public float verticalSpeed = 2.0F;
    [SerializeField, Header("Camera Angle"), Tooltip("between 0 and 90 degrees")]
    private float lowerAngle = 90.0F;
    [SerializeField, Tooltip("between 0 and 90 degrees")]
    private float upperAngle = 90.0F;

    [SerializeField, Header("Movement")]
    private float movementSpeed = 0.1f;


    private GameObject mainCamera;

#region Properties

    protected float LowerAngle
    {
        get => Mathf.Clamp(this.lowerAngle, 0.0f, 89.0f);
        set => this.lowerAngle = value;
    }

    protected float UpperAngle
    {
        get =>  360 - Mathf.Clamp(this.upperAngle, 0.0f, 89.0f);
        set => this.upperAngle = value;
    }


#endregion

#region Methods

    private void CameraMovement()
    {
        //Debug.DrawRay(mainCamera.transform.position, mainCamera.transform.forward, Color.green);

        float horizontalShift = horizontalSpeed * Input.GetAxis("Mouse X");
        float verticalShift = verticalSpeed * Input.GetAxis("Mouse Y");
        transform.Rotate(0, horizontalShift, 0, Space.World);
        float xAngle = mainCamera.transform.eulerAngles.x;
        float predictedRotation;

        if (verticalShift > 0 && xAngle >= UpperAngle) // moving up while looking up
        {
            predictedRotation = xAngle - verticalShift;
            //Debug.Log(predictedRotation);
            if (predictedRotation <= UpperAngle)
            {
                mainCamera.transform.Rotate(UpperAngle - xAngle + 0.01F, 0, 0);
            }
            else
            {
                mainCamera.transform.Rotate(-verticalShift, 0, 0);
            }
        }
        else if (verticalShift > 0 && xAngle <= LowerAngle) // moving up while looking down
        {
            predictedRotation = xAngle - verticalShift;
            if (predictedRotation < 0 && predictedRotation <= (UpperAngle - 360.0f))
            {
                mainCamera.transform.Rotate(xAngle - UpperAngle, 0, 0);
            }
            else
            {
                mainCamera.transform.Rotate(-verticalShift, 0, 0);
            }
        }
        else if (verticalShift < 0 && xAngle >= UpperAngle) // moving down while looking up
        {
            predictedRotation = xAngle - verticalShift;
            if (predictedRotation > 360 && (predictedRotation % 360 > LowerAngle))
            {
                mainCamera.transform.Rotate(LowerAngle - xAngle, 0, 0);
            }
            else
            {
                mainCamera.transform.Rotate(-verticalShift, 0, 0);
            }
        }
        else if (verticalShift < 0 && xAngle <= LowerAngle) // moving down while looking down
        {
            predictedRotation = xAngle - verticalShift;
            if (predictedRotation >= LowerAngle)
            {
                mainCamera.transform.Rotate(LowerAngle - xAngle - 0.01f, 0, 0);
            }
            else
            {
                mainCamera.transform.Rotate(-verticalShift, 0, 0);
            }
        }
    }
    

#endregion

#region Functions

    // Use this for initialization
    void Start()
    {
        //Cursor.lockState = CursorLockMode.Locked;
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
        
    }

    // Update is called once per frame
    void Update()
    {
        Cursor.lockState = CursorLockMode.None;
        if (Input.GetKey(KeyCode.Mouse1))
        {
            Cursor.lockState = CursorLockMode.Locked;
            CameraMovement();
            Vector3 moveDirection = new Vector3(Input.GetAxisRaw("Horizontal") , 0, Input.GetAxisRaw("Vertical"));
            Debug.Log(moveDirection);
            mainCamera.transform.Translate(moveDirection * movementSpeed);
        }
    }

#endregion

}
    