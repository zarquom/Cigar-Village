using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.Localization;

public class InteractableObject : MonoBehaviour
{
    private bool canInteract = true;
    [SerializeField] private LocalizedString interactionPromptTerm;
    [SerializeField] private InteractionType interactionType;
    [SerializeField] private GameObject interactionPrefab;
    [SerializeField] private Transform interactionPrefabTransform;
    public bool CanInteract => canInteract;
    public LocalizedString InteractionPromptTerm => interactionPromptTerm;
    public InteractionType InteractionType => interactionType;
    public GameObject InteractionPrefab => interactionPrefab;
    public void Interact()
    {
        if (canInteract)
        {
            PerformInteraction();
            Debug.Log($"Interacted with {gameObject.name}");
            canInteract = false; // Prevent further interactions
        }
    }

    private void PerformInteraction()
    {
        switch( interactionType ) {
            case InteractionType.Pickup:
                GameObject obj = Instantiate( interactionPrefab, transform );
                obj.transform.localPosition = new Vector3(-1.44f, -0.8f, 0.65f);
                break;
            case InteractionType.Talk:
                // Handle talk interaction
                break;
            case InteractionType.Examine:
                // Handle examine interaction
                break;
        }
    }
}

public enum InteractionType
{
    None,
    Pickup,
    Talk,
    Examine
}