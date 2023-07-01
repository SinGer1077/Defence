using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotator : MonoBehaviour
{
    [SerializeField]
    private Transform _heroTransform;

    [SerializeField]
    private float _rotationSpeed;

    [SerializeField]
    private float _maxCameraPitch;
    [SerializeField]
    private float _maxCameraYaw;
    [SerializeField]
    private float _minCameraPitch;
    [SerializeField]
    private float _minCameraYaw;

    private float _cameraPitch;
    private float _cameraYaw;

    private void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.DownArrow))
        {
            if (_cameraPitch + _rotationSpeed < _maxCameraPitch)
            {
                _cameraPitch += _rotationSpeed;
            }
        }

        if (Input.GetKey(KeyCode.UpArrow))
        {
            if (_cameraPitch - _rotationSpeed > _minCameraPitch)
            {
                _cameraPitch -= _rotationSpeed;
            }
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            if (_cameraYaw + _rotationSpeed < _maxCameraYaw)
            {
                _cameraYaw += _rotationSpeed;
            }
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            if (_cameraYaw - _rotationSpeed > _minCameraYaw)
            {
                _cameraYaw -= _rotationSpeed;
            }
        }

        Quaternion quat = new Quaternion();
        quat.eulerAngles = new Vector3(_cameraPitch, _cameraYaw, _heroTransform.rotation.eulerAngles.z);
        _heroTransform.rotation = quat;
    }
   
}
