using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Box : MonoBehaviour{

    public int PosX;
    public int PosY;
    public bool isClick = false;
    private int[] XY;

    GameManager gameManager;


    Button Btn;

    //Boxlarımız oluşturulduğunda genel atama ve ayarların yapıldığı metot.
    void Start()
    {
        XY = new int[2];
        XY[0] = PosX;
        XY[1] = PosY;
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        Btn = this.gameObject.GetComponent<Button>();
        Btn.onClick.AddListener(OnClick);

    }

    //Sahne içerisinde ki butonlara tıklanıldığında çağırılan metot.
    public void OnClick()
    {
        
        isClick = true;
        gameManager.OnClick(XY);
    }
   


}
