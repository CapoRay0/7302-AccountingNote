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
                <asp:Label ID="lblGUID" runat="server" Text="辨識碼"></asp:Label>
                <asp:TextBox ID="txtGuid" runat="server"></asp:TextBox>

                <a href="https://www.guidgenerator.com/online-guid-generator.aspx" target="_blank">請點我創建GUID辨識碼</a><br />


                <asp:Label ID="lblAccount" runat="server" Text="帳號"></asp:Label>
                <asp:TextBox ID="txtAccount" runat="server"></asp:TextBox><br />



                <asp:Label ID="lblPWD" runat="server" Text="密碼"></asp:Label>
                <asp:TextBox ID="txtPWD" runat="server" TextMode="Password"></asp:TextBox><br />
                <asp:Label ID="lblPWDConfirm" runat="server" Text="密碼確認"></asp:Label>
                <asp:TextBox ID="txtPWDConfirm" runat="server" TextMode="Password"></asp:TextBox><br />

                <asp:Label ID="lblName" runat="server" Text="姓名"></asp:Label>
                <asp:TextBox ID="txtName" runat="server"></asp:TextBox><br />

                <asp:Label ID="lblEmail" runat="server" Text="Email"></asp:Label>
                <asp:TextBox ID="txtEmail" runat="server" TextMode="Email"></asp:TextBox><br />

                <asp:Label ID="lblUserLevel" runat="server" Text="等級"></asp:Label>

                <asp:Panel runat="server">
                    <asp:RadioButton ID="RadbtnnewManager" runat="server" Text="管理者" GroupName="newMember" />
                    <asp:RadioButton ID="RadbtnnewUser" runat="server" Text="一般會員" GroupName="newMember" />
                </asp:Panel>
                <br />

                <asp:Button ID="btnSave" runat="server" Text="儲存" OnClick="btnSave_Click" />
                &nbsp;&nbsp;
                    
                <%--CommandArgument='<%# Eval("ID") %>' CommandName="DeleteAccounting--%>
                <br />
                <asp:Literal ID="ltmsg" runat="server"></asp:Literal>
            </td>
        </tr>
    </table>
</asp:Content>
