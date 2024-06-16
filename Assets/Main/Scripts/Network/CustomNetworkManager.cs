using UnityEngine;
using Mirror;
using UnityEngine.SceneManagement;
using System.Net.Sockets; 
using System.Net.NetworkInformation;
using NetworkDiscoveryUnity;

public class CustomNetworkManager : NetworkManager
{
    public static CustomNetworkManager Instance;
    public string LocalHost;
    public NetworkDiscovery networkDiscovery;

    public override void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        } 
        else
        {
            Destroy(gameObject);
        } 
    }

    public override void Start()
    {
        networkDiscovery = GetComponent<NetworkDiscovery>();
    }


 
    public void LoadMainMenu()
    {
        StopHost();
        SceneManager.LoadScene("MainMenu");
    }

    public void CreateHost()
    {
        GetLocalIPAddress();
    }
    string GetLocalIPAddress()
    {
        string ipAddress = "";
        foreach (var netInterface in NetworkInterface.GetAllNetworkInterfaces())
        {
            foreach (var addrInfo in netInterface.GetIPProperties().UnicastAddresses)
            {
                if (addrInfo.Address.AddressFamily == AddressFamily.InterNetwork)
                {
                    ipAddress = addrInfo.Address.ToString();
                    networkAddress = ipAddress;

                    Debug.Log(networkAddress);
                    networkDiscovery.EnsureServerIsInitialized();
                    StartHost(); 
                    break;
                }
            }
            if (!string.IsNullOrEmpty(ipAddress))
                break;
        }
        return ipAddress;
    }
    public void JoinHost()
    {
        networkDiscovery.SendBroadcast();
        networkDiscovery.onReceivedServerResponse.AddListener((NetworkDiscovery.DiscoveryInfo info) =>
        {
            Debug.Log (info.EndPoint.Address.ToString());
            networkAddress = info.EndPoint.Address.ToString();
            StartClient();
        });
    }  

    public override void OnServerAddPlayer(NetworkConnectionToClient conn)
    {
        base.OnServerAddPlayer(conn);
        GameManager.Instance.CMDAddPlayer(conn.identity);
    }
}

public static class CustomNetworkWriterExtensions
{
    public static void WriteSpriteRenderer(this NetworkWriter writer, SpriteRenderer spriteRenderer)
    {
        writer.WriteSprite(spriteRenderer.sprite);
        writer.WriteColor(spriteRenderer.color);
    }

    public static void WriteCardsType(this NetworkWriter writer, CardsType cardsType)
    {
        writer.WriteInt((int)cardsType);
    } 
}

public static class CustomNetworkReaderExtensions
{
    public static SpriteRenderer ReadSpriteRenderer(this NetworkReader reader)
    {
        SpriteRenderer spriteRenderer = new SpriteRenderer();
        spriteRenderer.sprite = reader.ReadSprite();
        spriteRenderer.color = reader.ReadColor();
        return spriteRenderer;
    }

    public static CardsType ReadCardsType(this NetworkReader reader)
    {
        return (CardsType)reader.ReadInt();
    } 
}