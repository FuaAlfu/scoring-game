using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 2021.7.4
/// </summary>

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    [Header("Player's props")]
    [SerializeField]
    private float moveSpeed;

    
    public Vector2 push = new Vector2(2f, 4f);

    [SerializeField]
    private float rotateAmount;

    [SerializeField]
    private float rotate;

    [Header("Levels props")]
    [SerializeField]
    GameObject g;

    [Tooltip("score to add")]
    [SerializeField]
    int score;

    [Tooltip("level score limit")]
    [SerializeField]
    int scoreLimit;

    //catch
    Rigidbody2D rb2D;


    // Start is called before the first frame update
    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        PlayerInputControler();
    }

    private void PlayerInputControler()
    {
        if (Input.GetMouseButton(0))
        {
            Vector3 mousePositionS = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            if (mousePositionS.x < 0)
            {
                rotate = rotateAmount;
            }
            else
            {
                rotate = -rotateAmount;
            }
            transform.Rotate(0, 0, rotate);
        }
    }

    private void FixedUpdate()
    {
        PlayerMovement();
    }

    private void PlayerMovement()
    {
        rb2D.velocity = transform.up * moveSpeed;
    }

    private void OnCollisionEnter2D(Collision2D c)
    {
        if(c.gameObject.CompareTag("tasty"))
        {
            Destroy(c.gameObject);
            GameSession.Instance.ScoreToAdd(score,scoreLimit); 
        }
        else if(c.gameObject.CompareTag("poison"))
        {
            //hit the player and the restart the level when the player die
            Destroy(c.gameObject);
            GameSession.Instance.LevelFailedLoader();
        }

        else if(c.gameObject.GetComponent<Wall>() || c.gameObject.CompareTag("wall"))
        {
            transform.position = g.transform.position;
        }
    }

    //todo
    void LevelLoader()
    {
        GameSession.Instance.LevelLoader();
    }
}
