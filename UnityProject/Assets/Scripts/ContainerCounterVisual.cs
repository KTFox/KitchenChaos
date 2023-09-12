using UnityEngine;

public class ContainerCounterVisual : MonoBehaviour {

    private const string OPEN_CLOSE = "OpenClose";

    [SerializeField] ContainerCounter containerCounter;

    private Animator animator;

    private void Awake() {
        animator = GetComponent<Animator>();
    }

    private void Start() {
        containerCounter.OnPlayerGrappedObject += ContainerCounter_OnPlayerGrappedObject;
    }

    private void ContainerCounter_OnPlayerGrappedObject(object sender, System.EventArgs e) {
        animator.SetTrigger(OPEN_CLOSE);
    }
}

