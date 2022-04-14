using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleManager : MonoBehaviour
{
    public class Move
    {
        public Enums.PuzzleMove move;
        public int quantity;

        public Move(Enums.PuzzleMove move, int quantity)
        {
            this.move = move;
            this.quantity = quantity;
        }
    }

    [SerializeField] int lines;
    [SerializeField] int columns;
    [SerializeField] PuzzleTile tilePrefab;
    [SerializeField] float speed;
    [SerializeField] float intervalToSelect;
    [SerializeField] float interval;
    [SerializeField] Transform initialPoint;
    [SerializeField] InputReader inputReader;
    [SerializeField] List<Vector2> blockedTiles = new List<Vector2>();
    [SerializeField] Transform container;
    [SerializeField] GameObject lockedImage;
    [SerializeField] GameObject unlockedImage;

    float time;
    bool moving;
    bool firstSelected;
    PuzzleTile firstTile;
    Vector2 movement;
    PuzzleTile currentTile;
    Vector2 currentPosition;

    List<PuzzleTile> tiles = new List<PuzzleTile>();
    Dictionary<Vector2, PuzzleTile> tilePositions = new Dictionary<Vector2, PuzzleTile>();
    List<Move> moves = new List<Move>();
    Enums.PuzzleMove moveDirection;
    bool blockDirection;
    bool selectedDirection;
    bool reversing;
    int movesQuantity;
    bool victory;

    bool puzzleOn;

    Coroutine currentCoroutine;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (!puzzleOn) return;
        Debug.Log(movement);
        time += Time.deltaTime;
        if (firstSelected)
        {
            if (selectedDirection && !reversing)
            {
                if (time > interval)
                {
                    if (SetCurrentPosition())
                    {
                        TurnOnCurrentTile();
                        movesQuantity++;
                    }

                    else
                    {
                        selectedDirection = false;
                        if (movesQuantity > 0)
                            moves.Add(new Move(moveDirection, movesQuantity));

                        if (MovementBlocked())
                        {
                            if (victory)
                            {
                                Debug.Log("Yeeeeaahhhh");
                                return;
                            }
                            selectedDirection = true;
                            reversing = true;
                        }
                    }
                    time = 0;
                }
            }

            if (reversing)
            {

                if (time > interval)
                {

                    if (moves.Count == 0)
                    {
                        currentTile = tilePositions[currentPosition];
                        currentTile.TurnOnYellow();
                        firstSelected = false;
                        reversing = false;
                        selectedDirection = false;
                        firstTile = null;
                        return;
                    }
                    Move move = moves[moves.Count - 1];


                    TurnOffCurrentTile();


                    GetReverseCurrentPosition(move);
                    move.quantity--;
                    time = 0;
                    if (move.quantity <= 0)
                    {
                        moves.RemoveAt(moves.Count - 1);
                    }
                }
            }

        }



        if (!moving) return;


        if (!firstSelected)
        {
            if (time > intervalToSelect)
            {
                if (SetCurrentPosition())
                    ChangeCurrentTile(tilePositions[currentPosition]);
                time = 0;
            }
        }

    }
    bool CheckMove(Vector2 move)
    {
        Vector2 aux = currentPosition + move;

        if (tilePositions.ContainsKey(aux) && !tilePositions[aux].isBlocked) return true;
        else return false;
    }
    bool MovementBlocked()
    {
        int blockedDirections = 0;
        currentPosition.y += 1;
        if (currentPosition.y >= columns || tilePositions[currentPosition].isBlocked)
        {
            blockedDirections++;
        }
        currentPosition.y -= 1;


        currentPosition.y -= 1;
        if (currentPosition.y < 0 || tilePositions[currentPosition].isBlocked)
        {
            blockedDirections++;
        }
        currentPosition.y += 1;

        currentPosition.x += 1;
        if (currentPosition.x >= lines || tilePositions[currentPosition].isBlocked)
        {
            blockedDirections++;
        }
        currentPosition.x -= 1;

        currentPosition.x -= 1;
        if (currentPosition.x < 0 || tilePositions[currentPosition].isBlocked)
        {
            blockedDirections++;
        }

        currentPosition.x += 1;

        int aux = 0;
        foreach (PuzzleTile t in tiles)
        {
            if (t.isBlocked)
            {
                aux++;
            }
        }
        if (aux == tiles.Count)
        {
            SetVictory();

            TurnScreenOff();
            CameraManager.Instance.ChangeCamera(true, true);
            this.GetComponentInParent<SolarPanel>().SetPuzzleComplete(this);
        }



        return blockedDirections >= 4;

    }

    public void SetVictory()
    {
        victory = true;
        tiles.ForEach(t => t.TurnOnGreen());
        lockedImage.SetActive(false);
        unlockedImage.SetActive(true);
    }

    public bool IsComplete()
    {
        return victory;
    }
    void TurnOnCurrentTile()
    {
        currentTile = tilePositions[currentPosition];
        currentTile.TurnOnYellow();
    }
    void TurnOffCurrentTile()
    {
        currentTile = tilePositions[currentPosition];
        currentTile.TurnOffLight();
    }

    void GetReverseCurrentPosition(Move m)
    {
        moveDirection = m.move;

        switch (moveDirection)
        {
            case Enums.PuzzleMove.up:
                currentPosition.x += 1;
                break;

            case Enums.PuzzleMove.down:
                currentPosition.x -= 1;
                break;

            case Enums.PuzzleMove.left:
                currentPosition.y += 1;
                break;

            case Enums.PuzzleMove.right:
                currentPosition.y -= 1;
                break;
        }

    }
    bool SetCurrentPosition()
    {
        if (movement.x > 0)
        {
            currentPosition.y += 1;
            if (currentPosition.y >= columns || tilePositions[currentPosition].isBlocked)
            {
                currentPosition.y -= 1;
                return false;
            }
            moveDirection = Enums.PuzzleMove.right;


        }
        else if (movement.x < 0)
        {
            currentPosition.y -= 1;
            if (currentPosition.y < 0 || tilePositions[currentPosition].isBlocked)
            {
                currentPosition.y += 1;
                return false;
            }
            moveDirection = Enums.PuzzleMove.left;

        }

        if (movement.y < 0)
        {
            currentPosition.x += 1;
            if (currentPosition.x >= lines || tilePositions[currentPosition].isBlocked)
            {
                currentPosition.x -= 1;
                return false;
            }
            moveDirection = Enums.PuzzleMove.down;

        }
        else if (movement.y > 0)
        {

            currentPosition.x -= 1;
            if (currentPosition.x < 0 || tilePositions[currentPosition].isBlocked)
            {
                currentPosition.x += 1;
                return false;
            }
            moveDirection = Enums.PuzzleMove.up;

        }

        return true;
        //currentPosition.y = Mathf.Clamp(currentPosition.y, 0, columns - 1);
        //currentPosition.x = Mathf.Clamp(currentPosition.x, 0, lines - 1);




    }
    void ChangeCurrentTile(PuzzleTile tile)
    {
        currentTile.TurnOffLight();
        currentTile = tile;
        currentTile.TurnOnYellow();
    }

    void CreatePuzzle()
    {
        float offsetX = tilePrefab.GetComponent<SpriteRenderer>().bounds.extents.x;
        offsetX += offsetX * 1.15f;

        float offsetY = tilePrefab.GetComponent<SpriteRenderer>().bounds.extents.y;
        offsetY += offsetY * 1.1f;

        Vector2 pos = initialPoint.position;
        for (int i = 0; i < lines; i++)
        {
            for (int j = 0; j < columns; j++)
            {
                PuzzleTile t = Instantiate(tilePrefab, pos, Quaternion.identity);
                t.transform.SetParent(container);
                tiles.Add(t);
                Vector2 newPos = new Vector2(i, j);
                tilePositions.Add(newPos, t);

                if (blockedTiles.Contains(newPos))
                {
                    t.Block();
                }
                pos.x += offsetX;
            }

            pos.x = initialPoint.position.x;
            pos.y -= offsetY;
        }

        currentPosition = Vector2.zero;
        currentTile = tilePositions[currentPosition];
        currentTile.TurnOnYellow();
    }

    public void TurnPuzzleOn()
    {
        if (currentCoroutine != null) StopCoroutine(currentCoroutine);
        currentCoroutine = StartCoroutine(TogglingPuzzle());

    }

    public void TurnScreenOn()
    {
        if (currentCoroutine != null) StopCoroutine(currentCoroutine);
        currentCoroutine = StartCoroutine(TogglingScreen(true));
    }

    public void TurnScreenOff()
    {
        if (currentCoroutine != null) StopCoroutine(currentCoroutine);
        currentCoroutine = StartCoroutine(TogglingScreen(false));
    }

    IEnumerator TogglingPuzzle()
    {
        float i = 0;
        float j = 0;
        foreach (Transform tile in container)
        {
            PuzzleTile t = tile.GetComponent<PuzzleTile>();
            t.TurnOffLight();
            Vector2 newPos = new Vector2(i, j);
            if (!puzzleOn)
            {
                tiles.Add(t);

                tilePositions.Add(newPos, t);
            }


            if (blockedTiles.Contains(newPos))
            {
                t.Block();
            }

            j++;
            if (j > 4)
            {
                i++;
                j = 0;
            }

            yield return new WaitForSeconds(.02f);
        }

        currentPosition = Vector2.zero;
        currentTile = tilePositions[currentPosition];
        currentTile.TurnOnYellow();
        puzzleOn = true;
    }
    IEnumerator TogglingScreen(bool isOn)
    {
        foreach (Transform tile in container)
        {
            PuzzleTile t = tile.GetComponent<PuzzleTile>();
            if (isOn)
                t.TurnOnYellow();
            else
                t.TurnOffLight();
            yield return new WaitForSeconds(.03f);
        }

        //if(!isOn)
        //{
        //    puzzleOn = false;
        //}
    }
    private void OnEnable()
    {
        if (inputReader != null)
        {
            inputReader.moveEvent += Movement;
            inputReader.moveEventCancel += MoveCancel;
            inputReader.interactEvent += Interact;


        }
    }

    private void OnDisable()
    {

        if (inputReader != null)
        {
            inputReader.moveEvent -= Movement;
            inputReader.moveEventCancel -= MoveCancel;

            inputReader.interactEvent -= Interact;

        }

    }
    private void Movement(Vector2 move)
    {

        if (!puzzleOn) return;
        if (move != Vector2.right && move != Vector2.left && move != Vector2.down && move != Vector2.up) return;
        if (firstSelected)
        {
            if (selectedDirection) return;

            selectedDirection = true;
            time = interval;
            movement = move;

            movesQuantity = 0;
            return;
        }

        time = intervalToSelect;
        moving = true;
        movement = move;


    }

    void MoveCancel()
    {
        moving = false;
    }

    void Interact()
    {
        if (!puzzleOn) return;
        if (firstSelected && !selectedDirection && !reversing)
        {
            reversing = true;
            return;
        }
        if (firstTile != null && firstTile == currentTile)
        {
            firstSelected = false;
            firstTile.TurnOnYellow();
            firstTile = null;
        }
        else if (!firstSelected)
        {
            firstTile = currentTile;
            firstTile.TurnOnGreen();
            firstSelected = true;
        }

    }

}
