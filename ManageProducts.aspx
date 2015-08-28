<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="ManageProducts.aspx.cs" Inherits="Pages_Management_ManageProducts" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <p>
        Name:</p>
    <p>
        <asp:TextBox ID="txtName" runat="server" Width="300px" CssClass="form-control"></asp:TextBox>
    </p>
    <p>
        Type:</p>
    <p>
        <asp:DropDownList ID="ddlType" runat="server" Width="300px" DataSourceID="SqlDataSource1" DataTextField="Name" DataValueField="ID" CssClass="form-control">
        </asp:DropDownList>
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:GarageDBConnectionString %>" SelectCommand="SELECT * FROM [ProductTypes]"></asp:SqlDataSource>
    </p>
    <p>
        Price:</p>
    <p>
        <asp:TextBox ID="txtPrice" runat="server" Width="300px" CssClass="form-control"></asp:TextBox>
    </p>
    <p>
        Image:</p>
        <asp:DropDownList ID="ddlImage" runat="server" Width="300px" CssClass="form-control"></asp:DropDownList>
    <p>
       
    </p>
    <p>
        Description:</p>
    <p>
        <asp:TextBox ID="txtDescription" runat="server" Height="74px" TextMode="MultiLine" Width="300px" CssClass="form-control"></asp:TextBox>
    </p>
    <p>
        <asp:Button ID="btnSubmit" runat="server" Text="Submit" OnClick="btnSubmit_Click" CssClass="btn btn-primary" />
    </p>
    <p>
        <asp:Label ID="lblResult" runat="server"></asp:Label>
    </p>
</asp:Content>

