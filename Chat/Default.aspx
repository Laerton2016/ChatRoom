﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="SRChat.Default1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>ChatRoom - Login </title>
    <link rel="stylesheet" href="Styles/reset.css"/>
    <link rel="stylesheet" href="Styles/style.css" media="screen" type="text/css" />
    <script src="Scripts/index.js"></script>
</head>
<body>
<form id="form1" runat="server">
        
    <div class="wrap">
        <div><h3 style= "text-align: center; color: white ">ChatRoom Login</h3></div>
        <div class="avatar">
            <img  src="Styles/Images/avatar.png"/>
        </div>
        <input id="login" name="login" type="text" placeholder="username" required runat="server"/>
        <div class="bar">
            <i></i>
        </div>
        <input id="password" name="password" type="password" placeholder="password" required runat="server"/>
        <a href="" class="forgot_link">esqueceu ?</a>
        <asp:Button CssClass="Button"  runat="server" OnClick="OnClick" Text="Entrar"/>
    </div>

        

</form>
</body>
</html>