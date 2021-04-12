using System.Collections;
using System.Collections.Generic;
using UnityEngine;


interface Player1
{
    void UpdateSpriteDirection();
}
public class Player : MonoBehaviour , Player1
{
    
    [Header("Stats")]
    public float moveSpeed;
    public float curHp;
    public float maxHp;
    public float damageTaken;
    public List<string> inventory = new List<string>();
    public float interactRange;
    private Vector2 facingDirection;
    public bool entered = false;

    [Header("Experience")]
    public int curLevel;
    public int curXp;
    public int xpToNextLevel;
    public float levelXpModifier;

  
    [Header("Sprites")]
    public Sprite downSprite;
    public Sprite upSprite;
    public Sprite leftSprite;
    public Sprite rightSprite;

    [Header("Inventory")]
    public bool hasHackySack = false;
    public bool hasBoomBox = false;
    public bool hasHulaHoop = false;
    public int money = 50;

    // components
    private Rigidbody2D rig;
    private SpriteRenderer sr;
    private PlayerUI ui;

void Awake()
{
    rig = GetComponent<Rigidbody2D>();
    sr = GetComponent<SpriteRenderer>();
        ui = FindObjectOfType<PlayerUI>();
}

void Start()
    {
        // initialize UI elements
        ui.UpdateHealthBar();
        ui.UpdateXpBar();
        ui.UpdateLevelText();
    }

void Update()
    {
        Move();

        CheckInteract();

        AddToInventory();
    }

void AddToInventory()
    {
        
        if (entered == true && Input.GetKeyUp("1") && money >= 40)
        {
            Debug.Log(entered);
            AddItemToInventory("Shrooms");

        }
        else if (entered == true && Input.GetKeyUp("2") && money >= 10)
        {
            AddItemToInventory("Acid");
        }
        else if (entered == true && Input.GetKeyUp("3") && money >= 20)
        {
            AddItemToInventory("Molly");
        }
    }

void CheckInteract ()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, facingDirection, interactRange, 1 << 9);

        if(hit.collider != null)
        {
            Interactable interactable = hit.collider.GetComponent<Interactable>();
            ui.SetInteractText(hit.collider.transform.position, interactable.interactDescription);

            if (Input.GetButtonDown("Fire1"))
                interactable.Interact();
        }
        else
        {
            ui.DisableInteractText();
        }
    }
 public void OnTriggerEnter2D(Collider2D collision)
   {
        if (collision.GetComponent<Dealer>() != null)
        {
            entered = true;
            Debug.Log(entered);
        }    
    }

   public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<Dealer>() != null)
        {
            entered = false;
            Debug.Log(entered);
        }
    }


    void Move()
{
    float x = Input.GetAxis("Horizontal");
    float y = Input.GetAxis("Vertical");

    // calculate velocity we will move at
    Vector2 vel = new Vector2(x, y);

    //calculate facing direction
    if (vel.magnitude != 0)
        facingDirection = vel;

    UpdateSpriteDirection();

    // set the velocity
    rig.velocity = vel * moveSpeed;
}

public void UpdateSpriteDirection()
{
    if (facingDirection == Vector2.up)
        sr.sprite = upSprite;
    else if (facingDirection == Vector2.down)
        sr.sprite = downSprite;
    else if (facingDirection == Vector2.right)
        sr.sprite = rightSprite;
    else if (facingDirection == Vector2.left)
        sr.sprite = leftSprite;


}

    public void AddXp (int xp)
    {
        curXp += xp;
        ui.UpdateXpBar();

        if (curXp >= xpToNextLevel)
            LevelUp();

    }

    void LevelUp()
    {
        curXp -= xpToNextLevel;
        curLevel++;

        xpToNextLevel = Mathf.RoundToInt((float)xpToNextLevel * levelXpModifier);

        ui.UpdateLevelText();
        ui.UpdateXpBar();
    }

    public void TakeDamage(int damageTaken)
    {
        curHp -= damageTaken;

        ui.UpdateHealthBar();
        if (curHp <= 0)
            Die();


    }

    void Die ()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Game");
    }

    // adds a new item to our inventory
    public void AddItemToInventory(string item)
    {
        switch (item)
        {
            case "Boombox":
                hasBoomBox = true;
                break;
            case "Hacky Sack":
                hasHackySack = true;
                break;
            case "Hula Hoop":
                hasHulaHoop = true;
                break;
            case "Shrooms":
                money -= 40;
                break;
            case "Acid":
                money -= 10;
                break;
            case "Molly":
                money -= 20;
                break;
            default:
                
                break;
        }

     
        inventory.Add(item);
        ui.UpdateInventoryText();
    }

    public void RemoveItemFromInventory(string item)
    {
        switch (item)
        {
            case "Boombox":
                hasBoomBox = false;
                break;
            case "Hacky Sack":
                hasHackySack = false;
                break;
            case "Hula Hoop":
                hasHulaHoop = false;
                break;
            default:

                break;
        }

        inventory.Remove(item);
        ui.UpdateInventoryText();
    }

}


