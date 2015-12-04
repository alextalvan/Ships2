using UnityEngine;
using System.Collections;

public class Wander : State
{

    void OnEnable()
    {
        speed = 10;
        coolDown = 30;
    }
    void searchForPlayer()
    {
        coolDown -= Time.fixedDeltaTime;
        foreach (CustomOnlinePlayer player in onlineRefs.allOnlinePlayers)
        {
            if ((transform.position - player.transform.position).magnitude <= attackRadius && coolDown <= 0)
            {
                targetPlayer = player;
                coolDown = 30;
                switchState(this, states[1]);
                //print("Switching State to: ATTACK");
            }
        }
    }

    protected override void execute()
    {
        searchForPlayer();
        //forward
        //Debug.DrawRay(transform.position, transform.forward * range, Color.white);
        if (Physics.Raycast(transform.position, transform.forward, out hit, range))
        {
            if (hit.transform != this.transform)
            {
                dir += hit.normal * repelForce;
                changeDirection();
            }
        }
        //left
        //Debug.DrawRay(transform.position, -transform.right * range, Color.red);
        if (Physics.Raycast(transform.position, -transform.right, out hit, range))
        {
            if (hit.transform != this.transform)
            {
                dir += hit.normal * repelForce;
                changeDirection();

            }
        }
        //right
        //Debug.DrawRay(transform.position, transform.right * range, Color.green);
        if (Physics.Raycast(transform.position, transform.right, out hit, range))
        {
            if (hit.transform != this.transform)
            {
                dir += hit.normal * repelForce;
                changeDirection();
            }
        }


        //Line of sight with target
        //Debug.DrawLine(transform.position, target, Color.red);
        if (Physics.Raycast(transform.position, target, out hit, 75))
        {
            if (hit.transform != transform)
            {
                //print("not clear");
                //print(hit.transform.name);
                searchTime += Time.fixedDeltaTime;
                if (searchTime >= 2f)
                {
                    generateTarget();
                }
            }
        }

        //down
        //Debug.DrawRay(transform.position, (transform.forward + -transform.up) * 5, Color.cyan);
        else if (Physics.Raycast(transform.position, (transform.forward + -transform.up).normalized, out hit, 5))
        {
            if (hit.transform != this.transform)
            {
                dir += hit.normal * repelForce;
                changeDirection();
                transform.position = new Vector3(transform.position.x, transform.position.y + 0.1f, transform.position.z);
            }
        }
        forceBack();
        //if target reached(almost)
        if ((target - transform.position).magnitude <= 10 || target == new Vector3(0, 0, 0))
        {
            generateTarget();
        }
        animate();
    }

    //void OnDrawGizmos()
    //{
    //    Gizmos.color = Color.red;
    //    Gizmos.DrawCube(transform.position, new Vector3(1, 1, 1));
    //    Gizmos.DrawCube(transform.position, new Vector3(1, 1, 1));
    //    Gizmos.DrawCube(transform.position, new Vector3(1, 1, 1));
    //    Gizmos.DrawCube(target, new Vector3(2, 2, 2));
    //}
}
