using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LineMeasurement : MonoBehaviour
{
    [SerializeField] Transform firePoint;

    [SerializeField] Transform playerPoint;
    [SerializeField] TextMeshProUGUI distance;

    LineRenderer lineRenderer;

    private void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();

        lineRenderer.positionCount++;
        lineRenderer.SetPosition(0, firePoint.position);
        lineRenderer.positionCount++;
        lineRenderer.SetPosition(1, playerPoint.position);

        Debug.Log(lineRenderer.positionCount);
    }

    private void Update()
    {
        //lineRenderer.SetPosition(1, new Vector3(playerPoint.position.x, 0, playerPoint.position.z));
        //lineRenderer.SetPosition(1, playerPoint.position);

        distance.text = $"firepoint: {firePoint.position} player point: {playerPoint.position} Distance: {Vector3.Distance(new Vector3(firePoint.position.x,0,firePoint.position.z), new Vector3(playerPoint.position.x,0,playerPoint.position.z))}";
    }
}
