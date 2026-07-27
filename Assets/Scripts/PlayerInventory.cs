using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInventory : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI cigarText;
    [SerializeField] private Image lifeImage;
    private float cigarCount = 0f;
    private bool hasTheKey = false;

    public bool HasTheKey => hasTheKey;

    public void OnPlayerColliderHit(Collider col)
    {
        if(col.tag == "cigar")
        {
            CigarObject cigarObject = col.gameObject.GetComponent<CigarObject>();
            cigarCount += cigarObject.CigarValue;
            cigarText.text = $"Cigars: {cigarCount:F0}";
            Destroy(col.gameObject);
        }else if (col.tag == "key")
        {
            hasTheKey = true;
            Destroy(col.gameObject);
        }
        else if (col.tag == "life")
        {
            LifeObject lifeObject = col.gameObject.GetComponent<LifeObject>();
            OnAddLife(lifeObject.LifeValue);
            Destroy(col.gameObject);
        }
    }

    public void OnAddLife(float lifeToAdd)
    {
        lifeImage.rectTransform.offsetMax = new Vector2(lifeImage.rectTransform.offsetMax.x + lifeToAdd, lifeImage.rectTransform.offsetMax.y);
    }
}
