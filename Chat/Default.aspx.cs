using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SRChat
{
    public partial class Default1 : System.Web.UI.Page
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
            Server.Transfer("SRChatClient.aspx?u=1&amp;n=" + login.Value);
        }
        public String Login { get { return login.Value; }  }
    }
}