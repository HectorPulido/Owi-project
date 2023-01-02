using System.Collections.Generic;
using UnityEngine;
using Cinemachine;


public class VirtualCameraManager : MonoBehaviour
{
    public CinemachineVirtualCamera vcam;

    [SerializeField]
    private float maxOrthoSize;

    [SerializeField]
    private float minOrthoSize;

    [SerializeField]
    private float maxDistance;

    [SerializeField]
    private float minDistance;

    private Vector3 initialPosition;

    public static VirtualCameraManager singleton;
    
    private void Awake(){
        if (singleton)
        {
            Destroy(gameObject);
            return;
        }
        singleton = this;
        initialPosition = vcam.transform.position;
    }

    private void Update()
    {
        MoveToAveragePosition();
        SetOrthographicSize();        
    }

    private void SetOrthographicSize(){
        float max_distance = 0;
        List<(int a, int b)> l = new List<(int a, int b)>();

        for (int i = 0; i < PlayerController.singleton.players.Count; i++)
        {
            for (int j = 0; j < PlayerController.singleton.players.Count; j++)
            {
                if(PlayerController.singleton.players[i] == PlayerController.singleton.players[j]){
                    continue;
                }
                if(l.Contains((j, i)) || l.Contains((i, j))){
                    continue;
                }
                
                l.Add((i,j));

                float distance = Vector3.Distance(
                        PlayerController.singleton.players[i].transform.position, 
                        PlayerController.singleton.players[j].transform.position
                    );

                if(max_distance < distance){
                    max_distance = distance;
                }
            }
        }

        max_distance = Mathf.Clamp(max_distance, minDistance, maxDistance);
        max_distance = (max_distance - minDistance) / (maxDistance - minDistance);

        vcam.m_Lens.OrthographicSize = Mathf.Lerp(minOrthoSize, maxOrthoSize, max_distance);
    }

    private void MoveToAveragePosition(){
        Vector3 avgPosition = Vector3.zero;

        for (int i = 0; i < PlayerController.singleton.players.Count; i++)
        {
            avgPosition += PlayerController.singleton.players[i].transform.position;
        }

        avgPosition /= PlayerController.singleton.players.Count;

        avgPosition.z = initialPosition.z;
        avgPosition.y = initialPosition.y;

        vcam.transform.position = avgPosition;
    }

    public static void SetMaxOrthoSize(float value){
        VirtualCameraManager.singleton.maxOrthoSize = value;
    }
    public static void SetMinOrthoSize(float value){
        VirtualCameraManager.singleton.minOrthoSize = value;
    }
    public static void SetMaxDistance(float value){
        VirtualCameraManager.singleton.maxDistance = value;
    }
    public static void SetMinDistance(float value){
        VirtualCameraManager.singleton.minDistance = value;
    }
}

