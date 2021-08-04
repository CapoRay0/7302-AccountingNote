<%@ Page Title="" Language="C#" MasterPageFile="~/SystemAdmin/SystemAdmin.Master" AutoEventWireup="true" CodeBehind="UserPassword.aspx.cs" Inherits="_7302AccountingNote.SystemAdmin.AccountManagement.UserPassword" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <table>
        <tr>
            <td colspan="2">
                <h3>會員管理</h3>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblAccount" runat="server" Text="帳號"></asp:Label>
                <asp:Literal ID="ltAccount" runat="server"></asp:Literal><br />
                <asp:Label ID="lblPwdOri" runat="server" Text="原密碼"></asp:Label>
                <asp:TextBox ID="txtPwdOri" runat="server" TextMode="Password"></asp:TextBox><br /><br />
                <asp:Label ID="lblPwdNew" runat="server" Text="新密碼"></asp:Label>
                <asp:TextBox ID="txtPwdNew" runat="server" TextMode="Password"></asp:TextBox><br />
                <asp:Label ID="lblPwdNewConfirm" runat="server" Text="確認新密碼"></asp:Label>
                <asp:TextBox ID="txtPwdNewConfirm" runat="server" TextMode="Password"></asp:TextBox><br />
                <asp:Button ID="btnSave" runat="server" Text="儲存" OnClick="btnSave_Click" />
                
                <br />
                <asp:Literal ID="ltmsg" runat="server"></asp:Literal>
            </td>
        </tr>
    </table>

</asp:Content>
