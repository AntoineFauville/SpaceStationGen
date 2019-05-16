using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using UnityStandardAssets.Characters.FirstPerson;

public class PlayerControllerFPS : MonoBehaviour
{
    [Inject] private GameSettings _gameSettings;

    Vector3 initialPosition;
    
    void Start()
    {
        initialPosition = new Vector3((_gameSettings.GridSize.x / 2) * _gameSettings.ModuleSpacing,
                                      (_gameSettings.GridSize.x / 2) * _gameSettings.ModuleSpacing + 1,
                                      (_gameSettings.GridSize.x / 2) * _gameSettings.ModuleSpacing);
        
        this.transform.position = initialPosition;
       // StartCoroutine(wait());
    }

    IEnumerator wait()
    {
        yield return new WaitForSeconds(0.1f);

        if (this.GetComponent<FirstPersonController>())
        {
            this.GetComponent<FirstPersonController>().enabled = true;
        }
    }
}
