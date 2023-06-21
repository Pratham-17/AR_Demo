using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System;
using TMPro;
using DG.Tweening;

public class Navigator : MonoBehaviour
{
    [SerializeField]
    NavMeshAgent agent;

    [SerializeField]
    List<Destination> destinations = new List<Destination>();

    [SerializeField]
    TMP_Dropdown dropdown;

    [SerializeField]
    LineRenderer path;

    [SerializeField]
    GameObject locationPin;

    string CurrentDestination = string.Empty;

    private void Start()
    {
        CurrentDestination = "None";
        path.startWidth = 0.15f;
        path.endWidth = 0.15f;
        path.positionCount = 0;
        locationPin.SetActive(false);
    }

    public void OnDestinationChanged()
    {
        CurrentDestination = dropdown.options[dropdown.value].text;
        if (CurrentDestination != "None")
        {
            locationPin.transform.position = GetDestinationPosition(CurrentDestination).position;
            path.enabled = true;
            locationPin.SetActive(true);
            locationPin.transform.DORotate(new Vector3(0, 360, 0), 3, RotateMode.FastBeyond360).SetLoops(-1, LoopType.Restart).SetRelative().SetEase(Ease.Linear);
            locationPin.transform.DOMoveY(-1, 1, false).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.InOutSine);
        }

    }

    void Update()
    {
        if (CurrentDestination != "None")
        {
            agent.SetDestination(GetDestinationPosition(CurrentDestination).position);
            if (agent.hasPath)
            {
                DrawPath();
            }
        }
        if (agent.hasPath && agent.remainingDistance < 0.5f)
        {
            locationPin.transform.DOKill(true);
            locationPin.SetActive(false);
            agent.ResetPath();
            path.enabled = false;
            dropdown.value = 0; //always should be in last line of the function as it triggers OnDestinationChanged
        }
    }
    void DrawPath()
    {
        path.positionCount = agent.path.corners.Length;
        path.SetPosition(0, agent.transform.position);
        /*if(agent.path.corners.Length>2)
        {
            return;
        }*/

        for (int i = 0; i < agent.path.corners.Length; i++)
        {
            Vector3 pointPosition = new Vector3(agent.path.corners[i].x,
                                                agent.path.corners[i].y,
                                                agent.path.corners[i].z);
            path.SetPosition(i, pointPosition);

        }


    }
    Transform GetDestinationPosition(string name)
    {
        for(int i=0; i<=destinations.Count;i++)
        {
            if(destinations[i].Name == name)
            {
                return destinations[i].Position;
            }
        }
        return null;
    }

    [Serializable]
    class Destination
    {
        [SerializeField]
        public Transform Position;

        [SerializeField]
        public string Name;
    }

}