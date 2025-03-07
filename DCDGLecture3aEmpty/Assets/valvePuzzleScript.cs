using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class valvePuzzleScript : MonoBehaviour
{

    [Header("Valves")]
    [SerializeField] private ValveScript[] valves;

    public bool puzzleComplete;
    // Start is called before the first frame update
    void Start()
    {
        puzzleComplete = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(!puzzleComplete) {
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
