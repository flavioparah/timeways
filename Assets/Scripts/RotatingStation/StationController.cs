using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StationController : MonoBehaviour
{

    public class Task
    {
        public Vector2 direction;
        public List<int> doorsToToggle = new List<int>();

        public Task()
        {
            doorsToToggle = new List<int>();
        }

    }


    [SerializeField] InputReader InputReader;
    [SerializeField] float angularSpeed;
    [SerializeField] bool rotatingRight;
    [SerializeField] bool rotatingLeft;
    [SerializeField] List<Door> doors = new List<Door>();

    [SerializeField] StationControllerUI ui;


    Vector2 gravityDirection;

    private void OnEnable()
    {
        if (InputReader != null)
        {
            InputReader.moveEvent += Move;
            InputReader.moveEventCancel += MoveCancel;
            InputReader.interactEvent += Interact;
            InputReader.mousePositionEvent += MousePosition;
            InputReader.mouseDownEvent += Click;
            InputReader.mouseUpEvent += MouseUp;
            InputReader.pauseEvent += Pause;
            InputReader.changeCameraEvent += ChangeCamera;


        }
    }

    private void OnDisable()
    {

        if (InputReader != null)
        {
            InputReader.moveEvent -= Move;
            InputReader.moveEventCancel -= MoveCancel;

            InputReader.interactEvent -= Interact;
            InputReader.mousePositionEvent -= MousePosition;
            InputReader.mouseDownEvent -= Click;
            InputReader.mouseUpEvent -= MouseUp;
            InputReader.pauseEvent -= Pause;
            InputReader.changeCameraEvent -= ChangeCamera;
        }

    }


    public void PerformTask(Task task)
    {
        ChangeGravityDirection(task.direction);
        foreach (int i in task.doorsToToggle)
        {
            doors[i].ToggleDoor();
        }
    }
    private void Move(Vector2 move)
    {
        rotatingRight = move.x > 0;
        rotatingLeft = move.x < 0;
    }

    void MoveCancel()
    {
        rotatingRight = false;
        rotatingLeft = false;
    }

    void Interact()
    {

    }

    void Click()
    {

    }

    public void ToggleDoor(int doorIndex)
    {
        doorIndex = Mathf.Clamp(doorIndex, 0, 7);

        Door door = doors[doorIndex];
        door.ToggleDoor();
    }

    void MousePosition(Vector2 pos)
    {

    }

    void MouseUp()
    {

    }

    void ChangeCamera()
    {

    }

    public Vector2 GetGravityDirection()
    {
        return  gravityDirection;
    }
    public void ChangeGravityDirection(Vector2 dir)
    {

        gravityDirection = dir;

        Physics2D.gravity = gravityDirection * 9.81f;
    }
    private void Pause()
    {
        if (PauseMenu.Instance.menuOpened)
        {
            PauseMenu.Instance.CloseMenu();
            return;
        }
        PauseMenu.Instance.OpenMenu();
    }

  

    public List<Door> GetDoors()
    {
        return doors;
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        float direction = 0;

        if (rotatingRight)
        {
            direction = 1;
        }
        else if (rotatingLeft)
        {
            direction = -1;
        }
        else
        {
            direction = 0;
        }
        Vector3 rotation = new Vector3(0, 0, angularSpeed * direction * Time.deltaTime);
        transform.Rotate(rotation);
    }
}
