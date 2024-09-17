using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public abstract class UIElement : MonoBehaviour
{
    protected Animator animator;

    private const string IsShowBoolName = "IsShow";
    private const string IsHideStateName= "Hide";
    private const string IsShowStateName= "Show";

    private void OnEnable()
    {
        animator = GetComponent<Animator>();
    }

    public virtual void SetShowState(bool _isShow)
    {
        if (_isShow)
            Show();
        else
            Hide();
    }

    private void Show()
    {
        gameObject.SetActive(true);
        animator.SetBool(IsShowBoolName, true);
    }

    private void Hide()
    {
        animator.SetBool(IsShowBoolName, false);
        StartCoroutine(DisableDelay());
    }

    private IEnumerator DisableDelay()
    {
        bool closedStateReached = false;
        bool wantToClose = true;
        while (!closedStateReached && wantToClose)
        {
            if (!animator.IsInTransition(0))
                closedStateReached = animator.GetCurrentAnimatorStateInfo(0).IsName(IsHideStateName);

            wantToClose = !animator.GetBool(IsShowBoolName);

            yield return new WaitForEndOfFrame();
        }

        if (wantToClose)
            animator.gameObject.SetActive(false);
    }
}
