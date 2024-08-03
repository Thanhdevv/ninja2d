using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolState : IsState
{
    float randomTime;
    float timer;
    float allowedYDifference = 1.0f; // Định nghĩa khoảng cách cho phép theo trục Y
    public void OnEnter(Enemy enemy)
    {
        timer = 0;
        randomTime = Random.Range(3f, 6f);
    }

    public void OnExecute(Enemy enemy)
    {
        timer += Time.deltaTime;

        if (enemy.Target != null)
        {
            // Đổi hướng Enemy => Player
            enemy.ChangeDirection(enemy.Target.transform.position.x > enemy.transform.position.x);

            // Kiểm tra vị trí theo trục Y
            float targetYDiff = enemy.Target.transform.position.y - enemy.transform.position.y;
            if (targetYDiff > allowedYDifference) // allowedYDifference is a variable you define
            {
                enemy.SetTarget(null); // Stop targeting the player if they are too high
               
                return;
            }

            if (enemy.IsTargetInRange())
            {
                enemy.ChangeState(new AttackState());
            }
            else
            {
                enemy.Moving();
            }
 
        }
        else {

            if (timer < randomTime)
            {
                enemy.Moving();
            }
            else
            {
                enemy.ChangeState(new IdleState());
            }
        }

       
        
    }

    public void OnExit(Enemy enemy)
    {
        
    }

   
    
}
