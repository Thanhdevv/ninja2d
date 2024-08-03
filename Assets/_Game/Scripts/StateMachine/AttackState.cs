using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : IsState
{
    float timer;
    float allowedYDifference = 1.0f; // Định nghĩa khoảng cách cho phép theo trục Y
    public void OnEnter(Enemy enemy)
    {
        timer = 0;
        if (enemy.Target != null)
        {
            // đổi hướng Enemy => Player
            enemy.ChangeDirection(enemy.Target.transform.position.x > enemy.transform.position.x);

            // Kiểm tra vị trí theo trục Y
            float targetYDiff = enemy.Target.transform.position.y - enemy.transform.position.y;
            if (targetYDiff > allowedYDifference) // allowedYDifference is a variable you define
            {
                enemy.SetTarget(null); // Stop targeting the player if they are too high
                
                return;
            }

            enemy.StopMoving();
            enemy.Attack();
        }
       
    }

    public void OnExecute(Enemy enemy)
    {
        timer += Time.deltaTime;
        if(timer >= 1.5f)
        {
            enemy.ChangeState(new PatrolState());
        }
    }

    public void OnExit(Enemy enemy)
    {
        
    }



}
