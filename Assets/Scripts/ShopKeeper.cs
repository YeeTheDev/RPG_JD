using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopKeeper : MonoBehaviour
{
    public string[] itemsForSale;

    private bool canOpen;

    private void Update()
    {
        if (canOpen && Input.GetButtonDown("Fire1") && PlayerController.instance.canMove && !Shop.instance.shopMenu.activeInHierarchy)
        {
            Shop.instance.itemsForSale = itemsForSale;
            Shop.instance.OpenShop();
        }
    }

    private void OnTriggerEnter2D(Collider2D other) { if (other.CompareTag("Player")) { canOpen = true; } }
    private void OnTriggerExit2D(Collider2D other) { if (other.CompareTag("Player")) { canOpen = false; } }
}
