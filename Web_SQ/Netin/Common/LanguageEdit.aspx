<%@ Page Title="" Language="C#" MasterPageFile="~/Netin/Layout.master" AutoEventWireup="true"
    Inherits="Nt.Pages.Common.LanguageEdit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CPH_Head" runat="Server">
    <style type="text/css">
        .flags { background-color: #F7F7F7; border: 1px solid #999999; display: none; height: 150px; padding-left: 5px; position: absolute; width: 800px; z-index: 200000; }
            .flags ul { width: 800px; position: relative; }
                .flags ul li { float: left; display: block; width: 16px; height: 11px; margin-right: 5px; margin-bottom: 5px; }
    </style>
    <script type="text/javascript">
        function selectFlags(sender) {
            if (!$(sender).data('flags-display')) {
                $('div.flags').show();
                $(sender).data('flags-display', 1);
            } else {
                $('div.flags').hide();
                $(sender).data('flags-display', 0);
            }
        }

        $(function () {
            $('div.flags img').click(function () {
                $('#flag-img').attr('src', $(this).attr('src'));
                $('#flag-img').attr('alt', $(this).attr('alt'));
                $('input[name="LanguageCode"]').val($(this).attr('alt'));
                $('div.flags').hide();
                $('#flag-img').data('flags-display', 0);
            })
        })

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPH_Body" runat="Server">
    <div class="admin-main-wrap">
        <div class="admin-main">
            <div class="admin-main-title">
                <%=EditTitlePrefix %>语言版本信息
            </div>

            <form method="post" name="EditForm" id="EditForm" action="<%=LocalUrl %>" onsubmit="<%=OnSubmitCall() %>">
                <table class="admin-table">
                    <tr>
                        <td class="left">语言符号：</td>
                        <td class="right">
                            <input type="hidden" name="LanguageCode" value="<%=Model.LanguageCode %>" />
                            <img id="flag-img" src="/Netin/Content/Flags/<%=Model.LanguageCode  %>.png" alt="<%=Model.LanguageCode %>" onclick="selectFlags(this)" />
                            <div class="flags">
                                <ul>
                                    <%foreach (var item in LanguageCodeProvider.LanguageCodes)
                                      {
                                          Response.Write("<li><img src=\"/Netin/Content/Flags/" + item + ".png\" alt=\"" + item + "\"/></li>");
                                      } %>
                                </ul>
                            </div>
                        </td>
                    </tr>

                    <tr>
                        <td class="left">名称：</td>
                        <td class="right">
                            <input type="text" class="input-text" name="Name" maxlength="255" value="<%=Model.Name %>" />
                        </td>
                    </tr>

                    <tr>
                        <td class="left">排序：</td>
                        <td class="right">
                            <input type="text" class="input-int32" maxlength="5" name="DisplayOrder" value="<%=Model.DisplayOrder==0?MaxID:Model.DisplayOrder %>" />
                        </td>
                    </tr>

                    <tr>
                        <td class="left">资源文件的路径：</td>
                        <td class="right">
                            <input type="text" class="input-text" maxlength="255" name="ResxPath" value="<%=Model.ResxPath %>" />
                        </td>
                    </tr>

                    <tr>
                        <td class="left">发布：</td>
                        <td class="right">
                            <%=HtmlHelper.CheckBox("发布",EnsureEdit?Model.Published:true,"Published", new { })%>
                        </td>
                    </tr>

                    <tr style="height: 60px;">
                        <td align="center" colspan="2">
                            <input type="submit" class="admin-button" value="保存" />
                            <input type="button" class="admin-button" onclick="<%=GoBackScript()%>" value="返回" />
                            <input type="hidden" name="Id" value="<%=NtID %>" />
                        </td>
                    </tr>
                </table>
            </form>

        </div>
    </div>
</asp:Content>

