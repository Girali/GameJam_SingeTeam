using System.Collections;
using UnityEngine;



namespace Monkeys
{
    
    public class Animationrandomizer : MonoBehaviour
    {
        [SerializeField]
        private Animator anim;
        
        [SerializeField]
        private float MinDelay = 0;
        [SerializeField]
        private float MaxDelay = 1;

        [SerializeField] private string RandomTrigger ="Action";
        [SerializeField] private string RandomIndexName = "ActionIndex";
        [SerializeField] private int MaxIndex = 6;
    

    
        // Start is called before the first frame update
        void Start()
        {


            if (anim is null)
            {
                return;
            }

            StartCoroutine(Randomise());
        }

        IEnumerator Randomise()
        {
            while (true)
            {
                yield return new WaitForSeconds(Mathf.Max(0, Random.Range(MinDelay, MaxDelay)));
                
                anim.SetInteger(RandomIndexName ,Random.Range(0,MaxIndex + 1));
                anim.SetTrigger(RandomTrigger);
                
            }
        }
    }
}
