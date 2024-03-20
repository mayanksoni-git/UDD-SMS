<%@ Page Language="C#" MasterPageFile="~/TemplateMasterAdmin2.master" AutoEventWireup="true" CodeFile="Menu_Analysis.aspx.cs" Inherits="Menu_Analysis" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="main-content">
        <div class="main-content-inner">
            <cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" EnablePartialRendering="true" EnablePageMethods="true" AsyncPostBackTimeout="6000">
            </cc1:ToolkitScriptManager>
            <asp:UpdatePanel ID="up" runat="server">
                <ContentTemplate>
                    <div class="page-content">
                        <div class="row">
                            <div class="col-12">
                                <div class="page-title-box d-sm-flex align-items-center justify-content-between">
                                    <h4 class="mb-sm-0">Project Progress Analysis (Physical and Financial)</h4>

                                </div>
                            </div>
                        </div>


                        <div class="row">
                            <div class="col-xl-3 col-md-6">
                                <div class="card card-height-100 bg-light-subtle shadow-none bg-opacity-40">
                                    <div class="card-body">
                                        <div class="row gy-4">
                                            <div class="col-md-6">
                                                <img src="assets/Analysis/Physical_Progress.png" width="60px" height="60px" />
                                            </div>
                                            <div class="col-md-6">
                                                <h4 class="fs-4 mb-3">
                                                    <a href="Report_ProjectWork_Physical_Statement.aspx" class="blue">Physical Progress Monthly Statement</a>
                                                </h4>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="col-xl-3 col-md-6">
                                <div class="card card-height-100 bg-dark-subtle shadow-none bg-opacity-10">
                                    <div class="card-body">
                                        <div class="row gy-4">
                                            <div class="col-md-6">
                                                <img src="assets/Analysis/Stagnant_Physical_Progress.png" width="60px" height="60px" />
                                            </div>
                                            <div class="col-md-6">
                                                <h4 class="fs-4 mb-3">
                                                    <a href="Report_ProjectWork_Physical_Progress_NoChange.aspx" class="blue">Stagnant Physical Progress</a>
                                                </h4>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="col-xl-3 col-md-6">
                                <div class="card card-height-100 bg-primary-subtle shadow-none bg-opacity-10">
                                    <div class="card-body">
                                        <div class="row gy-4">
                                            <div class="col-md-6">
                                                <img src="assets/Analysis/Financial_Progress.png" width="60px" height="60px" />
                                            </div>
                                            <div class="col-md-6">
                                                <h4 class="fs-4 mb-3">
                                                    <a href="Report_Project_Financial_Progress.aspx" class="blue">Financial Progress Monthly Statement</a>
                                                </h4>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="col-xl-3 col-md-6">
                                <div class="card card-height-100 bg-info-subtle shadow-none bg-opacity-10">
                                    <div class="card-body">
                                        <div class="row gy-4">
                                            <div class="col-md-6">
                                                <img src="assets/Analysis/Stagnant_Financial_Progress.png" width="60px" height="60px" />
                                            </div>
                                            <div class="col-md-6">
                                                <h4 class="fs-4 mb-3">
                                                    <a href="Report_Project_With_No_Invoice.aspx" class="blue">Stagnant Financial Progress</a>
                                                </h4>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>


                        <div class="row">
                            <div class="col-xl-3 col-md-6">
                                <div class="card card-height-100 bg-light-subtle shadow-none bg-opacity-40">
                                    <div class="card-body">
                                        <div class="row gy-4">
                                            <div class="col-md-6">
                                                <img src="assets/Analysis/Handover.png" width="60px" height="60px" />
                                            </div>
                                            <div class="col-md-6">
                                                <h4 class="fs-4 mb-3">
                                                    <a href="Report_ProjectWork_Financial_Closure.aspx" class="blue">Physical Handover & Financial Closure</a>
                                                </h4>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="col-xl-3 col-md-6">
                                <div class="card card-height-100 bg-dark-subtle shadow-none bg-opacity-10">
                                    <div class="card-body">
                                        <div class="row gy-4">
                                            <div class="col-md-6">
                                                <img src="assets/Analysis/Short_Closed.png" width="60px" height="60px" />
                                            </div>
                                            <div class="col-md-6">
                                                <h4 class="fs-4 mb-3">
                                                    <a href="Report_ProjectWork_Pre_Closure.aspx" class="blue">Physical / Financial Short Closed</a>
                                                </h4>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="col-xl-3 col-md-6">
                                <div class="card card-height-100 bg-primary-subtle shadow-none bg-opacity-10">
                                    <div class="card-body">
                                        <div class="row gy-4">
                                            <div class="col-md-6">
                                                <img src="assets/Analysis/Financial_Progress.png" width="60px" height="60px" />
                                            </div>
                                            <div class="col-md-6">
                                                <h4 class="fs-4 mb-3">
                                                    <a href="Report_ProjectWork_Physical_Statement.aspx" class="blue">Financial Progress Statement</a>
                                                </h4>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="col-xl-3 col-md-6">
                                <div class="card card-height-100 bg-info-subtle shadow-none bg-opacity-10">
                                    <div class="card-body">
                                        <div class="row gy-4">
                                            <div class="col-md-6">
                                                <img src="assets/Analysis/liquidated_damages.jpg" width="60px" height="60px" />
                                            </div>
                                            <div class="col-md-6">
                                                <h4 class="fs-4 mb-3">
                                                    <a href="Report_ProjectWorkPkg_LD_Imposed.aspx" class="blue">Liquidated Damages Imposed</a>
                                                </h4>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>


                        <div class="row">
                            <div class="col-12">
                                <div class="page-title-box d-sm-flex align-items-center justify-content-between">
                                    <h4 class="mb-sm-0">Project Gallary and Other Details</h4>

                                </div>
                            </div>
                        </div>

                        <div class="row">
                            <div class="col-xl-3 col-md-6">
                                <div class="card card-height-100 bg-light-subtle shadow-none bg-opacity-40">
                                    <div class="card-body">
                                        <div class="row gy-4">
                                            <div class="col-md-6">
                                                <img src="assets/Analysis/Gallery.png" width="60px" height="60px" />
                                            </div>
                                            <div class="col-md-6">
                                                <h4 class="fs-4 mb-3">
                                                    <a href="Report_ProjectWork_Gallery_Pic.aspx" class="blue">Project Wise Photo & Videos</a>
                                                </h4>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="col-xl-3 col-md-6">
                                <div class="card card-height-100 bg-dark-subtle shadow-none bg-opacity-10">
                                    <div class="card-body">
                                        <div class="row gy-4">
                                            <div class="col-md-6">
                                                <img src="assets/Analysis/Physical_components.png" width="60px" height="60px" />
                                            </div>
                                            <div class="col-md-6">
                                                <h4 class="fs-4 mb-3">
                                                    <a href="Report_Component_Details.aspx" class="blue">Component Wise Progress</a>
                                                </h4>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="col-xl-3 col-md-6">
                                <div class="card card-height-100 bg-primary-subtle shadow-none bg-opacity-10">
                                    <div class="card-body">
                                        <div class="row gy-4">
                                            <div class="col-md-6">
                                                <img src="assets/Analysis/Issue_Analysis.png" width="60px" height="60px" />
                                            </div>
                                            <div class="col-md-6">
                                                <h4 class="fs-4 mb-3">
                                                    <a href="Report_Issue_Details.aspx" class="blue">Project Wise Issue Analysis</a>
                                                </h4>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="col-xl-3 col-md-6">
                                <div class="card card-height-100 bg-info-subtle shadow-none bg-opacity-10">
                                    <div class="card-body">
                                        <div class="row gy-4">
                                            <div class="col-md-6">
                                                <img src="assets/Analysis/PMIS_Download.jpg" width="60px" height="60px" />
                                            </div>
                                            <div class="col-md-6">
                                                <h4 class="fs-4 mb-3">
                                                    <a href="Report_PMIS_Dump.aspx" class="blue">Data Download In Excel</a>
                                                </h4>
                                            </div>
                                        </div>
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

