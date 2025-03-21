using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneControl : MonoBehaviour
{
   [SerializeField]private string sceneBuildIndex;
   private void OnTriggerEnter2D(Collider2D other)
   {
        if(other.tag == "Player")
        {
            SceneManager.LoadScene(sceneBuildIndex);
        }
   }
}
