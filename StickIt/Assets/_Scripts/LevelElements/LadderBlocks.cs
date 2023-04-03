using UnityEngine;

public class LadderBlocks : MonoBehaviour
{
    [field: SerializeField] public Stickie MainBlock { get; private set; }
    [field: SerializeField] public Stickie LadderBlock { get; private set; }

    [field: SerializeField] public float RotationSpeedPerSecond { get; private set; } = 90f;
    private Rigidbody2D _mainRB;
    private Rigidbody2D _ladderRB;

    private void Start()
    {
        _mainRB = MainBlock.GetComponent<Rigidbody2D>();
        _ladderRB = LadderBlock.GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if (!MainBlock.IsStuck && !LadderBlock.IsStuck)
        {
            _mainRB.MoveRotation(_mainRB.rotation + RotationSpeedPerSecond * Time.fixedDeltaTime);
            _ladderRB.MoveRotation(_ladderRB.rotation + RotationSpeedPerSecond * Time.fixedDeltaTime);
        }
        else if (MainBlock.IsStuck && !LadderBlock.IsStuck)
        {
            Vector2 main2ladder = LadderBlock.transform.position - MainBlock.transform.position;
            // rotate vector
            main2ladder = Vector2.Perpendicular(main2ladder).normalized;
            _ladderRB.MovePosition(_ladderRB.position + main2ladder * Time.fixedDeltaTime * RotationSpeedPerSecond);
            // LadderBlock.transform.Rotate(Vector3.forward, RotationSpeedPerSecond * Time.fixedDeltaTime);
        }
        else if (!MainBlock.IsStuck && LadderBlock.IsStuck)
        {
            Vector2 ladder2main = MainBlock.transform.position - LadderBlock.transform.position;
            // rotate vector
            ladder2main = Vector2.Perpendicular(ladder2main).normalized;
            _mainRB.MovePosition(_mainRB.position + ladder2main * Time.fixedDeltaTime * RotationSpeedPerSecond);
            // MainBlock.transform.Rotate(Vector3.forward, -RotationSpeedPerSecond * Time.fixedDeltaTime);
        }
    }
}