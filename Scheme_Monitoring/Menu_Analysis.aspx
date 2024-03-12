<%@ Page Language="C#" MasterPageFile="~/TemplateMasterAdmin2.master" AutoEventWireup="true" CodeFile="Menu_Analysis.aspx.cs" Inherits="Menu_Analysis" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="main-content">
        <div class="main-content-inner">
            <cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" EnablePartialRendering="true" EnablePageMethods="true" AsyncPostBackTimeout="6000">
            </cc1:ToolkitScriptManager>
            <asp:UpdatePanel ID="up" runat="server">
                <ContentTemplate>
                    <div class="page-content">
                        <div class="table-header">Project Progress Analysis (Physical and Financial)</div>
                        <div class="row">
                            <div class="col-xs-12">
                                <div class="col-md-3">
                                    <div class="thumbnail search-thumbnail">
                                        <img class="media-object" src="assets/Analysis/Physical_Progress.png" data-holder-rendered="true" style="height: 100px; width: 100px; display: block;">
                                        <div class="caption">
                                            <h3 class="search-title">
                                                <a href="Report_ProjectWork_Physical_Statement.aspx" class="blue">Physical Progress Monthly Statement</a>
                                            </h3>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="thumbnail search-thumbnail">
                                        <img class="media-object" src="assets/Analysis/Stagnant_Physical_Progress.png" data-holder-rendered="true" style="height: 100px; width: 100px; display: block;">
                                        <div class="caption">
                                            <h3 class="search-title">
                                                <a href="Report_ProjectWork_Physical_Progress_NoChange.aspx" class="blue">Stagnant Physical Progress</a>
                                            </h3>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="thumbnail search-thumbnail">
                                        <img class="media-object" src="assets/Analysis/Financial_Progress.png" data-holder-rendered="true" style="height: 100px; width: 100px; display: block;">
                                        <div class="caption">
                                            <h3 class="search-title">
                                                <a href="Report_Project_Financial_Progress.aspx" class="blue">Financial Progress Monthly Statement</a>
                                            </h3>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="thumbnail search-thumbnail">
                                        <img class="media-object" src="assets/Analysis/Stagnant_Financial_Progress.png" data-holder-rendered="true" style="height: 100px; width: 100px; display: block;">
                                        <div class="caption">
                                            <h3 class="search-title">
                                                <a href="Report_Project_With_No_Invoice.aspx" class="blue">Stagnant Financial Progress</a>
                                            </h3>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="row">
                            <div class="col-xs-12">
                                <div class="col-md-3" runat="server" id="divTargetAchivment">
                                    <div class="thumbnail search-thumbnail">
                                        <img class="media-object" src="assets/Analysis/Target_Achivment.jpg" data-holder-rendered="true" style="height: 100px; width: 100px; display: block;">
                                        <div class="caption">
                                            <h3 class="search-title">
                                                <a href="Report_Project_Financial_Progress_CircleWise.aspx" class="blue">Monthly Target & Achivment</a>
                                            </h3>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="thumbnail search-thumbnail">
                                        <img class="media-object" src="assets/Analysis/Ranking.png" data-holder-rendered="true" style="height: 100px; width: 100px; display: block;">
                                        <div class="caption">
                                            <h3 class="search-title">
                                                <a href="MasterCircleWiseRanking.aspx" class="blue">Ranking</a>
                                            </h3>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="thumbnail search-thumbnail">
                                        <img class="media-object" src="assets/Analysis/PPA_Analysis.png" data-holder-rendered="true" style="height: 100px; width: 100px; display: block;">
                                        <div class="caption">
                                            <h3 class="search-title">
                                                <a href="Report_PPA_Analysis.aspx" class="blue">PPA Analysis</a>
                                            </h3>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="thumbnail search-thumbnail">
                                        <img class="media-object" src="assets/Analysis/Invoice_Pending.png" data-holder-rendered="true" style="height: 100px; width: 100px; display: block;">
                                        <div class="caption">
                                            <h3 class="search-title">
                                                <a href="Report_InvoicePendency_Details.aspx" class="blue">Invoice Pendency Details</a>
                                            </h3>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="table-header">Project Related Other Analysis</div>
                        <div class="row">
                            <div class="col-xs-12">
                                <div class="col-md-3">
                                    <div class="thumbnail search-thumbnail">
                                        <img class="media-object" src="assets/Analysis/Handover.png" data-holder-rendered="true" style="height: 100px; width: 100px; display: block;">
                                        <div class="caption">
                                            <h3 class="search-title">
                                                <a href="Report_ProjectWork_Financial_Closure.aspx" class="blue">Physical Handover & Financial Closure</a>
                                            </h3>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="thumbnail search-thumbnail">
                                        <img class="media-object" src="assets/Analysis/Short_Closed.png" data-holder-rendered="true" style="height: 100px; width: 100px; display: block;">
                                        <div class="caption">
                                            <h3 class="search-title">
                                                <a href="Report_ProjectWork_Pre_Closure.aspx" class="blue">Physical / Financial Short Closed</a>
                                            </h3>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="thumbnail search-thumbnail">
                                        <img class="media-object" src="assets/Analysis/Financial_Progress.png" data-holder-rendered="true" style="height: 100px; width: 100px; display: block;">
                                        <div class="caption">
                                            <h3 class="search-title">
                                                <a href="Report_ProjectWork_Physical_Statement.aspx" class="blue">Financial Progress Monthly Statement</a>
                                            </h3>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="thumbnail search-thumbnail">
                                        <img class="media-object" src="assets/Analysis/liquidated_damages.jpg" data-holder-rendered="true" style="height: 100px; width: 100px; display: block;">
                                        <div class="caption">
                                            <h3 class="search-title">
                                                <a href="Report_ProjectWorkPkg_LD_Imposed.aspx" class="blue">Liquidated Damages Imposed</a>
                                            </h3>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="table-header">Project Gallary and Field Visit Details</div>
                        <div class="row">
                            <div class="col-xs-12">
                                <div class="col-md-3">
                                    <div class="thumbnail search-thumbnail">
                                        <img class="media-object" src="assets/Analysis/Gallery.png" data-holder-rendered="true" style="height: 100px; width: 100px; display: block;">
                                        <div class="caption">
                                            <h3 class="search-title">
                                                <a href="Report_ProjectWork_Gallery_Pic.aspx" class="blue">Project Wise Photo & Videos</a>
                                            </h3>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="thumbnail search-thumbnail">
                                        <img class="media-object" src="assets/Analysis/Inspection.jpg" data-holder-rendered="true" style="height: 100px; width: 100px; display: block;">
                                        <div class="caption">
                                            <h3 class="search-title">
                                                <a href="Report_ProjectWork_Visit_Pic.aspx" class="blue">Field Visit and Inspection Report</a>
                                            </h3>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="thumbnail search-thumbnail">
                                        <img class="media-object" src="assets/Analysis/Physical_components.png" data-holder-rendered="true" style="height: 100px; width: 100px; display: block;">
                                        <div class="caption">
                                            <h3 class="search-title">
                                                <a href="Report_Component_Details.aspx" class="blue">Component Wise Progress</a>
                                            </h3>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="thumbnail search-thumbnail">
                                        <img class="media-object" src="assets/Analysis/Issue_Analysis.png" data-holder-rendered="true" style="height: 100px; width: 100px; display: block;">
                                        <div class="caption">
                                            <h3 class="search-title">
                                                <a href="Report_Issue_Details.aspx" class="blue">Project Wise Issue Analysis</a>
                                            </h3>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="table-header">Other Analysis and Details</div>
                        <div class="row">
                            <div class="col-xs-12">
                                <div class="col-md-3">
                                    <div class="thumbnail search-thumbnail">
                                        <img class="media-object" src="assets/Analysis/PMIS_Download.jpg" data-holder-rendered="true" style="height: 100px; width: 100px; display: block;">
                                        <div class="caption">
                                            <h3 class="search-title">
                                                <a href="Report_PMIS_Dump.aspx" class="blue">PMIS Data dump Download</a>
                                            </h3>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="thumbnail search-thumbnail">
                                        <img class="media-object" src="assets/Analysis/Document_Not_Available.png" data-holder-rendered="true" style="height: 100px; width: 100px; display: block;">
                                        <div class="caption">
                                            <h3 class="search-title">
                                                <a href="Report_ProjectDocumentNotAvailable.aspx" class="blue">Project Document Vault Incomplete</a>
                                            </h3>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="thumbnail search-thumbnail">
                                        <img class="media-object" src="assets/Analysis/Variation_Analysis.png" data-holder-rendered="true" style="height: 100px; width: 100px; display: block;">
                                        <div class="caption">
                                            <h3 class="search-title">
                                                <a href="Report_Variation_Status_Report.aspx" class="blue">Variation Analysis</a>
                                            </h3>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="thumbnail search-thumbnail">
                                        <img class="media-object" src="assets/Analysis/Road_reinstatement.png" data-holder-rendered="true" style="height: 100px; width: 100px; display: block;">
                                        <div class="caption">
                                            <h3 class="search-title">
                                                <a href="MasterProjectWork_Road_Reinst.aspx" class="blue">Road Reinstatement (Data Captured via Mobile App)</a>
                                            </h3>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-xs-12">
                                <div class="col-md-3">
                                    <div class="thumbnail search-thumbnail">
                                        <img class="media-object" src="assets/Analysis/shadow_emb.png" data-holder-rendered="true" style="height: 100px; width: 100px; display: block;">
                                        <div class="caption">
                                            <h3 class="search-title">
                                                <a href="Report_Shadow_EMB_Analysis.aspx" class="blue">Shadow EMB Analysis</a>
                                            </h3>
                                        </div>
                                    </div>
                                </div>

                                <div class="col-md-3">
                                    <div class="thumbnail search-thumbnail">
                                        <img class="media-object" src="assets/Analysis/calculation.jpeg" data-holder-rendered="true" style="height: 100px; width: 100px; display: block;">
                                        <div class="caption">
                                            <h3 class="search-title">
                                                <a href="Tender_Cost_Calculation_Analysis.aspx" class="blue">Tender Cost Calculation Analysis</a>
                                            </h3>
                                        </div>
                                    </div>
                                </div>

                                <div class="col-md-3">
                                    <div class="thumbnail search-thumbnail">
                                        <img class="media-object" src="assets/images/Darpan-Logo.png" data-holder-rendered="true" style="height: 100px; width: 100px; display: block;">
                                        <div class="caption">
                                            <h3 class="search-title">
                                                <a href="CMDashboardProjectUpdate.aspx" class="blue">Physical & Financial Targets (CM Dashboard - DARPAN)</a>
                                            </h3>
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

