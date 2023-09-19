using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSounds : MonoBehaviour {

    private float footstepTimer;
    private float footstepTimeMax = 0.1f;

    private void Update() {
        footstepTimer -= Time.deltaTime;

        if (footstepTimer < 0f) {
            footstepTimer = footstepTimeMax;

            if (Player.Instance.IsWalking()) {
                SoundManager.Instance.PlayFootstepSound(transform.position);
            }
        }
    }

}
