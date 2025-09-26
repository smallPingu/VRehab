using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

[System.Serializable]
public class ParamOveja
{
    public int cuentaAtras = 60;
    public float velocidadJuego = 1f;
}

[System.Serializable]
public class ParamCoches
{
    public float velocidadJuego = 1f;
    public float anguloMinimo = 25f;
}

[System.Serializable]
public class ParamArco
{
    public int cuentaAtras = 60;
    public float velocidadJuego = 1f;
    public int nivelInicial = 1;
}

public class ParametrosInicial : MonoBehaviour
{
    public static ParametrosInicial instance;

    public ParamOveja paramOrdenaOvejas;
    public ParamCoches paramRotacionCoches;
    public ParamArco paramTiroAlArco;

    public Slider sliderCuentaAtras;
    public Slider sliderVelocidadJuego;
    public Slider sliderAnguloMinimo;

    public TextMeshProUGUI cuentaAtrasText;
    public TextMeshProUGUI velocidadText;
    public TextMeshProUGUI anguloMinText;
    public TextMeshProUGUI nivelText;

    private const int VALOR_DEFECTO_CUENTA_ATRAS = 60;
    private const float VALOR_DEFECTO_VELOCIDAD = 1f;
    private const float VALOR_DEFECTO_ANGULO = 25f;
    private const int VALOR_DEFECTO_NIVEL = 1;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(this.gameObject);
    }

    void Start()
    {
        CargarSettingsInicializarUI();
    }

    private void CargarSettingsInicializarUI()
    {
        int cuentaAtrasGuardada = PlayerPrefs.GetInt("cuentaAtras", VALOR_DEFECTO_CUENTA_ATRAS);
        float velocidadGuardada = PlayerPrefs.GetFloat("velocidadJuego", VALOR_DEFECTO_VELOCIDAD);
        float anguloGuardado = PlayerPrefs.GetFloat("anguloMinimo", VALOR_DEFECTO_ANGULO);
        int nivelGuardado = PlayerPrefs.GetInt("nivelInicial", VALOR_DEFECTO_NIVEL);

        SetCuentaAtras_Data(cuentaAtrasGuardada);
        SetVelocidad_Data(velocidadGuardada);
        SetAnguloMin_Data(anguloGuardado);
        SetNivel_Data(nivelGuardado);

        sliderCuentaAtras.value = cuentaAtrasGuardada;
        sliderVelocidadJuego.value = velocidadGuardada;
        sliderAnguloMinimo.value = anguloGuardado;
    }

    public void OnCuentaAtrasFinEdit()
    {
        int valor = (int)sliderCuentaAtras.value;
        SetCuentaAtras_Data(valor);
        PlayerPrefs.SetInt("cuentaAtras", valor);
    }

    public void OnVelocidadFinEdit()
    {
        float valor = sliderVelocidadJuego.value;
        SetVelocidad_Data(valor);
        PlayerPrefs.SetFloat("velocidadJuego", valor);
    }

    public void OnAnguloMinFinEdit()
    {
        float valor = sliderAnguloMinimo.value;
        SetAnguloMin_Data(valor);
        PlayerPrefs.SetFloat("anguloMinimo", valor);
    }

    public void NivelPlus()
    {
        int nuevoNivel = paramTiroAlArco.nivelInicial + 1;
        if (nuevoNivel > 5)
        {
            nuevoNivel = 1;
        }
        SetNivel_Data(nuevoNivel);
        PlayerPrefs.SetInt("nivelInicial", nuevoNivel);
    }

    private void SetCuentaAtras_Data(int valor)
    {
        paramOrdenaOvejas.cuentaAtras = valor;
        paramTiroAlArco.cuentaAtras = valor;
        UpdateCuentaAtras_Text(valor);
    }

    private void UpdateCuentaAtras_Text(float valor)
    {
        if (cuentaAtrasText != null)
            cuentaAtrasText.text = "Cuenta Atrás: " + valor.ToString("F0");
    }

    private void SetVelocidad_Data(float valor)
    {
        paramOrdenaOvejas.velocidadJuego = valor;
        paramRotacionCoches.velocidadJuego = valor;
        paramTiroAlArco.velocidadJuego = valor;
        UpdateVelocidad_Text(valor);
    }

    private void UpdateVelocidad_Text(float valor)
    {
        if (velocidadText != null)
            velocidadText.text = "Velocidad: " + valor.ToString("F1");
    }

    private void SetAnguloMin_Data(float valor)
    {
        paramRotacionCoches.anguloMinimo = valor;
        UpdateAnguloMin_Text(valor);
    }

    private void UpdateAnguloMin_Text(float valor)
    {
        if (anguloMinText != null)
            anguloMinText.text = "Ángulo Min: " + valor.ToString("F0");
    }

    private void SetNivel_Data(int valor)
    {
        paramTiroAlArco.nivelInicial = valor;
        UpdateNivel_Text(valor);
    }

    private void UpdateNivel_Text(int valor)
    {
        if (nivelText != null)
            nivelText.text = "Nivel: " + valor;
    }
    
    public void ResetearAjustesPorDefecto()
    {
        SetCuentaAtras_Data(VALOR_DEFECTO_CUENTA_ATRAS);
        SetVelocidad_Data(VALOR_DEFECTO_VELOCIDAD);
        SetAnguloMin_Data(VALOR_DEFECTO_ANGULO);
        SetNivel_Data(VALOR_DEFECTO_NIVEL);

        sliderCuentaAtras.value = VALOR_DEFECTO_CUENTA_ATRAS;
        sliderVelocidadJuego.value = VALOR_DEFECTO_VELOCIDAD;
        sliderAnguloMinimo.value = VALOR_DEFECTO_ANGULO;

        PlayerPrefs.DeleteKey("cuentaAtras");
        PlayerPrefs.DeleteKey("velocidadJuego");
        PlayerPrefs.DeleteKey("anguloMinimo");
        PlayerPrefs.DeleteKey("nivelInicial");
        PlayerPrefs.Save();

        Debug.Log("Todos los ajustes han sido reseteados a sus valores por defecto.");
    }
}