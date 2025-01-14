using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DragUI : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    private bool isDragging = false;
    private Vector3 offset;
    private Camera mainCamera;

    public bool allowDragX = true;
    public bool allowDragY = true;

    private Button button;
    private Vector2 pointerDownPosition;
    private float dragThreshold = 1f;
    private bool canClick;

    public ActionType actionType; 
    private void Start()
    {
        mainCamera = Camera.main;
        button = GetComponent<Button>();

        if (button != null)
        {
            button.onClick.RemoveAllListeners();
        }
        Vector3 buttonScreenPosition = Vector3.zero;

        switch (actionType)
        {
            case ActionType.ShowHint:
                buttonScreenPosition = UIManager.Instance.ingameUI.allButtons[0].transform.position;
                break;
            case ActionType.Restart:
                buttonScreenPosition = UIManager.Instance.ingameUI.allButtons[1].transform.position;
                break;
            case ActionType.Pause:
                buttonScreenPosition = UIManager.Instance.ingameUI.allButtons[2].transform.position;
                break;
            case ActionType.MoveLeft:
                buttonScreenPosition = UIManager.Instance.ingameUI.playerControll[0].transform.position;
                break;
            case ActionType.MoveRight:
                buttonScreenPosition = UIManager.Instance.ingameUI.playerControll[1].transform.position;
                break;
            case ActionType.Jump:
                buttonScreenPosition = UIManager.Instance.ingameUI.playerControll[2].transform.position;
                break;
        }

        Vector3 worldPosition = mainCamera.ScreenToWorldPoint(buttonScreenPosition);
        worldPosition.z = 0;

        transform.position = worldPosition;
        //StartCoroutine(IEInit());
    }

    IEnumerator IEInit()
    {
        yield return new WaitForSeconds(1f);
        Vector3 buttonScreenPosition = Vector3.zero;

        switch (actionType)
        {
            case ActionType.ShowHint:
                buttonScreenPosition = UIManager.Instance.ingameUI.allButtons[0].transform.position;
                break;
            case ActionType.Restart:
                buttonScreenPosition = UIManager.Instance.ingameUI.allButtons[1].transform.position;
                break;
            case ActionType.Pause:
                buttonScreenPosition = UIManager.Instance.ingameUI.allButtons[2].transform.position;
                break;
            case ActionType.MoveLeft:
                buttonScreenPosition = UIManager.Instance.ingameUI.playerControll[0].transform.position;
                break;
            case ActionType.MoveRight:
                buttonScreenPosition = UIManager.Instance.ingameUI.playerControll[1].transform.position;
                break;
            case ActionType.Jump:
                buttonScreenPosition = UIManager.Instance.ingameUI.playerControll[2].transform.position;
                break;
        }

        Vector3 worldPosition = mainCamera.ScreenToWorldPoint(buttonScreenPosition);
        worldPosition.z = 0;
        
        transform.position = worldPosition;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (mainCamera == null) return;

        pointerDownPosition = eventData.position;
        canClick = true;

        Vector3 mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        offset = transform.position - new Vector3(mousePosition.x, mousePosition.y, transform.position.z);
        isDragging = true;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (Vector2.Distance(pointerDownPosition, eventData.position) > dragThreshold)
        {
            canClick = false;
            //Debug.Log("drag");
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (canClick)
        {
            HandleAction();
            //Debug.Log("click");
        }
        else
        {
            //Debug.Log("drag end");
        }

        isDragging = false;
    }

    private void LateUpdate()
    {
        if (isDragging && mainCamera != null)
        {
            Vector3 mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
            float newX = allowDragX ? mousePosition.x + offset.x : transform.position.x;
            float newY = allowDragY ? mousePosition.y + offset.y : transform.position.y;
            transform.position = new Vector3(newX, newY, transform.position.z);
        }
    }

    private void HandleAction()
    {
        switch (actionType)
        {
            case ActionType.Pause:
                UIManager.Instance.ingameUI.Pause();
                Debug.Log("Game Paused");
                break;
            case ActionType.Restart:
                UIManager.Instance.ingameUI.Restart();
                Debug.Log("Game Restarted");
                break;
            case ActionType.ShowHint:
                UIManager.Instance.ingameUI.ShowHint();
                Debug.Log("Hint Shown");
                break;
        }
    }
    public enum ActionType
    {
        Pause,
        Restart,
        ShowHint,
        MoveLeft,
        MoveRight,
        Jump
    }
}
