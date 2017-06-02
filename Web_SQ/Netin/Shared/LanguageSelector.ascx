<%@ Control Language="C#" AutoEventWireup="true"
    Inherits="Nt.Pages.Shared.UcLanguageSelector" %>
<script type="text/javascript">
    function switchLanguage(id) {
        var currentLang=<%=LanguageID%>;
        if(id==currentLang)
            return;
        parent.showMask();
        ntAjax({
            method: 'SwitchLanguage',
            data: '{"languageID":"' + id + '"}',
            success: function () {
                var mainUrl=parent.mainFrame.location.href.toLowerCase();
                if(mainUrl.indexOf('id=')>0)
                    parent.mainFrame.location.href="/netin/system/SystemDescription.aspx";
                else
                    parent.mainFrame.location.href=parent.mainFrame.location.href;
                parent.topFrame.location.reload();
                parent.removeMask();
                parent.ntAlert('切换成功');
            }
        })
    }
</script>
<span class="nt-language-selector">

    <%if (DataSource != null && DataSource.Rows.Count > 1)
      {
          foreach (System.Data.DataRow item in DataSource.Rows)
          {
              if (item["Id"].ToString() == NtContext.Current.LanguageID.ToString())
                  Response.Write("<a href=\"javascript:;\"><img style=\"border:2px solid #FF9A5A\" alt=\"" + item["Name"] + "\" src=\"/Netin/Content/Flags/" + item["LanguageCode"] + ".png\" /></a>&nbsp;&nbsp;");
              else
                  Response.Write("<a href=\"javascript:;\" onclick=\"switchLanguage(" + item["Id"] + ");\"><img alt=\"" + item["Name"] + "\" src=\"/Netin/Content/Flags/" + item["LanguageCode"] + ".png\" /></a>&nbsp;&nbsp;");
          }
          Response.Write("当前语言:" + WorkingLang.Name);
      }
    %>
    
</span>
