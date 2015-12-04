using UnityEngine;
using System.Collections;

public class Attack : State
{

    float attackTime = 5;

    HullOnline targetShip;
    HullOnline collidedHull;
    protected override void execute()
    {
        target = targetPlayer.transform.position;
        animate();

        if ((transform.position - targetPlayer.transform.position).magnitude >= 30f)
        {
            forceBack();
        }
        if ((transform.position - targetPlayer.transform.position).magnitude >= attackRadius)
        {
            targetPlayer = null;
            switchState(this, states[0]);
        }
    }
    void OnEnable()
    {
        speed = 10;
        attackTime = 5;
    }
    void OnCollisionEnter(Collision col)
    {
        if (targetPlayer != null)
        {
            targetShip = targetPlayer.GetComponent<HullOnline>();

            if (col.transform.GetComponent<HullOnline>() != null)
            {
                collidedHull = col.transform.GetComponent<HullOnline>();
            }
            if (collidedHull && targetShip == collidedHull)
            {
                //print("Colliding With Ship!!!");
                targetShip.Damage(targetPlayer.transform.position, targetShip.CurrentHealth, 5);
                if (targetShip.CurrentHealth <= 0)
                {
                    targetPlayer = null;
                    switchState(this, states[0]);
                    //print("Switching State to: WANDER");
                }
            }
        }
    }
    void attackAnimation()
    {
        swimTime = 0;
        attackTime += Time.deltaTime;
        if (attackTime >= 4f)
        {
            anim.Play("breathing");
            attackTime = 0;
            dir = (target - transform.position);
            changeDirection();
        }
    }

}
