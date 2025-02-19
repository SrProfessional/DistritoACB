using Data;
using Newtonsoft.Json;
using System;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using WebAPI;

public class PanelAñadirPlayer : Panel
{
    #region Fields and properties

    [Header("Panel main components")]
    [SerializeField] [Tooltip("Determina si es una colección")]
    private bool isColeccion;
    [SerializeField] [Tooltip("Título del panel")]
    private Text textTitle;
    [SerializeField] [Tooltip("Contenedor de los objetos arrastrables")]
    private GridLayoutGroup gridLayoutGroupCards;
    [SerializeField] [Tooltip("Contenedor de los objetos arrastrables")]
    private ToggleGroup toggleGroup;
    [SerializeField] [Tooltip("Botón de añadir jugador al equipo competitivo")]
    private Button addPlayerButton;
    [SerializeField] [Tooltip("Objeto arrastrable con datos de la carta")]
    private PanelTokenItemToggle panelCardItem;
    [SerializeField] [Tooltip("Se ejecuta cuando la traida de datos de liga es fallida")]
    private UnityEvent onFailed;
    [SerializeField] [Tooltip("Datos del contenedor de los objetos arrastrables")]
    private TokenContainer2 tokenContainer = new TokenContainer2();
    [SerializeField] [Tooltip("Primer título de alerta para añadir carta al equipo competitivo")]
    private string alertAdds = "¿Quieres añadir el token a tu Equipo Competitivo?";
    [SerializeField] [Tooltip("Segundo título de alerta para añadir carta al equipo competitivo")]
    private string alertFailAdds = "Elige el token que quieres añadir a tu Equipo.";

    [Space(10)]
    [Header("Dragable reference")]
    [SerializeField] [Tooltip("Posición final de objetos arrastrables")]
    private GameObject dragableTargetPosition;
    [SerializeField] [Tooltip("Panel de carta del jugador")]
    private PlayerCard playerCard;
    [SerializeField] [Tooltip("Spinner de carga")]
    private GameObject Spinner;

    private PanelTokenItemToggle draggedToogle;
    private int dragablesCount; //Contador de elementos arrastrables
    private int cardId; //Id de la carta
    private string title; //Texto de título del panel

    #endregion

    #region Unity Methods

    /// <summary>
    /// Suscribe acción de cerrar panel del equipo competitivo
    /// </summary>
    private void OnEnable()
    {
        PanelTeamCompetitivo.OnClose += Close;
    }

    /// <summary>
    /// Desuscribe el evento de cerrar panel al destruirse el objeto
    /// </summary>
    private void OnDestroy()
    {
        PanelTeamCompetitivo.OnClose -= Close;
    }

    /// <summary>
    /// Asigna que ningún objeto está siendo arrastrado
    /// </summary>
    private void Start()
    {
        draggedToogle = null;
    }

    #endregion

    #region Public Methods

    /// <summary>
    /// Carga los datos de la carta
    /// </summary>
    /// <param name="cardId">Id de la carta</param>
    /// <param name="title">Título de la colección</param>
    public void CallInfo(int cardId, string title)
    {
        textTitle.text = title;
        this.title = title;
        this.cardId = cardId;
        var cardbody = JsonConvert.SerializeObject(new BodyToken(){ card_id = cardId}) ;

        if (isColeccion)
        {
            WebProcedure.Instance.GetCardTokenCL(cardbody,snapshot =>
            {
                Debug.Log(snapshot.RawJson);
                tokenContainer = new TokenContainer2();
                JsonConvert.PopulateObject(snapshot.RawJson, tokenContainer);
                addPlayerButton?.onClick.AddListener(AddToTeam);

                if (gridLayoutGroupCards.transform.childCount > 0)
                {
                    for (int i = 0; i < gridLayoutGroupCards.transform.childCount; i++)
                    {
                        Destroy(gridLayoutGroupCards.transform.GetChild(i).gameObject);
                    }
                }

                if (tokenContainer.tokenData.tokenItems != null)
                {
                    dragablesCount = 0;
                    foreach (var transactionData in tokenContainer.tokenData.tokenItems)
                    {
                        var prefab= Instantiate(panelCardItem, gridLayoutGroupCards.transform);

                        if (dragableTargetPosition != null)
                        {
                            gridLayoutGroupCards.GetComponent<RectTransform>().sizeDelta = new Vector2((prefab.GetComponent<RectTransform>().sizeDelta.x + 80) * tokenContainer.tokenData.tokenItems.Count, gridLayoutGroupCards.GetComponent<RectTransform>().sizeDelta.y);
                            prefab.GetComponent<RectTransform>().anchoredPosition = new Vector2(((prefab.GetComponent<RectTransform>().sizeDelta.x + 80) * dragablesCount) + (prefab.GetComponent<RectTransform>().sizeDelta.x / 2) + 80, 0);
                            prefab.ShowInfo(transactionData, dragableTargetPosition, () => { UpdatePlayerCardView(transactionData, prefab); });
                            dragablesCount++;
                        }
                        else
                            prefab.ShowInfo(transactionData, null, null, () => { CallInfo(cardId, title); });

                        if (toggleGroup)
                        {
                            prefab.toggle.group = toggleGroup;  
                        }
                    } 
                }
                else
                {
                    ClosedSpinner();
                }
            
            }, error =>
            {
                onFailed.Invoke();
                ClosedSpinner();
            });
        }
        else
        {
            WebProcedure.Instance.GetUserCardTokenTC(cardbody,snapshot =>
            {
                Debug.Log(snapshot.RawJson);
                tokenContainer = new TokenContainer2();
                JsonConvert.PopulateObject(snapshot.RawJson, tokenContainer);
                addPlayerButton?.onClick.AddListener(AddToTeam);

                if (gridLayoutGroupCards.transform.childCount > 0)
                {
                    for(int i = 0; i < gridLayoutGroupCards.transform.childCount; i++)
                    {
                        Destroy(gridLayoutGroupCards.transform.GetChild(i).gameObject);
                    }
                }

                if (tokenContainer.tokenData.tokenItems != null)
                {
                    dragablesCount = 0;
                    foreach (var transactionData in tokenContainer.tokenData.tokenItems)
                    {
                        var prefab= Instantiate(panelCardItem, gridLayoutGroupCards.transform);

                        if (dragableTargetPosition != null)
                        {
                            gridLayoutGroupCards.GetComponent<RectTransform>().sizeDelta = new Vector2((prefab.GetComponent<RectTransform>().sizeDelta.x + 80) * tokenContainer.tokenData.tokenItems.Count, gridLayoutGroupCards.GetComponent<RectTransform>().sizeDelta.y);
                            prefab.GetComponent<RectTransform>().anchoredPosition = new Vector2(((prefab.GetComponent<RectTransform>().sizeDelta.x + 80) * dragablesCount) + (prefab.GetComponent<RectTransform>().sizeDelta.x / 2) + 80, 0);
                            prefab.ShowInfo(transactionData, dragableTargetPosition, () => { UpdatePlayerCardView(transactionData, prefab); });
                            dragablesCount++;
                        }
                        else
                            prefab.ShowInfo(transactionData, null, null, () => { CallInfo(cardId, title); });

                        if (toggleGroup)
                        {
                            prefab.toggle.group = toggleGroup;  
                        }
                    } 
                }
                else
                {
                    ClosedSpinner();
                }
            
            }, error =>
            {
                onFailed.Invoke();
                ClosedSpinner();
            });
        }
    }

