using UnityEngine;

public class Sword : MonoBehaviour {
    [SerializeField] private Player player;
    [SerializeField] private Collider2D hitbox;
    [SerializeField] private float idleAngle;
    [SerializeField] private float attackAngle;
    [SerializeField] private float idleLerp;
    [SerializeField] private float attackLerp;

    private void Update() {
        switch (player.State) {
            case PlayerState.Attack:
                hitbox.enabled = true;
                transform.rotation = Quaternion.Lerp(
                    transform.rotation,
                    Quaternion.Euler(0, 0, attackAngle),
                    attackLerp
                );
                break;

            case PlayerState.FollowThrough:
                hitbox.enabled = false;
                transform.rotation = Quaternion.Lerp(
                    transform.rotation,
                    Quaternion.Euler(0, 0, idleAngle),
                    idleLerp
                );

                break;
        }
    }
}