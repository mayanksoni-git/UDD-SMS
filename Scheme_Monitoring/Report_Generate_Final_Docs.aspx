<%@ Page Language="C#" MasterPageFile="~/TemplateMasterAdmin.master" AutoEventWireup="true"
    CodeFile="Report_Generate_Final_Docs.aspx.cs" Inherits="Report_Generate_Final_Docs" MaintainScrollPositionOnPostback="true" EnableEventValidation="false" ValidateRequest="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="main-content">
        <div class="main-content-inner">
            <cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" EnablePartialRendering="true" EnablePageMethods="true" AsyncPostBackTimeout="6000">
            </cc1:ToolkitScriptManager>
            <asp:UpdatePanel ID="up" runat="server">
                <ContentTemplate>
                    <div class="page-content">
                        <div class="row">
                            <div class="col-xs-12">
                                <div class="row">
                                    <div class="col-xs-12">
                                        <h3 class="header smaller lighter blue">Generate Final Document (PDF / PPT)</h3>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-12">
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <div class="well well-sm">1. Target and Achivment</div>
                                    </div>
                                </div>

                                <div class="col-md-3">
                                    <div class="form-group">
                                        <asp:Button ID="btnExportTarget" Text="Generate" OnClick="btnExportTarget_Click" runat="server" CssClass="btn btn-pink"></asp:Button>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group" runat="server" id="aTragetPDF">
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group" runat="server" id="aTragetPPT">
                                    </div>
                                </div>
                            </div>
                        </div>
                        <hr />
                        <div class="row">
                            <div class="col-md-12">
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <div class="well well-sm">2. Stagnant Physical Progress</div>
                                    </div>
                                </div>

                                <div class="col-md-3">
                                    <div class="form-group">
                                        <asp:Button ID="btnExportPhysical" Text="Generate" OnClick="btnExportPhysical_Click" runat="server" CssClass="btn btn-pink"></asp:Button>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group" runat="server" id="aPhysicalSPDF">
                                    </div>
                                    <div class="form-group" runat="server" id="aPhysicalDPDF">
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group" runat="server" id="aPhysicalSPPT">
                                    </div>
                                    <div class="form-group" runat="server" id="aPhysicalDPPT">
                                    </div>
                                </div>
                            </div>
                        </div>
                        <hr />
                        <div class="row">
                            <div class="col-md-12">
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <div class="well well-sm">3. Stagnant Financial Progress</div>
                                    </div>
                                </div>

                                <div class="col-md-3">
                                    <div class="form-group">
                                        <asp:Button ID="btnExportFinancial" Text="Generate" OnClick="btnExportFinancial_Click" runat="server" CssClass="btn btn-pink"></asp:Button>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group" runat="server" id="aFinancialSPDF">
                                    </div>
                                    <div class="form-group" runat="server" id="aFinancialDPDF">
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group" runat="server" id="aFinancialSPPT">
                                    </div>
                                    <div class="form-group" runat="server" id="aFinancialDPPT">
                                    </div>
                                </div>
                            </div>
                        </div>

                        <hr />
                        <div class="row">
                            <div class="col-md-12">
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <div class="well well-sm">4. Document Not Available</div>
                                    </div>
                                </div>

                                <div class="col-md-3">
                                    <div class="form-group">
                                        <asp:Button ID="btnExportDocument" Text="Generate" OnClick="btnExportDocument_Click" runat="server" CssClass="btn btn-pink"></asp:Button>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group" runat="server" id="aDocumentSPDF">
                                    </div>
                                    <div class="form-group" runat="server" id="aDocumentDPDF">
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group" runat="server" id="aDocumentSPPT">
                                    </div>
                                    <div class="form-group" runat="server" id="aDocumentDPPT">
                                    </div>
                                </div>
                            </div>
                        </div>

                        <hr />
                        <div class="row">
                            <div class="col-md-12">
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <div class="well well-sm">5. Time Overrun (Packages Which Require Extention)</div>
                                    </div>
                                </div>

                                <div class="col-md-3">
                                    <div class="form-group">
                                        <asp:Button ID="btnExportOverRun" Text="Generate" OnClick="btnExportOverRun_Click" runat="server" CssClass="btn btn-pink"></asp:Button>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group" runat="server" id="aOverRunSPDF">
                                    </div>
                                    <div class="form-group" runat="server" id="aOverRunDPDF">
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group" runat="server" id="aOverRunSPPT">
                                    </div>
                                    <div class="form-group" runat="server" id="aOverRunDPPT">
                                    </div>
                                </div>
                            </div>
                        </div>

                        <hr />
                        <div class="row">
                            <div class="col-md-12">
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <div class="well well-sm">6. Time Overrun (Packages Where Extension Timeline is over)</div>
                                    </div>
                                </div>

                                <div class="col-md-3">
                                    <div class="form-group">
                                        <asp:Button ID="btnExportOverRun1" Text="Generate" OnClick="btnExportOverRun1_Click" runat="server" CssClass="btn btn-pink"></asp:Button>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group" runat="server" id="aOverRunSPDF1">
                                    </div>
                                    <div class="form-group" runat="server" id="aOverRunDPDF1">
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group" runat="server" id="aOverRunSPPT1">
                                    </div>
                                    <div class="form-group" runat="server" id="aOverRunDPPT1">
                                    </div>
                                </div>
                            </div>
                        </div>

                        <hr />
                        <div class="row">
                            <div class="col-md-12">
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <div class="well well-sm">7. Payment is Pending Due To Unavailability Of SNA Limit</div>
                                    </div>
                                </div>

                                <div class="col-md-3">
                                    <div class="form-group">
                                        <asp:Button ID="btnExportSNANotAvailable" Text="Generate" OnClick="btnExportSNANotAvailable_Click" runat="server" CssClass="btn btn-pink"></asp:Button>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group" runat="server" id="aSNANotAvailableSPDF">
                                    </div>
                                    <div class="form-group" runat="server" id="aSNANotAvailableDPDF">
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group" runat="server" id="aSNANotAvailableSPPT">
                                    </div>
                                    <div class="form-group" runat="server" id="aSNANotAvailableDPPT">
                                    </div>
                                </div>
                            </div>
                        </div>
                         <hr />
                        <div class="row">
                            <div class="col-md-12">
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <div class="well well-sm">8. Limit is Available and Payment Can Be Done</div>
                                    </div>
                                </div>

                                <div class="col-md-3">
                                    <div class="form-group">
                                        <asp:Button ID="btnExportSNAAvailable" Text="Generate" OnClick="btnExportSNAAvailable_Click" runat="server" CssClass="btn btn-pink"></asp:Button>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group" runat="server" id="aSNAAvailableSPDF">
                                    </div>
                                    <div class="form-group" runat="server" id="aSNAAvailableDPDF">
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group" runat="server" id="aSNAAvailableSPPT">
                                    </div>
                                    <div class="form-group" runat="server" id="aSNAAvailableDPPT">
                                    </div>
                                </div>
                            </div>
                        </div>

                        <hr />
                        <div class="row">
                            <div class="col-md-12">
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <asp:Button ID="btnMergeAllPDF" Text="Merge All Files" OnClick="btnMergeAllPDF_Click" runat="server" CssClass="btn btn-pink"></asp:Button>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group" runat="server" id="aFinalPDF">
                                    </div>
                                    <div class="form-group" runat="server" id="aFinalPPT">
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

