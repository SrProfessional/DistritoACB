using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.UI;
using Data;
using WebAPI;
/// <summary>
/// Panel que aparece al seleccionar un item e intentar publicarlo
/// </summary>
public class PanelConfirmPublish : Panel
{
    public delegate void VoidDelegate();
    //Evento que se dispara al completar la publicaci�n en el mercadillo
    public event VoidDelegate OnConfirmedPublish;
    //Nombre de cada tyoo de item para ser reconocido en la venta
    public  enum itemTypes {BOOSTER, SKIN , ACBALL , TOKENCARD, TOKENHIGTHLIGHT };
    [Header("Skins")]
    //Url donde se alvergan las im�genes del proyecto
    public static string IMAGES_URL ="https://donext-dev.s3.eu-west-3.amazonaws.com/resources/";
    [SerializeField]
    [Tooltip("icono del item a mostrar")]
    public Image itemIcon;
    [SerializeField]
    [Tooltip("nombre del item a mostrar")]
    public Text itemDescription;
    [SerializeField]
    [Tooltip("precio del item a mostrar")]
    public Text itemPrice;
    [Tooltip("tipo del item a mostrar")]
    private itemTypes m_type;
    [Tooltip("Id del item a mostrar")]
    private int m_item_id;
    [SerializeField]
    [Tooltip("Primer t�tulo de alerta al publicar un item")]
    private string alertPublish = "�Est�s seguro?";
    [SerializeField]
    [Tooltip("Descripci�n de alerta al publicar un item")]
    private string alertPublishDescription = "�Se har� p�blica tu oferta. Si alguien complra tu token desaparecer� de tu lista. \n Recuerda que puedes eliminarla cuando quieras desde el detalle de tu oferta." ;
    private int itemPriceValue = 10;
    [Header("Potenciadores")]
    [SerializeField]
    private Text boosterStat;
    [SerializeField]
    [Tooltip("icono del booster a mostrar")]
    public Image boosterIcon;
    [Header("Cards")]
    [SerializeField]
    [Tooltip("Imagen de la carta en el equipo competitivo")]
    private GameObject team;
    [SerializeField]
    [Tooltip("Imagen de potenciador activo en la carta")]
    private GameObject booster;
    [SerializeField]
    [Tooltip("Imagen de carta con lesi�n")]
    private GameObject injured;
    [SerializeField]
    [Tooltip("icono del Token a mostrar")]
    public Image tokenIcon;
    [SerializeField]
    [Tooltip("Contenedor de Tokens")]
    private GameObject tokenIconContainer;
    [Header("highlight")]
    [Tooltip("icono del highlightIcon a mostrar")]
    public Image highlightIcon;
    /// <summary>
    /// Recibe la informaci�n del item y la dibuja
    /// </summary>
    /// <param name="itemData">informaci�n del item</param>
    public void Populate(ItemData itemData)
    {
        m_item_id = itemData.id;
        m_type = itemTypes.SKIN;
        if (itemIcon && !string.IsNullOrEmpty(itemData.img_show))
        {
            itemIcon.gameObject.SetActive(true);
            string imagePath = IMAGES_URL + itemData.img_show;
            WebProcedure.Instance.GetSprite(imagePath, OnSuccess, (WebError error) => { Debug.LogError(error); });
        }
        if (itemDescription) itemDescription.text = itemData.description;
        UpdateUI();
    }    /// <summary>
    /// Recibe la informaci�n del highlight y la dibuja
    /// </summary>
    /// <param name="itemData">informaci�n del item</param>
    public void Populate(HighLightData.HigthlightItems highlightData)
    {
        m_item_id = highlightData.id;
        m_type = itemTypes.TOKENHIGTHLIGHT;
        if (highlightIcon && !string.IsNullOrEmpty(highlightData.pathImgThumbnail))
        {
            //string imagePath = IMAGES_URL + highlightData.pathImgThumbnail;
            highlightIcon.transform.parent.gameObject.SetActive(true);
            WebProcedure.Instance.GetSprite(highlightData.pathImgThumbnail, (Sprite obj) => { highlightIcon.sprite = obj; }, (WebError error) => { Debug.LogError(error); });
        }
        if (itemDescription) itemDescription.text = highlightData.title;
        UpdateUI();
    } 
    /// <summary>
    /// Recibe la informaci�n del jugador y la dibuja
    /// </summary>
    /// <param name="itemData">informaci�n del item</param>
    public void Populate(TokenItemData itemData)
    {
        m_item_id = itemData.id;
        m_type = itemTypes.TOKENCARD;
        team.SetActive(itemData.isTeam);
        booster.SetActive(itemData.isBooster);
        injured.SetActive(itemData.isInjured);
        if (itemIcon && !string.IsNullOrEmpty(itemData.pathThumbnail))
        {
            tokenIconContainer.gameObject.SetActive(true);
            WebProcedure.Instance.GetSprite(itemData.pathThumbnail, OnTokenIconLoaded, (WebError error) => { Debug.LogError(error); });
        }
        if (itemDescription) itemDescription.text = itemData.name;
        UpdateUI();
    }  
    /// <summary>
    /// Recibe la informaci�n del ACBall y la dibuja
    /// </summary>
    /// <param name="ACBallData"></param>
    public void Populate(AcbBallContainer.AcbBallsData.AcBallsItems ACBallData)
    {
        m_item_id = ACBallData.id;
        m_type = itemTypes.ACBALL;
        if (itemIcon && !string.IsNullOrEmpty(ACBallData.path_img))
        {
            itemIcon.gameObject.SetActive(true);
            WebProcedure.Instance.GetSprite(ACBallData.path_img, OnSuccess, (WebError error) => { Debug.LogError(error); });
        }
        if (itemDescription) itemDescription.text = ACBallData.description;
        UpdateUI();
    }  
    /// <summary>
    /// Recibe la informaci�n del potenciador y la dibuja
    /// </summary>
    /// <param name="itemData">informaci�n del item</param>
    public void Populate(BoosterData.BoosterItemData boostData)
    {
        m_item_id = boostData.id;
        m_type = itemTypes.BOOSTER;
        string typeString = "";
        switch (boostData.type)
        {
            case BoosterType.TRIPLES:
                typeString = "Tiros de 2";
                break;

            case BoosterType.ASSISTS:
                typeString = "Asistencias";
                break;

            case BoosterType.FREESHOTS:
                typeString = "Tiros libres";
                break;

            case BoosterType.POINTS:
                typeString = "Puntos";
                break;

            case BoosterType.REBOUNDS:
                typeString = "Rebotes";
                break;
        }
        if (itemDescription) itemDescription.text ="Potenciador "+ typeString +" " + boostData.value ;

        if (boosterStat)
        {
            boosterStat.text = boostData.value ;
            boosterStat.gameObject.SetActive(true);
        }
        if (!string.IsNullOrEmpty(boostData.path_img))
        {
            if (boosterIcon)
            {
                boosterIcon.gameObject.SetActive(true);
                boosterIcon.sprite = boostData.GetSprite();
            }
        }
        UpdateUI();
    }
    /// <summary>
    /// si se encuentra el sprite, setea la imagen del item
    /// </summary>
    /// <param name="obj">sprite encontrado </param>
    private void OnSuccess(Sprite obj)
    {
        itemIcon.sprite = obj;
    }
    private void OnTokenIconLoaded(Sprite obj)
    {
        tokenIcon.sprite = obj;
    }
    /// <summary>
    /// Aumenta el valor del item a publicar
    /// </summary>
    public void IncreasePriceValue()
    {
        itemPriceValue += 5;
        UpdateUI();
    }
    /// <summary>
    /// Reduce el valor del item a publicar
    /// </summary>
    public void DecreasePriceValue()
    {
        itemPriceValue -= 5;
        UpdateUI();
    }
    /// <summary>
    /// Actualiza la interfaz
    /// </summary>
    public void UpdateUI()
    {
        itemPriceValue = Mathf.Clamp(itemPriceValue, 0, 9999);
        itemPrice.text = itemPriceValue.ToString();
    }
    /// <summary>
    /// intenta publicar el item seleccionado
    /// </summary>
    public void ClickPublish()
    {
        ACBSingleton.Instance.AlertPanel.SetupPanel(alertPublish, alertPublishDescription, true,Publish);
    }
    /// <summary>
    /// Publica el item seleccionado
    /// </summary>
    private void Publish()
    {   
        var cardbody = JsonConvert.SerializeObject(new SellData() { type = m_type.ToString(), price = itemPriceValue,item_id = m_item_id });
        WebProcedure.Instance.SellItem(cardbody, OnItemPublished, OnItemPublishedFailed);
    }
    /// <summary>
    /// Se dispara cuando el item ha sido satisfactoriamente publicado
    /// </summary>
    /// 
    private void OnItemPublished(DataSnapshot snapshot)
    {
        OnConfirmedPublish?.Invoke();
        Close();
    }
    /// <summary>
    /// no se pudo publicar el item
    /// </summary>
    private void OnItemPublishedFailed(WebError error)
    {
        Debug.LogError(error);
        Close();
    }
}
