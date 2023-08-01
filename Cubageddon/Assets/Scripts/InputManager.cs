using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    PlayerInput playerInput;

    [HideInInspector] public InputAction moveAction;

    public static InputManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(this);

            return;
        }

        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        playerInput = GetComponent<PlayerInput>();

        moveAction = playerInput.actions["Move"];
    }
}
