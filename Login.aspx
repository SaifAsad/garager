<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="Pages_Account_Login" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
  
    <h4>
        <asp:Literal ID="litStatus" runat="server" Text=""></asp:Literal>
    </h4>

    <h4>Log In</h4>
    <hr />
    <asp:Literal runat="server" ID="litErrorMsg" Text="Invalid username or password." Visible="false" />
    <asp:Label runat="server" AssociatedControlID="txtUserName">User name</asp:Label>
    <br />
    <br />
    <asp:TextBox runat="server" ID="txtUserName" CssClass="form-control" Width="300px" />
    <br />
    <br />
    <asp:Label runat="server" AssociatedControlID="txtPassword">Password</asp:Label>
    <br />
    <br />
    <asp:TextBox runat="server" ID="txtPassword" TextMode="Password" CssClass="form-control" Width="300px" />
    <br />
    <asp:Button ID="btnSignIn" runat="server" Text="Log in" OnClick="btnSignIn_OnClick" CssClass="btn btn-primary btn-lg" />
</asp:Content>

