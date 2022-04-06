using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;
using UnityEngine.Playables;
using System;
using UnityEngine.Events;

public class Player : MonoBehaviour
{

    public static event UnityAction padClosed;

    [SerializeField] InputReader InputReader;
    [SerializeField] Pad pad;
    [SerializeField] float speed;
    [SerializeField] float angularSpeed;
    [SerializeField] PathCreator path;
    [SerializeField] float distance;
    [SerializeField] bool gravityOn;
    [SerializeField] PlayableDirector EndStationCutscene;
    [SerializeField] JohnAnimation anim;
    bool changingSection;
    Quaternion orientation;
    Vector3 direction;

    [SerializeField] List<Section> sections = new List<Section>(8);
    int sectionIndex = 0;
    Section actualSection;
    Transform nextPos;

    //  SpriteRenderer spriteRenderer;

    bool moving;
    float move;
    bool padAccess;
    bool exitHatchButton;

    //-------- Space --------------
    bool clicking;
    Wreckage wreckage;

    Transform cursorImage;
    [SerializeField] float throwForce;
    Vector2 mousePosition;

    Rigidbody2D rb;

    [SerializeField] GameObject aimImage;
    [SerializeField] float aimDistance;
    Transform arm;
    Transform hand;
    bool throwing;
    Quaternion rotateArmTo;
    float halfThrowRotate;

    Vector2 nextPosition;
    Vector2 discreteDirection;
    [SerializeField] float aimSpeed;

    NPC npcNearby;
    Door door;
    bool movingRight = true;
    bool returning = false;
    [SerializeField] float returnDistance;
    float doorDistance;
    float distanceTarget;

    NPCTexts mouth;

    bool connectedPad;
    Panel panel;
    bool padInteracting;
    Ulises ulises;
    bool interacting;
    //----- SpaceToCampus -------
    bool insideMask;

    //-----------Inception-----------
    bool inceptioning;
    [SerializeField] Transform startPos;
    [SerializeField] Transform enterPos;
    [SerializeField] Transform framePos;
    [SerializeField] float inceptionSpeed;
    [SerializeField] Animator frameAnim;


    //JohnInTheUI
    [SerializeField] bool inTheUI;
    [SerializeField] bool confused;
    bool timelinePlayed;
    bool buttonTrigger;
    bool zoomingOut;

    bool cantMove;
    // Start is called before the first frame update
    void Start()
    {
        orientation = transform.rotation;
        anim = this.transform.GetChild(0).GetComponent<JohnAnimation>();
        // spriteRenderer = this.GetComponent<SpriteRenderer>();
        actualSection = sections[sectionIndex];
        rb = this.GetComponent<Rigidbody2D>();

        Transform UI = this.transform.Find("UI");
        if (UI != null)
            mouth = UI.GetComponent<NPCTexts>();
        if (gravityOn && !inTheUI)
            transform.position = path.path.GetPointAtDistance(distance);
        else
        {
            rb.AddForce(Vector2.right * throwForce);
            arm = this.transform.GetChild(0);
            hand = arm.transform.GetChild(0);
        }

        if (inTheUI)
        {
            anim.Fall();
        }

        if (confused)
        {
            mouth.Talk("What...? What is this? ");
        }
    }

    private void OnEnable()
    {
        if (InputReader != null)
        {
            InputReader.moveEvent += Move;
            InputReader.moveEventCancel += MoveCancel;
            InputReader.interactEvent += Interact;
            InputReader.interactPadEvent += InteractPad;
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
            InputReader.interactPadEvent -= InteractPad;
            InputReader.mousePositionEvent -= MousePosition;
            InputReader.mouseDownEvent -= Click;
            InputReader.mouseUpEvent -= MouseUp;
            InputReader.pauseEvent -= Pause;
            InputReader.changeCameraEvent -= ChangeCamera;
        }

    }
    // Update is called once per frame
    void Update()
    {
        if (inceptioning) return;
        if (padInteracting) return;

        if (!gravityOn)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, orientation, angularSpeed * Time.deltaTime);
            if (wreckage != null)
            {
                if (throwing)
                {
                    if (throwing)
                    {
                        arm.transform.rotation = Quaternion.Lerp(arm.transform.rotation, rotateArmTo, angularSpeed * Time.deltaTime);
                        if (Quaternion.Angle(arm.transform.rotation, rotateArmTo) <= halfThrowRotate)
                        {
                            ThrowWreckage();
                            throwing = false;
                        }
                    }
                    return;
                }
                // Vector2 cursorPos = Camera.main.ScreenToWorldPoint(mousePosition);
                Vector2 pos = GetDiscreteDirection() * aimDistance;// (cursorPos - (Vector2)transform.position).normalized * aimDistance;
                aimImage.transform.position = Vector2.Lerp(aimImage.transform.position, (Vector2)transform.position + pos, aimSpeed * Time.deltaTime);
                Debug.DrawLine(transform.position, (Vector2)transform.position + pos, Color.blue, 5);
                arm.transform.rotation = Quaternion.Lerp(arm.transform.rotation,
                    Quaternion.LookRotation(Vector3.forward, aimImage.transform.position - transform.position),
                    angularSpeed * Time.deltaTime);

                wreckage.transform.position = hand.transform.position;
            }


