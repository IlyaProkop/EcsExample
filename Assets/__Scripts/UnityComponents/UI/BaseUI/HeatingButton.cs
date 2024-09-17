using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class HeatingButton : ActionButton, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private ParticleSystem firePS;
    [SerializeField] private ParticleSystem smokePS;

    [HideInInspector] public UnityEvent<bool> OnChangePressState;

    private bool IsPressed;

    public void OnPointerDown(PointerEventData eventData)
    {
        IsPressed = true;

        //if (!firePS.isPlaying)
        //    firePS.Play();
        if (!smokePS.isPlaying)
            smokePS.Play();

        OnChangePressState.Invoke(IsPressed);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        IsPressed = false;

        //if (firePS.isPlaying)
        //    firePS.Stop();
        if (smokePS.isPlaying)
            smokePS.Stop();

        OnChangePressState.Invoke(IsPressed);
    }
}
