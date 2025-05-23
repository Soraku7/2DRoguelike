using UnityEngine;

public class DamageTextManager : MonoBehaviour
{
    [Header("Elements")]
    [SerializeField] private DamageText damageTextPrefab;

    [NaughtyAttributes.Button]
    private void InstantiateDamageText()
    {
        Vector3 spawnPosition = Random.insideUnitCircle * Random.Range(1f , 5f);
        DamageText damageTextInstance = Instantiate(damageTextPrefab ,spawnPosition , Quaternion.identity , transform);
        damageTextInstance.Animate();
    }
}
