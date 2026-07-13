using TMPro;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI cigarText;
    private float cigarCount = 0f;

    public void OnPlayerColliderHit(Collider col)
    {
        if(col.tag == "cigar")
        {
            CigarObject cigarObject = col.gameObject.GetComponent<CigarObject>();
            cigarCount += cigarObject.CigarValue;
            cigarText.text = $"Cigars: {cigarCount:F0}";
            Destroy(col.gameObject);
        }
    }
}
