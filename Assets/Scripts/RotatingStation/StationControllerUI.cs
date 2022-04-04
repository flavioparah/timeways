using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StationControllerUI : MonoBehaviour
{

    
    [SerializeField] StationController controller;
    Animator anim;
    bool isOpen;

    List<bool> closedDoors = new List<bool>(); //portas que estao fechadas quando abriu a ui
    List<bool> newDoorTasks = new List<bool>();

    //Vector2 gravityDirectionOld;
    //Vector2 gravityDirectionNew;
    StationController.Task task;

    int doorNumber;
    bool doorClosed;

    private void Start()
    {
        anim = this.GetComponent<Animator>();
    }
    public void OpenUI()//, Vector2 gravityDirection)
    {
        List<Door> doors = controller.GetDoors();
        closedDoors.Clear();
        newDoorTasks.Clear();
        int index = 0;
        foreach(Door d in doors)
        {
            closedDoors.Add(d.IsClosed());
            newDoorTasks.Add(d.IsClosed());
        }

        isOpen = true;
        anim.SetTrigger("Open");
        //Time.timeScale = 0;
        task = new StationController.Task();
        task.direction = controller.GetGravityDirection();
    }

    public void CLoseUI()
    {
        isOpen = false;
        anim.SetTrigger("Close");
    }

   public void GravityUp()
    {
        task.direction = Vector2.up;
    }

    public void GravityLeft()
    {
        task.direction = Vector2.left;
    }

    public void GravityDown()
    {
        task.direction = Vector2.down;
    }

    public void GravityRight()
    {
        task.direction = Vector2.right;
    }
    public void GravityZero()
    {
        task.direction = Vector2.zero;
    }
    public void Perform()
    {
        controller.PerformTask(task);
    }

    public void SelectDoor(int number)
    {
        doorNumber = number;
        doorClosed = closedDoors[doorNumber];
    }

    public void ToggleDoor()
    {
        doorClosed = !doorClosed;
        newDoorTasks[doorNumber] = doorClosed;
    }

    public void Confirm()
    {
        int index = 0;
        foreach(bool b in closedDoors)
        {
            if(b != newDoorTasks[index])
            {
                task.doorsToToggle.Add(index);
            }
            index++;
        }

        CLoseUI();
    }
}
