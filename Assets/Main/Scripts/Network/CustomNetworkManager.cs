using UnityEngine;
using Mirror;
using UnityEngine.SceneManagement;

public class CustomNetworkManager : NetworkManager
{
    public static CustomNetworkManager Instance;

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

    public void LoadMainMenu()
    {
        StopHost();
        SceneManager.LoadScene("MainMenu");
    }

    public void CreateHost()
    {
        StartHost();
    }

    public void JoinHost()
    {
        StartClient();
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