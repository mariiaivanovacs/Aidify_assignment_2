<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Navbar.ascx.cs" Inherits="Aidify.Controls.Navbar" %>

<nav class="navbar navbar-expand-lg aidify-navbar">
    <div class="container">
        <a class="navbar-brand fw-bold" href="<%= ResolveUrl("~/Default.aspx") %>">Aidify</a>

        <button class="navbar-toggler" type="button" data-bs-toggle="collapse" data-bs-target="#mainNavbar">
            <span class="navbar-toggler-icon"></span>
        </button>

        <div class="collapse navbar-collapse" id="mainNavbar">

            <!-- Visitor Navbar -->
            <asp:Panel ID="pnlVisitor" runat="server" CssClass="navbar-nav ms-auto">
                <a class="nav-link" href="<%= ResolveUrl("~/Default.aspx") %>">Home</a>
                <a class="nav-link" href="<%= ResolveUrl("~/Public/About.aspx") %>">About</a>
                <a class="nav-link" href="<%= ResolveUrl("~/Public/FAQ.aspx") %>">FAQ</a>
                <a class="nav-link" href="<%= ResolveUrl("~/Auth/Login.aspx") %>">Login</a>
                <a class="btn btn-aidify ms-lg-2" href="<%= ResolveUrl("~/Auth/Register.aspx") %>">Register</a>
            </asp:Panel>

            <!-- Learner Navbar -->
            <asp:Panel ID="pnlLearner" runat="server" CssClass="navbar-nav ms-auto" Visible="false">
                <a class="nav-link" href="<%= ResolveUrl("~/Learner/Dashboard.aspx") %>">Dashboard</a>
                <a class="nav-link" href="<%= ResolveUrl("~/Learner/Courses/Catalogue.aspx") %>">Courses</a>
                <a class="nav-link" href="<%= ResolveUrl("~/Learner/Progress.aspx") %>">Progress</a>
                <a class="nav-link" href="<%= ResolveUrl("~/Auth/Logout.aspx") %>">Logout</a>
            </asp:Panel>

            <!-- Instructor Navbar -->
            <asp:Panel ID="pnlInstructor" runat="server" CssClass="navbar-nav ms-auto" Visible="false">
                <a class="nav-link" href="<%= ResolveUrl("~/Instructor/Dashboard.aspx") %>">Dashboard</a>
                <a class="nav-link" href="<%= ResolveUrl("~/Instructor/Modules/Edit.aspx") %>">Modules</a>
                <a class="nav-link" href="<%= ResolveUrl("~/Instructor/Quizzes/Edit.aspx") %>">Quizzes</a>
                <a class="nav-link" href="<%= ResolveUrl("~/Auth/Logout.aspx") %>">Logout</a>
            </asp:Panel>

            <!-- Admin Navbar -->
            <asp:Panel ID="pnlAdmin" runat="server" CssClass="navbar-nav ms-auto" Visible="false">
                <a class="nav-link" href="<%= ResolveUrl("~/Admin/Dashboard.aspx") %>">Dashboard</a>
                <a class="nav-link" href="<%= ResolveUrl("~/Admin/Users/List.aspx") %>">Users</a>
                <a class="nav-link" href="<%= ResolveUrl("~/Admin/Analytics.aspx") %>">Analytics</a>
                <a class="nav-link" href="<%= ResolveUrl("~/Auth/Logout.aspx") %>">Logout</a>
            </asp:Panel>

        </div>
    </div>
</nav>