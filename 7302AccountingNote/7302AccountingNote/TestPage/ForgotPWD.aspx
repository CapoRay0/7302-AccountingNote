<%@ Page Title="" Language="C#" MasterPageFile="~/Default.Master" AutoEventWireup="true" CodeBehind="ForgotPWD.aspx.cs" Inherits="_7302AccountingNote.TestPage.ForgotPWD" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    原帳號<asp:TextBox ID="txtOrigAccount" runat="server"></asp:TextBox><br />
    Email<asp:TextBox ID="txtEmailCheck" runat="server"></asp:TextBox><br />
    姓名<asp:TextBox ID="txtNameCheck" runat="server"></asp:TextBox><br />
    
    會員等級<asp:DropDownList ID="DropDownList1" runat="server">
        <asp:ListItem id="Manager" Value="0">管理員</asp:ListItem>
        <asp:ListItem id="Normal" Value="1">一般會員</asp:ListItem>
        </asp:DropDownList><br /><br />
    新密碼<asp:TextBox ID="txtNewPWD" runat="server"></asp:TextBox><br />
    確認新密碼<asp:TextBox ID="NewPWDConfirm" runat="server"></asp:TextBox><br />
    <asp:Button ID="btnConfirm" runat="server" Text="確認" OnClick="btnConfirm_Click"/><br />
    <asp:Literal ID="WrongShow" runat="server"></asp:Literal>
</asp:Content>
