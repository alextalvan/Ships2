using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;


[RequireComponent(typeof(OnlineTransform))]
public abstract class State : NetworkBehaviour
{
    [SerializeField]
    protected Animator anim;
    [SerializeField]
    protected State[] states;
    [SerializeField]
    protected Transform patrolArea;

    protected RaycastHit hit;
    [SerializeField]
    protected float speed = 10f;
    protected float searchTime = 0;
    protected float range = 15f;
    protected float repelForce = 20f;
    protected float swimTime = 0;
    protected float coolDown = 30f;
    protected float maxY = -4.5f;
    protected float attackRadius = 50f;

    [SerializeField]
    protected OnlineSceneReferences onlineRefs;

    protected CustomOnlinePlayer targetPlayer;
    protected Vector3 target;
    protected Vector3 dirFull;
    protected Vector3 dir;

    // Use this for initialization
    protected void Awake()
    {
        states = GetComponents<State>();
        onlineRefs = GameObject.Find("OnlineSceneReferences").GetComponent<OnlineSceneReferences>();
        patrolArea = GameObject.Find("WhaleBox").transform;
		onlineRefs.whale = this.gameObject;
    }
    protected void setPlayerTarget(CustomOnlinePlayer newTarget)
    {
        target = newTarget.transform.position;
        targetPlayer = newTarget;
    }
    protected void generateTarget()
    {
        Vector3 rndPosWithinPatrolArea;
        rndPosWithinPatrolArea = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), Random.Range(-1f, 1f));
        rndPosWithinPatrolArea = patrolArea.TransformPoint(rndPosWithinPatrolArea * 0.5f);

        //print("New Target Aquired! : " + (target - transform.position).magnitude);

        Vector3 newTarget = rndPosWithinPatrolArea;

        //print("new target is: " + newTarget);
        target = newTarget;
        changeDirection();
        searchTime = 0;
    }
    protected void forceBack()
    {
        //brute force back in water in case of undesired collsions
        if (transform.position.y > maxY)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y - 0.1f, transform.position.z);
            if (transform.position.y > -2) transform.position = new Vector3(transform.position.x, transform.position.y - 0.5f, transform.position.z);
            dir = (target - transform.position).normalized;
            changeDirection();
        }
    }
    protected void changeDirection()
    {
        Quaternion rot = Quaternion.LookRotation(dir);
        transform.rotation = Quaternion.Slerp(transform.rotation, rot, Time.deltaTime);
        dir = (target - transform.position).normalized;
    }
    protected void switchState(State from, State to)
    {
        //does not take care of self! ...BAD
        if (targetPlayer != null)
        {
            to.setPlayerTarget(targetPlayer);
        }
        from.enabled = false;
        to.enabled = true;
    }

    protected virtual void animate()
    {
        swimTime += Time.deltaTime;
        if (swimTime >= 1.25f)
        {
            anim.PlayInFixedTime("Swim");
            swimTime = 0;
        }
    }
    protected virtual void execute()
    {

    }
    protected void FixedUpdate()
    {
        execute();

        //movement
        transform.position += transform.forward * (speed * Time.deltaTime);

        dir = (target - transform.position);
        changeDirection();
    }
}
