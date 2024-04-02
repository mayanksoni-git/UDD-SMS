<%@ Page Language="C#" MasterPageFile="~/TemplateMasterAdmin2.master" AutoEventWireup="true" CodeFile="Menu_Analysis.aspx.cs" Inherits="Menu_Analysis" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="main-content">
        <div class="main-content-inner">
            <cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" EnablePartialRendering="true" EnablePageMethods="true" AsyncPostBackTimeout="6000">
            </cc1:ToolkitScriptManager>
            <asp:UpdatePanel ID="up" runat="server">
                <ContentTemplate>
                    <div class="page-content">
                        <div class="container-fluid">
                            <div class="row">
                                <div class="col-12">
                                    <div class="page-title-box d-sm-flex align-items-center justify-content-between">
                                        <h4 class="mb-sm-0">Project Progress Analysis (Physical and Financial)</h4>

                                    </div>
                                </div>
                            </div>


                            <div class="row">
                                <div class="col-xl-3 col-md-6">
                                    <div class="card card-animate anal-btn-details">
                                        <div class="d-flex justify-content-between">
                                            <h3 class="ff-secondary fw-semibold"><span>Physical Progress Monthly Statement</span></h3>
                                            <span class="avatar-title bg-danger bg-gradient rounded-circle fs-4">
                                                <img src="assets/images/menu_analysis_icon/Physical-progress-monthly-statement.png" />
                                            </span>
                                        </div>
                                        <a href="Report_ProjectWork_Physical_Statement.aspx" class="">View More  </a>
                                    </div>
                                </div>



                                <div class="col-xl-3 col-md-6">
                                     <div class="card card-animate anal-btn-details">
                                        <div class="d-flex justify-content-between">
                                            <h3 class="ff-secondary fw-semibold"><span>Stagnant Physical Progress</span></h3>
                                            <span class="avatar-title bg-primary bg-gradient rounded-circle fs-4">
                                                <img src="assets/images/menu_analysis_icon/Stagnant-physical-progress.png" />
                                            </span>
                                        </div>
                                        <a href="Report_ProjectWork_Physical_Progress_NoChange.aspx" class="">View More  </a>
                                    </div>


                                </div>
                                <div class="col-xl-3 col-md-6">
                                     <div class="card card-animate anal-btn-details">
                                        <div class="d-flex justify-content-between">
                                            <h3 class="ff-secondary fw-semibold"><span>Financial Progress Monthly Statement</span></h3>
                                            <span class="avatar-title bg-success bg-gradient rounded-circle fs-4">
                                                <img src="assets/images/menu_analysis_icon/Financial-progress-monthly-statement.png" />
                                            </span>
                                        </div>
                                        <a href="Report_Project_Financial_Progress.aspx" class="">View More  </a>
                                    </div>


                                </div>
                                <div class="col-xl-3 col-md-6">
                                    <div class="card card-animate anal-btn-details">
                                        <div class="d-flex justify-content-between">
                                            <h3 class="ff-secondary fw-semibold"><span>Financial Progress Statement</span></h3>
                                            <span class="avatar-title bg-info bg-gradient rounded-circle fs-4">
                                                <img src="assets/images/menu_analysis_icon/Financial-progress-statement.png" />
                                            </span>
                                        </div>
                                        <a href="Report_ProjectWork_Physical_Statement.aspx" class="">View More  </a>
                                    </div>
                                </div>
                            </div>



                            <div class="row mt-5">
                                <div class="col-12">
                                    <div class="page-title-box d-sm-flex align-items-center justify-content-between">
                                        <h4 class="mb-sm-0">Project Gallary and Other Details</h4>
                                    </div>
                                </div>
                                <div class="col-xl-3 col-md-6">
                                      <div class="card card-animate anal-btn-details">
                                        <div class="d-flex justify-content-between">
                                            <h3 class="ff-secondary fw-semibold"><span>Project Wise Photo & Videos</span></h3>
                                            <span class="avatar-title bg-warning bg-gradient rounded-circle fs-4">
                                                <img src="assets/images/menu_analysis_icon/Project-wise-photos-videos.png" />
                                            </span>
                                        </div>
                                        <a href="Report_ProjectWork_Gallery_Pic.aspx" class="">View More  </a>
                                    </div>
                                </div>

                            </div>
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
            <asp:UpdateProgress ID="UpdateProgress1" DynamicLayout="true" runat="server" AssociatedUpdatePanelID="up">
                <ProgressTemplate>
                    <div style="position: fixed; z-index: 999; height: 100%; width: 100%; top: 0; background-color: Black; filter: alpha(opacity=60); opacity: 0.6; -moz-opacity: 0.8; cursor: not-allowed;">
                        <div style="z-index: 1000; margin: 300px auto; padding: 10px; width: 130px; background-color: transparent; border-radius: 1px; filter: alpha(opacity=100); opacity: 1; -moz-opacity: 1;">
                            <img src="assets/images/mb/mbloader.gif" style="height: 150px; width: 150px;" />
                        </div>
                    </div>
                </ProgressTemplate>
            </asp:UpdateProgress>
        </div>

    </div>
</asp:Content>

