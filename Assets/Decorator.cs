using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class WallMaterial
{
    public string uiName;
    public int price;
    public Texture material;
    public int temperatureChange;

    public List<WallColor> possibleColors;
    [HideInInspector]public WallColor currentColor;
}

[System.Serializable]
public class WallColor
{
    public string uiName;
    public int price;
    public Color color;
    public int temperatureChange;
}

[System.Serializable]
public class WallInsulation
{
    public string uiName;
    public int price;
    public int temperatureChange;
}
[System.Serializable]
public class Decorator : MonoBehaviour
{
    [Header("Whole building references")]
    public GameObject wallObject;

    public List<WallMaterial> possibleMaterials = new List<WallMaterial>();
    public List<WallInsulation> possibleInsulations = new List<WallInsulation>();

    private Material[] allMaterials;
    private List<Material> innerWall = new List<Material>();
    private List<Material> outerWall = new List<Material>();

    private WallMaterial currentInteriorMaterial;
    private WallMaterial currentExteriorMaterial;
    private WallInsulation currentInsulation;

    [SerializeField] private GameObject optionPrefab;
    [SerializeField] private GameObject materialWindowContent_Inner;
    [SerializeField] private GameObject colorWindowContent_Inner;
    [SerializeField] private GameObject insulationWindowContent_Inner;

    [SerializeField] private GameObject materialWindowContent_Outer;
    [SerializeField] private GameObject colorWindowContent_Outer;
    [SerializeField] private GameObject insulationWindowContent_Outer;


    enum WindowPos { East, West, North, South }
    [SerializeField] WindowPos dataType;
    // Start is called before the first frame update
    void Start()
    { 
        // events
        //EventManager.Instance.OnTPressed += EnableTransparent;

        allMaterials = wallObject.GetComponent<Renderer>().materials;

        foreach (Material mat in allMaterials)
        {
            if (mat.name.Contains("Int"))
            {
                innerWall.Add(mat);
            }
            else if (mat.name.Contains("Ext"))
            {
                outerWall.Add(mat);
            }
        }

        
    }


    public void PaintInner(WallColor wColor)
    {
        currentInteriorMaterial.currentColor = wColor;
        GenerateMaterialWindowInner();
        if (GameManager.Instance.points > wColor.price)
        {
            GameManager.Instance.ChangeTemperatureForAll(wColor.temperatureChange);
            foreach (Material mat in innerWall)
            {
                mat.color = wColor.color;
            }
            GameManager.Instance.points -= wColor.price;
        }
        else
        {
            Debug.Log(wColor.price - GameManager.Instance.points + " points is not enough!");
        }

    }
        
    public void PaintOuter(WallColor wColor)
    {
        currentExteriorMaterial.currentColor = wColor;
        GenerateMaterialWindowOuter();
        if (GameManager.Instance.points > wColor.price)
        {
            GameManager.Instance.ChangeTemperatureForAll(wColor.temperatureChange);
            foreach (Material mat in outerWall)
            {
                mat.color = wColor.color;
            }
            GameManager.Instance.points -= wColor.price;
        }
        else
        {
            Debug.Log(wColor.price - GameManager.Instance.points + " points is not enough!");
        }
    }

    public void ApplyMaterialOuter(WallMaterial wMaterial)
    {
        currentExteriorMaterial = wMaterial;
        currentExteriorMaterial.currentColor = null;
        GenerateMaterialWindowOuter();

        if (GameManager.Instance.points > wMaterial.price)
        {
            GameManager.Instance.ChangeTemperatureForAll(wMaterial.temperatureChange);
            foreach (Material mat in outerWall)
            {
                mat.color = Color.gray;
                mat.EnableKeyword("_DETAIL_MULX2");
                mat.SetTexture("_DetailAlbedoMap", wMaterial.material);
            }
            GameManager.Instance.points -= wMaterial.price;
        }
        else
        {
            Debug.Log(wMaterial.price - GameManager.Instance.points + " points is not enough!");
        }
    }

    public void ApplyInsulation(WallInsulation wInsulation)
    {
        GameManager.Instance.ChangeTemperatureForAll(wInsulation.temperatureChange);
        currentInsulation = wInsulation;
        GenerateMaterialWindowInner();
        GenerateMaterialWindowOuter();
    }

