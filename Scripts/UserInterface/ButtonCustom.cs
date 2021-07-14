using System;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace UnityEngine.UI
{
    public class ButtonCustom : Selectable, IPointerClickHandler, ISubmitHandler
    {
        private enum AnimationType { Type1, Type2, Type3 };
        [Serializable] public class ButtonCustomEvent : UnityEvent { }

        [SerializeField] private Animator m_animator;
        [SerializeField] private AnimationType m_animationType;
        [SerializeField] private SceneController m_sceneController;
        [SerializeField] private ButtonCustomEvent m_onClick;

        protected ButtonCustom() { }

        protected override void Start()
        {
            base.Start();
            m_sceneController = FindObjectOfType<SceneController>();
        }
        public virtual void OnPointerClick(PointerEventData eventData)
        {
            if (eventData.button != PointerEventData.InputButton.Left) return;
            m_animator.SetBool("Pressed", true);
        }
        public virtual void OnSubmit(BaseEventData eventData)
        {
            if (!IsActive() || !IsInteractable()) return;
            m_animator.SetBool("Pressed", true);
        }
        public override void OnSelect(BaseEventData eventData)
        {
            base.OnSelect(eventData);
            m_animator.SetBool("Selected", true);
        }
        public override void OnDeselect(BaseEventData eventData)
        {
            base.OnDeselect(eventData);
            m_animator.SetBool("Selected", false);
            m_animator.SetBool("Pressed", false);
        }
        protected override void OnEnable()
        {
            m_animator.SetLayerWeight((int)m_animationType, 1f);
        }
        
        public void Press()
        {
            if (!IsActive() || !IsInteractable()) return;
            m_onClick.Invoke();
        }
        public void Switch(Menu next)
        {
            Menu root = transform.parent.GetComponent<Menu>();

            // Enable next menu and disable current menu
            next.gameObject.SetActive(true);
            root.gameObject.SetActive(false);
            // Set "isChange" of next menu to true 
            next.m_isChanged = true;
        }
        public void LoadStage(string stageName)
        {
            m_sceneController.SetState(new SceneStage(m_sceneController, stageName));
        }
        public void LoadMain()
        {
            m_sceneController.SetState(new SceneMain(m_sceneController));
        }
        public void Resume()
        {
            FindObjectOfType<Game>().isPaused = false;
        }
        public void Quit()
        {
            Application.Quit();
        }
    }
}

