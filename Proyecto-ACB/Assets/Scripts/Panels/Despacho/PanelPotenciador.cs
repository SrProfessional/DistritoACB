using Data;
using System;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Controla el panel donde se muestran los potenciadores que tiene el jugador
/// </summary>
public class PanelPotenciador : MonoBehaviour
{
    [SerializeField] [Tooltip("Bot�n de cerrar panel")]
    private Button button;
    [SerializeField] [Tooltip("Muestra estad�sticas del potenciador")]
    private Text textStat;
    [SerializeField] [Tooltip("Imagen del potenciador")]
    public Image imageBooter;
    [HideInInspector] [Tooltip("Informaci�n del potenciador")]
    public BoosterData.BoosterItemData boostData ;
    
    /// <summary>
    /// Muestra la informaci�n del potenciador
    /// </summary>
    /// <param name="boosterdata">Datos del potenciador</param>
    /// <param name="onshowconfirmation">Acci�n que se ejecuta al mostrar la confirmaci�n</param>
    public void ShowInfo(BoosterData.BoosterItemData boosterdata, Action onshowconfirmation = null)
    {
        if (boosterdata != null) boostData = boosterdata;
        textStat.text = boosterdata.value;
        if (!string.IsNullOrEmpty(boosterdata.path_img))
        {
            if (imageBooter)
            {
                imageBooter.sprite = boosterdata.GetSprite();
            }
            if (onshowconfirmation != null)
            {
                button.onClick.AddListener(() =>
                {
                    onshowconfirmation?.Invoke();
                });
            }            
        }
    }
}
