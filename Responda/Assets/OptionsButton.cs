using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionsButton : MonoBehaviour
{
    [SerializeField] Navegacao navegacao;
	[SerializeField] Animator animator;
	[SerializeField] int thisIndex;

    // Update is called once per frame
    void Update()
    {
        if(navegacao.index == thisIndex)
		{
			animator.SetBool ("selected", true);
			if(Input.GetAxis ("Submit") == 1){
				animator.SetBool ("pressed", true);
			}else if (animator.GetBool ("pressed")){
				animator.SetBool ("pressed", false);
			}
		}else{
			animator.SetBool ("selected", false);
		}
        
    }
}
