using UnityEngine;
using System.Collections;

public class Onomatopoeia : MonoBehaviour
{
    public int poolSize = 10;
    public OnomatopeiaItem prefab;
    
    public float timeToDisable = 1f;

    private OnomatopeiaItem[] pool;

    public static Onomatopoeia singleton;

    private Transform temporalOnomatopoeiaTransform;
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
            StartCoroutine(DisableItem(pool[i]));
            return;
        }
    }

    IEnumerator DisableItem(OnomatopeiaItem item) {
        yield return new WaitForSeconds(timeToDisable);
        item.gameObject.SetActive(false);
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

