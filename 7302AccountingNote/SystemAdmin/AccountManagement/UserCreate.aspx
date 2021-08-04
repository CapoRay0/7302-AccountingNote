<%@ Page Title="" Language="C#" MasterPageFile="~/SystemAdmin/SystemAdmin.Master" AutoEventWireup="true" CodeBehind="UserCreate.aspx.cs" Inherits="_7302AccountingNote.SystemAdmin.AccountManagement.UserCreate" %>

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
                <asp:Label ID="lblName" runat="server" Text="姓名"></asp:Label>
                <asp:TextBox ID="txtName" runat="server"></asp:TextBox><br />
                <asp:Label ID="lblEmail" runat="server" Text="Email"></asp:Label>
                <asp:TextBox ID="txtEmail" runat="server" TextMode="Email"></asp:TextBox><br />
                <asp:Label ID="lblUserLevel" runat="server" Text="等級"></asp:Label>
                <asp:Literal ID="ltUserLevel" runat="server"></asp:Literal><br />
                <asp:Label ID="lblCreateDate" runat="server" Text="建立時間"></asp:Label>
                <asp:Literal ID="ltCreateDate" runat="server"></asp:Literal><br />

                <asp:Button ID="btnSave" runat="server" Text="儲存" OnClick="btnSave_Click" />
                &nbsp;&nbsp;
                    
                <%--CommandArgument='<%# Eval("ID") %>' CommandName="DeleteAccounting--%>
                <br />
                <asp:Literal ID="ltmsg" runat="server"></asp:Literal>
            </td>
        </tr>
    </table>
</asp:Content>
