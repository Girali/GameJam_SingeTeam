using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;

public class MonkController : MonoBehaviour
{
    // Parameters
    [SerializeField] private Vector3 MonkStartPos = Vector3.zero;
    [SerializeField] private Vector3 MonkStartRot = Vector3.zero;
    [SerializeField] private Vector3 MonkWindowPos = Vector3.zero;

    [SerializeField] private UnityEvent OnEnterInCarBegin;
    [SerializeField] private UnityEvent OnEnterInCarMiddle;
    [SerializeField] private UnityEvent OnEnterInCarEnd;
    
    [SerializeField] private Shotable Glass;
    // Members

    private Transform monkOrigin;
    private bool isCarEntered = false;
    private Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        monkOrigin = transform.GetChild(0);
        anim = monkOrigin.GetChild(0).GetComponent<Animator>();

        monkOrigin.localPosition = new Vector3(0, -1000, 0);
        monkOrigin.localRotation = Quaternion.Euler(MonkStartRot);
    }
    public void EnterInCar()
    {
        if (isCarEntered)
            return;

        isCarEntered = true;
        
        monkOrigin.localPosition = MonkStartPos;
        anim.SetTrigger("Enter");
        
        OnEnterInCarBegin.Invoke();
        monkOrigin.DOLocalMove(MonkWindowPos, 1.0f).SetEase(Ease.OutCirc).OnComplete(() => {
            if (Glass != null)
            {
                Glass.Shoted(Glass.transform.position,
                    MonkWindowPos.x > 0 ? new Vector3(-1, 0, 0) : new Vector3(1, 0, 0));
            }
            OnEnterInCarMiddle.Invoke();
            
            monkOrigin.DOLocalMove(new Vector3(0f,0f,0f), 0.75f).SetEase(Ease.InSine);
            monkOrigin.DOLocalRotate(new Vector3(0f,0f,0f), 0.75f).SetEase(Ease.InSine).OnComplete(() =>
            {
                OnEnterInCarEnd.Invoke();
            });
        });
    }
}
