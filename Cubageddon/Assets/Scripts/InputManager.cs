using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class InputManager : MonoBehaviour
{
    PlayerInput playerInput;

    [HideInInspector] public InputAction moveAction;
    [HideInInspector] public InputAction primaryFireAction;
    [HideInInspector] public InputAction secondaryFireAction;
    [HideInInspector] public InputAction movementSkillAction;

    public static InputManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(this);

            return;
        }

        Instance = this;

        DontDestroyOnLoad(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        playerInput = GetComponent<PlayerInput>();

        moveAction = playerInput.actions["Move"];
        primaryFireAction = playerInput.actions["PrimaryFire"];
        secondaryFireAction = playerInput.actions["SecondaryFire"];
        movementSkillAction = playerInput.actions["MovementSkill"];

        //TEMP
        SceneManager.LoadScene("Main");
    }
}
