<%@ Page Title="" Language="C#" MasterPageFile="~/Default.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="_7302AccountingNote.LoginPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">


    <%--    <asp:PlaceHolder ID="plcLogin" runat="server" Visible="false">--%>
    <table border="0" align="center" valign="center">
        <tr align="center">
            <td>
                <asp:Label ID="Label1" runat="server"><h2>用戶登入</h2></asp:Label>
            </td>
        </tr>
        <tr align="center">
            <td>
                <p class="auto-style3">
                    Fill in the Account and PWD to log in this awesome AccountingNote System.
                </p>
            </td>
        </tr>
        <tr align="center">
            <td>
                <img src="Images/account.png" style="height: 20px; width: 20px" id="icon1" /> 帳號 &nbsp &nbsp
                <asp:TextBox ID="txtAccount" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr align="center">
            <td>
                <img src="Images/password.png" style="height: 20px; width: 20px" id="icon2" /> 密碼 &nbsp &nbsp
            <asp:TextBox ID="txtPWD" runat="server" TextMode="Password"></asp:TextBox>
            </td>
        </tr>
        <tr align="center">
            <td>
                <asp:Button ID="btnLogin" runat="server" Text="登入" OnClick="btnLogin_Click" /><br />
                <asp:Literal ID="ltlMsg" runat="server"></asp:Literal><br />
                
                新增測試項目<br />
                <asp:Button ID="btnNewAccount" runat="server" Text="申請帳號" OnClick="btnNewAccount_Click" />
                <asp:Button ID="btnNewPWD" runat="server" Text="忘記密碼"  OnClick="btnNewPWD_Click"/>
                
                
            </td>
        </tr>
        <%--</asp:PlaceHolder>--%>
    </table>

</asp:Content>
