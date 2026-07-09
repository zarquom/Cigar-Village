using UnityEngine;

public class PlayerReposition : MonoBehaviour
{
    [SerializeField] private CharacterController playerCharController;
    [SerializeField] private float boundValue;
    void Update()
    {
        Debug.Log(playerCharController.transform.localPosition.z);
        if (playerCharController.transform.localPosition.z < -boundValue)
        {
            playerCharController.enabled = false;
            playerCharController.transform.localPosition = new Vector3(playerCharController.transform.localPosition.x, playerCharController.transform.localPosition.y, boundValue);
            playerCharController.enabled = true;
        }
        if (playerCharController.transform.localPosition.z > boundValue)
        {
            playerCharController.enabled = false;
            playerCharController.transform.localPosition = new Vector3(playerCharController.transform.localPosition.x, playerCharController.transform.localPosition.y, -boundValue);
            playerCharController.enabled = true;
        }
    }
}
