using Data;
using Newtonsoft.Json;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using WebAPI;

namespace Panels
{
    /// <summary>
    /// Panel con los datos de los desafios disponibles
    /// </summary>
    public class PanelTablonDesafio : Panel
    {
        #region Fields and properties

        [Header("Panel components")]
        [SerializeField] [Tooltip("Contenedor de los desafios publicados")]
        private RectTransform challengeDataContainer;
        [SerializeField] [Tooltip("Prefab del objeto donde se muestra un desafio publicado")]
        private GameObject PaneltabloDesafioData;
        [SerializeField] [Tooltip("Evento que se ejeuta cuando los desafios no se han podido cargar")]
        private UnityEvent onFailed;
        [SerializeField] [Tooltip("Bot�n de creaci�n de desafio")]
        private Button createChallengeButton;
        [SerializeField] [Tooltip("Bot�n de cerrar panel")]
        private Button closePanelButton;
        [SerializeField] [Tooltip("Datos con los desafios publicados")]
        private ChallengeContainer challengeContainer = new ChallengeContainer();

        [Space(5)]
        [Header("Panel opener data")]
        [SerializeField] [Tooltip("Clase que controla la apertura de nuevos paneles a mostrar")]
        private PanelOpener panelOpener;
        [SerializeField] [Tooltip("Panel que muestra una alerta de equipo competitivo incompleto")]
        private GameObject challengeTeamImcompleteAlertPrefab;

        [Space(5)]
        [Header("Creation challenge alert texts")]
        [SerializeField] [Tooltip("Texto de creaci�n de desafio que se muestra en el panel de alerta")]
        private string createChallengeText;
        [SerializeField] [Tooltip("Texto que se muestra en el panel de alerta cuando hay un error al crear un desafio")]
        private string CreationErrorText;

        [SerializeField] [Tooltip("Descripci�n de costo del desafio que se muestra en el panel de alerta")]
        private string mensaje;

        private List<GameObject> postedChallenges; //Lista de desafios publicados

        #endregion

        #region Unity Methods

