using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsManager : MonoBehaviour
{
    public static PhysicsManager Instance;
    [SerializeField] SpaceStation spaceStation;
    float gravityMagnitude;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        gravityMagnitude = Physics2D.gravity.magnitude;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetGravity(Transform octante)
    {
        //Debug.Log("chanhging gravity");
        //Vector2 direction = (octante.position - spaceStation.transform.position).normalized;

        //Physics2D.gravity = direction * gravityMagnitude;
    }
}
