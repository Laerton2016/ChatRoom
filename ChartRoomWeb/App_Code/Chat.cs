using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
using ChatRoomCore.model;
using ChatRoomWeb;

/// <summary>
/// Descrição resumida de Chat
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
[ToolboxItem(false)]
[ScriptService]
// Para permitir que esse serviço da web seja chamado a partir do script, usando ASP.NET AJAX, remova os comentários da linha a seguir. 
// [System.Web.Script.Services.ScriptService]
public class Chat : System.Web.Services.WebService
{

    public Chat()
    {

        //Remova os comentários da linha a seguir se usar componentes designados 
        //InitializeComponent(); 
    }

    private void CheckSiteSecuryty()
    {
        if (new SessionWrapper(HttpContext.Current.Session).User == null)
        {
            throw  new SecurityException("Sessão perdida. Efetue novamente o login para o entrar no chat.");
        }
    }

    [WebMethod(EnableSession = true)]
    public int EnterRoom(int roomId)
    {
        CheckSiteSecuryty();
        ChatManager manager = new ChatManager();
        return manager.EnterRoom(roomId);
    }

    [WebMethod(EnableSession = true)]
    public ChatResponse CheckUsers()
    {
        CheckSiteSecuryty();
        ChatManager manager = new ChatManager();
        return manager.CheckUsers();
    }

    [WebMethod(EnableSession = true)]
    public ChatResponse CheckMessages(int lastMessage)
    {
        CheckSiteSecuryty();
        ChatManager manager = new ChatManager();
        return manager.CheckMessages(lastMessage);
    }

    [WebMethod(EnableSession = true)]
    public ChatResponse SendMessage(string message, int lasMessageId)
    {
        CheckSiteSecuryty();
        ChatManager manager = new ChatManager();
        return manager.SendMessage(message, lasMessageId);
    }



}
