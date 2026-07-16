using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [System.Serializable]
    public class WaypointData
    {
        public float pathPosition;

        public bool stopHere;

        public Transform lookTarget;
    }

    [Header("Camara y TrackedDolly")]
    public CinemachineVirtualCamera virtualCamera;

    private CinemachineTrackedDolly trackedDolly;

    [Header("Waypoints")]
    public List<WaypointData> waypoints = new List<WaypointData>();

    [Header("Movimiento")]
    public float minMoveTime = 2f;
    public float maxMoveTime = 5f;

    [Header("Rotación")]
    public Transform cameraTransform;
    public float rotationSpeed = 5f;

    private int currentWaypoint = 0;

    private bool isMoving = false;
    private bool isStopped = false;

    private void Start()
    {
        trackedDolly = virtualCamera.GetCinemachineComponent<CinemachineTrackedDolly>();

        if (waypoints.Count > 0)
        {
            trackedDolly.m_PathPosition =
                waypoints[0].pathPosition;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            AdvanceToNextWaypoint();
        }

        if (isMoving)
        {
            LookForward();
        }

        if (isStopped)
        {
            LookAtTarget();
        }
    }

    public void AdvanceToNextWaypoint()
    {
        if (isMoving)
            return;

        if (currentWaypoint >= waypoints.Count - 1)
            return;

        StartCoroutine(
            MoveToWaypoint(currentWaypoint + 1)
        );
    }
    IEnumerator MoveToWaypoint(int targetIndex)
    {
        isMoving = true;
        isStopped = false;

        float startPos = trackedDolly.m_PathPosition;

        float targetPos = waypoints[targetIndex].pathPosition;

        float distance = Mathf.Abs(targetPos - startPos);

        float moveTime = Mathf.Lerp(
            minMoveTime,
            maxMoveTime,
            distance / 20f
        );

        moveTime = Mathf.Clamp(
            moveTime,
            minMoveTime,
            maxMoveTime
        );

        float elapsed = 0f;

        while (elapsed < moveTime)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / moveTime;
            trackedDolly.m_PathPosition = Mathf.Lerp(startPos, targetPos, t);
            yield return null;
        }

        trackedDolly.m_PathPosition = targetPos;
        currentWaypoint = targetIndex;
        isMoving = false;

        if (waypoints[targetIndex].stopHere)
        {
            isStopped = true;
        }
        else
        {
            AdvanceToNextWaypoint();
        }
    }

    void LookForward()
    {
        CinemachinePathBase path = trackedDolly.m_Path;

        Vector3 tangent = path.EvaluateTangent(trackedDolly.m_PathPosition);

        Quaternion targetRotation = Quaternion.LookRotation(tangent);

        cameraTransform.rotation = Quaternion.Slerp(cameraTransform.rotation,
                                                    targetRotation,
                                                    rotationSpeed * Time.deltaTime);
    }

    void LookAtTarget()
    {
        Transform target = waypoints[currentWaypoint].lookTarget;

        if (target == null)
            return;

        Vector3 dir = target.position - cameraTransform.position;

        Quaternion targetRotation = Quaternion.LookRotation(dir);

        cameraTransform.rotation = Quaternion.Slerp(cameraTransform.rotation,
                                                    targetRotation,
                                                    rotationSpeed * Time.deltaTime);
    }
}
