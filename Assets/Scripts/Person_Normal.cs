using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Person_Normal : MonoBehaviour
{

    public List<Transform> markers;
    public float walkSpeed;


    public List<string> Dialogues;

    [HideInInspector] public Rigidbody rb;


    //State Variables;
    State_Person_Normal currentState;
    public State_Person_Normal idleState  = new IdleState_Person_Normal();
    public State_Person_Normal walkState = new WalkState_Person_Normal();
    public State_Person_Normal talkState = new TalkingState_Person_Normal();

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();

        idleState.InitiateState(this);
        walkState.InitiateState(this);
        talkState.InitiateState(this);

        currentState = idleState;
        currentState.EnterState(this);

    }

    // Update is called once per frame
    void Update()
    {
        currentState.FrameUpdate(this);
    }
    private void FixedUpdate()
    {
        currentState.PhysicsUpdate(this);
    }

    public void ChangeState(State_Person_Normal targetState)
    {
        //This function is for Changing States
        currentState.ExitState(this);

        currentState = targetState;

        currentState.EnterState(this);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out PlayerInteraction player))
        {   
            player.ChangePersonInContact(this);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out PlayerInteraction player))
        {
            player.RemoveContactPerson();
        }
    }

    public void OnPlayerInteract(PlayerInteraction player)
    {
        //Call the Dialoue Manager and pass the Dialogues
        ChangeState(talkState);
    }
}

public class State_Person_Normal
{
    virtual public void InitiateState(Person_Normal root) { }
    virtual public void EnterState(Person_Normal root) { }
    virtual public void PhysicsUpdate(Person_Normal root) { }
    virtual public void FrameUpdate(Person_Normal root) { }
    virtual public void ExitState(Person_Normal root) { }
}
public class WalkState_Person_Normal : State_Person_Normal
{
    int currentMarkerIndex;
    Vector3 moveDir;
    float walkSpeed;
    int targetMarkerIndex;

    bool canMove = true;
    override public void InitiateState(Person_Normal root)
    {
        currentMarkerIndex = 0;
    }
    override public void EnterState(Person_Normal root)
    {
        //Change target marker

        if (currentMarkerIndex + 1 < root.markers.Count)
        {
            targetMarkerIndex = currentMarkerIndex + 1;
        }
        else
        {
            targetMarkerIndex = 0;
        }

        

        //Get the Direction of move
        moveDir = root.markers[targetMarkerIndex].position - root.transform.position;
        moveDir.Normalize();

        Debug.Log(targetMarkerIndex + " ||||  " + root.markers[targetMarkerIndex].position);

        //Set the walk speed
        walkSpeed = root.walkSpeed;

        canMove = true;
    }
    override public void FrameUpdate(Person_Normal root)
    {
        //Check if the given position is reached
        if(Mathf.Abs((root.transform.position - root.markers[targetMarkerIndex].position).magnitude) < 1f && canMove)
        {
            //Change State to Idle State
            root.ChangeState(root.idleState);


            //Change current Marker
            currentMarkerIndex++;

            root.rb.velocity = Vector3.zero;

            canMove = false;
        }
        /*Debug.Log(Mathf.Abs((root.transform.position - root.markers[targetMarkerIndex].position).magnitude));*/
    }
    override public void PhysicsUpdate(Person_Normal root)
    {
        if(canMove)
        {
            //Move the player in the said location
            root.rb.AddForce(moveDir.normalized * walkSpeed, ForceMode.Force);
        }
    }
    override public void ExitState(Person_Normal root)
    {
        //Debug.Log("AYOOOO");
    }
}
public class IdleState_Person_Normal : State_Person_Normal
{
    private float WAIT_TIME = 1f;

    private float _tempTime;

    bool startTimer;
    public override void EnterState(Person_Normal root)
    {
        _tempTime = 0;
        //Enter Idle State then wait for some seconds to start moving
        base.EnterState(root);

        startTimer = true;
    }

    public override void ExitState(Person_Normal root)
    {
        base.ExitState(root);
        //Debug.Log("Exited Idle State");
        startTimer = false;
    }
    public override void FrameUpdate(Person_Normal root)
    {
        Debug.Log(startTimer);
        if (!startTimer) return;

        _tempTime += Time.deltaTime;

        if(_tempTime > WAIT_TIME)
        {
            //Change to Walk State
            _tempTime = 0;
            root.ChangeState(root.walkState);
        }
        base.FrameUpdate(root);
    }

    public override void InitiateState(Person_Normal root)
    {
        base.InitiateState(root);
    }

    public override void PhysicsUpdate(Person_Normal root)
    {
        base.PhysicsUpdate(root);
    }
}
public class TalkingState_Person_Normal : State_Person_Normal
{
    public override void EnterState(Person_Normal root)
    {
        base.EnterState(root);
    }

    public override void ExitState(Person_Normal root)
    {
        base.ExitState(root);
    }

    public override void FrameUpdate(Person_Normal root)
    {
        base.FrameUpdate(root);
    }

    public override void PhysicsUpdate(Person_Normal root)
    {
        base.PhysicsUpdate(root);
    }
}