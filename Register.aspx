<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Register.aspx.cs" Inherits="Pages_Account_Register" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <h4>Register a new user</h4>
    <hr />
    <p>
        <asp:Literal runat="server" ID="litStatusMessage" />
    </p>

    User name:<br />
    <asp:TextBox runat="server" ID="txtUserName" CssClass="form-control" Width="300px" /><br />

    Password:
    <br />
    <asp:TextBox runat="server" ID="txtPassword" TextMode="Password " CssClass="form-control" Width="300px" /><br />

    Confirm password:
    <br />
    <asp:TextBox runat="server" ID="txtConfirmPassword" TextMode="Password" CssClass="form-control" Width="300px" /><br />
    
    First Name:<br />
    <asp:TextBox runat="server" ID="txtFirstName" CssClass="form-control" Width="300px" /><br />
    
    Last Name:<br />
    <asp:TextBox runat="server" ID="txtLastName" CssClass="form-control" Width="300px" /><br />
    
    Address:<br />
    <asp:TextBox runat="server" ID="txtAddress" CssClass="form-control" Width="300px" /><br />
    
    Postal Code:<br />
    <asp:TextBox runat="server" ID="txtPostalCode" CssClass="form-control" Width="300px" /><br />

    <p>
        <asp:Button ID="btnRegister" runat="server" Text="Register" OnClick="btnRegister_Click" CssClass="btn btn-primary btn-lg" Width="150px" />
    </p>
</asp:Content>

