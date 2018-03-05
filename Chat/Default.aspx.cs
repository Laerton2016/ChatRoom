using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Principal;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void OnClick(object sender, EventArgs e)
    {
        if (login.Value == null)
        {
            string script = "alert(\"Login não pode ser em branco.\");";
            ScriptManager.RegisterStartupScript(this, GetType(),
                "ServerControlScript", script, true);
        }
       //wc.OpenRead("Chat.aspx?u=1&amp;n=" + login.Value);
        Server.Transfer("ChatPage.aspx?u=1&amp;n=" + login.Value);
    }
}