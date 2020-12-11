using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableObject : MonoBehaviour {
    bool canInteract;
    public bool hadInteracted;
    public ObjectType objectType;

    [SerializeField] private float totalCooldown = 10f;

    private PlayerController player;
    private HumanController human;
    private float cooldownTimer;

    [SerializeField] private GameObject ObjectToConfigure;

    public enum ObjectType {
        TOY,
        DISPLAY,
        BOOKSHELF,
        TOASTER,
        LIGHT,
        FAUCET,
        LIGHTS2,
        LIGHTS3,
        PC,
        TOILETFAUCET,
        KITCHENFAUCET
    };

    // Start is called before the first frame update
    void Start() {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        human = GameObject.FindGameObjectWithTag("Human").GetComponent<HumanController>();
    }

    // Update is called once per frame
    void Update() {
        if (canInteract) {
            if (Input.GetKeyDown(KeyCode.E)) {
                if (objectType != ObjectType.TOY)
                {
                    player.animator.SetTrigger("Interact");
                }
                Interact();
                human.AlreadyDistracted = true;
            }
        }
        Cooldown();

        if (objectType == ObjectType.TOY) {
            if (!hadInteracted) {
                if (transform.parent != null) {
                    DropToy();
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other) {
        switch (other.tag) {
            case "Player":
                if (!hadInteracted) {
                    if (!human.AlreadyDistracted) {
                        {
                            canInteract = true;
                            player.GetComponent<PlayerController>().canInteract = true;
                        }
                    } else if (objectType == ObjectType.TOY) {
                        canInteract = true;
                        player.GetComponent<PlayerController>().canInteract = true;
                    }
                }
                break;
        }
    }

    private void OnTriggerExit(Collider other) {
        switch (other.tag) {
            case "Player":
                canInteract = false;
                player.GetComponent<PlayerController>().canInteract = false;
                break;
        }
    }

    private void Interact() {
        if (!hadInteracted) {
            switch (objectType) {
                case ObjectType.TOY:
                    Debug.Log("Interact with TOY");
                    player.GetComponent<PlayerController>().canInteract = false;
                    player.moveSpeed -= 4;
                    player.canJump = false;
                    gameObject.transform.SetParent(player.transform);
                    gameObject.GetComponent<Rigidbody>().useGravity = false;
                    gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
                    gameObject.transform.position = GameObject.Find("ToyPickupPosition").transform.position;
                    player.animator.SetBool("Carrying", true);
                    hadInteracted = true;
                    break;
                case ObjectType.DISPLAY:
                    if (!human.AlreadyDistracted) {
                        Debug.Log("Interact with Display");
                        player.GetComponent<PlayerController>().canInteract = false;
                        human.isTVOn = true;
                        hadInteracted = true;
                        ObjectToConfigure.GetComponent<TvRemote>().Trigger(true);
                    }
                    break;
                case ObjectType.PC:
                    if (!human.AlreadyDistracted) {
                        Debug.Log("Interact with PC");
                        player.GetComponent<PlayerController>().canInteract = false;
                        human.isPCOn = true;
                        hadInteracted = true;
                        ObjectToConfigure.GetComponent<TvRemote>().Trigger(true);
                    }
                    break;
                case ObjectType.BOOKSHELF:
                    if (!human.AlreadyDistracted) {
                        Debug.Log("Interact with Bookshelf");
                        player.GetComponent<PlayerController>().canInteract = false;
                        human.bookNeedsFix = true;
                        hadInteracted = true;
                        ObjectToConfigure.GetComponent<ToyCannon>().Trigger();
                    }
                    break;
                case ObjectType.TOASTER:
                    if (!human.AlreadyDistracted) {
                        Debug.Log("Interact with Toaster");
                        player.GetComponent<PlayerController>().canInteract = false;
                        human.isToasterOn = true;
                        hadInteracted = true;
                        ObjectToConfigure.GetComponent<Toaster>().Trigger(true);
                    }
                    break;
                case ObjectType.LIGHT:
                    if (!human.AlreadyDistracted) {
                        Debug.Log("Interact with Lights");
                        player.GetComponent<PlayerController>().canInteract = false;
                        human.isLightsOn = true;
                        hadInteracted = true;
                        ObjectToConfigure.GetComponent<LightSwitch>().Trigger(true);
                    }
                    break;
                case ObjectType.LIGHTS2:
                    if (!human.AlreadyDistracted) {
                        Debug.Log("Interact with Light2");
                        player.GetComponent<PlayerController>().canInteract = false;
                        human.isLights2On = true;
                        hadInteracted = true;
                        ObjectToConfigure.GetComponent<LightSwitch>().Trigger(true);
                    }
                    break;
                case ObjectType.LIGHTS3:
                    if (!human.AlreadyDistracted) {
                        Debug.Log("Interact with Light3");
                        player.GetComponent<PlayerController>().canInteract = false;
                        human.isLights3On = true;
                        hadInteracted = true;
                        ObjectToConfigure.GetComponent<LightSwitch>().Trigger(true);
                    }
                    break;
                case ObjectType.FAUCET:
                    if (!human.AlreadyDistracted) {
                        Debug.Log("Interact with Faucet");
                        player.GetComponent<PlayerController>().canInteract = false;
                        human.isFaucetOn = true;
                        hadInteracted = true;
                        ObjectToConfigure.GetComponent<Faucet>().Trigger(true);
                    }
                    break;
                case ObjectType.TOILETFAUCET:
                    if (!human.AlreadyDistracted) {
                        Debug.Log("Interact with Toilet Faucet");
                        player.GetComponent<PlayerController>().canInteract = false;
                        human.isToiletFaucetOn = true;
                        hadInteracted = true;
                        ObjectToConfigure.GetComponent<Faucet>().Trigger(true);
                    }
                    break;
                case ObjectType.KITCHENFAUCET:
                    if (!human.AlreadyDistracted) {
                        Debug.Log("Interact with Kitchen Faucet");
                        player.GetComponent<PlayerController>().canInteract = false;
                        human.isKitchenFaucetOn = true;
                        hadInteracted = true;
                        ObjectToConfigure.GetComponent<Faucet>().Trigger(true);
                    }
                    break;
            }
        }
    }

    public void DropToy() {
        gameObject.GetComponent<Rigidbody>().useGravity = true;
        gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
        transform.SetParent(null);
        player.moveSpeed = 10f;
        player.canJump = true;
        player.animator.SetBool("Carrying", false);
    }

    private void Cooldown() {
        if (hadInteracted) {
            cooldownTimer += Time.deltaTime;
            if (cooldownTimer > totalCooldown) {
                hadInteracted = false;
                cooldownTimer = 0f;
            }
        }
    }
}