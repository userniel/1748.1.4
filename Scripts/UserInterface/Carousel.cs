using System;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace UnityEngine.UI
{
    public class Carousel : Selectable, IPointerClickHandler, ISubmitHandler, IEventSystemHandler, ICanvasElement
    {
        [Serializable] public class CarouselEvent : UnityEvent<int> { }

        [Tooltip("Text of current displayed option")] [SerializeField] private Text m_label;
        [Tooltip("Index of current displayed option")] public int m_index;
        [Tooltip("Array of all selectable options")] public string[] m_options;
        [SerializeField] private CarouselEvent m_onValueChanged = new CarouselEvent();

        private int m_indexConfirm = 0;

        protected Carousel() { }

        protected override void Start()
        {
            base.Start();
            m_label.text = m_options[m_index];
        }
        public virtual void Rebuild(CanvasUpdate executing)
        {
#if UNITY_EDITOR
            if (executing == CanvasUpdate.Prelayout) m_onValueChanged.Invoke(m_index);
#endif
        }
        public virtual void LayoutComplete() { }
        public virtual void GraphicUpdateComplete() { }
        public virtual void OnPointerClick(PointerEventData eventData)
        {
            if (eventData.button != PointerEventData.InputButton.Left) return;

            if (RectTransformUtility.ScreenPointToLocalPointInRectangle(gameObject.GetComponent<RectTransform>(), eventData.pointerPressRaycast.screenPosition, eventData.pressEventCamera, out Vector2 localMousePos))
            {
                if (localMousePos.x > 0) RotateRight();
                if (localMousePos.x < 0) RotateLeft();
            }
        }
        public virtual void OnSubmit(BaseEventData eventData)
        {
            if (m_index != m_indexConfirm)
            {
                m_onValueChanged.Invoke(m_index);
                m_indexConfirm = m_index;
            }
            else return;
        }
        public override void OnDeselect(BaseEventData eventData)
        {
            base.OnDeselect(eventData);
            if (m_index != m_indexConfirm)
            {
                m_onValueChanged.Invoke(m_index);
                m_indexConfirm = m_index;
            }
            else return;
        }
        public override void OnMove(AxisEventData eventData)
        {
            if (!IsActive() || !IsInteractable())
            {
                base.OnMove(eventData);
                return;
            }

            switch (eventData.moveDir)
            {
                case MoveDirection.Left:
                    if (FindSelectableOnLeft() == null) RotateLeft();
                    else base.OnMove(eventData);
                    break;
                case MoveDirection.Right:
                    if (FindSelectableOnRight() == null) RotateRight();
                    else base.OnMove(eventData);
                    break;
            }
        }
        public override Selectable FindSelectableOnLeft()
        {
            if (navigation.mode == Navigation.Mode.Automatic) return null;
            return base.FindSelectableOnLeft();
        }
        public override Selectable FindSelectableOnRight()
        {
            if (navigation.mode == Navigation.Mode.Automatic) return null;
            return base.FindSelectableOnRight();
        }

        private void RotateLeft()
        {
            if (!IsActive() || !IsInteractable()) return;

            m_index = (m_index > 0) ? m_index - 1 : m_options.Length - 1;
            m_label.text = m_options[m_index];
        }
        private void RotateRight()
        {
            if (!IsActive() || !IsInteractable()) return;

            m_index = (m_index < m_options.Length - 1) ? m_index + 1 : 0;
            m_label.text = m_options[m_index];
        }
    }
}
