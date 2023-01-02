using UnityEngine;
using System.Collections.Generic;

public class WorldAssetGenerator : MonoBehaviour
{
    [SerializeField]
    private float densityByUnit = 0.7f;

    [SerializeField]
    private int maxRetry = 100;

    [SerializeField]
    private Transform parent;

    [SerializeField]
    private Vector2 areaToGenerate;

    [SerializeField]
    private GameObject[] worldAssetsPrefabs;
    
    [SerializeField]
    private List<Vector3> assetPosition;
    private float spacing;
    private int numberOfAssetsToGenerate;

    [ContextMenu("Generate assets")]
    private void GenerateAssets(){
        numberOfAssetsToGenerate = (int) (densityByUnit * areaToGenerate.x * areaToGenerate.y);
        spacing = densityByUnit * 2f * 0.8f;

        assetPosition = new List<Vector3>();

        for(int i = 0; i < numberOfAssetsToGenerate; i++){
            for(int j = 0; j < maxRetry; j++){

                var selectedPrefab = worldAssetsPrefabs[Random.Range(0, worldAssetsPrefabs.Length)];

                var selectedPostion = new Vector3(Random.Range(0, areaToGenerate.x), 0, Random.Range(0, areaToGenerate.y));
                selectedPostion.x += transform.position.x;
                selectedPostion.z += transform.position.z;

                if(!ValidSpacing(selectedPostion)){
                    continue;
                }

                var zRotation = Mathf.Floor(Random.Range(-1, 1));
                var rotation = Quaternion.Euler(0, zRotation == 0 ? 0 : 180, 0);

                Instantiate(selectedPrefab, selectedPostion, rotation, parent);
                assetPosition.Add(selectedPostion);
                break;
            }
        }
    }

    private bool ValidSpacing(Vector3 newSelectedPostion) {
        foreach (var lastPosition in assetPosition)
        {
            if(Vector3.Distance(lastPosition, newSelectedPostion) < spacing) {
                return false;
            }
        }
        return true;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        var position = new Vector3(areaToGenerate.x, 1, areaToGenerate.y);
        Gizmos.DrawWireCube(transform.position + position/2, position);
    }
}
