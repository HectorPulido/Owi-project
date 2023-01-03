using UnityEngine;
using System.Collections;

public class Onomatopoeia : MonoBehaviour
{
    public static Onomatopoeia singleton;

    public int poolSize = 10;
    public OnomatopeiaItem prefab;    

    private OnomatopeiaItem[] pool;

    [SerializeField]
    private Transform temporalOnomatopoeiaTransform;
    [SerializeField]
    private string temporalOnomatopoeiaText;

    private void Awake()
    {
        // Singleton pattern
        if (singleton != null)
        {
            Destroy(gameObject);
            return;
        }

        singleton = this;

        // Fill pool        
        if (pool == null || pool.Length < poolSize)
            Pool();
    }

    [ContextMenu("Pool")]
    public void Pool() {
        // Object pooling
        pool = new OnomatopeiaItem[poolSize];
        for (int i = 0; i < poolSize; i++)
        {
            pool[i] = Instantiate(prefab, transform);
            pool[i].gameObject.SetActive(false);
            pool[i].gameObject.name = "Onomatopoeia " + i;
        }
    }

    public void SetOnomatopoeia(Vector3 position, string textToShow) {
        for (int i = 0; i < poolSize; i++)
        {
            if (pool[i].gameObject.activeInHierarchy)
            {
                continue;
            }

            pool[i].gameObject.SetActive(true);
            pool[i].SetOnomatopoeia(position, textToShow);
            return;
        }
    }

    public void MoveText(Transform t) {
        temporalOnomatopoeiaTransform = t;
        SetOnomatopoeiaTemporal();
    }

    public void SetText(string textToShow) {
        temporalOnomatopoeiaText = textToShow;
        SetOnomatopoeiaTemporal();
    }

    private void SetOnomatopoeiaTemporal() {
        if (temporalOnomatopoeiaTransform == null)
            return;
        
        if (temporalOnomatopoeiaText == "")
            return;

        SetOnomatopoeia(temporalOnomatopoeiaTransform.position, temporalOnomatopoeiaText);
        temporalOnomatopoeiaTransform = null;
        temporalOnomatopoeiaText = "";
    }

}

