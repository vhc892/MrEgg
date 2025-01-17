using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;

public class TapWater : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private GameObject waterPrefab;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private Transform waterLevel;
    [SerializeField] private float spawnInterval = 0.2f;
    [SerializeField] private int maxWaterDrops = 10;
    [SerializeField] private float waterLifetime = 3f;
    [SerializeField] private Vector2 randomOffset = new Vector2(0.2f, 0f);
    [SerializeField] private Vector2 initialForceRange = new Vector2(0f, -2f);
    [SerializeField] private float waterRiseAmount = 0.1f;
    [SerializeField] private float riseDuration = 0.5f;
    [SerializeField] private float maxWaterLevelY = 5f;

    private bool isSpawning = false;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (!isSpawning)
        {
            StartCoroutine(SpawnWater());
        }
    }

    private IEnumerator SpawnWater()
    {
        isSpawning = true;
        int spawnedCount = 0;

        while (spawnedCount < maxWaterDrops)
        {
            Vector3 randomPosition = spawnPoint.position + new Vector3(
                Random.Range(-randomOffset.x, randomOffset.x),
                Random.Range(-randomOffset.y, randomOffset.y),
                0);

            GameObject water = Instantiate(waterPrefab, randomPosition, Quaternion.identity);
            water.transform.parent = transform.parent;

            Rigidbody2D rb = water.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                Vector2 randomForce = new Vector2(
                    Random.Range(-initialForceRange.x, initialForceRange.x),
                    Random.Range(initialForceRange.y, 0));
                rb.AddForce(randomForce, ForceMode2D.Impulse);
            }

            WaterDrop drop = water.GetComponent<WaterDrop>();
            drop.Setup(this);

            Destroy(water, waterLifetime);

            spawnedCount++;
            yield return new WaitForSeconds(spawnInterval);
        }

        isSpawning = false;
    }

    public void IncreaseWaterLevel()
    {
        if (waterLevel.position.y >= maxWaterLevelY)
        {
            return;
        }

        float newY = Mathf.Min(waterLevel.position.y + waterRiseAmount, maxWaterLevelY);
        waterLevel.DOMoveY(newY, riseDuration).SetEase(Ease.OutQuad);
    }
}
