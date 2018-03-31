using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    public GameObject BoxPrefab;
    Canvas canvas;
    GameObject boxSpawnpoint;
    private int gameSize =0;
    public Box[] boxArray;
    public int BoxNumber = 1;
    int movesCount = 0;

    public Sprite[] boxSprite;
    public int currentBoxType = 0;


    [SerializeField]
    Text WarningPanel;

    [SerializeField]
    Text ScoreTxt;

    [SerializeField]
    Text HighScoreTxt;

    //Oyunumuz başladığında gerekli ayarlamaları yaptığımız Metot Start
    //ilk olarak oyunumuzu önceden ayarladığımız çözünürlükte kullanıcını bilgisayarında gerekli ayarlamarı yapıyoruz
    //Kullancı eğer bir box type seçmedi ise Default olarak 0 ataması yaparak standart box type'ını belirliyoruz
    //Eğer daha önceden bir boxtype seçtiyse geçerli boxtype seçili boxtype'ı atıyoruz.
    //kullanıcının seçtiği game size bilgisini saklayıp oyun başladığında gerekli ayarlamaları yapıp oyunu oluşturuyoruz.
    //HighScore bilgisi var ise oyun başladığında ekranda bu bilgiyi gösteriyoruz.
    void Start()
    {
        Screen.SetResolution(1024, 768,false);
        PlayerPrefs.GetInt("Boxtype", 0);

        currentBoxType = PlayerPrefs.GetInt("Boxtype");



        PlayerPrefs.GetInt("gameSize", 0);

        if(PlayerPrefs.GetInt("gameSize") != 0)
        {

            BoxNumber = 1;
            movesCount= 0;
            boxArray = new Box[PlayerPrefs.GetInt("gameSize") * PlayerPrefs.GetInt("gameSize")];
            Create_Box(PlayerPrefs.GetInt("gameSize"));
            gameSize = PlayerPrefs.GetInt("gameSize");
        }

        PlayerPrefs.GetInt("HighScore", 0);
        if(PlayerPrefs.GetInt("HighScore") != 0)
        {
            HighScoreTxt.text = "High Score : " + PlayerPrefs.GetInt("HighScore");
        }

    }

    
       
