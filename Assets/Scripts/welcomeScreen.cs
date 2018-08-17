using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class welcomeScreen : MonoBehaviour {

    public void AnimationEndCallBack() {
       UIController.GetInstance().Arrow.GetComponent<Animator>().Play("Arrow");
    }
}
