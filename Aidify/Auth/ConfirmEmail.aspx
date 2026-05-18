<%@ Page Title="Confirm Email" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ConfirmEmail.aspx.cs" Inherits="Aidify.Auth.ConfirmEmail" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <section class="auth-section">
        <div class="container">
            <div class="row justify-content-center">
                <div class="col-md-5 auth-right text-center">

                    <h2>Email Confirmation</h2>

                    <asp:Label ID="lblMessage" runat="server" CssClass="d-block mt-3"></asp:Label>

                    <p class="auth-bottom-text mt-4">
                        <a href="Login.aspx">Go to Login</a>
                    </p>

                </div>
            </div>
        </div>
    </section>

</asp:Content>
