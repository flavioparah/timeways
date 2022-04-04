using UnityEngine;

public class CursorManager : MonoBehaviour
{
    [SerializeField] bool visible;
    [SerializeField] InputReader inputReader;
    [SerializeField] Animator animator;
    private Vector3 currentPosition;
    private Vector3 previousPosition;
    Camera cameraMain;

    private void OnEnable()
    {
        if (inputReader != null)
        {
            inputReader.mousePositionEvent += MousePosition;
            inputReader.attackEvent += ClickEvent;
        }
    }

    private void OnDisable()
    {
        if (inputReader != null)
        {
            inputReader.mousePositionEvent -= MousePosition;
            inputReader.attackEvent -= ClickEvent;
        }
    }

    private void Start()
    {
        animator = GetComponent<Animator>();
        cameraMain = Camera.main;
        Cursor.visible = false;
        currentPosition = Vector3.zero;
    }

    private void MousePosition(Vector2 position)
    {
        currentPosition = position;
    }


    void Update()
    {
        Cursor.visible = this.visible;
        CursorUpdate();
    }

    private void CursorUpdate()
    {
        if (cameraMain != null)
        {
            Vector3 mousePosition = cameraMain.ScreenToWorldPoint(new Vector3(currentPosition.x, currentPosition.y, 0));
            Vector3 pos = new Vector3(mousePosition.x, mousePosition.y, 0);
            transform.position = pos;
            Vector3 dir = (pos - previousPosition).normalized;
            previousPosition = pos;
        }
        else
        {
            cameraMain = Camera.main;
        }
    }

    private void ClickEvent()
    {
        if (animator != null)
        {
            animator.SetTrigger("Click");
        }
    }
}
