<%@ Page Title="" Language="C#" MasterPageFile="~/Default.Master" AutoEventWireup="true" CodeBehind="NewAccount.aspx.cs" Inherits="_7302AccountingNote.TestPage.NewAccount" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Label ID="Label1" runat="server" Text="創建新帳號"></asp:Label><br />
    帳號<asp:TextBox ID="txtNewAccount" runat="server"></asp:TextBox><br />
    密碼<asp:TextBox ID="txtNewPWD" runat="server"></asp:TextBox><br />
    Email<asp:TextBox ID="txtEmail" runat="server"></asp:TextBox><br />
    姓名<asp:TextBox ID="txtName" runat="server"></asp:TextBox><br />
    
    

    <asp:Button ID="btnConfirm" runat="server" Text="確認" OnClick="btnConfirm_Click" />
</asp:Content>
