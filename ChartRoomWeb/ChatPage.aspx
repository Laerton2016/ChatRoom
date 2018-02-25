<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ChatPage.aspx.cs" Inherits="ChatPage" %>
<%@ Register TagPrefix="UC" Src="~/controle/Chat.ascx" TagName="Chat" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>Sala de Chat</title>
</head>
<body>
    <h1>Sala de Chat</h1>
    <form id="form1" runat="server" onsubmit="return false;">
        <UC:Chat ID="Chat1" runat="server"/>
    </form>
</body>
</html>