    public void ApplyMaterialInner(WallMaterial wMaterial)
    {
        currentInteriorMaterial = wMaterial;
        currentInteriorMaterial.currentColor = null;
        GenerateMaterialWindowInner();

        if (GameManager.Instance.points > wMaterial.price)
        {
            GameManager.Instance.ChangeTemperatureForAll(wMaterial.temperatureChange);
            foreach (Material mat in innerWall)
            {
                mat.color = Color.gray;
                mat.EnableKeyword("_DETAIL_MULX2");
                mat.SetTexture("_DetailAlbedoMap", wMaterial.material);
            }
            GameManager.Instance.points -= wMaterial.price;
        }
        else
        {
            Debug.Log(wMaterial.price - GameManager.Instance.points + " points is not enough!");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    #region  UIUpdateInner

    public void GenerateMaterialWindowInner()
    {
        // generate materials
        foreach (Transform child in materialWindowContent_Inner.transform)
        {
            GameObject.Destroy(child.gameObject);
        }
        
        foreach (WallMaterial wallMaterial in possibleMaterials)
        {
            
            GameObject optionInstance = Instantiate(optionPrefab, materialWindowContent_Inner.transform);
            optionInstance.GetComponent<OptionManager>().InitializeContent(wallMaterial.uiName, wallMaterial.price);
            if (currentInteriorMaterial != null && currentInteriorMaterial.uiName == wallMaterial.uiName)
            {
                optionInstance.GetComponent<OptionManager>().isPressed = true;
            }
            optionInstance.GetComponentInChildren<Button>().onClick.AddListener( delegate { ApplyMaterialInner(wallMaterial); });
        }

        // generate colors
        if (currentInteriorMaterial != null)
        {
            foreach (Transform child in colorWindowContent_Inner.transform)
            {
                GameObject.Destroy(child.gameObject);
            }
            foreach (WallColor wallColor in currentInteriorMaterial.possibleColors)
            {
                GameObject optionInstance = Instantiate(optionPrefab, colorWindowContent_Inner.transform);
                optionInstance.GetComponent<OptionManager>().InitializeContent(wallColor.uiName, wallColor.price);
                if (currentInteriorMaterial.currentColor != null && currentInteriorMaterial.currentColor.uiName == wallColor.uiName)
                {
                    optionInstance.GetComponent<OptionManager>().isPressed = true;
                }
                optionInstance.GetComponentInChildren<Button>().onClick.AddListener(delegate { PaintInner(wallColor); });
            }

        }


        // generate insulation
        foreach (Transform child in insulationWindowContent_Inner.transform)
        {
            GameObject.Destroy(child.gameObject);
        }
        foreach (WallInsulation wallInsulation in possibleInsulations)
        {
            
            GameObject optionInstance = Instantiate(optionPrefab, insulationWindowContent_Inner.transform);
            optionInstance.GetComponent<OptionManager>().InitializeContent(wallInsulation.uiName, wallInsulation.price);
            if (currentInsulation == wallInsulation)
            {
                optionInstance.GetComponent<OptionManager>().isPressed = true;
            }
            optionInstance.GetComponentInChildren<Button>().onClick.AddListener(delegate { ApplyInsulation(wallInsulation); });

        }
    }

    public void GenerateMaterialWindowOuter()
    {
        // generate materials
        foreach (Transform child in materialWindowContent_Outer.transform)
        {
            GameObject.Destroy(child.gameObject);
        }

        foreach (WallMaterial wallMaterial in possibleMaterials)
        {

            GameObject optionInstance = Instantiate(optionPrefab, materialWindowContent_Outer.transform);
            optionInstance.GetComponent<OptionManager>().InitializeContent(wallMaterial.uiName, wallMaterial.price);
            if (currentExteriorMaterial != null && currentExteriorMaterial.uiName == wallMaterial.uiName)
            {
                optionInstance.GetComponent<OptionManager>().isPressed = true;
            }
            optionInstance.GetComponentInChildren<Button>().onClick.AddListener(delegate { ApplyMaterialOuter(wallMaterial); });
        }

        // generate colors
        if (currentExteriorMaterial != null)
        {
            foreach (Transform child in colorWindowContent_Outer.transform)
            {
                GameObject.Destroy(child.gameObject);
            }
            foreach (WallColor wallColor in currentExteriorMaterial.possibleColors)
            {
                GameObject optionInstance = Instantiate(optionPrefab, colorWindowContent_Outer.transform);
                optionInstance.GetComponent<OptionManager>().InitializeContent(wallColor.uiName, wallColor.price);
                if (currentExteriorMaterial.currentColor != null && currentExteriorMaterial.currentColor.uiName == wallColor.uiName)
                {
                    optionInstance.GetComponent<OptionManager>().isPressed = true;
                }
                optionInstance.GetComponentInChildren<Button>().onClick.AddListener(delegate { PaintOuter(wallColor); });
            }

        }


        // generate insulation
        foreach (Transform child in insulationWindowContent_Outer.transform)
        {
            GameObject.Destroy(child.gameObject);
        }
        foreach (WallInsulation wallInsulation in possibleInsulations)
        {

            GameObject optionInstance = Instantiate(optionPrefab, insulationWindowContent_Outer.transform);
            optionInstance.GetComponent<OptionManager>().InitializeContent(wallInsulation.uiName, wallInsulation.price);
            if (currentInsulation == wallInsulation)
            {
                optionInstance.GetComponent<OptionManager>().isPressed = true;
            }
            optionInstance.GetComponentInChildren<Button>().onClick.AddListener(delegate { ApplyInsulation(wallInsulation); });

        }
    }
    #endregion
}
