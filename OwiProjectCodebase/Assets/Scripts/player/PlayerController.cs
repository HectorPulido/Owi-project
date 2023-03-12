using UnityEngine;
using System.Collections.Generic;


public class PlayerController : MonoBehaviour
{
    public static PlayerController singleton;
    
    public CameraControl cameraControl;
    public bool canMove = true;
    public int currentPlayer;
    public Transform playerIndicator;

    public float partnerMinDistance = 2;
    public float interactionRadius = 5;

    // COMPONENTS
    [HideInInspector]
    public List<GameObject> players;
    private List<Movement> movement;
    private List<Bumping> bumping;
    private List<HealthAndMana> healthAndManas;
    private List<AttackManager> attackManagers;
    private HealthUI[] healthBars;

    public GameObject currentPlayerGameObject
    {
        get
        {
            if (players == null || players.Count == 0)
            {
                return null;
            }
            return players[currentPlayer];
        }
    }

    // Input
    private float vertical;
    private float horizontal;
    private bool togglePlayerEnter;
    private bool followPlayer;
    private bool triggerEnter;

    private bool triggerAttack1;
    private bool triggerAttack2;

    private Vector2 movementDirection;
    private bool moving;

    private void Awake()
    {
        if (singleton)
        {
            Destroy(gameObject);
            return;
        }
        singleton = this;

        HealthUIStart();
        UpdatePlayers();
        CamerStart();
    }

    private void CamerStart()
    {
        cameraControl = GameObject.FindObjectOfType<CameraControl>();
        cameraControl.target = currentPlayerGameObject.transform;
    }

    [ContextMenu("Health UI Start")]
    private void HealthUIStart()
    {
        healthBars = GameObject.FindObjectsOfType<HealthUI>(true);
        foreach (var bar in healthBars)
        {
            bar.gameObject.SetActive(false);
        }
    }

    [ContextMenu("Update players")]
    public void UpdatePlayers()
    {
        players = new List<GameObject>(GameObject.FindGameObjectsWithTag("Player"));
        movement = new List<Movement>();
        bumping = new List<Bumping>();
        attackManagers = new List<AttackManager>();
        healthAndManas = new List<HealthAndMana>();

        for (int i = 0; i < players.Count; i++)
        {
            Movement t_movement = players[i].GetComponent<Movement>();
            HealthAndMana playerStats = players[i].GetComponent<HealthAndMana>();
            AttackManager attackManager = players[i].GetComponent<AttackManager>();
            movement.Add(t_movement);
            bumping.Add(t_movement.bumping);
            healthAndManas.Add(playerStats);
            attackManagers.Add(attackManager);

            BindHealthAndMana(playerStats, i);
        }

    }

    private void BindHealthAndMana(HealthAndMana playerStats, int id)
    {
        playerStats.healthCallback = null;
        playerStats.manaCallback = null;
        playerStats.aliveCallback = null;

        if (healthBars.Length <= id)
        {
            return;
        }

        var healthBar = healthBars[id];
        healthBar.gameObject.SetActive(true);
        healthBar.characterName.text = playerStats.playerName;

        healthBar.manaBar.fillAmount = playerStats.healthPercentage;
        healthBar.healthBar.fillAmount = playerStats.manaPercentage;

        playerStats.manaCallback = (float mana) => healthBar.manaBar.fillAmount = mana;
        playerStats.healthCallback = (float health) => healthBar.healthBar.fillAmount = health;
    }

    private void MoveIndicator()
    {
        playerIndicator.position = movement[currentPlayer].pivot.transform.position;
    }

    private void Update()
    {
        GetInputs();

        if (!canMove)
        {
            return;
        }

        if (togglePlayerEnter)
        {
            TogglePlayer();
        }

        MovePrincipal();
        MoveSecondaries();

        if (triggerEnter)
        {
            Trigger();
        }

        if (triggerAttack1)
        {
            Attack();
        }
    }

    private void FixedUpdate()
    {
        MoveIndicator();
    }

    private void Attack()
    {
        attackManagers[currentPlayer].Attack();
    }

    public void MovePrincipal()
    {
        if (DialogSystem.singleton && DialogSystem.DialogIsActive)
        {
            horizontal = 0;
            vertical = 0;
        }

        moving = vertical != 0 || horizontal != 0;
        bumping[currentPlayer].moving = moving;

        movement[currentPlayer].following = false;
        movementDirection = new Vector2(horizontal, vertical);
        movement[currentPlayer].Move(movementDirection);
    }

    public void MoveSecondaries()
    {
        for (int i = 0; i < players.Count; i++)
        {
            if (i == currentPlayer)
                continue;
            movement[i].Follow(players[currentPlayer].transform, partnerMinDistance, followPlayer);
        }
    }

    public void TogglePlayer()
    {
        currentPlayer = (currentPlayer + 1) % players.Count;
        for (int i = 0; i < healthAndManas.Count; i++)
        {
            if (i == currentPlayer)
            {
                healthAndManas[i].autoHeal = false;
                continue;
            }
            healthAndManas[i].autoHeal = true;
            
        }

        cameraControl.target = currentPlayerGameObject.transform;
    }

    private void Trigger()
    {
        if (DialogSystem.singleton && DialogSystem.DialogIsActive)
        {
            DialogSystem.singleton.ContinueDialog();
            return;
        }

        var cc = Physics.OverlapSphere(
            players[currentPlayer].transform.position,
            interactionRadius
        );

        int maxPriority = int.MinValue;
        Actionable maxActionable = null;

        foreach (var col in cc)
        {
            if (col.transform == players[currentPlayer])
            {
                continue;
            }

            var actionable = col.GetComponent<Actionable>();
            if (actionable != null)
            {
                if (actionable.priority > maxPriority)
                {
                    maxActionable = actionable;
                    maxPriority = actionable.priority;
                }
            }
        }

        if (maxActionable != null)
        {
            maxActionable.Act();
        }
    }

    private void GetInputs()
    {
        vertical = InputManager.VerticalAxis;
        horizontal = InputManager.HorizontalAxis;
        togglePlayerEnter = InputManager.TogglePlayerEnter;
        followPlayer = !InputManager.FollowPlayer;
        triggerEnter = InputManager.TriggerEnter;

        triggerAttack1 = InputManager.Attack1Enter;
        triggerAttack2 = InputManager.Attack2Enter;
    }

}
