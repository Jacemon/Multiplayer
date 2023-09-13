using System;
using UnityEngine;

public class BodyController : MonoBehaviour
{
    [Header("Spine")]
    public Transform spineController;
    public float offset = 1f;

    private void Update()
    {
        Spine();
    }

    private void Spine()
    {
        var cameraTransform = PlayerController.camera.transform;

        var targetPoint = cameraTransform.position + cameraTransform.forward * offset;

        spineController.position = targetPoint;
    }
}
