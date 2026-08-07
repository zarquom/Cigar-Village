using ElmanGameDevTools.PlayerSystem;
using System;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.Localization;
using Yarn.Unity;

public class InteractableObject : MonoBehaviour
{
    private bool canInteract = true;
    [SerializeField] private LocalizedString interactionPromptTerm;
    [SerializeField] private InteractionType interactionType;
    [SerializeField] private GameObject interactionPrefab;
    [SerializeField] DialogueRunner dialogueRunner;
    [SerializeField] private Vector3 interactionPrefabPosition;
    [SerializeField] private Quaternion interactionPrefabRotation;
    [SerializeField] private bool needsKey = false;
    public bool CanInteract => canInteract;
    public LocalizedString InteractionPromptTerm => interactionPromptTerm;
    public InteractionType InteractionType => interactionType;
    public GameObject InteractionPrefab => interactionPrefab;

    private PlayerInventory playerInventory;
    private PlayerController playerController;
    private CharacterController playerCharController;
    private ShopManager shopManager;

    private void Start()
    {
        playerInventory = FindAnyObjectByType<PlayerInventory>();
        playerController = FindAnyObjectByType<PlayerController>();
        playerCharController = FindAnyObjectByType<CharacterController>();
        shopManager = FindAnyObjectByType<ShopManager>();
        if (interactionType == InteractionType.Talk && dialogueRunner == null)
        {
            dialogueRunner = FindAnyObjectByType<DialogueRunner>();
        }
    }
    public void Interact()
    {
        if (canInteract)
        {
            if (needsKey && !playerInventory.HasTheKey)
            {
                Debug.Log($"Cannot interact with {gameObject.name} without the key.");
                return;
            }

            canInteract = false; // Prevent further interactions
            PerformInteraction();
            Debug.Log($"Interacted with {gameObject.name}");        }
    }

    bool shopOpened = false;
    private void PerformInteraction()
    {
        switch( interactionType ) {
            case InteractionType.Pickup:
                GameObject obj = Instantiate( interactionPrefab, transform );
                obj.transform.SetLocalPositionAndRotation(interactionPrefabPosition, interactionPrefabRotation);
                break;
            case InteractionType.Talk:
                dialogueRunner.StartDialogue("Start");
                dialogueRunner.onDialogueComplete.AddListener(() =>
                {
                    if (!shopOpened)
                    {
                        playerController.SetCanMove(true);
                        canInteract = true; // Allow interaction again after dialogue is complete
                    }
                });
                dialogueRunner.AddCommandHandler("open_shop", () =>
                {
                    shopOpened = true;
                    shopManager.OpenShop(() =>
                    {
                        shopOpened = false;
                        playerController.SetCanMove(true);
                        canInteract = true; // Allow interaction again after shop is closed
                    });
                });
                playerController.SetCanMove(false);
                break;
            case InteractionType.Examine:
                // Handle examine interaction
                break;
            case InteractionType.Open:
                transform.localPosition = interactionPrefabPosition;
                transform.localRotation = interactionPrefabRotation;
                break;
            case InteractionType.Teleport:
                playerCharController.enabled = false;
                playerCharController.transform.position = new Vector3(interactionPrefab.transform.position.x, interactionPrefab.transform.position.y, interactionPrefab.transform.position.z);
                playerCharController.enabled = true;
                canInteract = true; // Allow interaction again after teleporting
                break;
        }
    }
}

public enum InteractionType
{
    None,
    Pickup,
    Talk,
    Examine,
    Open,
    Teleport
}