        /// <summary>
        /// M�todo que se ejecuta cuando se activa el panel, actualiza la informaci�n de los desafios
        /// </summary>
        private void OnEnable()
        {
            postedChallenges = new List<GameObject>();
            closePanelButton.onClick.AddListener(() => { ACBSingleton.Instance.PanelBuildingSelection.ResetCachedMapData(); Close(); });
            CallInfo();
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Actualiza la informaci�n de los desafios que se van a mostrar en panel
        /// </summary>
        private void CallInfo()
        {
            createChallengeButton.onClick.AddListener(CreateChallenge);

            if(postedChallenges.Count > 0)
            {
                postedChallenges.ForEach(challenge => Destroy(challenge));
                postedChallenges.Clear();
            }

            postedChallenges = new List<GameObject>();
            challengeContainer = new ChallengeContainer();

            WebProcedure.Instance.GetChallengesTablon(snapshot =>
            {
                JsonConvert.PopulateObject(snapshot.RawJson, challengeContainer);
                Debug.Log(snapshot.RawJson);
                
                createChallengeButton.gameObject.SetActive(true);
                
                if (challengeContainer.challengeData.challengeItems != null && challengeContainer.challengeData.challengeItems.Count > 0)
                {
                    challengeDataContainer.sizeDelta = new Vector2(challengeDataContainer.sizeDelta.x, PaneltabloDesafioData.GetComponent<LayoutElement>().preferredHeight * challengeContainer.challengeData.challengeItems.Count);

                    foreach (var transactionData in challengeContainer.challengeData.challengeItems)
                    {
                        GameObject prefab = Instantiate(PaneltabloDesafioData, challengeDataContainer.transform);
                        prefab.GetComponent<PanelTabloDesafioData>().ShowInfo(transactionData, challengeContainer.challengeData.TeamComplete, Close, challengeContainer.challengeData.canAcceptChallenge, 
                            challengeContainer.challengeData.challengeFree,challengeContainer.challengeData.challengeFreeMessage,challengeContainer.challengeData.canAcceptChallengeMessage);
                        postedChallenges.Add(prefab);
                    } 
                }
                else
                {
                    SetSpinnerState(false);
                }

            }, error =>
            {
                onFailed.Invoke();
                SetSpinnerState(false);
            });
        }

        /// <summary>
        /// Llama a la alerta de confirmaci�n de creaci�n de desafio
        /// </summary>
        private void CreateChallenge()
        {
            
            if (challengeContainer.challengeData.TeamComplete)
            {
                if (challengeContainer.challengeData.canPostChallenge)
                {
                    if (challengeContainer.challengeData.challengeFree)
                    {
                        byte[] tempBytes;
                        tempBytes = System.Text.Encoding.Default.GetBytes(mensaje);
                        string message = System.Text.Encoding.UTF8.GetString(tempBytes);
                        ACBSingleton.Instance.AlertPanel.SetupPanel(createChallengeText, challengeContainer.challengeData.challengeFreeMessage, true, CreateAlertChallenge);
                    }
                    else
                    {
                        byte[] tempBytes;
                        tempBytes = System.Text.Encoding.Default.GetBytes(mensaje);
                        string message= System.Text.Encoding.UTF8.GetString(tempBytes);
                        ACBSingleton.Instance.AlertPanel.SetupPanel(createChallengeText, message + ACBSingleton.Instance.GameData.costChallenge + " acbCoins", true, CreateAlertChallenge);
                    }
                  
                }
                else
                {
                    panelOpener.popupPrefab = challengeTeamImcompleteAlertPrefab;
                    panelOpener.OpenPopup();
                    panelOpener.popup.GetComponent<ChallengeIncompleteTeamPanel>().OpenAlertNoCanPostChallenge(challengeContainer.challengeData.canPostChallengeMessage);
                }
            }
            else
            {
                panelOpener.popupPrefab = challengeTeamImcompleteAlertPrefab;
                panelOpener.OpenPopup();
                panelOpener.popup.GetComponent<ChallengeIncompleteTeamPanel>().OpenAlert();
            }
        }

        /// <summary>
        /// Verifica los datos del desafio a crear
        /// </summary>
        private void CreateAlertChallenge()
        {
            Debug.Log("Creating Challenge");
            createChallengeButton.gameObject.SetActive(false);
            SetSpinnerState(true);
            WebProcedure.Instance.PostCreateChallenge((obj) =>
            {
                OnSuccess(obj);
                Debug.Log("Challenge created");
                
            }, (error) =>
                ACBSingleton.Instance.AlertPanel.SetupPanel(CreationErrorText, "", false, null)); 

        }

        /// <summary>
        /// M�todo que se ejecuta cuando el desafio ha sido correctamente publicado
        /// </summary>
        /// <param name="obj">Datos del desafio publicado devueltos desde backend</param>
        private void OnSuccess(DataSnapshot obj)
        {
            Debug.Log(obj.RawJson);
            if (obj.Code == 0 || obj.Code == 200)
            {
                Firebase.Analytics.FirebaseAnalytics.LogEvent("deal_publish_ok");
                Debug.Log("Analytic deal_publish_ok logged");
                Debug.Log("Desafio");
                ACBSingleton.Instance.AlertPanel.SetupPanel(obj.MessageCustom, "", false, CallInfo, null, 0, "Volver");
            }
            else
                ACBSingleton.Instance.AlertPanel.SetupPanel(obj.MessageCustom, "", false, null, null, 0, "Volver");
                SetSpinnerState(false);
        }

        /// <summary>
        /// Activa o desactiva el spinner de carga
        /// </summary>
        /// <param name="state">Estado de activaci�n del spinner</param>
        private void SetSpinnerState(bool state)
        {
            GameObject spinner = GameObject.Find("Spinner_TablonDesafio");
            for (int i = 0; i < spinner.transform.childCount; i++)
            {
                spinner.transform.GetChild(i).gameObject.SetActive(state);
            }
        }

        #endregion
    }
}

