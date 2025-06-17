using TMPro;
using UnityEngine;

public class BattleNotification : MonoBehaviour
{
    public float awakeTime;
    public TextMeshProUGUI notificationText;

    public void Activate()
    {
        gameObject.SetActive(true);
        Invoke("Deactivate", awakeTime);
    }

    private void Deactivate() => gameObject.SetActive(false);
}
