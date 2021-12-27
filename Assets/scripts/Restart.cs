using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Restart : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        var gm = GameObject.FindObjectOfType<GameManager>();
        if (gm != null)
            Destroy(gm.gameObject);

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            SceneManager.LoadScene("Game", LoadSceneMode.Single);
        }
    }
}
