using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    [Tooltip("The desired movement speed of character.")]
    public float moveSpeed = 20.0f;

    Animator animController;
    Camera   cam;

    // start and end points of movement
    Vector3 startPos;
    Vector3 targetPos;
    // linear interpolation parameter t
    float t = 0.0f;

    // co-routine method variable (pointer)
    IEnumerator moveCoRoutine;

    private IEnumerator MoveToLocation ()
    {
        // direction vector (with magnitude)
        Vector3 moveDir;

        while (t < 1.0f)
        {
            moveDir = targetPos - startPos;

            // find next interpolation step, enforcing constant
            // a move rate to the target based on desired speed
            t += Time.deltaTime * moveSpeed / moveDir.magnitude;

            // transform between start position and end position
            transform.position = Vector3.Lerp(startPos, targetPos, t);

            // pause execution for 1 tick of game loop
            yield return null;
        }

        // finished with co-routine
        yield return null;
    }

    // Start is called before the first frame update
    void Start()
    {
        // get the animation controller on startup
        animController = GetComponent<Animator>();

        // find and save reference to scene camera
        cam = (Camera) FindObjectOfType (typeof (Camera));
    }

    // Update is called once per frame
    void Update()
    {
        // left mouse button (on map): Move to location
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            if (moveCoRoutine != null)
                StopCoroutine(moveCoRoutine);

            Ray ray = cam.ScreenPointToRay (Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                // where we started this movement
                startPos  = transform.position;
                // desired target position in map
                targetPos = hit.point;
                // reset interpolation parameter
                t = 0.0f;

                // store a pointer to active co-routine
                moveCoRoutine = MoveToLocation();

                // execute co-routine to carry out move
                StartCoroutine(moveCoRoutine);
            }
        }

        // right mouse button or spacebar: Magical sword attack
        if (Input.GetKeyDown(KeyCode.Mouse1) || Input.GetKeyDown(KeyCode.Space))
        {
            animController.SetTrigger("Attack");
        }
    }
}
