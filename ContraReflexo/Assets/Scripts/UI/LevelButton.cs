using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class LevelButton : MonoBehaviour
{
    [SerializeField] private Text txt;
    [SerializeField] public int id;

    void Start()
    {
        txt.text = id.ToString();
        if(SceneManager.sceneCountInBuildSettings -1 > id)
        {
            GameObject nextbt = Instantiate<GameObject>(this.gameObject, this.gameObject.transform.parent);
            nextbt.GetComponent<LevelButton>().id = id+1;
        }
    }

    public void Click()
    {
        SceneManager.LoadScene("Fase"+id);
    }


}
