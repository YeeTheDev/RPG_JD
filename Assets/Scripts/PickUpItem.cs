using UnityEngine;

public class PickUpItem : MonoBehaviour
{
    private bool canPickup;

    private void Update()
    {
        if (canPickup && Input.GetButtonDown("Fire1"))
        {
            GameManager.instance.AddItem(GetComponent<Item>().itemName);
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other) { if (other.CompareTag("Player")) { canPickup = true; } }
    private void OnTriggerExit2D(Collider2D other) { if (other.CompareTag("Player")) { canPickup = false; } }
}
