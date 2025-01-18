using UnityEngine;

public class WaterDrop : MonoBehaviour
{
    private TapWater faucet;

    public void Setup(TapWater faucet)
    {
        this.faucet = faucet;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("WaterRaise"))
        {
            faucet.IncreaseWaterLevel();
            Destroy(gameObject);
        }
    }
}