            return;
        }

        if (returning)
        {
            if (Mathf.Abs(distance - distanceTarget) < 0.05f)
            {
                returning = false;
                MoveCancel();
            }
            distance = distance + (movingRight ? 1 : -1) * speed * Time.deltaTime;
            transform.position = path.path.GetPointAtDistance(distance);
            return;
        }
        if (moving)
        {



            distance += move * speed * Time.deltaTime;
            // transform.Translate(transform.right * -speed * Time.deltaTime, Space.Self);
        }

        if (!moving) return;

        //{
        //    if (!timelinePlayed) return;
        //    transform.rotation = Quaternion.LookRotation(Vector3.forward, path.path.GetNormalAtDistance(distance));
        //    transform.position = path.path.GetPointAtDistance(distance);
        //    return;
        //}

        transform.rotation = Quaternion.LookRotation(Vector3.forward, path.path.GetNormalAtDistance(distance));
        transform.position = path.path.GetPointAtDistance(distance);

    }

    private void Move(Vector2 move)
    {
        if (cantMove) return;
        if (inTheUI) return;
        if (padInteracting) return;
        if (interacting) return;
        if (zoomingOut) return;
        if (PauseMenu.Instance.menuOpened) return;
        moving = true;
        this.move = move.x > 0 ? 1 : -1;

        movingRight = move.x > 0;

        bool turned = anim.TurnAround(move.x > 0);

        // spriteRenderer.flipX = move.x < 0;
        anim.StartWalk();
    }

    void MoveCancel()
    {
        if (returning) return;
        anim.StopWalk();
        moving = false;
    }

    void InteractPad()
    {
        if (cantMove) return;
        #region In The UI Scene
        if (inTheUI && !timelinePlayed)
        {
            PlayableDirector timeline = GameObject.Find("Timeline").GetComponent<PlayableDirector>();
            if (timeline.state != PlayState.Playing)
            {
                timeline.Play();
                timeline.stopped += TimelinePlayed;
            }

            return;
        }
        #endregion

        if (!padAccess) return;
        if (padInteracting)
        {
            pad.ClosePadScreen();
            return;
        }

        if (interacting)
            OpenPad();
        else
            anim.ActivePad();
        padInteracting = true;

    }


    public void OpenPad()
    {
        pad.OpenPad(panel);
    }

    public void Interact()
    {
       // if (cantMove) return;

        if (exitHatchButton)
        {
            anim.StartInteract(Enums.Interaction.ExitHatch);

        }


        if (buttonTrigger)
        {
            zoomingOut = true;
            anim.StartInteract(Enums.Interaction.zoomOutButton);
        }

        if (confused) return;
        if (ulises != null)
        {
            if (interacting)
            {
                //  ulises.StopTalk();

                return;
            }
            MoveCancel();
            anim.StartInteract(Enums.Interaction.Ulises);


            return;
        }
        if (npcNearby != null)
        {
            npcNearby.Interact();
        }


    }

    public void CloseUlises()
    {
        anim.StopInteract();
        anim.ShowPlayer();
        interacting = false;
    }
    public void UlisesInteraction()
    {
        ulises.Show();

        anim.HidePlayer();
        interacting = true;
    }
    public void ClosePad()
    {
        if (interacting)
        {
            StopPadInteraction();
            return;
        }

        anim.DeactivatePad();
    }

    public void StopPadInteraction()
    {
        padInteracting = false;
        padClosed?.Invoke();
    }
    //void OpenPadCanvas()
    //{

    //}
    private void Pause()
    {
        if (PauseMenu.Instance.menuOpened)
        {
            PauseMenu.Instance.CloseMenu();
            return;
        }
        PauseMenu.Instance.OpenMenu();
    }

    void Click()
    {
        clicking = true;
    }
    private void MouseUp()
    {
        clicking = false;
        if (wreckage != null)
        {
            rotateArmTo = Quaternion.LookRotation(Vector3.forward, transform.position - aimImage.transform.position);
            halfThrowRotate = Quaternion.Angle(arm.transform.rotation, rotateArmTo) / 2;
            throwing = true;//ThrowWreckage();
        }
    }
    void RightClick()
    {

    }

    void ThrowWreckage()
    {

        discreteDirection = GetDiscreteDirection();
        Wreckage wreckageAux = wreckage;
        wreckage = null;
        wreckageAux.Throw(discreteDirection.normalized, throwForce);
        rb.velocity = Vector2.zero;
        rb.AddForce((discreteDirection * -1).normalized * throwForce);

        orientation = Quaternion.LookRotation(Vector3.forward, discreteDirection * -1);

        aimImage.SetActive(false);
    }

    Vector2 GetDiscreteDirection()
    {
        Vector2 cursorPosition = Camera.main.ScreenToWorldPoint(mousePosition);
        Vector2 direction = cursorPosition - (Vector2)transform.position;
        Vector2 discreteDirection = Vector2.up;

        if (Vector2.Angle(direction, Vector2.down) <= 90 && Vector2.Angle(direction, Vector2.right) <= 90)
        {
            Vector3 projection1 = Vector3.Project(direction, Vector2.right);
            Vector3 projection2 = Vector3.Project(direction, Vector2.down);

            if (projection1.magnitude >= projection2.magnitude)
                discreteDirection = Vector3.right;
            else discreteDirection = Vector2.down;
        }
        else if (Vector2.Angle(direction, Vector2.right) <= 90 && Vector2.Angle(direction, Vector2.up) <= 90)
        {
            Vector3 projection1 = Vector3.Project(direction, Vector2.right);
            Vector3 projection2 = Vector3.Project(direction, Vector2.up);

            if (projection1.magnitude >= projection2.magnitude)
                discreteDirection = Vector3.right;
            else discreteDirection = Vector2.up;
        }
        else if (Vector2.Angle(direction, Vector2.left) <= 90 && Vector2.Angle(direction, Vector2.up) <= 90)
        {
            Vector3 projection1 = Vector3.Project(direction, Vector2.left);
            Vector3 projection2 = Vector3.Project(direction, Vector2.up);

            if (projection1.magnitude >= projection2.magnitude)
                discreteDirection = Vector3.left;
            else discreteDirection = Vector2.up;
        }
        else if (Vector2.Angle(direction, Vector2.left) <= 90 && Vector2.Angle(direction, Vector2.down) <= 90)
        {
            Vector3 projection1 = Vector3.Project(direction, Vector2.left);
            Vector3 projection2 = Vector3.Project(direction, Vector2.down);

            if (projection1.magnitude >= projection2.magnitude)
                discreteDirection = Vector3.left;
            else discreteDirection = Vector2.down;
        }
        return discreteDirection;

    }
    private void ChangeCamera()
    {
        CameraManager.Instance.ChangeCamera();
    }

    private void StopCamera()
    {
        CameraManager.Instance.StopCamera();
    }

    void MousePosition(Vector2 mousePosition)
    {
        this.mousePosition = mousePosition;
    }

    void GetWreckage(Wreckage w)
    {
        wreckage = w;
        wreckage.Get();
        aimImage.SetActive(true);
    }

    void EndPart()
    {
        if (wreckage == null)
        {
            Debug.Log("GAME OVER - no wreckage");
            return;
        }

        SpaceManager.Instance.NextPart();
        // transform.position = point.position;
    }
    private void OnTriggerStay2D(Collider2D collision)
    {

        //if (collision.tag == "Wreckage")
        //{
        //    if (clicking && wreckage == null)
        //    {
        //        GetWreckage(collision.GetComponent<Wreckage>());

        //    }
        //}
        //if (collision.tag == "Octant")
        //{
        //    // PhysicsManager.Instance.SetGravity(collision.transform);
        //    direction = collision.transform.up;
        //    orientation = Quaternion.LookRotation(Vector3.forward, direction);
        //}
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.tag == "NPC")
        {
            npcNearby = collision.gameObject.GetComponent<NPC>();
        }

        if (collision.tag == "Wreckage")
        {
            if (wreckage == null)
            {
                GetWreckage(collision.GetComponent<Wreckage>());

            }
        }
        if (collision.tag == "LastWreckage")
        {
            if (wreckage == null)
            {
                GetWreckage(collision.GetComponent<Wreckage>());
                UIManager.Instance.ActivateIndicator();
            }
        }

        if (collision.tag == "FastWreckage")
        {
            rb.velocity = Vector2.zero;
            StopCamera();
            this.transform.SetParent(collision.transform);


        }
        if (collision.tag == "WreckageTutorial")
        {
            SpaceManager.Instance.OpenTutorial();
        }

        if (collision.tag == "EndPart")
        {
            EndPart();
        }
        if (collision.tag == "Respawner")
        {
            collision.gameObject.SetActive(false);
        }

        if (collision.tag == "Station")
        {
            Debug.Log("Win");
        }

        if (collision.tag == "Inception")
        {
            Inception();
        }

        if (collision.tag == "ChangeCamera")
        {
            ChangeCameraInception();
        }

        if (collision.tag == "Door")
        {
            door = collision.GetComponent<Door>();

        }
        if (collision.tag == "DoorCollider")
        {
            DoorClosed();

        }

        if (collision.tag == "ZoomOut")
        {
            buttonTrigger = true;
        }
        if (collision.tag == "Section")
        {
            actualSection = collision.GetComponent<Section>();
            if (actualSection.name == "SectionA4" && GameManager.Instance.GetSceneName() == "Station2")
            {
                CameraManager.Instance.ChangeCamera();
            }
        }
        if (collision.tag == "ExitHatch")
        {
            exitHatchButton = true;
        }


    }


    public void OpenHatch()
    {
        EndStationCutscene.Play();
        EndStationCutscene.stopped += EndLevel;
        // actualSection.GetComponent<ExitHatch>().OpenHatch();
    }

    void EndLevel(PlayableDirector director)
    {
        EndStationCutscene.stopped -= EndLevel;
        GameManager.Instance.EndLevel();
    }
    private void OnTriggerExit2D(Collider2D collision)
    {

        if (collision.tag == "Debris")
        {
            Debug.Log("GameOver - left debris");
        }

        if (collision.tag == "NPC")
        {
            npcNearby = null;
        }

        if (collision.tag == "Door")
        {
            door = null;

        }

        if (collision.tag == "ZoomOut")
        {
            buttonTrigger = false;
        }

        if (collision.tag == "ExitHatch")
        {
            exitHatchButton = false;
        }

        //if(collision.tag == "Section")
        //{
        //    Debug.Log("saiu");
        //    ChangeSection();
        //}
    }

    void DoorClosed()
    {
        returning = true;
        doorDistance = distance;
        distanceTarget = doorDistance + (movingRight ? -returnDistance : returnDistance);
        movingRight = !movingRight;
        bool turned = anim.TurnAround(movingRight);
        if (mouth != null)
        {
            mouth.Talk("The door is closed...");
        }
        if (door != null)
        {
            door.Blink();
        }
    }


    void Inception()
    {
        inceptioning = true;
        anim.Inception();
        StartCoroutine(GettingSucked());
    }

    void ChangeCameraInception()
    {
        InceptionCamera.Instance.SetCamera2();
    }

    IEnumerator GettingSucked()
    {
        frameAnim.enabled = false;
        InceptionCamera.Instance.SetCamera3();
        Vector2 start = transform.position;
        Vector2 final = framePos.position;
        Vector2 startScale = transform.localScale;
        Vector2 finalScale = new Vector2(.075f, .075f);
        float factor = 0;

        while (factor < 1)
        {
            factor += inceptionSpeed * Time.deltaTime;

            transform.position = Vector2.Lerp(start, framePos.position, factor);
            transform.localScale = Vector2.Lerp(startScale, finalScale, factor);

            yield return null;
        }

        InceptionCamera.Instance.SetCamera4();
        distance = 0;
        transform.rotation = Quaternion.LookRotation(Vector3.forward, path.path.GetNormalAtDistance(distance));

        transform.position = enterPos.position;
        transform.localScale = startScale;

        start = transform.position;
        final = startPos.position;

        factor = 0;

        while (factor < 1)
        {
            factor += inceptionSpeed * Time.deltaTime;

            transform.position = Vector2.Lerp(start, final, factor);


            yield return null;
        }
        anim.StopInception();

        InceptionCamera.Instance.SetCamera1();
        inceptioning = false;
        frameAnim.enabled = true;
    }

    public void ConnectPad(Panel panel)
    {
        connectedPad = true;
        this.panel = panel;
    }

    public void DisconnectPad()
    {
        connectedPad = false;
        this.panel = null;
    }

    public void DisconnectUlises()
    {
        ulises = null;
    }

    public void ConnectUlises(Ulises ulises)
    {
        this.ulises = ulises;
    }

    public void GrantPadAccess()
    {
        padAccess = true;
    }
    public void TimelinePlayed(PlayableDirector playableDirector)
    {
        //  anim.EndTimeline();
        //  mouth.Talk("What...? What is this? ");
        timelinePlayed = true;
        //  this.gameObject.SetActive(false);
    }

    public void CantMoveForSeconds(float seconds)
    {
        cantMove = true;
        StartCoroutine(NotMovingForSeconds(seconds));
    }

    IEnumerator NotMovingForSeconds(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        cantMove = false;
    }
}
