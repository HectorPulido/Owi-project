using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class WorldPainter : MonoBehaviour
{
    public GameObject prefab;
    public Transform parentTransform;

    private void Update()
    {
        if(InputManager.MouseClick) {
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            worldPosition.z = 0;

            Transform instancedPrefab = Instantiate(prefab.transform, worldPosition, Quaternion.identity, parentTransform);
            if(Random.value > 0.5f){
                Vector3 scale = instancedPrefab.localScale;                
                scale.x *= -1;
                instancedPrefab.localScale = scale;
            }

        }
    }
}
