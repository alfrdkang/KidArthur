/*
 * Author: Alfred Kang Jing Rui
 * Date Created: 30/04/2024
 * Date Modified: 30/04/2024
 * Description: Player Camera Script
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    /// <summary>
    /// Fetches player model
    /// </summary>
    [SerializeField] Transform playerCapsule;

    /// <summary>
    /// Camera Mouse Sensitivity Multiplier
    /// </summary>
    public float mouseSens = 100.0f;

    /// <summary>
    /// Rotates camera along X Axis
    /// </summary>
    float xRotate = 0f;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSens * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSens * Time.deltaTime;

        xRotate -= mouseY;
        xRotate = Mathf.Clamp(xRotate, -90f, 80f);

        transform.localRotation = Quaternion.Euler(xRotate, 0f, 0f);
        playerCapsule.Rotate(Vector3.up * mouseX);
    }
}
