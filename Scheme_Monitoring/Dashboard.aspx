<%@ Page Language="C#" MasterPageFile="~/TemplateMasterAdmin.master" AutoEventWireup="true" CodeFile="Dashboard.aspx.cs" Inherits="Dashboard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div id="layout-wrapper">
        <header id="page-topbar" class="">
            <div class="layout-width">

                <div class="layout-width">
                    <div class="navbar-header">
                        <div class="d-flex">
                            <!-- LOGO -->
                            <div class="navbar-brand-box horizontal-logo">
                                <%--<a href="index.html" class="logo logo-dark">
                        <span class="logo-sm">
                            <img src="assets/images/logo-dark.png" alt="" height="22">
                        </span>
                        <span class="logo-lg">
                            <img src="assets/images/logo-dark.png" alt="" height="17">
                        </span>
                    </a>--%>

                                <%--   <a href="index.html" class="logo logo-light">
                        <span class="logo-sm">
                            <img src="assets/images/logo-dark.png" alt="" height="22">
                        </span>
                        <span class="logo-lg">
                            <img src="assets/images/logo.png" alt="" height="17">
                        </span>
                    </a>--%>
                            </div>

                            <button type="button" class="btn btn-sm px-3 fs-16 header-item vertical-menu-btn topnav-hamburger" id="topnav-hamburger-icon">
                                <span class="hamburger-icon">
                                    <span></span>
                                    <span></span>
                                    <span></span>
                                </span>
                            </button>
                            <div class="logo">
                                <h2>Schemes Monitoring System <span>Urban Development Department, Government of Uttar Pradesh</span> </h2>
                            </div>


                        </div>

                        <div class="d-flex align-items-center">
                            <div class="ms-1 header-item d-none d-sm-flex">
                                <button type="button" class="btn btn-icon btn-topbar btn-ghost-secondary rounded-circle" data-toggle="fullscreen">
                                    <i class="bx bx-fullscreen fs-22"></i>
                                </button>
                            </div>



                            <div class="dropdown ms-sm-3 header-item topbar-user">
                                <button type="button" class="btn" id="page-header-user-dropdown" data-bs-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
                                    <span class="d-flex align-items-center">
                                        <img class="rounded-circle header-profile-user" src="assets/images/users/avatar-1.jpg" alt="Header Avatar">
                                        <span class="text-start ms-xl-2">
                                            <span class="d-none d-xl-inline-block ms-1 fw-medium user-name-text">Anna Adame</span>
                                            <span class="d-none d-xl-block ms-1 fs-12 user-name-sub-text">Founder</span>
                                        </span>
                                    </span>
                                </button>
                                <div class="dropdown-menu dropdown-menu-end">

                                    <h6 class="dropdown-header">Welcome Anna!</h6>
                                    <a class="dropdown-item" href="pages-profile.html"><i class="mdi mdi-account-circle text-muted fs-16 align-middle me-1"></i><span class="align-middle">Profile</span></a>


                                    <a class="dropdown-item" href="pages-profile-settings.html"><span class="badge bg-success-subtle text-success mt-1 float-end">New</span><i class="mdi mdi-cog-outline text-muted fs-16 align-middle me-1"></i> <span class="align-middle">Settings</span></a>
                                    <a class="dropdown-item" href="auth-lockscreen-basic.html"><i class="mdi mdi-lock text-muted fs-16 align-middle me-1"></i><span class="align-middle">Lock screen</span></a>
                                    <a class="dropdown-item" href="auth-logout-basic.html"><i class="mdi mdi-logout text-muted fs-16 align-middle me-1"></i><span class="align-middle" data-key="t-logout">Logout</span></a>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </header>

        <!-- removeNotificationModal -->
        <div id="removeNotificationModal" class="modal fade zoomIn" tabindex="-1" aria-hidden="true">
            <div class="modal-dialog modal-dialog-centered">
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close" id="NotificationModalbtn-close"></button>
                    </div>
                    <div class="modal-body">
                        <div class="mt-2 text-center">
                            <lord-icon src="https://cdn.lordicon.com/gsqxdxog.json" trigger="loop" colors="primary:#f7b84b,secondary:#f06548" style="width: 100px; height: 100px"></lord-icon>
                            <div class="mt-4 pt-2 fs-15 mx-4 mx-sm-5">
                                <h4>Are you sure ?</h4>
                                <p class="text-muted mx-4 mb-0">Are you sure you want to remove this Notification ?</p>
                            </div>
                        </div>
                        <div class="d-flex gap-2 justify-content-center mt-4 mb-2">
                            <button type="button" class="btn w-sm btn-light" data-bs-dismiss="modal">Close</button>
                            <button type="button" class="btn w-sm btn-danger" id="delete-notification">Yes, Delete It!</button>
                        </div>
                    </div>
                </div>
                <!-- /.modal-content -->
            </div>
            <!-- /.modal-dialog -->
        </div>
        <!-- /.modal -->
        <!-- ========== App Menu ========== -->
        <div class="app-menu navbar-menu">
            <!-- LOGO -->
            <div class="navbar-brand-box">
                <!-- Dark Logo-->
                <a href="index.html" class="logo logo-dark"><span class="logo-sm">
                    <img src="assets/images/logo.png" alt="" height="22">
                </span><span class="logo-lg">
                    <img src="assets/images/logo-dark.png" alt="" height="17">
                </span></a>
                <!-- Light Logo-->
                <a href="index.html" class="logo logo-light"><span class="logo-sm">
                    <img src="assets/images/logo.png" alt="" height="22">
                </span><span class="logo-lg">
                    <img src="assets/images/logo.png" alt="" height="17">
                </span></a>
                <button type="button" class="btn btn-sm p-0 fs-20 header-item float-end btn-vertical-sm-hover" id="vertical-hover"><i class="ri-record-circle-line"></i></button>
            </div>
            <div id="scrollbar" data-simplebar="init" class="h-100 simplebar-scrollable-y">
                <div class="simplebar-wrapper" style="margin: 0px;">
                    <div class="simplebar-height-auto-observer-wrapper">
                        <div class="simplebar-height-auto-observer"></div>
                    </div>
                    <div class="simplebar-mask">
                        <div class="simplebar-offset" style="right: 0px; bottom: 0px;">
                            <div class="simplebar-content-wrapper" tabindex="0" role="region" aria-label="scrollable content" style="height: 100%; overflow: hidden scroll;">
                                <div class="simplebar-content" style="padding: 0px;">
                                    <div class="container-fluid">
                                        <div id="two-column-menu"></div>
                                        <ul class="navbar-nav" id="navbar-nav" data-simplebar="init">
                                            <div class="simplebar-wrapper" style="margin: 0px;">
                                                <div class="simplebar-height-auto-observer-wrapper">
                                                    <div class="simplebar-height-auto-observer"></div>
                                                </div>
                                                <div class="simplebar-mask">
                                                    <div class="simplebar-offset" style="right: 0px; bottom: 0px;">
                                                        <div class="simplebar-content-wrapper" tabindex="0" role="region" aria-label="scrollable content" style="height: auto; overflow: hidden;">
                                                            <div class="simplebar-content" style="padding: 0px;">
                                                                <li class="menu-title"><span data-key="t-menu">Menu</span></li>
                                                                <li class="nav-item"><a class="nav-link menu-link" href="#sidebarDashboards" data-bs-toggle="collapse" role="button" aria-expanded="false" aria-controls="sidebarDashboards">
                                                                    <svg xmlns="http://www.w3.org/2000/svg" width="24" height="24" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round" class="feather feather-home icon-dual">
                                                                        <path d="M3 9l9-7 9 7v11a2 2 0 0 1-2 2H5a2 2 0 0 1-2-2z"></path><polyline points="9 22 9 12 15 12 15 22"></polyline></svg>
                                                                    <span data-key="t-dashboards">Dashboards</span> </a></li>
                                                                <!-- end Dashboard Menu -->
                                                                <li class="nav-item"><a class="nav-link menu-link" href="#sidebarApps" data-bs-toggle="collapse" role="button" aria-expanded="false" aria-controls="sidebarApps">
                                                                    <svg xmlns="http://www.w3.org/2000/svg" width="24" height="24" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round" class="feather feather-grid icon-dual">
                                                                        <rect x="3" y="3" width="7" height="7"></rect><rect x="14" y="3" width="7" height="7"></rect><rect x="14" y="14" width="7" height="7"></rect><rect x="3" y="14" width="7" height="7"></rect></svg>
                                                                    <span data-key="t-apps">Apps</span> </a>

                                                                </li>
                                                                <li class="nav-item"><a class="nav-link menu-link" href="#sidebarLayouts" data-bs-toggle="collapse" role="button" aria-expanded="false" aria-controls="sidebarLayouts">
                                                                    <svg xmlns="http://www.w3.org/2000/svg" width="24" height="24" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round" class="feather feather-layout icon-dual">
                                                                        <rect x="3" y="3" width="18" height="18" rx="2" ry="2"></rect><line x1="3" y1="9" x2="21" y2="9"></line><line x1="9" y1="21" x2="9" y2="9"></line></svg>
                                                                    <span data-key="t-layouts">Layouts</span>  </a>

                                                                </li>
                                                                <!-- end Dashboard Menu -->


                                                                <li class="nav-item"><a class="nav-link menu-link collapsed" href="#sidebarAuth" data-bs-toggle="collapse" role="button" aria-expanded="false" aria-controls="sidebarAuth">
                                                                    <svg xmlns="http://www.w3.org/2000/svg" width="24" height="24" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round" class="feather feather-users icon-dual">
                                                                        <path d="M17 21v-2a4 4 0 0 0-4-4H5a4 4 0 0 0-4 4v2"></path><circle cx="9" cy="7" r="4"></circle><path d="M23 21v-2a4 4 0 0 0-3-3.87"></path><path d="M16 3.13a4 4 0 0 1 0 7.75"></path></svg>
                                                                    <span data-key="t-authentication">Authentication</span> </a>
                                                                    <div class="collapse menu-dropdown" id="sidebarAuth">
                                                                        <ul class="nav nav-sm flex-column">
                                                                            <li class="nav-item"><a href="#sidebarSignIn" class="nav-link collapsed" data-bs-toggle="collapse" role="button" aria-expanded="false" aria-controls="sidebarSignIn" data-key="t-signin">Sign In </a>
                                                                                <div class="collapse menu-dropdown" id="sidebarSignIn">
                                                                                    <ul class="nav nav-sm flex-column">

                                                                                        <li class="nav-item"><a href="auth-signin-cover.html" class="nav-link" data-key="t-cover">Cover </a></li>
                                                                                    </ul>
                                                                                </div>
                                                                            </li>
                                                                            <li class="nav-item"><a href="#sidebarSignUp" class="nav-link collapsed" data-bs-toggle="collapse" role="button" aria-expanded="false" aria-controls="sidebarSignUp" data-key="t-signup">Sign Up </a>
                                                                                <div class="collapse menu-dropdown" id="sidebarSignUp">
                                                                                    <ul class="nav nav-sm flex-column">

                                                                                        <li class="nav-item"><a href="auth-signup-cover.html" class="nav-link" data-key="t-cover">Cover </a></li>
                                                                                    </ul>
                                                                                </div>
                                                                            </li>
                                                                            <li class="nav-item"><a href="#sidebarResetPass" class="nav-link collapsed" data-bs-toggle="collapse" role="button" aria-expanded="false" aria-controls="sidebarResetPass" data-key="t-password-reset">Password Reset </a>
                                                                                <div class="collapse menu-dropdown" id="sidebarResetPass">
                                                                                    <ul class="nav nav-sm flex-column">

                                                                                        <li class="nav-item"><a href="auth-pass-reset-cover.html" class="nav-link" data-key="t-cover">Cover </a></li>
                                                                                    </ul>
                                                                                </div>
                                                                            </li>
                                                                            <li class="nav-item"><a href="#sidebarchangePass" class="nav-link collapsed" data-bs-toggle="collapse" role="button" aria-expanded="false" aria-controls="sidebarchangePass" data-key="t-password-create">Password Create </a>
                                                                                <div class="collapse menu-dropdown" id="sidebarchangePass">
                                                                                    <ul class="nav nav-sm flex-column">

                                                                                        <li class="nav-item"><a href="auth-pass-change-cover.html" class="nav-link" data-key="t-cover">Cover </a></li>
                                                                                    </ul>
                                                                                </div>
                                                                            </li>
                                                                            <li class="nav-item"><a href="#sidebarLockScreen" class="nav-link collapsed" data-bs-toggle="collapse" role="button" aria-expanded="false" aria-controls="sidebarLockScreen" data-key="t-lock-screen">Lock Screen </a>
                                                                                <div class="collapse menu-dropdown" id="sidebarLockScreen">
                                                                                    <ul class="nav nav-sm flex-column">

                                                                                        <li class="nav-item"><a href="auth-lockscreen-cover.html" class="nav-link" data-key="t-cover">Cover </a></li>
                                                                                    </ul>
                                                                                </div>
                                                                            </li>
                                                                            <li class="nav-item"><a href="#sidebarLogout" class="nav-link collapsed" data-bs-toggle="collapse" role="button" aria-expanded="false" aria-controls="sidebarLogout" data-key="t-logout">Logout </a>
                                                                                <div class="collapse menu-dropdown" id="sidebarLogout">
                                                                                    <ul class="nav nav-sm flex-column">

                                                                                        <li class="nav-item"><a href="auth-logout-cover.html" class="nav-link" data-key="t-cover">Cover </a></li>
                                                                                    </ul>
                                                                                </div>
                                                                            </li>
                                                                            <li class="nav-item"><a href="#sidebarSuccessMsg" class="nav-link collapsed" data-bs-toggle="collapse" role="button" aria-expanded="false" aria-controls="sidebarSuccessMsg" data-key="t-success-message">Success Message </a>
                                                                                <div class="collapse menu-dropdown" id="sidebarSuccessMsg">
                                                                                    <ul class="nav nav-sm flex-column">

                                                                                        <li class="nav-item"><a href="auth-success-msg-cover.html" class="nav-link" data-key="t-cover">Cover </a></li>
                                                                                    </ul>
                                                                                </div>
                                                                            </li>
                                                                            <li class="nav-item"><a href="#sidebarTwoStep" class="nav-link collapsed" data-bs-toggle="collapse" role="button" aria-expanded="false" aria-controls="sidebarTwoStep" data-key="t-two-step-verification">Two Step Verification </a>
                                                                                <div class="collapse menu-dropdown" id="sidebarTwoStep">
                                                                                    <ul class="nav nav-sm flex-column">

                                                                                        <li class="nav-item"><a href="auth-twostep-cover.html" class="nav-link" data-key="t-cover">Cover </a></li>
                                                                                    </ul>
                                                                                </div>
                                                                            </li>

                                                                        </ul>
                                                                    </div>
                                                                </li>



                                                                <li class="nav-item"><a class="nav-link menu-link" href="#sidebarUI" data-bs-toggle="collapse" role="button" aria-expanded="false" aria-controls="sidebarUI">
                                                                    <svg xmlns="http://www.w3.org/2000/svg" width="24" height="24" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round" class="feather feather-package icon-dual">
                                                                        <line x1="16.5" y1="9.4" x2="7.5" y2="4.21"></line><path d="M21 16V8a2 2 0 0 0-1-1.73l-7-4a2 2 0 0 0-2 0l-7 4A2 2 0 0 0 3 8v8a2 2 0 0 0 1 1.73l7 4a2 2 0 0 0 2 0l7-4A2 2 0 0 0 21 16z"></path><polyline points="3.27 6.96 12 12.01 20.73 6.96"></polyline><line x1="12" y1="22.08" x2="12" y2="12"></line></svg>
                                                                    <span data-key="t-base-ui">Base UI</span> </a>

                                                                </li>

                                                                <li class="nav-item"><a class="nav-link menu-link" href="widgets.html">
                                                                    <svg xmlns="http://www.w3.org/2000/svg" width="24" height="24" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round" class="feather feather-copy icon-dual">
                                                                        <rect x="9" y="9" width="13" height="13" rx="2" ry="2"></rect><path d="M5 15H4a2 2 0 0 1-2-2V4a2 2 0 0 1 2-2h9a2 2 0 0 1 2 2v1"></path></svg>
                                                                    <span data-key="t-widgets">Widgets</span> </a></li>
                                                                <li class="nav-item"><a class="nav-link menu-link collapsed" href="#sidebarForms" data-bs-toggle="collapse" role="button" aria-expanded="false" aria-controls="sidebarForms">
                                                                    <svg xmlns="http://www.w3.org/2000/svg" width="24" height="24" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round" class="feather feather-file-text icon-dual">
                                                                        <path d="M14 2H6a2 2 0 0 0-2 2v16a2 2 0 0 0 2 2h12a2 2 0 0 0 2-2V8z"></path><polyline points="14 2 14 8 20 8"></polyline><line x1="16" y1="13" x2="8" y2="13"></line><line x1="16" y1="17" x2="8" y2="17"></line><polyline points="10 9 9 9 8 9"></polyline></svg>
                                                                    <span data-key="t-forms">Forms</span> </a>
                                                                    <div class="collapse menu-dropdown" id="sidebarForms">
                                                                        <ul class="nav nav-sm flex-column">
                                                                            <li class="nav-item"><a href="forms-elements.html" class="nav-link" data-key="t-basic-elements">Basic Elements</a> </li>
                                                                            <li class="nav-item"><a href="forms-select.html" class="nav-link" data-key="t-form-select">Form Select </a></li>
                                                                            <li class="nav-item"><a href="forms-checkboxs-radios.html" class="nav-link" data-key="t-checkboxs-radios">Checkboxs &amp; Radios</a> </li>
                                                                            <li class="nav-item"><a href="forms-pickers.html" class="nav-link" data-key="t-pickers">Pickers </a></li>
                                                                            <li class="nav-item"><a href="forms-masks.html" class="nav-link" data-key="t-input-masks">Input Masks</a> </li>
                                                                            <li class="nav-item"><a href="forms-advanced.html" class="nav-link" data-key="t-advanced">Advanced</a> </li>
                                                                            <li class="nav-item"><a href="forms-range-sliders.html" class="nav-link" data-key="t-range-slider">Range Slider </a></li>
                                                                            <li class="nav-item"><a href="forms-validation.html" class="nav-link" data-key="t-validation">Validation</a> </li>
                                                                            <li class="nav-item"><a href="forms-wizard.html" class="nav-link" data-key="t-wizard">Wizard</a> </li>
                                                                            <li class="nav-item"><a href="forms-editors.html" class="nav-link" data-key="t-editors">Editors</a> </li>
                                                                            <li class="nav-item"><a href="forms-file-uploads.html" class="nav-link" data-key="t-file-uploads">File Uploads</a> </li>
                                                                            <li class="nav-item"><a href="forms-layouts.html" class="nav-link" data-key="t-form-layouts">Form Layouts</a> </li>
                                                                            <li class="nav-item"><a href="forms-select2.html" class="nav-link" data-key="t-select2">Select2</a> </li>
                                                                        </ul>
                                                                    </div>
                                                                </li>
                                                                <li class="nav-item"><a class="nav-link menu-link collapsed" href="#sidebarTables" data-bs-toggle="collapse" role="button" aria-expanded="false" aria-controls="sidebarTables">
                                                                    <svg xmlns="http://www.w3.org/2000/svg" width="24" height="24" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round" class="feather feather-database icon-dual">
                                                                        <ellipse cx="12" cy="5" rx="9" ry="3"></ellipse><path d="M21 12c0 1.66-4 3-9 3s-9-1.34-9-3"></path><path d="M3 5v14c0 1.66 4 3 9 3s9-1.34 9-3V5"></path></svg>
                                                                    <span data-key="t-tables">Tables</span> </a>
                                                                    <div class="collapse menu-dropdown" id="sidebarTables">
                                                                        <ul class="nav nav-sm flex-column">
                                                                            <li class="nav-item"><a href="tables-basic.html" class="nav-link" data-key="t-basic-tables">Basic Tables</a> </li>
                                                                            <li class="nav-item"><a href="tables-gridjs.html" class="nav-link" data-key="t-grid-js">Grid Js</a> </li>
                                                                            <li class="nav-item"><a href="tables-listjs.html" class="nav-link" data-key="t-list-js">List Js</a> </li>
                                                                            <li class="nav-item"><a href="tables-datatables.html" class="nav-link" data-key="t-datatables">Datatables</a> </li>
                                                                        </ul>
                                                                    </div>
                                                                </li>

                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="simplebar-placeholder" style="width: 249px; height: 393px;"></div>
                                            </div>
                                            <div class="simplebar-track simplebar-horizontal" style="visibility: hidden;">
                                                <div class="simplebar-scrollbar" style="width: 0px; display: none;"></div>
                                            </div>
                                            <div class="simplebar-track simplebar-vertical" style="visibility: hidden;">
                                                <div class="simplebar-scrollbar" style="height: 0px; display: none;"></div>
                                            </div>
                                            <div class="simplebar-track simplebar-horizontal">
                                                <div class="simplebar-scrollbar"></div>
                                            </div>
                                            <div class="simplebar-track simplebar-vertical">
                                                <div class="simplebar-scrollbar"></div>
                                            </div>
                                        </ul>
                                    </div>
                                    <!-- Sidebar -->
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="simplebar-placeholder" style="width: 249px; height: 393px;"></div>
                </div>
                <div class="simplebar-track simplebar-horizontal" style="visibility: hidden;">
                    <div class="simplebar-scrollbar" style="width: 0px; display: none;"></div>
                </div>
                <div class="simplebar-track simplebar-vertical" style="visibility: visible;">
                    <div class="simplebar-scrollbar" style="height: 229px; display: block; transform: translate3d(0px, 0px, 0px);"></div>
                </div>
            </div>
            <div class="sidebar-background"></div>
        </div>
        <!-- Left Sidebar End -->
        <!-- Vertical Overlay-->
        <div class="vertical-overlay"></div>

        <!-- ============================================================== -->
        <!-- Start right Content here -->
        <!-- ============================================================== -->
        <div class="main-content">
            <div class="page-content">
                <div class="container-fluid">

                    <!-- start page title -->
                    <div class="row">
                        <div class="col-12 mb-4">
                            <div class="page-title-box d-sm-flex align-items-center justify-content-between">
                                <h4 class="mb-sm-0">Dashboard</h4>
                                <div class="page-title-right">
                                    <ol class="breadcrumb m-0">
                                        <li class="breadcrumb-item"><a href="javascript: void(0);">Home</a></li>
                                        <li class="breadcrumb-item active">Dashboard</li>
                                    </ol>
                                </div>
                            </div>
                        </div>
                    </div>
                    <!-- end page title -->

                    <div class="row">
                        <div class="col-xxl-12">
                            <div class="d-flex flex-column h-100">

                                <!-- end row-->

                                <div class="row">
                                    <div class="col-md-3">
                                        <div class="card card-animate">
                                            <div class="card-body">
                                                <div class="d-flex justify-content-between">
                                                    <div>
                                                        <p class="fw-medium text-muted mb-0">Users</p>
                                                        <h2 class="mt-4 ff-secondary fw-semibold"><span class="counter-value" data-target="28.05">28.05</span>k</h2>
                                                        <p class="mb-0 text-muted"><span class="badge bg-light text-success mb-0"><i class="ri-arrow-up-line align-middle"></i>16.24 % </span>vs. previous month</p>
                                                    </div>
                                                    <div>
                                                        <div class="avatar-sm flex-shrink-0">
                                                            <span class="avatar-title bg-primary-subtle rounded-circle fs-2">
                                                                <svg xmlns="http://www.w3.org/2000/svg" width="24" height="24" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round" class="feather feather-users text-primary">
                                                                    <path d="M17 21v-2a4 4 0 0 0-4-4H5a4 4 0 0 0-4 4v2"></path><circle cx="9" cy="7" r="4"></circle><path d="M23 21v-2a4 4 0 0 0-3-3.87"></path><path d="M16 3.13a4 4 0 0 1 0 7.75"></path></svg>
                                                            </span>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <!-- end card body -->
                                        </div>
                                        <!-- end card-->
                                    </div>
                                    <!-- end col-->

                                    <div class="col-md-3">
                                        <div class="card card-animate">
                                            <div class="card-body">
                                                <div class="d-flex justify-content-between">
                                                    <div>
                                                        <p class="fw-medium text-muted mb-0">Sessions</p>
                                                        <h2 class="mt-4 ff-secondary fw-semibold"><span class="counter-value" data-target="97.66">97.66</span>k</h2>
                                                        <p class="mb-0 text-muted"><span class="badge bg-light text-danger mb-0"><i class="ri-arrow-down-line align-middle"></i>3.96 % </span>vs. previous month</p>
                                                    </div>
                                                    <div>
                                                        <div class="avatar-sm flex-shrink-0">
                                                            <span class="avatar-title bg-primary-subtle rounded-circle fs-2">
                                                                <svg xmlns="http://www.w3.org/2000/svg" width="24" height="24" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round" class="feather feather-activity text-primary">
                                                                    <polyline points="22 12 18 12 15 21 9 3 6 12 2 12"></polyline></svg>
                                                            </span>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <!-- end card body -->
                                        </div>
                                        <!-- end card-->
                                    </div>
                                    <div class="col-md-3">
                                        <div class="card card-animate">
                                            <div class="card-body">
                                                <div class="d-flex justify-content-between">
                                                    <div>
                                                        <p class="fw-medium text-muted mb-0">Sessions</p>
                                                        <h2 class="mt-4 ff-secondary fw-semibold"><span class="counter-value" data-target="97.66">97.66</span>k</h2>
                                                        <p class="mb-0 text-muted"><span class="badge bg-light text-danger mb-0"><i class="ri-arrow-down-line align-middle"></i>3.96 % </span>vs. previous month</p>
                                                    </div>
                                                    <div>
                                                        <div class="avatar-sm flex-shrink-0">
                                                            <span class="avatar-title bg-primary-subtle rounded-circle fs-2">
                                                                <svg xmlns="http://www.w3.org/2000/svg" width="24" height="24" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round" class="feather feather-activity text-primary">
                                                                    <polyline points="22 12 18 12 15 21 9 3 6 12 2 12"></polyline></svg>
                                                            </span>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <!-- end card body -->
                                        </div>
                                        <!-- end card-->
                                    </div>
                                    <div class="col-md-3">
                                        <div class="card card-animate">
                                            <div class="card-body">
                                                <div class="d-flex justify-content-between">
                                                    <div>
                                                        <p class="fw-medium text-muted mb-0">Sessions</p>
                                                        <h2 class="mt-4 ff-secondary fw-semibold"><span class="counter-value" data-target="97.66">97.66</span>k</h2>
                                                        <p class="mb-0 text-muted"><span class="badge bg-light text-danger mb-0"><i class="ri-arrow-down-line align-middle"></i>3.96 % </span>vs. previous month</p>
                                                    </div>
                                                    <div>
                                                        <div class="avatar-sm flex-shrink-0">
                                                            <span class="avatar-title bg-primary-subtle rounded-circle fs-2">
                                                                <svg xmlns="http://www.w3.org/2000/svg" width="24" height="24" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round" class="feather feather-activity text-primary">
                                                                    <polyline points="22 12 18 12 15 21 9 3 6 12 2 12"></polyline></svg>
                                                            </span>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <!-- end card body -->
                                        </div>
                                        <!-- end card-->
                                    </div>
                                    <!-- end col-->
                                </div>
                                <!-- end row-->

                                <div class="row">
                                    <div class="col-md-3">
                                        <div class="card card-animate">
                                            <div class="card-body">
                                                <div class="d-flex justify-content-between">
                                                    <div>
                                                        <p class="fw-medium text-muted mb-0">Avg. Visit Duration</p>
                                                        <h2 class="mt-4 ff-secondary fw-semibold"><span class="counter-value" data-target="3">3</span>m <span class="counter-value" data-target="40">40</span>sec </h2>
                                                        <p class="mb-0 text-muted"><span class="badge bg-light text-danger mb-0"><i class="ri-arrow-down-line align-middle"></i>0.24 % </span>vs. previous month</p>
                                                    </div>
                                                    <div>
                                                        <div class="avatar-sm flex-shrink-0">
                                                            <span class="avatar-title bg-primary-subtle rounded-circle fs-2">
                                                                <svg xmlns="http://www.w3.org/2000/svg" width="24" height="24" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round" class="feather feather-clock text-primary">
                                                                    <circle cx="12" cy="12" r="10"></circle><polyline points="12 6 12 12 16 14"></polyline></svg>
                                                            </span>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <!-- end card body -->
                                        </div>
                                        <!-- end card-->
                                    </div>
                                    <!-- end col-->

                                    <div class="col-md-3">
                                        <div class="card card-animate">
                                            <div class="card-body">
                                                <div class="d-flex justify-content-between">
                                                    <div>
                                                        <p class="fw-medium text-muted mb-0">Bounce Rate</p>
                                                        <h2 class="mt-4 ff-secondary fw-semibold"><span class="counter-value" data-target="33.48">33.48</span>%</h2>
                                                        <p class="mb-0 text-muted"><span class="badge bg-light text-success mb-0"><i class="ri-arrow-up-line align-middle"></i>7.05 % </span>vs. previous month</p>
                                                    </div>
                                                    <div>
                                                        <div class="avatar-sm flex-shrink-0">
                                                            <span class="avatar-title bg-primary-subtle rounded-circle fs-2">
                                                                <svg xmlns="http://www.w3.org/2000/svg" width="24" height="24" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round" class="feather feather-external-link text-primary">
                                                                    <path d="M18 13v6a2 2 0 0 1-2 2H5a2 2 0 0 1-2-2V8a2 2 0 0 1 2-2h6"></path><polyline points="15 3 21 3 21 9"></polyline><line x1="10" y1="14" x2="21" y2="3"></line></svg>
                                                            </span>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <!-- end card body -->
                                        </div>
                                        <!-- end card-->
                                    </div>
                                    <div class="col-md-3">
                                        <div class="card card-animate">
                                            <div class="card-body">
                                                <div class="d-flex justify-content-between">
                                                    <div>
                                                        <p class="fw-medium text-muted mb-0">Bounce Rate</p>
                                                        <h2 class="mt-4 ff-secondary fw-semibold"><span class="counter-value" data-target="33.48">33.48</span>%</h2>
                                                        <p class="mb-0 text-muted"><span class="badge bg-light text-success mb-0"><i class="ri-arrow-up-line align-middle"></i>7.05 % </span>vs. previous month</p>
                                                    </div>
                                                    <div>
                                                        <div class="avatar-sm flex-shrink-0">
                                                            <span class="avatar-title bg-primary-subtle rounded-circle fs-2">
                                                                <svg xmlns="http://www.w3.org/2000/svg" width="24" height="24" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round" class="feather feather-external-link text-primary">
                                                                    <path d="M18 13v6a2 2 0 0 1-2 2H5a2 2 0 0 1-2-2V8a2 2 0 0 1 2-2h6"></path><polyline points="15 3 21 3 21 9"></polyline><line x1="10" y1="14" x2="21" y2="3"></line></svg>
                                                            </span>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <!-- end card body -->
                                        </div>
                                        <!-- end card-->
                                    </div>
                                    <div class="col-md-3">
                                        <div class="card card-animate">
                                            <div class="card-body">
                                                <div class="d-flex justify-content-between">
                                                    <div>
                                                        <p class="fw-medium text-muted mb-0">Bounce Rate</p>
                                                        <h2 class="mt-4 ff-secondary fw-semibold"><span class="counter-value" data-target="33.48">33.48</span>%</h2>
                                                        <p class="mb-0 text-muted"><span class="badge bg-light text-success mb-0"><i class="ri-arrow-up-line align-middle"></i>7.05 % </span>vs. previous month</p>
                                                    </div>
                                                    <div>
                                                        <div class="avatar-sm flex-shrink-0">
                                                            <span class="avatar-title bg-primary-subtle rounded-circle fs-2">
                                                                <svg xmlns="http://www.w3.org/2000/svg" width="24" height="24" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round" class="feather feather-external-link text-primary">
                                                                    <path d="M18 13v6a2 2 0 0 1-2 2H5a2 2 0 0 1-2-2V8a2 2 0 0 1 2-2h6"></path><polyline points="15 3 21 3 21 9"></polyline><line x1="10" y1="14" x2="21" y2="3"></line></svg>
                                                            </span>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <!-- end card body -->
                                        </div>
                                        <!-- end card-->
                                    </div>
                                    <!-- end col-->
                                </div>
                                <!-- end row-->
                            </div>
                        </div>
                        <!-- end col-->

                        <!-- end col -->
                    </div>
                    <!-- end row-->

                    <div class="row">
                        <div class="col-xl-6">
                            <div class="card">
                                <div class="card-header border-0 align-items-center d-flex">
                                    <h4 class="card-title mb-0 flex-grow-1">Audiences Metrics</h4>
                                    <div>
                                        <button type="button" class="btn btn-soft-secondary btn-sm">ALL </button>
                                        <button type="button" class="btn btn-soft-secondary btn-sm">1M </button>
                                        <button type="button" class="btn btn-soft-secondary btn-sm">6M </button>
                                        <button type="button" class="btn btn-soft-primary btn-sm">1Y </button>
                                    </div>
                                </div>
                                <!-- end card header -->
                                <div class="card-header p-0 border-0 bg-light-subtle">
                                    <div class="row g-0 text-center">
                                        <div class="col-6 col-sm-4">
                                            <div class="p-3 border border-dashed border-start-0">
                                                <h5 class="mb-1"><span class="counter-value" data-target="854">854</span> <span class="text-success ms-1 fs-12">49%<i class="ri-arrow-right-up-line ms-1 align-middle"></i></span> </h5>
                                                <p class="text-muted mb-0">Avg. Session</p>
                                            </div>
                                        </div>
                                        <!--end col-->
                                        <div class="col-6 col-sm-4">
                                            <div class="p-3 border border-dashed border-start-0">
                                                <h5 class="mb-1"><span class="counter-value" data-target="1278">1,278</span> <span class="text-success ms-1 fs-12">60%<i class="ri-arrow-right-up-line ms-1 align-middle"></i></span> </h5>
                                                <p class="text-muted mb-0">Conversion Rate</p>
                                            </div>
                                        </div>
                                        <!--end col-->
                                        <div class="col-6 col-sm-4">
                                            <div class="p-3 border border-dashed border-start-0 border-end-0">
                                                <h5 class="mb-1"><span class="counter-value" data-target="3">3</span>m <span class="counter-value" data-target="40">40</span>sec <span class="text-success ms-1 fs-12">37%<i class="ri-arrow-right-up-line ms-1 align-middle"></i></span> </h5>
                                                <p class="text-muted mb-0">Avg. Session Duration</p>
                                            </div>
                                        </div>
                                        <!--end col-->
                                    </div>
                                </div>
                                <!-- end card header -->
                                <div class="card-body p-0 pb-2">
                                    <div>
                                        <div id="audiences_metrics_charts" data-colors="[&quot;--vz-primary&quot;, &quot;--vz-light&quot;]" class="apex-charts" dir="ltr" style="min-height: 324px;">
                                            <div id="apexcharts2s0r5y8sk" class="apexcharts-canvas apexcharts2s0r5y8sk apexcharts-theme-light" style="width: 597px; height: 309px;">
                                                <svg id="SvgjsSvg1145" width="597" height="309" xmlns="http://www.w3.org/2000/svg" version="1.1" xmlns:xlink="http://www.w3.org/1999/xlink" xmlns:svgjs="http://svgjs.dev" class="apexcharts-svg" xmlns:data="ApexChartsNS" transform="translate(0, 0)" style="background: transparent;">
                                                    <foreignObject x="0" y="0" width="597" height="309">
                                                        <div class="apexcharts-legend apexcharts-align-center apx-legend-position-bottom" xmlns="http://www.w3.org/1999/xhtml" style="inset: auto 0px 5px 20px; position: absolute; max-height: 154.5px;">
                                                            <div class="apexcharts-legend-series" rel="1" seriesname="LastxYear" data:collapsed="false" style="margin: 2px 5px;"><span class="apexcharts-legend-marker" rel="1" data:collapsed="false" style="background: rgb(37, 160, 226) !important; color: rgb(37, 160, 226); height: 9px; width: 9px; left: 0px; top: 0px; border-width: 0px; border-color: rgb(255, 255, 255); border-radius: 4px;"></span><span class="apexcharts-legend-text" rel="1" i="0" data:default-text="Last%20Year" data:collapsed="false" style="color: rgb(55, 61, 63); font-size: 8px; font-weight: 400; font-family: Helvetica, Arial, sans-serif;">Last Year</span></div><div class="apexcharts-legend-series" rel="2" seriesname="CurrentxYear" data:collapsed="false" style="margin: 2px 5px;"><span class="apexcharts-legend-marker" rel="2" data:collapsed="false" style="background: rgb(243, 246, 249) !important; color: rgb(243, 246, 249); height: 9px; width: 9px; left: 0px; top: 0px; border-width: 0px; border-color: rgb(255, 255, 255); border-radius: 4px;"></span><span class="apexcharts-legend-text" rel="2" i="1" data:default-text="Current%20Year" data:collapsed="false" style="color: rgb(55, 61, 63); font-size: 8px; font-weight: 400; font-family: Helvetica, Arial, sans-serif;">Current Year</span></div>
                                                        </div>
                                                        <style type="text/css">
                                                            .apexcharts-legend {
                                                                display: flex;
                                                                overflow: auto;
                                                                padding: 0 10px;
                                                            }

                                                                .apexcharts-legend.apx-legend-position-bottom, .apexcharts-legend.apx-legend-position-top {
                                                                    flex-wrap: wrap
                                                                }

                                                                .apexcharts-legend.apx-legend-position-right, .apexcharts-legend.apx-legend-position-left {
                                                                    flex-direction: column;
                                                                    bottom: 0;
                                                                }

                                                                .apexcharts-legend.apx-legend-position-bottom.apexcharts-align-left, .apexcharts-legend.apx-legend-position-top.apexcharts-align-left, .apexcharts-legend.apx-legend-position-right, .apexcharts-legend.apx-legend-position-left {
                                                                    justify-content: flex-start;
                                                                }

                                                                .apexcharts-legend.apx-legend-position-bottom.apexcharts-align-center, .apexcharts-legend.apx-legend-position-top.apexcharts-align-center {
                                                                    justify-content: center;
                                                                }

                                                                .apexcharts-legend.apx-legend-position-bottom.apexcharts-align-right, .apexcharts-legend.apx-legend-position-top.apexcharts-align-right {
                                                                    justify-content: flex-end;
                                                                }

                                                            .apexcharts-legend-series {
                                                                cursor: pointer;
                                                                line-height: normal;
                                                            }

                                                            .apexcharts-legend.apx-legend-position-bottom .apexcharts-legend-series, .apexcharts-legend.apx-legend-position-top .apexcharts-legend-series {
                                                                display: flex;
                                                                align-items: center;
                                                            }

                                                            .apexcharts-legend-text {
                                                                position: relative;
                                                                font-size: 14px;
                                                            }

                                                                .apexcharts-legend-text *, .apexcharts-legend-marker * {
                                                                    pointer-events: none;
                                                                }

                                                            .apexcharts-legend-marker {
                                                                position: relative;
                                                                display: inline-block;
                                                                cursor: pointer;
                                                                margin-right: 3px;
                                                                border-style: solid;
                                                            }

                                                            .apexcharts-legend.apexcharts-align-right .apexcharts-legend-series, .apexcharts-legend.apexcharts-align-left .apexcharts-legend-series {
                                                                display: inline-block;
                                                            }

                                                            .apexcharts-legend-series.apexcharts-no-click {
                                                                cursor: auto;
                                                            }

                                                            .apexcharts-legend .apexcharts-hidden-zero-series, .apexcharts-legend .apexcharts-hidden-null-series {
                                                                display: none !important;
                                                            }

                                                            .apexcharts-inactive-legend {
                                                                opacity: 0.45;
                                                            }
                                                        </style>
                                                    </foreignObject><g id="SvgjsG1263" class="apexcharts-yaxis" rel="0" transform="translate(-18, 0)"></g><g id="SvgjsG1147" class="apexcharts-inner apexcharts-graphical" transform="translate(12, 30)"><defs id="SvgjsDefs1146"><linearGradient id="SvgjsLinearGradient1149" x1="0" y1="0" x2="0" y2="1"><stop id="SvgjsStop1150" stop-opacity="0.4" stop-color="rgba(216,227,240,0.4)" offset="0"></stop>
                                                        <stop id="SvgjsStop1151" stop-opacity="0.5" stop-color="rgba(190,209,230,0.5)" offset="1"></stop>
                                                        <stop id="SvgjsStop1152" stop-opacity="0.5" stop-color="rgba(190,209,230,0.5)" offset="1"></stop>
                                                    </linearGradient>
                                                        <clipPath id="gridRectMask2s0r5y8sk">
                                                            <rect id="SvgjsRect1154" width="581" height="218.112" x="-3" y="-1" rx="0" ry="0" opacity="1" stroke-width="0" stroke="none" stroke-dasharray="0" fill="#fff"></rect>
                                                        </clipPath>
                                                        <clipPath id="forecastMask2s0r5y8sk"></clipPath>
                                                        <clipPath id="nonForecastMask2s0r5y8sk"></clipPath>
                                                        <clipPath id="gridRectMarkerMask2s0r5y8sk">
                                                            <rect id="SvgjsRect1155" width="579" height="220.112" x="-2" y="-2" rx="0" ry="0" opacity="1" stroke-width="0" stroke="none" stroke-dasharray="0" fill="#fff"></rect>
                                                        </clipPath>
                                                    </defs>
                                                        <rect id="SvgjsRect1153" width="9.583333333333332" height="216.112" x="0" y="0" rx="0" ry="0" opacity="1" stroke-width="0" stroke-dasharray="3" fill="url(#SvgjsLinearGradient1149)" class="apexcharts-xcrosshairs" y2="216.112" filter="none" fill-opacity="0.9"></rect>
                                                        <g id="SvgjsG1211" class="apexcharts-grid">
                                                            <g id="SvgjsG1212" class="apexcharts-gridlines-horizontal" style="display: none;">
                                                                <line id="SvgjsLine1215" x1="0" y1="0" x2="575" y2="0" stroke="#e0e0e0" stroke-dasharray="0" stroke-linecap="butt" class="apexcharts-gridline"></line>
                                                                <line id="SvgjsLine1216" x1="0" y1="54.028" x2="575" y2="54.028" stroke="#e0e0e0" stroke-dasharray="0" stroke-linecap="butt" class="apexcharts-gridline"></line>
                                                                <line id="SvgjsLine1217" x1="0" y1="108.056" x2="575" y2="108.056" stroke="#e0e0e0" stroke-dasharray="0" stroke-linecap="butt" class="apexcharts-gridline"></line>
                                                                <line id="SvgjsLine1218" x1="0" y1="162.084" x2="575" y2="162.084" stroke="#e0e0e0" stroke-dasharray="0" stroke-linecap="butt" class="apexcharts-gridline"></line>
                                                                <line id="SvgjsLine1219" x1="0" y1="216.112" x2="575" y2="216.112" stroke="#e0e0e0" stroke-dasharray="0" stroke-linecap="butt" class="apexcharts-gridline"></line>
                                                            </g>
                                                            <g id="SvgjsG1213" class="apexcharts-gridlines-vertical" style="display: none;"></g>
                                                            <line id="SvgjsLine1221" x1="0" y1="216.112" x2="575" y2="216.112" stroke="transparent" stroke-dasharray="0" stroke-linecap="butt"></line>
                                                            <line id="SvgjsLine1220" x1="0" y1="1" x2="0" y2="216.112" stroke="transparent" stroke-dasharray="0" stroke-linecap="butt"></line>
                                                        </g>
                                                        <g id="SvgjsG1156" class="apexcharts-bar-series apexcharts-plot-series">
                                                            <g id="SvgjsG1157" class="apexcharts-series" seriesName="LastxYear" rel="1" data:realIndex="0">
                                                                <path id="SvgjsPath1161" d="M 19.166666666666664 216.113 L 19.166666666666664 147.76757999999998 L 26.749999999999996 147.76757999999998 L 26.749999999999996 216.113 z" fill="rgba(37,160,226,1)" fill-opacity="1" stroke="transparent" stroke-opacity="1" stroke-linecap="round" stroke-width="2" stroke-dasharray="0" class="apexcharts-bar-area" index="0" clip-path="url(#gridRectMask2s0r5y8sk)" pathTo="M 19.166666666666664 216.113 L 19.166666666666664 147.76757999999998 L 26.749999999999996 147.76757999999998 L 26.749999999999996 216.113 z" pathFrom="M 19.166666666666664 216.113 L 19.166666666666664 216.113 L 26.749999999999996 216.113 L 26.749999999999996 216.113 L 26.749999999999996 216.113 L 26.749999999999996 216.113 L 26.749999999999996 216.113 L 19.166666666666664 216.113 z" cy="147.76657999999998" cx="66.08333333333333" j="0" val="25.3" barHeight="68.34542" barWidth="9.583333333333332"></path>
                                                                <path id="SvgjsPath1163" d="M 67.08333333333333 216.113 L 67.08333333333333 182.3455 L 74.66666666666666 182.3455 L 74.66666666666666 216.113 z" fill="rgba(37,160,226,1)" fill-opacity="1" stroke="transparent" stroke-opacity="1" stroke-linecap="round" stroke-width="2" stroke-dasharray="0" class="apexcharts-bar-area" index="0" clip-path="url(#gridRectMask2s0r5y8sk)" pathTo="M 67.08333333333333 216.113 L 67.08333333333333 182.3455 L 74.66666666666666 182.3455 L 74.66666666666666 216.113 z" pathFrom="M 67.08333333333333 216.113 L 67.08333333333333 216.113 L 74.66666666666666 216.113 L 74.66666666666666 216.113 L 74.66666666666666 216.113 L 74.66666666666666 216.113 L 74.66666666666666 216.113 L 67.08333333333333 216.113 z" cy="182.34449999999998" cx="114" j="1" val="12.5" barHeight="33.7675" barWidth="9.583333333333332"></path>
                                                                <path id="SvgjsPath1165" d="M 115 216.113 L 115 161.54472 L 122.58333333333333 161.54472 L 122.58333333333333 216.113 z" fill="rgba(37,160,226,1)" fill-opacity="1" stroke="transparent" stroke-opacity="1" stroke-linecap="round" stroke-width="2" stroke-dasharray="0" class="apexcharts-bar-area" index="0" clip-path="url(#gridRectMask2s0r5y8sk)" pathTo="M 115 216.113 L 115 161.54472 L 122.58333333333333 161.54472 L 122.58333333333333 216.113 z" pathFrom="M 115 216.113 L 115 216.113 L 122.58333333333333 216.113 L 122.58333333333333 216.113 L 122.58333333333333 216.113 L 122.58333333333333 216.113 L 122.58333333333333 216.113 L 115 216.113 z" cy="161.54372" cx="161.91666666666666" j="2" val="20.2" barHeight="54.568279999999994" barWidth="9.583333333333332"></path>
                                                                <path id="SvgjsPath1167" d="M 162.91666666666666 216.113 L 162.91666666666666 166.1371 L 170.5 166.1371 L 170.5 216.113 z" fill="rgba(37,160,226,1)" fill-opacity="1" stroke="transparent" stroke-opacity="1" stroke-linecap="round" stroke-width="2" stroke-dasharray="0" class="apexcharts-bar-area" index="0" clip-path="url(#gridRectMask2s0r5y8sk)" pathTo="M 162.91666666666666 216.113 L 162.91666666666666 166.1371 L 170.5 166.1371 L 170.5 216.113 z" pathFrom="M 162.91666666666666 216.113 L 162.91666666666666 216.113 L 170.5 216.113 L 170.5 216.113 L 170.5 216.113 L 170.5 216.113 L 170.5 216.113 L 162.91666666666666 216.113 z" cy="166.1361" cx="209.83333333333331" j="3" val="18.5" barHeight="49.975899999999996" barWidth="9.583333333333332"></path>
                                                                <path id="SvgjsPath1169" d="M 210.83333333333331 216.113 L 210.83333333333331 106.97644000000001 L 218.41666666666666 106.97644000000001 L 218.41666666666666 216.113 z" fill="rgba(37,160,226,1)" fill-opacity="1" stroke="transparent" stroke-opacity="1" stroke-linecap="round" stroke-width="2" stroke-dasharray="0" class="apexcharts-bar-area" index="0" clip-path="url(#gridRectMask2s0r5y8sk)" pathTo="M 210.83333333333331 216.113 L 210.83333333333331 106.97644000000001 L 218.41666666666666 106.97644000000001 L 218.41666666666666 216.113 z" pathFrom="M 210.83333333333331 216.113 L 210.83333333333331 216.113 L 218.41666666666666 216.113 L 218.41666666666666 216.113 L 218.41666666666666 216.113 L 218.41666666666666 216.113 L 218.41666666666666 216.113 L 210.83333333333331 216.113 z" cy="106.97544" cx="257.75" j="4" val="40.4" barHeight="109.13655999999999" barWidth="9.583333333333332"></path>
                                                                <path id="SvgjsPath1171" d="M 258.75 216.113 L 258.75 147.49744 L 266.3333333333333 147.49744 L 266.3333333333333 216.113 z" fill="rgba(37,160,226,1)" fill-opacity="1" stroke="transparent" stroke-opacity="1" stroke-linecap="round" stroke-width="2" stroke-dasharray="0" class="apexcharts-bar-area" index="0" clip-path="url(#gridRectMask2s0r5y8sk)" pathTo="M 258.75 216.113 L 258.75 147.49744 L 266.3333333333333 147.49744 L 266.3333333333333 216.113 z" pathFrom="M 258.75 216.113 L 258.75 216.113 L 266.3333333333333 216.113 L 266.3333333333333 216.113 L 266.3333333333333 216.113 L 266.3333333333333 216.113 L 266.3333333333333 216.113 L 258.75 216.113 z" cy="147.49644" cx="305.6666666666667" j="5" val="25.4" barHeight="68.61555999999999" barWidth="9.583333333333332"></path>
                                                                <path id="SvgjsPath1173" d="M 306.6666666666667 216.113 L 306.6666666666667 173.43088 L 314.25 173.43088 L 314.25 216.113 z" fill="rgba(37,160,226,1)" fill-opacity="1" stroke="transparent" stroke-opacity="1" stroke-linecap="round" stroke-width="2" stroke-dasharray="0" class="apexcharts-bar-area" index="0" clip-path="url(#gridRectMask2s0r5y8sk)" pathTo="M 306.6666666666667 216.113 L 306.6666666666667 173.43088 L 314.25 173.43088 L 314.25 216.113 z" pathFrom="M 306.6666666666667 216.113 L 306.6666666666667 216.113 L 314.25 216.113 L 314.25 216.113 L 314.25 216.113 L 314.25 216.113 L 314.25 216.113 L 306.6666666666667 216.113 z" cy="173.42988" cx="353.58333333333337" j="6" val="15.8" barHeight="42.68212" barWidth="9.583333333333332"></path>
                                                                <path id="SvgjsPath1175" d="M 354.58333333333337 216.113 L 354.58333333333337 155.87178 L 362.1666666666667 155.87178 L 362.1666666666667 216.113 z" fill="rgba(37,160,226,1)" fill-opacity="1" stroke="transparent" stroke-opacity="1" stroke-linecap="round" stroke-width="2" stroke-dasharray="0" class="apexcharts-bar-area" index="0" clip-path="url(#gridRectMask2s0r5y8sk)" pathTo="M 354.58333333333337 216.113 L 354.58333333333337 155.87178 L 362.1666666666667 155.87178 L 362.1666666666667 216.113 z" pathFrom="M 354.58333333333337 216.113 L 354.58333333333337 216.113 L 362.1666666666667 216.113 L 362.1666666666667 216.113 L 362.1666666666667 216.113 L 362.1666666666667 216.113 L 362.1666666666667 216.113 L 354.58333333333337 216.113 z" cy="155.87078" cx="401.50000000000006" j="7" val="22.3" barHeight="60.24122" barWidth="9.583333333333332"></path>
                                                                <path id="SvgjsPath1177" d="M 402.50000000000006 216.113 L 402.50000000000006 164.24612 L 410.08333333333337 164.24612 L 410.08333333333337 216.113 z" fill="rgba(37,160,226,1)" fill-opacity="1" stroke="transparent" stroke-opacity="1" stroke-linecap="round" stroke-width="2" stroke-dasharray="0" class="apexcharts-bar-area" index="0" clip-path="url(#gridRectMask2s0r5y8sk)" pathTo="M 402.50000000000006 216.113 L 402.50000000000006 164.24612 L 410.08333333333337 164.24612 L 410.08333333333337 216.113 z" pathFrom="M 402.50000000000006 216.113 L 402.50000000000006 216.113 L 410.08333333333337 216.113 L 410.08333333333337 216.113 L 410.08333333333337 216.113 L 410.08333333333337 216.113 L 410.08333333333337 216.113 L 402.50000000000006 216.113 z" cy="164.24512" cx="449.41666666666674" j="8" val="19.2" barHeight="51.866879999999995" barWidth="9.583333333333332"></path>
                                                                <path id="SvgjsPath1179" d="M 450.41666666666674 216.113 L 450.41666666666674 147.76757999999998 L 458.00000000000006 147.76757999999998 L 458.00000000000006 216.113 z" fill="rgba(37,160,226,1)" fill-opacity="1" stroke="transparent" stroke-opacity="1" stroke-linecap="round" stroke-width="2" stroke-dasharray="0" class="apexcharts-bar-area" index="0" clip-path="url(#gridRectMask2s0r5y8sk)" pathTo="M 450.41666666666674 216.113 L 450.41666666666674 147.76757999999998 L 458.00000000000006 147.76757999999998 L 458.00000000000006 216.113 z" pathFrom="M 450.41666666666674 216.113 L 450.41666666666674 216.113 L 458.00000000000006 216.113 L 458.00000000000006 216.113 L 458.00000000000006 216.113 L 458.00000000000006 216.113 L 458.00000000000006 216.113 L 450.41666666666674 216.113 z" cy="147.76657999999998" cx="497.3333333333334" j="9" val="25.3" barHeight="68.34542" barWidth="9.583333333333332"></path>
                                                                <path id="SvgjsPath1181" d="M 498.3333333333334 216.113 L 498.3333333333334 182.3455 L 505.91666666666674 182.3455 L 505.91666666666674 216.113 z" fill="rgba(37,160,226,1)" fill-opacity="1" stroke="transparent" stroke-opacity="1" stroke-linecap="round" stroke-width="2" stroke-dasharray="0" class="apexcharts-bar-area" index="0" clip-path="url(#gridRectMask2s0r5y8sk)" pathTo="M 498.3333333333334 216.113 L 498.3333333333334 182.3455 L 505.91666666666674 182.3455 L 505.91666666666674 216.113 z" pathFrom="M 498.3333333333334 216.113 L 498.3333333333334 216.113 L 505.91666666666674 216.113 L 505.91666666666674 216.113 L 505.91666666666674 216.113 L 505.91666666666674 216.113 L 505.91666666666674 216.113 L 498.3333333333334 216.113 z" cy="182.34449999999998" cx="545.2500000000001" j="10" val="12.5" barHeight="33.7675" barWidth="9.583333333333332"></path>
                                                                <path id="SvgjsPath1183" d="M 546.2500000000001 216.113 L 546.2500000000001 161.54472 L 553.8333333333335 161.54472 L 553.8333333333335 216.113 z" fill="rgba(37,160,226,1)" fill-opacity="1" stroke="transparent" stroke-opacity="1" stroke-linecap="round" stroke-width="2" stroke-dasharray="0" class="apexcharts-bar-area" index="0" clip-path="url(#gridRectMask2s0r5y8sk)" pathTo="M 546.2500000000001 216.113 L 546.2500000000001 161.54472 L 553.8333333333335 161.54472 L 553.8333333333335 216.113 z" pathFrom="M 546.2500000000001 216.113 L 546.2500000000001 216.113 L 553.8333333333335 216.113 L 553.8333333333335 216.113 L 553.8333333333335 216.113 L 553.8333333333335 216.113 L 553.8333333333335 216.113 L 546.2500000000001 216.113 z" cy="161.54372" cx="593.1666666666667" j="11" val="20.2" barHeight="54.568279999999994" barWidth="9.583333333333332"></path>
                                                                <g id="SvgjsG1159" class="apexcharts-bar-goals-markers" style="pointer-events: none">
                                                                    <g id="SvgjsG1160" className="apexcharts-bar-goals-groups" class="apexcharts-hidden-element-shown" clip-path="url(#gridRectMarkerMask2s0r5y8sk)"></g>
                                                                    <g id="SvgjsG1162" className="apexcharts-bar-goals-groups" class="apexcharts-hidden-element-shown" clip-path="url(#gridRectMarkerMask2s0r5y8sk)"></g>
                                                                    <g id="SvgjsG1164" className="apexcharts-bar-goals-groups" class="apexcharts-hidden-element-shown" clip-path="url(#gridRectMarkerMask2s0r5y8sk)"></g>
                                                                    <g id="SvgjsG1166" className="apexcharts-bar-goals-groups" class="apexcharts-hidden-element-shown" clip-path="url(#gridRectMarkerMask2s0r5y8sk)"></g>
                                                                    <g id="SvgjsG1168" className="apexcharts-bar-goals-groups" class="apexcharts-hidden-element-shown" clip-path="url(#gridRectMarkerMask2s0r5y8sk)"></g>
                                                                    <g id="SvgjsG1170" className="apexcharts-bar-goals-groups" class="apexcharts-hidden-element-shown" clip-path="url(#gridRectMarkerMask2s0r5y8sk)"></g>
                                                                    <g id="SvgjsG1172" className="apexcharts-bar-goals-groups" class="apexcharts-hidden-element-shown" clip-path="url(#gridRectMarkerMask2s0r5y8sk)"></g>
                                                                    <g id="SvgjsG1174" className="apexcharts-bar-goals-groups" class="apexcharts-hidden-element-shown" clip-path="url(#gridRectMarkerMask2s0r5y8sk)"></g>
                                                                    <g id="SvgjsG1176" className="apexcharts-bar-goals-groups" class="apexcharts-hidden-element-shown" clip-path="url(#gridRectMarkerMask2s0r5y8sk)"></g>
                                                                    <g id="SvgjsG1178" className="apexcharts-bar-goals-groups" class="apexcharts-hidden-element-shown" clip-path="url(#gridRectMarkerMask2s0r5y8sk)"></g>
                                                                    <g id="SvgjsG1180" className="apexcharts-bar-goals-groups" class="apexcharts-hidden-element-shown" clip-path="url(#gridRectMarkerMask2s0r5y8sk)"></g>
                                                                    <g id="SvgjsG1182" className="apexcharts-bar-goals-groups" class="apexcharts-hidden-element-shown" clip-path="url(#gridRectMarkerMask2s0r5y8sk)"></g>
                                                                </g>
                                                            </g>
                                                            <g id="SvgjsG1184" class="apexcharts-series" seriesName="CurrentxYear" rel="2" data:realIndex="1">
                                                                <path id="SvgjsPath1188" d="M 19.166666666666664 147.76858 L 19.166666666666664 55.97789999999997 C 19.166666666666664 52.97789999999997 22.166666666666664 49.97789999999997 25.166666666666664 49.97789999999997 L 25.166666666666664 49.97789999999997 C 25.95833333333333 49.97789999999997 26.749999999999996 52.97789999999997 26.749999999999996 55.97789999999997 L 26.749999999999996 147.76858 z " fill="rgba(243,246,249,1)" fill-opacity="1" stroke="transparent" stroke-opacity="1" stroke-linecap="round" stroke-width="2" stroke-dasharray="0" class="apexcharts-bar-area" index="1" clip-path="url(#gridRectMask2s0r5y8sk)" pathTo="M 19.166666666666664 147.76858 L 19.166666666666664 55.97789999999997 C 19.166666666666664 52.97789999999997 22.166666666666664 49.97789999999997 25.166666666666664 49.97789999999997 L 25.166666666666664 49.97789999999997 C 25.95833333333333 49.97789999999997 26.749999999999996 52.97789999999997 26.749999999999996 55.97789999999997 L 26.749999999999996 147.76858 z " pathFrom="M 19.166666666666664 147.76858 L 19.166666666666664 147.76858 L 26.749999999999996 147.76858 L 26.749999999999996 147.76858 L 26.749999999999996 147.76858 L 26.749999999999996 147.76858 L 26.749999999999996 147.76858 L 19.166666666666664 147.76858 z" cy="49.97689999999997" cx="66.08333333333333" j="0" val="36.2" barHeight="97.79068000000001" barWidth="9.583333333333332"></path>
                                                                <path id="SvgjsPath1190" d="M 67.08333333333333 182.3465 L 67.08333333333333 127.83514 C 67.08333333333333 124.83514 70.08333333333333 121.83514 73.08333333333333 121.83514 L 73.08333333333333 121.83514 C 73.875 121.83514 74.66666666666666 124.83514 74.66666666666666 127.83514 L 74.66666666666666 182.3465 z " fill="rgba(243,246,249,1)" fill-opacity="1" stroke="transparent" stroke-opacity="1" stroke-linecap="round" stroke-width="2" stroke-dasharray="0" class="apexcharts-bar-area" index="1" clip-path="url(#gridRectMask2s0r5y8sk)" pathTo="M 67.08333333333333 182.3465 L 67.08333333333333 127.83514 C 67.08333333333333 124.83514 70.08333333333333 121.83514 73.08333333333333 121.83514 L 73.08333333333333 121.83514 C 73.875 121.83514 74.66666666666666 124.83514 74.66666666666666 127.83514 L 74.66666666666666 182.3465 z " pathFrom="M 67.08333333333333 182.3465 L 67.08333333333333 182.3465 L 74.66666666666666 182.3465 L 74.66666666666666 182.3465 L 74.66666666666666 182.3465 L 74.66666666666666 182.3465 L 74.66666666666666 182.3465 L 67.08333333333333 182.3465 z" cy="121.83413999999999" cx="114" j="1" val="22.4" barHeight="60.511359999999996" barWidth="9.583333333333332"></path>
                                                                <path id="SvgjsPath1192" d="M 115 161.54572000000002 L 115 64.35224 C 115 61.352239999999995 118 58.35224 121 58.35224 L 121 58.35224 C 121.79166666666666 58.35224 122.58333333333333 61.352239999999995 122.58333333333333 64.35224 L 122.58333333333333 161.54572000000002 z " fill="rgba(243,246,249,1)" fill-opacity="1" stroke="transparent" stroke-opacity="1" stroke-linecap="round" stroke-width="2" stroke-dasharray="0" class="apexcharts-bar-area" index="1" clip-path="url(#gridRectMask2s0r5y8sk)" pathTo="M 115 161.54572000000002 L 115 64.35224 C 115 61.352239999999995 118 58.35224 121 58.35224 L 121 58.35224 C 121.79166666666666 58.35224 122.58333333333333 61.352239999999995 122.58333333333333 64.35224 L 122.58333333333333 161.54572000000002 z " pathFrom="M 115 161.54572000000002 L 115 161.54572000000002 L 122.58333333333333 161.54572000000002 L 122.58333333333333 161.54572000000002 L 122.58333333333333 161.54572000000002 L 122.58333333333333 161.54572000000002 L 122.58333333333333 161.54572000000002 L 115 161.54572000000002 z" cy="58.351240000000004" cx="161.91666666666666" j="2" val="38.2" barHeight="103.19348000000001" barWidth="9.583333333333332"></path>
                                                                <path id="SvgjsPath1194" d="M 162.91666666666666 166.1381 L 162.91666666666666 89.74540000000002 C 162.91666666666666 86.74540000000002 165.91666666666666 83.74540000000002 168.91666666666666 83.74540000000002 L 168.91666666666666 83.74540000000002 C 169.70833333333331 83.74540000000002 170.5 86.74540000000002 170.5 89.74540000000002 L 170.5 166.1381 z " fill="rgba(243,246,249,1)" fill-opacity="1" stroke="transparent" stroke-opacity="1" stroke-linecap="round" stroke-width="2" stroke-dasharray="0" class="apexcharts-bar-area" index="1" clip-path="url(#gridRectMask2s0r5y8sk)" pathTo="M 162.91666666666666 166.1381 L 162.91666666666666 89.74540000000002 C 162.91666666666666 86.74540000000002 165.91666666666666 83.74540000000002 168.91666666666666 83.74540000000002 L 168.91666666666666 83.74540000000002 C 169.70833333333331 83.74540000000002 170.5 86.74540000000002 170.5 89.74540000000002 L 170.5 166.1381 z " pathFrom="M 162.91666666666666 166.1381 L 162.91666666666666 166.1381 L 170.5 166.1381 L 170.5 166.1381 L 170.5 166.1381 L 170.5 166.1381 L 170.5 166.1381 L 162.91666666666666 166.1381 z" cy="83.74440000000001" cx="209.83333333333331" j="3" val="30.5" barHeight="82.39269999999999" barWidth="9.583333333333332"></path>
                                                                <path id="SvgjsPath1196" d="M 210.83333333333331 106.97744000000002 L 210.83333333333331 41.660480000000014 C 210.83333333333331 38.660480000000014 213.83333333333331 35.660480000000014 216.83333333333331 35.660480000000014 L 216.83333333333331 35.660480000000014 C 217.625 35.660480000000014 218.41666666666666 38.660480000000014 218.41666666666666 41.660480000000014 L 218.41666666666666 106.97744000000002 z " fill="rgba(243,246,249,1)" fill-opacity="1" stroke="transparent" stroke-opacity="1" stroke-linecap="round" stroke-width="2" stroke-dasharray="0" class="apexcharts-bar-area" index="1" clip-path="url(#gridRectMask2s0r5y8sk)" pathTo="M 210.83333333333331 106.97744000000002 L 210.83333333333331 41.660480000000014 C 210.83333333333331 38.660480000000014 213.83333333333331 35.660480000000014 216.83333333333331 35.660480000000014 L 216.83333333333331 35.660480000000014 C 217.625 35.660480000000014 218.41666666666666 38.660480000000014 218.41666666666666 41.660480000000014 L 218.41666666666666 106.97744000000002 z " pathFrom="M 210.83333333333331 106.97744000000002 L 210.83333333333331 106.97744000000002 L 218.41666666666666 106.97744000000002 L 218.41666666666666 106.97744000000002 L 218.41666666666666 106.97744000000002 L 218.41666666666666 106.97744000000002 L 218.41666666666666 106.97744000000002 L 210.83333333333331 106.97744000000002 z" cy="35.659480000000016" cx="257.75" j="4" val="26.4" barHeight="71.31696" barWidth="9.583333333333332"></path>
                                                                <path id="SvgjsPath1198" d="M 258.75 147.49844000000002 L 258.75 71.37588000000002 C 258.75 68.37588000000002 261.75 65.37588000000002 264.75 65.37588000000002 L 264.75 65.37588000000002 C 265.54166666666663 65.37588000000002 266.3333333333333 68.37588000000002 266.3333333333333 71.37588000000002 L 266.3333333333333 147.49844000000002 z " fill="rgba(243,246,249,1)" fill-opacity="1" stroke="transparent" stroke-opacity="1" stroke-linecap="round" stroke-width="2" stroke-dasharray="0" class="apexcharts-bar-area" index="1" clip-path="url(#gridRectMask2s0r5y8sk)" pathTo="M 258.75 147.49844000000002 L 258.75 71.37588000000002 C 258.75 68.37588000000002 261.75 65.37588000000002 264.75 65.37588000000002 L 264.75 65.37588000000002 C 265.54166666666663 65.37588000000002 266.3333333333333 68.37588000000002 266.3333333333333 71.37588000000002 L 266.3333333333333 147.49844000000002 z " pathFrom="M 258.75 147.49844000000002 L 258.75 147.49844000000002 L 266.3333333333333 147.49844000000002 L 266.3333333333333 147.49844000000002 L 266.3333333333333 147.49844000000002 L 266.3333333333333 147.49844000000002 L 266.3333333333333 147.49844000000002 L 258.75 147.49844000000002 z" cy="65.37488000000002" cx="305.6666666666667" j="5" val="30.4" barHeight="82.12256" barWidth="9.583333333333332"></path>
                                                                <path id="SvgjsPath1200" d="M 306.6666666666667 173.43188 L 306.6666666666667 124.86360000000002 C 306.6666666666667 121.86360000000002 309.6666666666667 118.86360000000002 312.6666666666667 118.86360000000002 L 312.6666666666667 118.86360000000002 C 313.45833333333337 118.86360000000002 314.25 121.86360000000002 314.25 124.86360000000002 L 314.25 173.43188 z " fill="rgba(243,246,249,1)" fill-opacity="1" stroke="transparent" stroke-opacity="1" stroke-linecap="round" stroke-width="2" stroke-dasharray="0" class="apexcharts-bar-area" index="1" clip-path="url(#gridRectMask2s0r5y8sk)" pathTo="M 306.6666666666667 173.43188 L 306.6666666666667 124.86360000000002 C 306.6666666666667 121.86360000000002 309.6666666666667 118.86360000000002 312.6666666666667 118.86360000000002 L 312.6666666666667 118.86360000000002 C 313.45833333333337 118.86360000000002 314.25 121.86360000000002 314.25 124.86360000000002 L 314.25 173.43188 z " pathFrom="M 306.6666666666667 173.43188 L 306.6666666666667 173.43188 L 314.25 173.43188 L 314.25 173.43188 L 314.25 173.43188 L 314.25 173.43188 L 314.25 173.43188 L 306.6666666666667 173.43188 z" cy="118.86260000000001" cx="353.58333333333337" j="6" val="20.2" barHeight="54.568279999999994" barWidth="9.583333333333332"></path>
                                                                <path id="SvgjsPath1202" d="M 354.58333333333337 155.87278 L 354.58333333333337 81.91134000000001 C 354.58333333333337 78.91134000000001 357.58333333333337 75.91134000000001 360.58333333333337 75.91134000000001 L 360.58333333333337 75.91134000000001 C 361.375 75.91134000000001 362.1666666666667 78.91134000000001 362.1666666666667 81.91134000000001 L 362.1666666666667 155.87278 z " fill="rgba(243,246,249,1)" fill-opacity="1" stroke="transparent" stroke-opacity="1" stroke-linecap="round" stroke-width="2" stroke-dasharray="0" class="apexcharts-bar-area" index="1" clip-path="url(#gridRectMask2s0r5y8sk)" pathTo="M 354.58333333333337 155.87278 L 354.58333333333337 81.91134000000001 C 354.58333333333337 78.91134000000001 357.58333333333337 75.91134000000001 360.58333333333337 75.91134000000001 L 360.58333333333337 75.91134000000001 C 361.375 75.91134000000001 362.1666666666667 78.91134000000001 362.1666666666667 81.91134000000001 L 362.1666666666667 155.87278 z " pathFrom="M 354.58333333333337 155.87278 L 354.58333333333337 155.87278 L 362.1666666666667 155.87278 L 362.1666666666667 155.87278 L 362.1666666666667 155.87278 L 362.1666666666667 155.87278 L 362.1666666666667 155.87278 L 354.58333333333337 155.87278 z" cy="75.91034" cx="401.50000000000006" j="7" val="29.6" barHeight="79.96144" barWidth="9.583333333333332"></path>
                                                                <path id="SvgjsPath1204" d="M 402.50000000000006 164.24712 L 402.50000000000006 140.80186 C 402.50000000000006 137.80186 405.50000000000006 134.80186 408.50000000000006 134.80186 L 408.50000000000006 134.80186 C 409.29166666666674 134.80186 410.08333333333337 137.80186 410.08333333333337 140.80186 L 410.08333333333337 164.24712 z " fill="rgba(243,246,249,1)" fill-opacity="1" stroke="transparent" stroke-opacity="1" stroke-linecap="round" stroke-width="2" stroke-dasharray="0" class="apexcharts-bar-area" index="1" clip-path="url(#gridRectMask2s0r5y8sk)" pathTo="M 402.50000000000006 164.24712 L 402.50000000000006 140.80186 C 402.50000000000006 137.80186 405.50000000000006 134.80186 408.50000000000006 134.80186 L 408.50000000000006 134.80186 C 409.29166666666674 134.80186 410.08333333333337 137.80186 410.08333333333337 140.80186 L 410.08333333333337 164.24712 z " pathFrom="M 402.50000000000006 164.24712 L 402.50000000000006 164.24712 L 410.08333333333337 164.24712 L 410.08333333333337 164.24712 L 410.08333333333337 164.24712 L 410.08333333333337 164.24712 L 410.08333333333337 164.24712 L 402.50000000000006 164.24712 z" cy="134.80086" cx="449.41666666666674" j="8" val="10.9" barHeight="29.44526" barWidth="9.583333333333332"></path>
                                                                <path id="SvgjsPath1206" d="M 450.41666666666674 147.76858 L 450.41666666666674 55.97789999999997 C 450.41666666666674 52.97789999999997 453.41666666666674 49.97789999999997 456.41666666666674 49.97789999999997 L 456.41666666666674 49.97789999999997 C 457.20833333333337 49.97789999999997 458.00000000000006 52.97789999999997 458.00000000000006 55.97789999999997 L 458.00000000000006 147.76858 z " fill="rgba(243,246,249,1)" fill-opacity="1" stroke="transparent" stroke-opacity="1" stroke-linecap="round" stroke-width="2" stroke-dasharray="0" class="apexcharts-bar-area" index="1" clip-path="url(#gridRectMask2s0r5y8sk)" pathTo="M 450.41666666666674 147.76858 L 450.41666666666674 55.97789999999997 C 450.41666666666674 52.97789999999997 453.41666666666674 49.97789999999997 456.41666666666674 49.97789999999997 L 456.41666666666674 49.97789999999997 C 457.20833333333337 49.97789999999997 458.00000000000006 52.97789999999997 458.00000000000006 55.97789999999997 L 458.00000000000006 147.76858 z " pathFrom="M 450.41666666666674 147.76858 L 450.41666666666674 147.76858 L 458.00000000000006 147.76858 L 458.00000000000006 147.76858 L 458.00000000000006 147.76858 L 458.00000000000006 147.76858 L 458.00000000000006 147.76858 L 450.41666666666674 147.76858 z" cy="49.97689999999997" cx="497.3333333333334" j="9" val="36.2" barHeight="97.79068000000001" barWidth="9.583333333333332"></path>
                                                                <path id="SvgjsPath1208" d="M 498.3333333333334 182.3465 L 498.3333333333334 127.83514 C 498.3333333333334 124.83514 501.3333333333334 121.83514 504.3333333333334 121.83514 L 504.3333333333334 121.83514 C 505.1250000000001 121.83514 505.91666666666674 124.83514 505.91666666666674 127.83514 L 505.91666666666674 182.3465 z " fill="rgba(243,246,249,1)" fill-opacity="1" stroke="transparent" stroke-opacity="1" stroke-linecap="round" stroke-width="2" stroke-dasharray="0" class="apexcharts-bar-area" index="1" clip-path="url(#gridRectMask2s0r5y8sk)" pathTo="M 498.3333333333334 182.3465 L 498.3333333333334 127.83514 C 498.3333333333334 124.83514 501.3333333333334 121.83514 504.3333333333334 121.83514 L 504.3333333333334 121.83514 C 505.1250000000001 121.83514 505.91666666666674 124.83514 505.91666666666674 127.83514 L 505.91666666666674 182.3465 z " pathFrom="M 498.3333333333334 182.3465 L 498.3333333333334 182.3465 L 505.91666666666674 182.3465 L 505.91666666666674 182.3465 L 505.91666666666674 182.3465 L 505.91666666666674 182.3465 L 505.91666666666674 182.3465 L 498.3333333333334 182.3465 z" cy="121.83413999999999" cx="545.2500000000001" j="10" val="22.4" barHeight="60.511359999999996" barWidth="9.583333333333332"></path>
                                                                <path id="SvgjsPath1210" d="M 546.2500000000001 161.54572000000002 L 546.2500000000001 64.35224 C 546.2500000000001 61.352239999999995 549.2500000000001 58.35224 552.2500000000001 58.35224 L 552.2500000000001 58.35224 C 553.0416666666667 58.35224 553.8333333333335 61.352239999999995 553.8333333333335 64.35224 L 553.8333333333335 161.54572000000002 z " fill="rgba(243,246,249,1)" fill-opacity="1" stroke="transparent" stroke-opacity="1" stroke-linecap="round" stroke-width="2" stroke-dasharray="0" class="apexcharts-bar-area" index="1" clip-path="url(#gridRectMask2s0r5y8sk)" pathTo="M 546.2500000000001 161.54572000000002 L 546.2500000000001 64.35224 C 546.2500000000001 61.352239999999995 549.2500000000001 58.35224 552.2500000000001 58.35224 L 552.2500000000001 58.35224 C 553.0416666666667 58.35224 553.8333333333335 61.352239999999995 553.8333333333335 64.35224 L 553.8333333333335 161.54572000000002 z " pathFrom="M 546.2500000000001 161.54572000000002 L 546.2500000000001 161.54572000000002 L 553.8333333333335 161.54572000000002 L 553.8333333333335 161.54572000000002 L 553.8333333333335 161.54572000000002 L 553.8333333333335 161.54572000000002 L 553.8333333333335 161.54572000000002 L 546.2500000000001 161.54572000000002 z" cy="58.351240000000004" cx="593.1666666666667" j="11" val="38.2" barHeight="103.19348000000001" barWidth="9.583333333333332"></path>
                                                                <g id="SvgjsG1186" class="apexcharts-bar-goals-markers" style="pointer-events: none">
                                                                    <g id="SvgjsG1187" className="apexcharts-bar-goals-groups" class="apexcharts-hidden-element-shown" clip-path="url(#gridRectMarkerMask2s0r5y8sk)"></g>
                                                                    <g id="SvgjsG1189" className="apexcharts-bar-goals-groups" class="apexcharts-hidden-element-shown" clip-path="url(#gridRectMarkerMask2s0r5y8sk)"></g>
                                                                    <g id="SvgjsG1191" className="apexcharts-bar-goals-groups" class="apexcharts-hidden-element-shown" clip-path="url(#gridRectMarkerMask2s0r5y8sk)"></g>
                                                                    <g id="SvgjsG1193" className="apexcharts-bar-goals-groups" class="apexcharts-hidden-element-shown" clip-path="url(#gridRectMarkerMask2s0r5y8sk)"></g>
                                                                    <g id="SvgjsG1195" className="apexcharts-bar-goals-groups" class="apexcharts-hidden-element-shown" clip-path="url(#gridRectMarkerMask2s0r5y8sk)"></g>
                                                                    <g id="SvgjsG1197" className="apexcharts-bar-goals-groups" class="apexcharts-hidden-element-shown" clip-path="url(#gridRectMarkerMask2s0r5y8sk)"></g>
                                                                    <g id="SvgjsG1199" className="apexcharts-bar-goals-groups" class="apexcharts-hidden-element-shown" clip-path="url(#gridRectMarkerMask2s0r5y8sk)"></g>
                                                                    <g id="SvgjsG1201" className="apexcharts-bar-goals-groups" class="apexcharts-hidden-element-shown" clip-path="url(#gridRectMarkerMask2s0r5y8sk)"></g>
                                                                    <g id="SvgjsG1203" className="apexcharts-bar-goals-groups" class="apexcharts-hidden-element-shown" clip-path="url(#gridRectMarkerMask2s0r5y8sk)"></g>
                                                                    <g id="SvgjsG1205" className="apexcharts-bar-goals-groups" class="apexcharts-hidden-element-shown" clip-path="url(#gridRectMarkerMask2s0r5y8sk)"></g>
                                                                    <g id="SvgjsG1207" className="apexcharts-bar-goals-groups" class="apexcharts-hidden-element-shown" clip-path="url(#gridRectMarkerMask2s0r5y8sk)"></g>
                                                                    <g id="SvgjsG1209" className="apexcharts-bar-goals-groups" class="apexcharts-hidden-element-shown" clip-path="url(#gridRectMarkerMask2s0r5y8sk)"></g>
                                                                </g>
                                                            </g>
                                                            <g id="SvgjsG1158" class="apexcharts-datalabels" data:realIndex="0"></g>
                                                            <g id="SvgjsG1185" class="apexcharts-datalabels" data:realIndex="1"></g>
                                                        </g>
                                                        <g id="SvgjsG1214" class="apexcharts-grid-borders" style="display: none;"></g>
                                                        <line id="SvgjsLine1222" x1="0" y1="0" x2="575" y2="0" stroke="#b6b6b6" stroke-dasharray="0" stroke-width="1" stroke-linecap="butt" class="apexcharts-ycrosshairs"></line>
                                                        <line id="SvgjsLine1223" x1="0" y1="0" x2="575" y2="0" stroke-dasharray="0" stroke-width="0" stroke-linecap="butt" class="apexcharts-ycrosshairs-hidden"></line>
                                                        <g id="SvgjsG1224" class="apexcharts-xaxis" transform="translate(0, 0)">
                                                            <g id="SvgjsG1225" class="apexcharts-xaxis-texts-g" transform="translate(0, -4)">
                                                                <text id="SvgjsText1227" font-family="Helvetica, Arial, sans-serif" x="23.958333333333332" y="245.112" text-anchor="middle" dominant-baseline="auto" font-size="12px" font-weight="400" fill="#373d3f" class="apexcharts-text apexcharts-xaxis-label " style="font-family: Helvetica, Arial, sans-serif;">
                                                                    <tspan id="SvgjsTspan1228">Jan</tspan>
                                                                    <title>Jan</title>
                                                                </text>
                                                                <text id="SvgjsText1230" font-family="Helvetica, Arial, sans-serif" x="71.875" y="245.112" text-anchor="middle" dominant-baseline="auto" font-size="12px" font-weight="400" fill="#373d3f" class="apexcharts-text apexcharts-xaxis-label " style="font-family: Helvetica, Arial, sans-serif;">
                                                                    <tspan id="SvgjsTspan1231">Feb</tspan>
                                                                    <title>Feb</title>
                                                                </text>
                                                                <text id="SvgjsText1233" font-family="Helvetica, Arial, sans-serif" x="119.79166666666667" y="245.112" text-anchor="middle" dominant-baseline="auto" font-size="12px" font-weight="400" fill="#373d3f" class="apexcharts-text apexcharts-xaxis-label " style="font-family: Helvetica, Arial, sans-serif;">
                                                                    <tspan id="SvgjsTspan1234">Mar</tspan>
                                                                    <title>Mar</title>
                                                                </text>
                                                                <text id="SvgjsText1236" font-family="Helvetica, Arial, sans-serif" x="167.70833333333331" y="245.112" text-anchor="middle" dominant-baseline="auto" font-size="12px" font-weight="400" fill="#373d3f" class="apexcharts-text apexcharts-xaxis-label " style="font-family: Helvetica, Arial, sans-serif;">
                                                                    <tspan id="SvgjsTspan1237">Apr</tspan>
                                                                    <title>Apr</title>
                                                                </text>
                                                                <text id="SvgjsText1239" font-family="Helvetica, Arial, sans-serif" x="215.62499999999997" y="245.112" text-anchor="middle" dominant-baseline="auto" font-size="12px" font-weight="400" fill="#373d3f" class="apexcharts-text apexcharts-xaxis-label " style="font-family: Helvetica, Arial, sans-serif;">
                                                                    <tspan id="SvgjsTspan1240">May</tspan>
                                                                    <title>May</title>
                                                                </text>
                                                                <text id="SvgjsText1242" font-family="Helvetica, Arial, sans-serif" x="263.5416666666667" y="245.112" text-anchor="middle" dominant-baseline="auto" font-size="12px" font-weight="400" fill="#373d3f" class="apexcharts-text apexcharts-xaxis-label " style="font-family: Helvetica, Arial, sans-serif;">
                                                                    <tspan id="SvgjsTspan1243">Jun</tspan>
                                                                    <title>Jun</title>
                                                                </text>
                                                                <text id="SvgjsText1245" font-family="Helvetica, Arial, sans-serif" x="311.45833333333337" y="245.112" text-anchor="middle" dominant-baseline="auto" font-size="12px" font-weight="400" fill="#373d3f" class="apexcharts-text apexcharts-xaxis-label " style="font-family: Helvetica, Arial, sans-serif;">
                                                                    <tspan id="SvgjsTspan1246">Jul</tspan>
                                                                    <title>Jul</title>
                                                                </text>
                                                                <text id="SvgjsText1248" font-family="Helvetica, Arial, sans-serif" x="359.37500000000006" y="245.112" text-anchor="middle" dominant-baseline="auto" font-size="12px" font-weight="400" fill="#373d3f" class="apexcharts-text apexcharts-xaxis-label " style="font-family: Helvetica, Arial, sans-serif;">
                                                                    <tspan id="SvgjsTspan1249">Aug</tspan>
                                                                    <title>Aug</title>
                                                                </text>
                                                                <text id="SvgjsText1251" font-family="Helvetica, Arial, sans-serif" x="407.29166666666674" y="245.112" text-anchor="middle" dominant-baseline="auto" font-size="12px" font-weight="400" fill="#373d3f" class="apexcharts-text apexcharts-xaxis-label " style="font-family: Helvetica, Arial, sans-serif;">
                                                                    <tspan id="SvgjsTspan1252">Sep</tspan>
                                                                    <title>Sep</title>
                                                                </text>
                                                                <text id="SvgjsText1254" font-family="Helvetica, Arial, sans-serif" x="455.2083333333334" y="245.112" text-anchor="middle" dominant-baseline="auto" font-size="12px" font-weight="400" fill="#373d3f" class="apexcharts-text apexcharts-xaxis-label " style="font-family: Helvetica, Arial, sans-serif;">
                                                                    <tspan id="SvgjsTspan1255">Oct</tspan>
                                                                    <title>Oct</title>
                                                                </text>
                                                                <text id="SvgjsText1257" font-family="Helvetica, Arial, sans-serif" x="503.12500000000006" y="245.112" text-anchor="middle" dominant-baseline="auto" font-size="12px" font-weight="400" fill="#373d3f" class="apexcharts-text apexcharts-xaxis-label " style="font-family: Helvetica, Arial, sans-serif;">
                                                                    <tspan id="SvgjsTspan1258">Nov</tspan>
                                                                    <title>Nov</title>
                                                                </text>
                                                                <text id="SvgjsText1260" font-family="Helvetica, Arial, sans-serif" x="551.0416666666666" y="245.112" text-anchor="middle" dominant-baseline="auto" font-size="12px" font-weight="400" fill="#373d3f" class="apexcharts-text apexcharts-xaxis-label " style="font-family: Helvetica, Arial, sans-serif;">
                                                                    <tspan id="SvgjsTspan1261">Dec</tspan>
                                                                    <title>Dec</title>
                                                                </text>
                                                            </g>
                                                            <line id="SvgjsLine1262" x1="0" y1="217.112" x2="575" y2="217.112" stroke="#e0e0e0" stroke-dasharray="0" stroke-width="1" stroke-linecap="butt"></line>
                                                        </g>
                                                        <g id="SvgjsG1264" class="apexcharts-yaxis-annotations"></g>
                                                        <g id="SvgjsG1265" class="apexcharts-xaxis-annotations"></g>
                                                        <g id="SvgjsG1266" class="apexcharts-point-annotations"></g>
                                                    </g></svg><div class="apexcharts-tooltip apexcharts-theme-light">
                                                        <div class="apexcharts-tooltip-title" style="font-family: Helvetica, Arial, sans-serif; font-size: 12px;"></div>
                                                        <div class="apexcharts-tooltip-series-group" style="order: 1;">
                                                            <span class="apexcharts-tooltip-marker" style="background-color: rgb(37, 160, 226);"></span>
                                                            <div class="apexcharts-tooltip-text" style="font-family: Helvetica, Arial, sans-serif; font-size: 12px;">
                                                                <div class="apexcharts-tooltip-y-group"><span class="apexcharts-tooltip-text-y-label"></span><span class="apexcharts-tooltip-text-y-value"></span></div>
                                                                <div class="apexcharts-tooltip-goals-group"><span class="apexcharts-tooltip-text-goals-label"></span><span class="apexcharts-tooltip-text-goals-value"></span></div>
                                                                <div class="apexcharts-tooltip-z-group"><span class="apexcharts-tooltip-text-z-label"></span><span class="apexcharts-tooltip-text-z-value"></span></div>
                                                            </div>
                                                        </div>
                                                        <div class="apexcharts-tooltip-series-group" style="order: 2;">
                                                            <span class="apexcharts-tooltip-marker" style="background-color: rgb(243, 246, 249);"></span>
                                                            <div class="apexcharts-tooltip-text" style="font-family: Helvetica, Arial, sans-serif; font-size: 12px;">
                                                                <div class="apexcharts-tooltip-y-group"><span class="apexcharts-tooltip-text-y-label"></span><span class="apexcharts-tooltip-text-y-value"></span></div>
                                                                <div class="apexcharts-tooltip-goals-group"><span class="apexcharts-tooltip-text-goals-label"></span><span class="apexcharts-tooltip-text-goals-value"></span></div>
                                                                <div class="apexcharts-tooltip-z-group"><span class="apexcharts-tooltip-text-z-label"></span><span class="apexcharts-tooltip-text-z-value"></span></div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                <div class="apexcharts-yaxistooltip apexcharts-yaxistooltip-0 apexcharts-yaxistooltip-left apexcharts-theme-light">
                                                    <div class="apexcharts-yaxistooltip-text"></div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <!-- end card body -->
                            </div>
                            <!-- end card -->
                        </div>
                        <!-- end col -->
                        <div class="col-xl-6">
                            <div class="card card-height-100">
                                <div class="card-header align-items-center d-flex">
                                    <h4 class="card-title mb-0 flex-grow-1">Users by Device</h4>
                                    <div class="flex-shrink-0">
                                        <div class="dropdown card-header-dropdown">
                                            <a class="text-reset" href="#" data-bs-toggle="dropdown" aria-haspopup="true" aria-expanded="false"><span class="text-muted fs-16"><i class="mdi mdi-dots-vertical align-middle"></i></span></a>
                                            <div class="dropdown-menu dropdown-menu-end"><a class="dropdown-item" href="#">Today</a> <a class="dropdown-item" href="#">Last Week</a> <a class="dropdown-item" href="#">Last Month</a> <a class="dropdown-item" href="#">Current Year</a> </div>
                                        </div>
                                    </div>
                                </div>
                                <!-- end card header -->
                                <div class="card-body">
                                    <div id="user_device_pie_charts" data-colors="[&quot;--vz-primary&quot;, &quot;--vz-primary-rgb, 0.60&quot;, &quot;--vz-primary-rgb, 0.75&quot;]" class="apex-charts" dir="ltr" style="min-height: 201.8px;">
                                        <div id="apexcharts25c2nkwz" class="apexcharts-canvas apexcharts25c2nkwz apexcharts-theme-light" style="width: 565px; height: 201.8px;">
                                            <svg id="SvgjsSvg1267" width="565" height="201.79999999999998" xmlns="http://www.w3.org/2000/svg" version="1.1" xmlns:xlink="http://www.w3.org/1999/xlink" xmlns:svgjs="http://svgjs.dev" class="apexcharts-svg" xmlns:data="ApexChartsNS" transform="translate(0, 0)" style="background: transparent;">
                                                <foreignObject x="0" y="0" width="565" height="201.79999999999998">
                                                    <div class="apexcharts-legend" xmlns="http://www.w3.org/1999/xhtml" style="max-height: 109.5px;"></div>
                                                </foreignObject><g id="SvgjsG1269" class="apexcharts-inner apexcharts-graphical" transform="translate(186, 0)"><defs id="SvgjsDefs1268"><clipPath id="gridRectMask25c2nkwz"><rect id="SvgjsRect1270" width="199" height="217" x="-2" y="0" rx="0" ry="0" opacity="1" stroke-width="0" stroke="none" stroke-dasharray="0" fill="#fff"></rect>
                                                </clipPath>
                                                    <clipPath id="forecastMask25c2nkwz"></clipPath>
                                                    <clipPath id="nonForecastMask25c2nkwz"></clipPath>
                                                    <clipPath id="gridRectMarkerMask25c2nkwz">
                                                        <rect id="SvgjsRect1271" width="199" height="221" x="-2" y="-2" rx="0" ry="0" opacity="1" stroke-width="0" stroke="none" stroke-dasharray="0" fill="#fff"></rect>
                                                    </clipPath>
                                                </defs>
                                                    <g id="SvgjsG1272" class="apexcharts-pie">
                                                        <g id="SvgjsG1273" transform="translate(0, 0) scale(1)">
                                                            <circle id="SvgjsCircle1274" r="69.25268292682928" cx="97.5" cy="97.5" fill="transparent"></circle>
                                                            <g id="SvgjsG1275" class="apexcharts-slices">
                                                                <g id="SvgjsG1276" class="apexcharts-series apexcharts-pie-series" seriesName="Desktop" rel="1" data:realIndex="0">
                                                                    <path id="SvgjsPath1277" d="M 97.5 6.378048780487802 A 91.1219512195122 91.1219512195122 0 0 1 172.25192721855683 149.60911025111386 L 154.3114646861032 137.10292379084655 A 69.25268292682928 69.25268292682928 0 0 0 97.5 28.24731707317072 L 97.5 6.378048780487802 z" fill="rgba(37,160,226,1)" fill-opacity="1" stroke-opacity="1" stroke-linecap="butt" stroke-width="0" stroke-dasharray="0" class="apexcharts-pie-area apexcharts-donut-slice-0" index="0" j="0" data:angle="124.88011657173138" data:startAngle="0" data:strokeWidth="0" data:value="78.56" data:pathOrig="M 97.5 6.378048780487802 A 91.1219512195122 91.1219512195122 0 0 1 172.25192721855683 149.60911025111386 L 154.3114646861032 137.10292379084655 A 69.25268292682928 69.25268292682928 0 0 0 97.5 28.24731707317072 L 97.5 6.378048780487802 z"></path>
                                                                </g>
                                                                <g id="SvgjsG1278" class="apexcharts-series apexcharts-pie-series" seriesName="Mobile" rel="2" data:realIndex="1">
                                                                    <path id="SvgjsPath1279" d="M 172.25192721855683 149.60911025111386 A 91.1219512195122 91.1219512195122 0 0 1 12.907224529888694 63.62859122630862 L 33.20949064271541 71.75772933199454 A 69.25268292682928 69.25268292682928 0 0 0 154.3114646861032 137.10292379084655 L 172.25192721855683 149.60911025111386 z" fill="rgba(37,160,226, 0.60)" fill-opacity="1" stroke-opacity="1" stroke-linecap="butt" stroke-width="0" stroke-dasharray="0" class="apexcharts-pie-area apexcharts-donut-slice-1" index="0" j="1" data:angle="166.94131673069285" data:startAngle="124.88011657173138" data:strokeWidth="0" data:value="105.02" data:pathOrig="M 172.25192721855683 149.60911025111386 A 91.1219512195122 91.1219512195122 0 0 1 12.907224529888694 63.62859122630862 L 33.20949064271541 71.75772933199454 A 69.25268292682928 69.25268292682928 0 0 0 154.3114646861032 137.10292379084655 L 172.25192721855683 149.60911025111386 z"></path>
                                                                </g>
                                                                <g id="SvgjsG1280" class="apexcharts-series apexcharts-pie-series" seriesName="Tablet" rel="3" data:realIndex="2">
                                                                    <path id="SvgjsPath1281" d="M 12.907224529888694 63.62859122630862 A 91.1219512195122 91.1219512195122 0 0 1 97.48409621938451 6.378050168354477 L 97.48791312673222 28.2473181279494 A 69.25268292682928 69.25268292682928 0 0 0 33.20949064271541 71.75772933199454 L 12.907224529888694 63.62859122630862 z" fill="rgba(37,160,226, 0.75)" fill-opacity="1" stroke-opacity="1" stroke-linecap="butt" stroke-width="0" stroke-dasharray="0" class="apexcharts-pie-area apexcharts-donut-slice-2" index="0" j="2" data:angle="68.17856669757583" data:startAngle="291.8214333024242" data:strokeWidth="0" data:value="42.89" data:pathOrig="M 12.907224529888694 63.62859122630862 A 91.1219512195122 91.1219512195122 0 0 1 97.48409621938451 6.378050168354477 L 97.48791312673222 28.2473181279494 A 69.25268292682928 69.25268292682928 0 0 0 33.20949064271541 71.75772933199454 L 12.907224529888694 63.62859122630862 z"></path>
                                                                </g>
                                                            </g>
                                                        </g>
                                                    </g>
                                                    <line id="SvgjsLine1282" x1="0" y1="0" x2="195" y2="0" stroke="#b6b6b6" stroke-dasharray="0" stroke-width="1" stroke-linecap="butt" class="apexcharts-ycrosshairs"></line>
                                                    <line id="SvgjsLine1283" x1="0" y1="0" x2="195" y2="0" stroke-dasharray="0" stroke-width="0" stroke-linecap="butt" class="apexcharts-ycrosshairs-hidden"></line>
                                                </g></svg><div class="apexcharts-tooltip apexcharts-theme-dark">
                                                    <div class="apexcharts-tooltip-series-group" style="order: 1;">
                                                        <span class="apexcharts-tooltip-marker" style="background-color: rgb(37, 160, 226);"></span>
                                                        <div class="apexcharts-tooltip-text" style="font-family: Helvetica, Arial, sans-serif; font-size: 12px;">
                                                            <div class="apexcharts-tooltip-y-group"><span class="apexcharts-tooltip-text-y-label"></span><span class="apexcharts-tooltip-text-y-value"></span></div>
                                                            <div class="apexcharts-tooltip-goals-group"><span class="apexcharts-tooltip-text-goals-label"></span><span class="apexcharts-tooltip-text-goals-value"></span></div>
                                                            <div class="apexcharts-tooltip-z-group"><span class="apexcharts-tooltip-text-z-label"></span><span class="apexcharts-tooltip-text-z-value"></span></div>
                                                        </div>
                                                    </div>
                                                    <div class="apexcharts-tooltip-series-group" style="order: 2;">
                                                        <span class="apexcharts-tooltip-marker" style="background-color: rgba(37, 160, 226, 0.6);"></span>
                                                        <div class="apexcharts-tooltip-text" style="font-family: Helvetica, Arial, sans-serif; font-size: 12px;">
                                                            <div class="apexcharts-tooltip-y-group"><span class="apexcharts-tooltip-text-y-label"></span><span class="apexcharts-tooltip-text-y-value"></span></div>
                                                            <div class="apexcharts-tooltip-goals-group"><span class="apexcharts-tooltip-text-goals-label"></span><span class="apexcharts-tooltip-text-goals-value"></span></div>
                                                            <div class="apexcharts-tooltip-z-group"><span class="apexcharts-tooltip-text-z-label"></span><span class="apexcharts-tooltip-text-z-value"></span></div>
                                                        </div>
                                                    </div>
                                                    <div class="apexcharts-tooltip-series-group" style="order: 3;">
                                                        <span class="apexcharts-tooltip-marker" style="background-color: rgba(37, 160, 226, 0.75);"></span>
                                                        <div class="apexcharts-tooltip-text" style="font-family: Helvetica, Arial, sans-serif; font-size: 12px;">
                                                            <div class="apexcharts-tooltip-y-group"><span class="apexcharts-tooltip-text-y-label"></span><span class="apexcharts-tooltip-text-y-value"></span></div>
                                                            <div class="apexcharts-tooltip-goals-group"><span class="apexcharts-tooltip-text-goals-label"></span><span class="apexcharts-tooltip-text-goals-value"></span></div>
                                                            <div class="apexcharts-tooltip-z-group"><span class="apexcharts-tooltip-text-z-label"></span><span class="apexcharts-tooltip-text-z-value"></span></div>
                                                        </div>
                                                    </div>
                                                </div>
                                        </div>
                                    </div>
                                    <div class="table-responsive mt-3">
                                        <table class="table table-borderless table-sm table-centered align-middle table-nowrap mb-0">
                                            <tbody class="border-0">
                                                <tr>
                                                    <td>
                                                        <h4 class="text-truncate fs-14 fs-medium mb-0"><i class="ri-stop-fill align-middle fs-18 text-primary me-2"></i>Desktop
                            Users</h4>
                                                    </td>
                                                    <td>
                                                        <p class="text-muted mb-0">
                                                            <svg xmlns="http://www.w3.org/2000/svg" width="24" height="24" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round" class="feather feather-users me-2 icon-sm">
                                                                <path d="M17 21v-2a4 4 0 0 0-4-4H5a4 4 0 0 0-4 4v2"></path><circle cx="9" cy="7" r="4"></circle><path d="M23 21v-2a4 4 0 0 0-3-3.87"></path><path d="M16 3.13a4 4 0 0 1 0 7.75"></path></svg>78.56k
                                                        </p>
                                                    </td>
                                                    <td class="text-end">
                                                        <p class="text-success fw-medium fs-12 mb-0"><i class="ri-arrow-up-s-fill fs-5 align-middle"></i>2.08% </p>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <h4 class="text-truncate fs-14 fs-medium mb-0"><i class="ri-stop-fill align-middle fs-18 text-success me-2"></i>Mobile
                            Users</h4>
                                                    </td>
                                                    <td>
                                                        <p class="text-muted mb-0">
                                                            <svg xmlns="http://www.w3.org/2000/svg" width="24" height="24" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round" class="feather feather-users me-2 icon-sm">
                                                                <path d="M17 21v-2a4 4 0 0 0-4-4H5a4 4 0 0 0-4 4v2"></path><circle cx="9" cy="7" r="4"></circle><path d="M23 21v-2a4 4 0 0 0-3-3.87"></path><path d="M16 3.13a4 4 0 0 1 0 7.75"></path></svg>105.02k
                                                        </p>
                                                    </td>
                                                    <td class="text-end">
                                                        <p class="text-danger fw-medium fs-12 mb-0"><i class="ri-arrow-down-s-fill fs-5 align-middle"></i>10.52% </p>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <h4 class="text-truncate fs-14 fs-medium mb-0"><i class="ri-stop-fill align-middle fs-18 text-primary me-2"></i>Tablet
                            Users</h4>
                                                    </td>
                                                    <td>
                                                        <p class="text-muted mb-0">
                                                            <svg xmlns="http://www.w3.org/2000/svg" width="24" height="24" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round" class="feather feather-users me-2 icon-sm">
                                                                <path d="M17 21v-2a4 4 0 0 0-4-4H5a4 4 0 0 0-4 4v2"></path><circle cx="9" cy="7" r="4"></circle><path d="M23 21v-2a4 4 0 0 0-3-3.87"></path><path d="M16 3.13a4 4 0 0 1 0 7.75"></path></svg>42.89k
                                                        </p>
                                                    </td>
                                                    <td class="text-end">
                                                        <p class="text-danger fw-medium fs-12 mb-0"><i class="ri-arrow-down-s-fill fs-5 align-middle"></i>7.36% </p>
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>
                                    </div>
                                </div>
                                <!-- end card body -->
                            </div>
                            <!-- end card -->
                        </div>

                        <!-- end col -->
                    </div>
                    <!-- end row -->

                    <div class="row">

                        <!-- end col -->

                        <div class="col-xl-6 col-md-6">
                            <div class="card card-height-100">
                                <div class="card-header align-items-center d-flex">
                                    <h4 class="card-title mb-0 flex-grow-1">Top Referrals Pages</h4>
                                    <div class="flex-shrink-0">
                                        <button type="button" class="btn btn-soft-primary btn-sm">Export Report </button>
                                    </div>
                                </div>
                                <div class="card-body">
                                    <div class="row align-items-center">
                                        <div class="col-6">
                                            <h6 class="text-muted text-uppercase fw-semibold text-truncate fs-12 mb-3">Total Referrals Page</h6>
                                            <h4 class="fs- mb-0">725,800</h4>
                                            <p class="mb-0 mt-2 text-muted"><span class="badge bg-success-subtle text-success mb-0"><i class="ri-arrow-up-line align-middle"></i>15.72 % </span>vs. previous month</p>
                                        </div>
                                        <!-- end col -->
                                        <div class="col-6">
                                            <div class="text-center">
                                                <img src="assets/images/illustrator-1.png" class="img-fluid" alt="">
                                            </div>
                                        </div>
                                        <!-- end col -->
                                    </div>
                                    <!-- end row -->
                                    <div class="mt-3 pt-2">
                                        <div class="progress progress-lg rounded-pill">
                                            <div class="progress-bar bg-primary" role="progressbar" style="width: 25%" aria-valuenow="25" aria-valuemin="0" aria-valuemax="100"></div>
                                            <div class="progress-bar bg-secondary" role="progressbar" style="width: 18%" aria-valuenow="18" aria-valuemin="0" aria-valuemax="100"></div>
                                            <div class="progress-bar bg-success" role="progressbar" style="width: 22%" aria-valuenow="22" aria-valuemin="0" aria-valuemax="100"></div>
                                            <div class="progress-bar bg-warning" role="progressbar" style="width: 16%" aria-valuenow="16" aria-valuemin="0" aria-valuemax="100"></div>
                                            <div class="progress-bar bg-danger" role="progressbar" style="width: 19%" aria-valuenow="19" aria-valuemin="0" aria-valuemax="100"></div>
                                        </div>
                                    </div>
                                    <!-- end -->

                                    <div class="mt-3 pt-2">
                                        <div class="d-flex mb-2">
                                            <div class="flex-grow-1">
                                                <p class="text-truncate text-muted fs-14 mb-0"><i class="mdi mdi-circle align-middle text-primary me-2"></i>www.google.com </p>
                                            </div>
                                            <div class="flex-shrink-0">
                                                <p class="mb-0">24.58%</p>
                                            </div>
                                        </div>
                                        <!-- end -->
                                        <div class="d-flex mb-2">
                                            <div class="flex-grow-1">
                                                <p class="text-truncate text-muted fs-14 mb-0"><i class="mdi mdi-circle align-middle text-secondary me-2"></i>www.youtube.com </p>
                                            </div>
                                            <div class="flex-shrink-0">
                                                <p class="mb-0">17.51%</p>
                                            </div>
                                        </div>
                                        <!-- end -->
                                        <div class="d-flex mb-2">
                                            <div class="flex-grow-1">
                                                <p class="text-truncate text-muted fs-14 mb-0"><i class="mdi mdi-circle align-middle text-success me-2"></i>www.meta.com </p>
                                            </div>
                                            <div class="flex-shrink-0">
                                                <p class="mb-0">23.05%</p>
                                            </div>
                                        </div>
                                        <!-- end -->
                                        <div class="d-flex mb-2">
                                            <div class="flex-grow-1">
                                                <p class="text-truncate text-muted fs-14 mb-0"><i class="mdi mdi-circle align-middle text-warning me-2"></i>www.medium.com </p>
                                            </div>
                                            <div class="flex-shrink-0">
                                                <p class="mb-0">12.22%</p>
                                            </div>
                                        </div>
                                        <!-- end -->
                                        <div class="d-flex">
                                            <div class="flex-grow-1">
                                                <p class="text-truncate text-muted fs-14 mb-0"><i class="mdi mdi-circle align-middle text-danger me-2"></i>Other </p>
                                            </div>
                                            <div class="flex-shrink-0">
                                                <p class="mb-0">17.58%</p>
                                            </div>
                                        </div>
                                        <!-- end -->
                                    </div>
                                    <!-- end -->

                                    <div class="mt-2 text-center">
                                        <a href="javascript:void(0);" class="text-muted text-decoration-underline">Show
                  All</a>
                                    </div>
                                </div>
                                <!-- end card body -->
                            </div>
                            <!-- end card -->
                        </div>
                        <!-- end col -->

                        <div class="col-xl-6 col-md-6">
                            <div class="card card-height-100">
                                <div class="card-header align-items-center d-flex">
                                    <h4 class="card-title mb-0 flex-grow-1">Top Pages</h4>
                                    <div class="flex-shrink-0">
                                        <div class="dropdown card-header-dropdown">
                                            <a class="text-reset" href="#" data-bs-toggle="dropdown" aria-haspopup="true" aria-expanded="false"><span class="text-muted fs-16"><i class="mdi mdi-dots-vertical align-middle"></i></span></a>
                                            <div class="dropdown-menu dropdown-menu-end"><a class="dropdown-item" href="#">Today</a> <a class="dropdown-item" href="#">Last Week</a> <a class="dropdown-item" href="#">Last Month</a> <a class="dropdown-item" href="#">Current Year</a> </div>
                                        </div>
                                    </div>
                                </div>
                                <!-- end card header -->
                                <div class="card-body">
                                    <div class="table-responsive table-card">
                                        <table class="table align-middle table-borderless table-centered table-nowrap mb-0">
                                            <thead class="text-muted table-light">
                                                <tr>
                                                    <th scope="col" style="width: 62;">Active Page</th>
                                                    <th scope="col">Active</th>
                                                    <th scope="col">Users</th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                <tr>
                                                    <td><a href="javascript:void(0);" class="link-secondary">/themesbrand/skote-25867</a></td>
                                                    <td>99</td>
                                                    <td>25.3%</td>
                                                </tr>
                                                <!-- end -->
                                                <tr>
                                                    <td><a href="javascript:void(0);" class="link-secondary">/dashonic/chat-24518</a></td>
                                                    <td>86</td>
                                                    <td>22.7%</td>
                                                </tr>
                                                <!-- end -->
                                                <tr>
                                                    <td><a href="javascript:void(0);" class="link-secondary">/skote/timeline-27391</a></td>
                                                    <td>64</td>
                                                    <td>18.7%</td>
                                                </tr>
                                                <!-- end -->
                                                <tr>
                                                    <td><a href="javascript:void(0);" class="link-secondary">/themesbrand/minia-26441</a></td>
                                                    <td>53</td>
                                                    <td>14.2%</td>
                                                </tr>
                                                <!-- end -->
                                                <tr>
                                                    <td><a href="javascript:void(0);" class="link-secondary">/dashon/dashboard-29873</a></td>
                                                    <td>33</td>
                                                    <td>12.6%</td>
                                                </tr>
                                                <!-- end -->
                                                <tr>
                                                    <td><a href="javascript:void(0);" class="link-secondary">/doot/chats-29964</a></td>
                                                    <td>20</td>
                                                    <td>10.9%</td>
                                                </tr>
                                                <!-- end -->
                                                <tr>
                                                    <td><a href="javascript:void(0);" class="link-secondary">/minton/pages-29739</a></td>
                                                    <td>10</td>
                                                    <td>07.3%</td>
                                                </tr>
                                                <!-- end -->
                                            </tbody>
                                            <!-- end tbody -->
                                        </table>
                                        <!-- end table -->
                                    </div>
                                    <!-- end -->
                                </div>
                                <!-- end cardbody -->
                            </div>
                            <!-- end card -->
                        </div>
                        <!-- end col -->
                    </div>
                    <!-- end row -->

                </div>
                <!-- container-fluid -->
            </div>
            <!-- End Page-content -->

            <footer class="footer">
                <div class="container-fluid">
                    <div class="row">
                        <div class="col-sm-6">© Copyright 2024-25 Schemes Monitoring System, Urban Development Department, GoUP | All Rights Reserved </div>
                        <div class="col-sm-6">
                            <div class="text-sm-end d-none d-sm-block">Powered by <a href="https://www.margsoft.com/" target="_blank">MARGSOFT Technologies (P) Ltd. </a></div>
                        </div>
                    </div>
                </div>
            </footer>
        </div>
        <!-- end main content-->

    </div>
</asp:Content>
