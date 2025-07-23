using UnityEngine;

public class InfiniteChildMover : MonoBehaviour
{
    [Header("Elements")]
    [SerializeField] private Transform playerTransform;
    
    [Header("Settings")]
    [SerializeField] private float mapChunkSize;
    [SerializeField] private float distanceThreshold = 1.5f;

    private void Update()
    {
        UpdateChildern();
    }

    private void UpdateChildern()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            Transform child = transform.GetChild(i);
            
            Vector3 distance = playerTransform.position - child.position;

            float calculateedDistanceThreshold = distanceThreshold * mapChunkSize;
            if (Mathf.Abs(distance.x) > calculateedDistanceThreshold)
                child.position += Vector3.right * (calculateedDistanceThreshold * 2 * Mathf.Sign(distance.x));
            
            if (Mathf.Abs(distance.y) > calculateedDistanceThreshold)
                child.position += Vector3.up * (calculateedDistanceThreshold * 2 * Mathf.Sign(distance.y)); 
        }
    }
}
