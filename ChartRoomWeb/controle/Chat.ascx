<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Chat.ascx.cs" Inherits="controle_Chat" %>
<asp:ScriptManager id ="sm" runat="server">
    <Services>
        <asp:ServiceReference Path="~/Chat/Services/Chat.asmx"/>
    </Services>
</asp:ScriptManager>
<script type="text/javascript" src="~/js/Chat.js"/>
<div id="chatPanel">
    <div id="chatBody">
        <div id="messagePanel">
            <ul id="messageList"></ul>
        </div>
        <div id="userPainel">

        </div>
        <div id="chatFooter">
            <input type="text" id="txtMessage" onkeypress="return Codeproject.Chat.CheckSend(event); "/>
            <input type="button" id="cmdSend" value="Send" style="display: none;" onclick="Codeproject.Chat.SendMessage; return false;"/>
        </div>
    </div>
</div>
<div id="chatDebugPanel" style="display: none;">
    <span><!-- required --></span>
</div>
<script type="text/javascript">
    window.onload = function() {
        Codeproject.Chat.Init();
    }
</script>