    /// <summary>
    /// Añadir carta al equipo competitivo
    /// </summary>
    public void AddToTeam()
    {
        if (GetSelectedToggle())
        {
            ACBSingleton.Instance.AlertPanel.SetupPanel(alertAdds, string.Empty, true, CallTeam);
        }
        else
        {
            ACBSingleton.Instance.AlertPanel.SetupPanel(alertFailAdds, string.Empty, false, null); 
        }
    }

    /// <summary>
    /// Carga los datos de la carta
    /// </summary>
    private void CallTeam()
    {
        Debug.Log("Token data " + GetSelectedToggle().GetComponent<PanelTokenItemToggle>().CurrentToken.token);
        var cardbody = JsonConvert.SerializeObject(new BodyTokenCard(){ tokenCard = GetSelectedToggle().GetComponent<PanelTokenItemToggle>().CurrentToken.token}) ;
        WebProcedure.Instance.PostAddTokenToTeam(cardbody, snapshot =>
        {
            Debug.Log(snapshot.RawJson);
            ACBSingleton.Instance.AlertPanel.SetupPanel(snapshot.MessageCustom, string.Empty, false, () =>
            {
                var team =  JsonConvert.DeserializeObject<TokenContainer>(snapshot.RawJson);
                if (snapshot.Code == 200)
                {
                    PanelTeamCompetitivo.OnDeleteOrAdd?.Invoke(team);
                    PanelTeamCompetitivo.OnClose?.Invoke();
                }
            }, null, 0, "Volver");  
        }, error =>
        {
            onFailed.Invoke();
            ClosedSpinner();
        });
    }

    /// <summary>
    /// Elimina la carta del equipo competitivo
    /// </summary>
    private void DeleteCardFromTeam()
    {
        var cardbody = JsonConvert.SerializeObject(new BodyTokenCard() { tokenCard = GetSelectedToggle().GetComponent<PanelTokenItemToggle>().CurrentToken.token });

        WebProcedure.Instance.DelRemoveTokenOfTeam(cardbody, snapshot =>
        {
            ACBSingleton.Instance.AlertPanel.SetupPanel(snapshot.MessageCustom, string.Empty, false, () =>
            {
                var team = JsonConvert.DeserializeObject<TokenContainer>(snapshot.RawJson);
                if (snapshot.Code == 200)
                {
                    PanelTeamCompetitivo.OnDeleteOrAdd?.Invoke(team);
                    PanelTeamCompetitivo.OnClose?.Invoke();
                    Close();
                }
            });

        }, error =>
        {
            ClosedSpinner();
        });
    }

    /// <summary>
    /// Obtiene el objeto arrastrable seleccionado
    /// </summary>
    /// <returns>Referencia del objeto arrastrable</returns>
    private Toggle GetSelectedToggle()
    {
        var toggles = gridLayoutGroupCards.transform.GetComponentsInChildren<Toggle>();
        return toggles.FirstOrDefault(t => t.isOn);
    }

    /// <summary>
    /// Actualiza vista de las caetas
    /// </summary>
    /// <param name="cardNewData">Datos de la carta</param>
    /// <param name="draggedToogle">Objeto arrastrable</param>
    private void UpdatePlayerCardView(TokenItemData cardNewData, PanelTokenItemToggle draggedToogle)
    {
        if (this.draggedToogle != null)
            this.draggedToogle.Dragable.ResetDragable();

        Action actionView = DeleteCardFromTeam;
        if (!cardNewData.isTeam)
            actionView = CallTeam;

        playerCard.SetupCardData(cardNewData, actionView, true, cardNewData.isTeam, true, true, () => { CallInfo(cardId, title); });
        gridLayoutGroupCards.CalculateLayoutInputHorizontal();
        
        this.draggedToogle = draggedToogle;
    }

    /// <summary>
    /// Oculta el spinner de carga
    /// </summary>
    private void ClosedSpinner()
    {
        Spinner.gameObject.SetActive(false);
    }

    #endregion
}
