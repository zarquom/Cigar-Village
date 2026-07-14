using UnityEditor;
using UnityEngine;
using UnityEngine.Localization;

public class InteractableObject : MonoBehaviour
{
    private bool canInteract = true;
    [SerializeField] private LocalizedString interactionPromptTerm;
    public bool CanInteract => canInteract;
    public LocalizedString InteractionPromptTerm => interactionPromptTerm;
    public void Interact()
    {
        if (canInteract)
        {
            // Perform interaction logic here
            Debug.Log($"Interacted with {gameObject.name}");
            canInteract = false; // Prevent further interactions
        }
    }
}
