using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicAttackStateBehavior : StateMachineBehaviour
{
    public GameObject shotObject;
    GameObject shot;
    public GameObject otherObject;
    GameObject other;
    public Vector3 AdjustPoint;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Vector3 position = animator.transform.position;
        //クオータニオン用意
        Quaternion shotQuaternion = Quaternion.identity;
        //キャラクターの向き（スケールの正負）によって変える
         //方向の設定（向きによって回転させる）
       if (animator.transform.localScale.x > 0.0f)
        {
            shotQuaternion = Quaternion.Euler(0.0f, 0.0f, 180.0f);
            position.x -= AdjustPoint.x;
        }
        else
        {
            shotQuaternion = Quaternion.Euler(0.0f, 0.0f, 0.0f);
            position.x += AdjustPoint.x;

        }
        //Debug.Log("**" + animator.name);
        //Debug.Log("**" + animator.transform.localScale.normalized);

        position.y += AdjustPoint.y;
        //position.z= 0.0f;


        shot = Instantiate(shotObject, position, shotQuaternion);
        other = Instantiate(otherObject, position, otherObject.transform.rotation);
    }
    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    //override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
