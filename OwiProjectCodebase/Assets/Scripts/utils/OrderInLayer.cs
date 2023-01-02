using UnityEngine;


public class OrderInLayer : MonoBehaviour
{
    public bool updateOrderInLayer;
    private SpriteRenderer[] srs;

    private void Awake()
    {
        srs = GetComponentsInChildren<SpriteRenderer>();

        SortOrder();
    }

    private void Update()
    {
        if (!updateOrderInLayer)
        {
            return;
        }
        
        SortOrder();
    }

    private void SortOrder(){
        for (int i = 0; i < srs.Length; i++){
            srs[i].sortingOrder = -(int)(transform.position.y * 100) + 10000;
        }
    }
}