//Ekranın üst bölümünde bulunana uyarı paneline gerekli değişiklikleri yapmamızı sağlayan metot WarningPanelChange
    //Kullanıcını kazanması , kaybetmesi veya bir hamlesinde onu bilgilendirmemiz gerektiği durumlarda içerideki yapı ile gerekli bilgilendirmeleri sağlıyoruz.
    public void WarningPanelChange()
    {
        if(movesCount == 0)
        {
            

            WarningPanel.color = HexToRGB("#9A2B05");
            WarningPanel.text = "There is no possible moves left . You are lose :(";
            if(BoxNumber > PlayerPrefs.GetInt("HighScore"))
            {
                PlayerPrefs.SetInt("HighScore", BoxNumber--);
            }
        }
        else
        {
            WarningPanel.text = movesCount + " moves left !";
            movesCount = 0;
        }
        if (BoxNumber > gameSize * gameSize)
        {
            WarningPanel.color = HexToRGB("#91D147");
            WarningPanel.text = "Congratulations !";

            if (BoxNumber > PlayerPrefs.GetInt("HighScore"))
            {
                PlayerPrefs.SetInt("HighScore", BoxNumber--);
            }
        }
        

    }
    //Dışarıdan aldığımız Hex kodunu unity'nin Color sınıfına dönüştürmemize yarayan metot
    Color HexToRGB(string Color_Hex_A)
    {
        Color myColor = new Color();
        ColorUtility.TryParseHtmlString(Color_Hex_A, out myColor);

        return myColor;

    }

    //Oyun başladığında çalıştırdığımız metot gerekli ayarlamaları kaydedip oyunu tekrar başlatıp oluşturma işlemini yerine getiriyor.
    public void Create_Game(int GameSize)
    {

        
        PlayerPrefs.SetInt("gameSize",GameSize);
        Restart_Game();
    }

    
    //Box sınıfında bir tıklama olayı olduğu zaman çalıştırdığımız metot
    //Tıklanılan butonun kordinatlarını alarak tüm butonları içerisinde geziyoruz ve bize oyunu hazırlamamız için verilen patern'e uygun çıktı alabilmek için
    //butonların durumlarını ayarladığımız metot.
    public void OnClick(int[] xy)
    {
        int btnPosX = xy[0];
        int btnPosY = xy[1];
        
        
        foreach (Box box in boxArray)
        {
            
            Button btnBox = box.GetComponentInParent<Button>();

            if(btnPosX == box.PosX && btnPosY == box.PosY)
            {
                ScoreTxt.text ="Score : "+BoxNumber.ToString();
                box.GetComponentInChildren<Text>().text = BoxNumber++.ToString();

            }
            

            if (btnPosX+2 == box.PosX && btnPosY+1 == box.PosY && box.isClick == false)
            {
                btnBox.interactable = true;
                movesCount++;
            }
            else if(btnPosX + 2 == box.PosX && btnPosY -1 == box.PosY && box.isClick == false)
            {
                btnBox.interactable = true;
                movesCount++;
            }
            else if (btnPosX - 2 == box.PosX && btnPosY + 1 == box.PosY && box.isClick == false)
            {
                btnBox.interactable = true;
                movesCount++;
            }
            else if (btnPosX - 2 == box.PosX && btnPosY - 1 == box.PosY && box.isClick == false)
            {
                btnBox.interactable = true;
                movesCount++;
            }
            else if (btnPosX + 1 == box.PosX && btnPosY + 2 == box.PosY && box.isClick == false)
            {
                btnBox.interactable = true;
                movesCount++;
            }
            else if (btnPosX -1 == box.PosX && btnPosY + 2 == box.PosY && box.isClick == false)
            {
                btnBox.interactable = true;
                movesCount++;
            }
            else if (btnPosX + 1 == box.PosX && btnPosY - 2 == box.PosY && box.isClick == false)
            {
                btnBox.interactable = true;
                movesCount++;
            }
            else if (btnPosX - 1 == box.PosX && btnPosY - 2 == box.PosY && box.isClick == false)
            {
                btnBox.interactable = true;
                movesCount++;
            }
            else
            {
                btnBox.interactable = false;
            }

          

            
        }


        WarningPanelChange();


    }

    //Kullanıcın yaptığı BoxType seçimini alıp bilgiyi saklayıp tekrar oyunu oluşturduğumuz metot.
    public void Change_Box_Type(int boxType)
    {
        if(boxType == 5)
        {
            PlayerPrefs.SetInt("Boxtype", Random.Range(0, 4));
        }
        else
        {
            PlayerPrefs.SetInt("Boxtype", boxType);
        }
        
        Restart_Game();
    }

    
    //Oyunumuzu tekrar başlatmak için sahnemizi baştan yüklediğimiz metot.
    public void Restart_Game()
    {
        SceneManager.LoadScene("Game");
        
       
    }

    
    //Gerekli Gamesize bilgisiyle Sahnede boxları oluşturduğumuz metot.
    //Tüm nesneleri oluşturup sahnemiz içerisindeki Canvas elementimize atıp daha sonrasında Boxların boyutlarını dikkate alarak
    //Sahnenin orta bölümüne yerleştiriyoruz.
    //Aynı zamanda oyun içerisinde Box lara rahatça erişip kontrolleri gerçekteştirebilmek için
    //Boxlarımıza Box sınıfında ki posizyon değerine sanal pozisyonlar verip boxArray listemize ekliyoruz.
    
    public void Create_Box(int GameSize)
    {

        canvas = GameObject.Find("Canvas").GetComponent<Canvas>();
        boxSpawnpoint = GameObject.Find("BoxSpawnPoint");
        RectTransform boxSpawnRect = boxSpawnpoint.GetComponent<RectTransform>();

        boxSpawnRect.localPosition = new Vector3(((GameSize*60) / 2)-25, ((GameSize * 60) / 2)-25);


        int k = -1;
        int l = -1;
        int q = -1;
        for (int i = 0; i < GameSize*60; i += 60)
        {
            l++;
            k = -1;
            for (int j = 0; j < GameSize * 60; j += 60)
            {
                Debug.Log("i = " + j);
                GameObject box = Instantiate(BoxPrefab, new Vector2(i, j), Quaternion.identity);
                box.GetComponent<Image>().sprite = boxSprite[currentBoxType];
                RectTransform rectTransform = box.GetComponent<RectTransform>();
                box.transform.parent = canvas.transform;
                k++;
                q++;
                boxArray[q] = box.GetComponent<Box>();
                boxArray[q].PosX = l;
                boxArray[q].PosY = k;
                
                rectTransform.localPosition = new Vector3(i, j, 0);
                box.transform.parent = boxSpawnpoint.transform;


            }

        }
        
        boxSpawnpoint.transform.localPosition = new Vector3(0, 0, 0);

    }


}
