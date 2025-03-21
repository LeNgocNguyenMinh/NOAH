using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseEnemyBoss : MonoBehaviour
{
   public EnemyStatus enemyData;

   public float DetectRange = 2f;
   
   private HealthControl playerHealthControl;
   private Transform player;

   private void Start()
   {
      player = FindObjectOfType<PlayerControl>().transform;
      playerHealthControl = player.GetComponent<HealthControl>();
   }

   private void Update()
   {
      Move();  
      DetectTarget();
   }

   public abstract void Move();

   public Collider2D DetectTarget()
   {
      Collider2D[] targets = Physics2D.OverlapCircleAll(transform.position, DetectRange);

      foreach (Collider2D target in targets)
      {
         if (target.gameObject.CompareTag("Player"))
         {
            return target;
         }
      }

      return null;
   }

   public virtual void Attack()
   {
      var target = DetectTarget();
      var health = target.gameObject.GetComponent<HealthControl>();
      health.PlayerHurt(enemyData.enemyDamage);
   }
}
