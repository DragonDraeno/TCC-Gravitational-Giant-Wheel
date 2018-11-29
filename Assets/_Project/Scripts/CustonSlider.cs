using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CustonSlider : Slider {

    public virtual CanvasMainMenu canvas { get; set; }

    public override void OnPointerUp(PointerEventData eventData)
    {
        base.OnPointerUp(eventData);
        //CanvasMainMenu.handlleUp();
    }

}
