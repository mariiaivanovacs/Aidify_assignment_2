<%@ Page Title="Content Approval Queue" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ApprovalQueue.aspx.cs" Inherits="Aidify.Admin.Content.ApprovalQueue" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

<style>
    .aidify-navbar,
    .aidify-footer {
        display: none !important;
    }

    body {
        background-color: #f9f9f9;
    }

    /* ── TOPBAR ── */
    .approval-topbar {
        height: 70px;
        background: #ffffff;
        border-bottom: 1px solid #e2e2e2;
        display: flex;
        align-items: center;
        position: sticky;
        top: 0;
        z-index: 50;
    }

    .approval-brand {
        color: #E53935;
        font-size: 22px;
        font-weight: 800;
        text-decoration: none;
    }

    .approval-nav a {
        color: #3d2a28;
        text-decoration: none;
        margin-left: 22px;
        font-weight: 600;
        font-size: 14px;
    }

    .approval-nav a.active {
        color: #E53935;
        border-bottom: 2px solid #E53935;
        padding-bottom: 6px;
    }

    .topbar-login {
        border: 1.5px solid #ccc;
        background: #fff;
        color: #333;
        font-weight: 600;
        border-radius: 8px;
        padding: 6px 20px;
        text-decoration: none;
        font-size: 14px;
        margin-right: 8px;
    }

    .topbar-login:hover {
        border-color: #E53935;
        color: #E53935;
    }

    /* ── PAGE ── */
    .approval-page {
        padding: 44px 0 60px;
        background-color: #f9f9f9;
    }

    .approval-page h1 {
        font-size: 30px;
        font-weight: 800;
        color: #1a1a1a;
        margin-bottom: 4px;
    }

    /* ── FILTER BAR ── */
    .filter-bar {
        background: #f3f3f3;
        border-radius: 12px;
        padding: 12px 16px;
        border: 1px solid #e2e2e2;
        margin-bottom: 24px;
    }

    .filter-btn {
        background: #ffffff;
        border: 1px solid #e2e2e2;
        color: #444;
        font-weight: 600;
        font-size: 13px;
        border-radius: 8px;
        padding: 6px 16px;
        margin-right: 6px;
        cursor: pointer;
        transition: 0.15s;
    }

    .filter-btn:hover,
    .filter-btn.active {
        background: #1f2933;
        color: #fff;
        border-color: #1f2933;
    }

    .filter-search {
        border: 1px solid #e2e2e2;
        border-radius: 8px;
        font-size: 13px;
        height: 36px;
        padding: 0 12px;
        width: 240px;
        outline: none;
    }

    .filter-search:focus {
        border-color: #E53935;
    }

    /* ── CARDS ── */
    .approval-card {
        background: #ffffff;
        border: 1px solid #e2e2e2;
        border-radius: 14px;
        overflow: hidden;
        transition: box-shadow 0.2s;
    }

    .approval-card:hover {
        box-shadow: 0 4px 18px rgba(0,0,0,0.08);
    }

    .badge-category {
        font-size: 10px;
        font-weight: 800;
        text-transform: uppercase;
        letter-spacing: 0.8px;
    }

    /* Grey thumbnail box */
    .preview-thumb {
        width: 90px;
        height: 90px;
        flex-shrink: 0;
        background: #e8e8e8;
        border-radius: 10px;
        display: flex;
        align-items: center;
        justify-content: center;
        font-size: 30px;
        color: #E53935;
    }

    /* Side preview for horizontal card */
    .preview-side {
        background: #e8e8e8;
        display: flex;
        align-items: center;
        justify-content: center;
        font-size: 52px;
        color: #E53935;
        min-height: 180px;
    }

    /* Comment textarea */
    .comment-area {
        border: 1px solid #e2e2e2;
        border-radius: 8px;
        font-size: 13px;
        resize: none;
        width: 100%;
        padding: 10px 12px;
        color: #555;
        background: #fafafa;
    }

    .comment-area:focus {
        border-color: #E53935;
        outline: none;
        background: #fff;
    }

    /* Author chip */
    .author-chip {
        width: 34px;
        height: 34px;
        border-radius: 50%;
        color: white;
        font-weight: 700;
        font-size: 11px;
        display: flex;
        align-items: center;
        justify-content: center;
        flex-shrink: 0;
    }

    .chip-red    { background: #E53935; }
    .chip-teal   { background: #0d9488; }
    .chip-blue   { background: #2563eb; }
    .chip-purple { background: #7c3aed; }

    /* Buttons */
    .btn-sm-outline-grey {
        border: 1px solid #d0d0d0;
        background: #fff;
        color: #444;
        border-radius: 7px;
        font-size: 13px;
        font-weight: 600;
        padding: 6px 14px;
        cursor: pointer;
        display: inline-flex;
        align-items: center;
        gap: 5px;
        text-decoration: none;
    }

    .btn-sm-outline-grey:hover { border-color: #888; }

    .btn-sm-outline-danger {
        border: 1px solid #fca5a5;
        background: #fff;
        color: #E53935;
        border-radius: 7px;
        font-size: 13px;
        font-weight: 600;
        padding: 6px 14px;
        cursor: pointer;
        display: inline-flex;
        align-items: center;
        gap: 5px;
    }

    .btn-sm-outline-danger:hover { background: #fff5f5; }

    .btn-sm-approve {
        background: #E53935;
        color: #fff;
        border: none;
        border-radius: 7px;
        font-size: 13px;
        font-weight: 700;
        padding: 6px 14px;
        cursor: pointer;
        display: inline-flex;
        align-items: center;
        gap: 5px;
    }

    .btn-sm-approve:hover { background: #c62828; }

    /* ── PAGINATION ── */
    .pagination-bar {
        display: flex;
        justify-content: center;
        align-items: center;
        gap: 6px;
        margin-top: 36px;
    }

    .page-btn {
        width: 36px;
        height: 36px;
        border-radius: 8px;
        border: 1px solid #e2e2e2;
        background: #fff;
        color: #444;
        font-weight: 600;
        font-size: 14px;
        display: flex;
        align-items: center;
        justify-content: center;
        cursor: pointer;
        text-decoration: none;
        transition: 0.15s;
    }

    .page-btn:hover { border-color: #E53935; color: #E53935; }
    .page-btn.active { background: #E53935; color: #fff; border-color: #E53935; }
    .page-btn.dots { border: none; background: none; cursor: default; color: #aaa; }

    /* ── FOOTER ── */
    .approval-footer {
        background: #f3f3f3;
        border-top: 1px solid #e2e2e2;
        padding: 28px 0 16px;
    }

    .approval-footer .footer-brand {
        color: #E53935;
        font-weight: 800;
        font-size: 17px;
    }

    .approval-footer a {
        color: #555;
        text-decoration: none;
        font-size: 13px;
    }

    .approval-footer a:hover { color: #E53935; }

    @media (max-width: 768px) {
        .approval-nav { display: none; }
    }
</style>

<!-- ── TOPBAR ── -->
<header class="approval-topbar">
    <div class="container d-flex justify-content-between align-items-center">

        <a href="../Dashboard.aspx" class="approval-brand" style="display:flex; align-items:center; gap:10px;">
            Aidify
            <span style="background:#db322f; color:#fff; font-size:11px; padding:3px 9px; border-radius:20px; font-weight:700;">ADMIN</span>
        </a>

        <nav class="approval-nav">
            <a href="../Dashboard.aspx">Dashboard</a>
            <a href="../Users/List.aspx">Users</a>
            <a href="../Roles.aspx">Roles</a>
            <a href="ManagePublicPages.aspx">Public Pages</a>
            <a href="ApprovalQueue.aspx" class="active">Approvals</a>
            <a href="../Analytics.aspx">Analytics</a>
        </nav>

        <div class="d-flex align-items-center gap-3">
            <i class="bi bi-bell fs-5"></i>
            <div class="text-end d-none d-md-block">
                <div class="fw-bold" style="font-size:14px;">Dr. Sarah Chen</div>
                <small class="text-muted">System Administrator</small>
            </div>
            <div style="width:42px; height:42px; border-radius:50%; background:#E53935; color:white; display:flex; align-items:center; justify-content:center; font-weight:800;">A</div>
        
        </div>

    </div>
</header>

<!-- ── MAIN ── -->
<main class="approval-page">
    <div class="container">

        <div class="mb-4">
            <h1>Content Approval Queue</h1>
            <p class="text-muted" style="font-size:15px;">
                Review and manage pending medical training modules and assessments.
            </p>
        </div>

        <!-- Filter Bar -->
        <div class="filter-bar d-flex flex-wrap justify-content-between align-items-center gap-2">
            <div>
                <button class="filter-btn active">All Requests (12)</button>
                <button class="filter-btn">First Aid (8)</button>
                <button class="filter-btn">Quizzes (4)</button>
            </div>
            <div class="d-flex align-items-center gap-2">
                <i class="bi bi-search text-muted" style="font-size:13px;"></i>
                <input type="text" class="filter-search" placeholder="Search by author or topic..." />
            </div>
        </div>

        <asp:Label ID="lblQueueStatus" runat="server" CssClass="d-block mb-3" Visible="false"></asp:Label>

        <!-- Cards — dynamic from DB -->
        <div class="row g-4">
            <asp:Repeater ID="rptPending" runat="server" OnItemCommand="rptPending_ItemCommand">
                <ItemTemplate>
                    <div class="col-lg-6">
                        <div class="approval-card p-4 d-flex flex-column h-100">
                            <div class="d-flex justify-content-between mb-3">
                                <div class="p-2 rounded" style="background:#ffe1e4;color:#E53935;">
                                    <i class="bi bi-file-earmark-medical-fill"></i>
                                </div>
                                <small class="text-muted"><%# ((DateTime)Eval("CreatedAt")).ToString("MMM dd, yyyy") %></small>
                            </div>
                            <span class="badge-category text-danger mb-1"><%# Server.HtmlEncode(Eval("DifficultyLevel").ToString()) %> Module</span>
                            <h3 class="h5 fw-bold mb-2"><%# Server.HtmlEncode(Eval("Title").ToString()) %></h3>
                            <p class="text-muted small mb-3 flex-grow-1">
                                Submitted by <span class="fw-semibold text-dark"><%# Server.HtmlEncode(Eval("CreatedByName").ToString()) %></span>
                            </p>
                            <div class="d-flex gap-2">
                                <asp:Button runat="server" Text="Approve"
                                    CssClass="btn-sm-approve flex-grow-1"
                                    CommandName="Approve"
                                    CommandArgument='<%# Eval("ModuleId") %>'
                                    Style="justify-content:center;" />
                                <asp:Button runat="server" Text="Reject"
                                    CssClass="btn-sm-outline-danger flex-grow-1"
                                    CommandName="Reject"
                                    CommandArgument='<%# Eval("ModuleId") %>'
                                    Style="justify-content:center;" />
                            </div>
                        </div>
                    </div>
                </ItemTemplate>
                <EmptyDataTemplate>
                    <div class="col-12">
                        <div class="approval-card p-4 text-center text-muted">
                            <i class="bi bi-check2-circle fs-2 d-block mb-2"></i>
                            No modules pending review.
                        </div>
                    </div>
                </EmptyDataTemplate>
            </asp:Repeater>
        </div>

        <!-- Pagination -->
        <div class="pagination-bar">
            <a class="page-btn">&lsaquo;</a>
            <a class="page-btn active">1</a>
            <a class="page-btn">2</a>
            <a class="page-btn">3</a>
            <span class="page-btn dots">...</span>
            <a class="page-btn">&rsaquo;</a>
        </div>

    </div>
</main>

<!-- ── FOOTER ── -->
<footer class="approval-footer">
    <div class="container">
        <div class="d-flex justify-content-between align-items-start flex-wrap gap-3 mb-3">
            <div>
                <div class="footer-brand">Aidify</div>
                <p class="text-muted small mb-0">Administrative control center for the Aidify learning platform.</p>
            </div>
            <div class="d-flex align-items-center gap-3">
                <a href="../Dashboard.aspx">Dashboard</a>
                <span class="text-muted">|</span>
                <a href="#">Privacy Policy</a>
                <span class="text-muted">|</span>
                <a href="#">Terms of Service</a>
            </div>
        </div>
        <hr class="mt-1 mb-2" />
        <p class="text-muted small mb-0 text-center">© 2026 Aidify Admin Panel. Educational use only.</p>
    </div>
</footer>

</asp:Content>