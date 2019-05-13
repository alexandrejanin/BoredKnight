using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Player : MonoBehaviour {
    [SerializeField] private EventSystem eventSystem;
    [SerializeField] private GraphicRaycaster graphicRaycaster;
    [SerializeField] private Rigidbody2D knight;

    [TabGroup("Attack"), SerializeField] private float attackDuration;
    [TabGroup("Attack"), SerializeField] private float followThroughDuration;

    [TabGroup("Jump"), SerializeField] private LayerMask ground;
    [TabGroup("Jump"), SerializeField] private float jumpForce;

    [SerializeField] private UnityEvent onDeath;

    private PlayerState state = PlayerState.Idle;
    public PlayerState State => state;

    private float timer;

    private void Update() {
        // Detect tap outside UI
        if (Input.GetMouseButtonDown(0)) {
            var pointerEventData = new PointerEventData(eventSystem) {
                position = Input.mousePosition
            };

            var results = new List<RaycastResult>();

            graphicRaycaster.Raycast(pointerEventData, results);

            if (results.Count == 0) {
                if (Input.mousePosition.x < Screen.width / 2f) {
                    Jump();
                }
                else {
                    Attack();
                }
            }
        }

        switch (state) {
            case PlayerState.Idle:
                break;

            case PlayerState.Attack:
                timer += Time.fixedDeltaTime;
                if (timer >= attackDuration) {
                    timer = 0;
                    state = PlayerState.FollowThrough;
                }

                break;

            case PlayerState.FollowThrough:
                timer += Time.fixedDeltaTime;
                if (timer >= followThroughDuration) {
                    timer = 0;
                    state = PlayerState.Idle;
                }

                break;

            case PlayerState.Jump:
                if (knight.velocity.y <= 0 && transform.position.y <= .52f) {
                    state = PlayerState.Idle;
                }

                break;

            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    private void Attack() {
        if (state == PlayerState.Idle)
            state = PlayerState.Attack;
    }

    private void Jump() {
        if (state == PlayerState.Idle) {
            knight.AddForce(jumpForce * Vector3.up, ForceMode2D.Impulse);
            state = PlayerState.Jump;
        }
    }

    public void Die() {
        onDeath?.Invoke();
        Destroy(gameObject);
    }
}

public enum PlayerState {
    Idle,
    Attack,
    FollowThrough,
    Jump
}