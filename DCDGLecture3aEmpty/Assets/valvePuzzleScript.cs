using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class valvePuzzleScript : MonoBehaviour
{
    [SerializeField] private float reach;
    [SerializeField] private LayerMask valveLayerMask;
    [SerializeField] private Transform playerCameraTransform; 


    [Header("Valves")]
    [SerializeField] private ValveScript[] valves;

    public bool puzzleComplete;

    private ValveScript script;
    // Start is called before the first frame update
    void Start()
    {
        puzzleComplete = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(!puzzleComplete) {
            if (Physics.Raycast(playerCameraTransform.position, playerCameraTransform.forward, out RaycastHit raycastHit, reach, valveLayerMask))
            {
                if(Input.GetKeyDown(KeyCode.E)) {
                    raycastHit.transform.TryGetComponent<ValveScript>(out script);
                    script.crankValve();
                }
            }


            bool valve0 = valves[0].activeValve;
            bool valve1 = valves[1].activeValve;
            bool valve2 = valves[2].activeValve;
            bool valve3 = valves[3].activeValve;
            bool valve4 = valves[4].activeValve;

            if(valve0 && valve3 && !(valve1 || valve2 || valve4)) {
                puzzleComplete = true;

                foreach (ValveScript vs in valves) {
                    vs.enabled = false;
                }
            }
        }
    }
}
