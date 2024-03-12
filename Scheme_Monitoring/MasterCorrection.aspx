<%@ Page Language="C#" MasterPageFile="~/TemplateMasterAdmin.master" AutoEventWireup="true"
    CodeFile="MasterCorrection.aspx.cs" Inherits="MasterCorrection" MaintainScrollPositionOnPostback="true" EnableEventValidation="false" ValidateRequest="false" %>

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
                                <div class="table-header">
                                    Invoice Correction
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-12">
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <asp:Label ID="Label1" runat="server" Text="Correction For*"></asp:Label>
                                        <asp:RadioButtonList ID="rbtType" runat="server" RepeatDirection="Horizontal">
                                            <asp:ListItem Value="I" Text="&nbsp;&nbsp;Invoice&nbsp;&nbsp;" Selected="True"></asp:ListItem>
                                            <asp:ListItem Value="E" Text="&nbsp;&nbsp;EMB&nbsp;&nbsp;"></asp:ListItem>
                                            <asp:ListItem Value="A" Text="&nbsp;&nbsp;Other Department&nbsp;&nbsp;"></asp:ListItem>
                                            <asp:ListItem Value="D" Text="&nbsp;&nbsp;Deuction Release&nbsp;&nbsp;"></asp:ListItem>
                                            <asp:ListItem Value="DPR" Text="&nbsp;&nbsp;DPR&nbsp;&nbsp;"></asp:ListItem>
                                        </asp:RadioButtonList>
                                    </div>
                                </div>
                                <div class="col-md-4" id="MId" runat="server">
                                    <div class="form-group">
                                        <asp:Label ID="lblDepartment" runat="server" Text="Master Id/Invoice Id*" CssClass="control-label no-padding-right"></asp:Label>
                                        <asp:TextBox ID="txtMasterId" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-12">
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <asp:DropDownList ID="DropDownList1" runat="server" AutoPostBack="true" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged">
                                            <asp:ListItem Value="-1" Selected="True">--Select Action--</asp:ListItem>
                                            <asp:ListItem Value="0">Check Formate</asp:ListItem>
                                            <asp:ListItem Value="1">Swap Type OLD <-> NEW</asp:ListItem>
                                            <asp:ListItem Value="2">Quated Rate (+/-) % </asp:ListItem>
                                            <asp:ListItem Value="3">Invoice Amount Update</asp:ListItem>
                                            <asp:ListItem Value="4">Add Designe and Drawing</asp:ListItem>
                                            <asp:ListItem Value="5">Add GST IN OLD Formate</asp:ListItem>
                                            <%--<asp:ListItem Value="6">--Update Rate As Per Modified BOQ--</asp:ListItem>--%>
                                            <asp:ListItem Value="7">Add Or Replace Documents</asp:ListItem>
                                            <asp:ListItem Value="8">Since Privious Correction</asp:ListItem>
                                            <asp:ListItem Value="9">Approve For Payment</asp:ListItem>
                                            <asp:ListItem Value="10">Change GST</asp:ListItem>
                                            <asp:ListItem Value="11">Fund Unavailability</asp:ListItem>
                                            <asp:ListItem Value="12">Change RA</asp:ListItem>
                                            <asp:ListItem Value="13">Change Physical MB No</asp:ListItem>
                                            <asp:ListItem Value="14">Loop Correction</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="row">
                            <div class="col-md-12">
                                <div class="col-md-4" id="CheckFormate" runat="server">
                                    <div class="form-group">
                                        <asp:Button ID="btnSave" Text="Check Formate" OnClick="btnSave_Click" runat="server" CssClass="btn btn-info"></asp:Button>
                                    </div>
                                </div>
                                <div class="col-md-4" id="oldNew" runat="server">
                                    <div class="form-group">
                                        <asp:Button ID="btnUpdate" Text="Change Format" OnClick="btnUpdate_Click" runat="server" CssClass="btn btn-warning"></asp:Button>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row" id="quatedRate" runat="server">
                            <div class="col-md-12">
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <asp:Label ID="Label2" runat="server" Text="Below (+/-)* (Invoice)" CssClass="control-label no-padding-right"></asp:Label>
                                        <asp:TextBox ID="txtBelow" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <asp:Button ID="btnUpdateQ" Text="Update Quoted Rate" OnClick="btnUpdateQ_Click" runat="server" CssClass="btn btn-warning"></asp:Button>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row" id="invoiceAmount" runat="server">
                            <div class="col-md-12">
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <asp:Label ID="lblIA" runat="server" Text="Invoice Amount" CssClass="control-label no-padding-right"></asp:Label>
                                        <asp:TextBox ID="txtIA" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <asp:Button ID="btnUpdateIA" Text="Update Invoice Ammount" OnClick="btnUpdateIA_Click" runat="server" CssClass="btn btn-warning"></asp:Button>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row" id="designeAndDrawing" runat="server">
                            <div class="col-md-12">
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <asp:Label ID="Label3" runat="server" Text="Designe And Drawing (%)" CssClass="control-label no-padding-right"></asp:Label>
                                        <asp:TextBox ID="txtDD" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <br />
                                        <asp:Button ID="btnUpdateDD" Text="Update Designe And Drawing" OnClick="btnUpdateDD_Click" runat="server" CssClass="btn btn-warning"></asp:Button>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row" id="gst" runat="server">
                            <div class="col-md-12">
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <asp:Button ID="btnUpdateGST" Text="Update GST (OLD Formate) " OnClick="btnUpdateGST_Click" runat="server" CssClass="btn btn-warning"></asp:Button>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row" id="BOQ_Rate" runat="server">
                            <div class="col-md-12">
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <asp:Label ID="lblIID" runat="server" Text="Package Invoice Item Id" CssClass="control-label no-padding-right"></asp:Label>
                                        <asp:TextBox ID="txtIID" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <asp:Label ID="lblBOQRate" runat="server" Text="New BOQ Rate (Quoted)" CssClass="control-label no-padding-right"></asp:Label>
                                        <asp:TextBox ID="txtBOQRate" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <asp:Button ID="btnUpdateBOQRate" Text="Update BOQ Rate" OnClick="btnUpdateBOQRate_Click" runat="server" CssClass="btn btn-warning"></asp:Button>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row" id="UploadDoc" runat="server">
                            <div class="col-md-12">
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <asp:Label ID="lblDoc" runat="server" Text="Select Document Type" CssClass="control-label no-padding-right"></asp:Label>
                                        <asp:DropDownList ID="ddlDoc" runat="server" CssClass="form-control">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <asp:Label ID="lblON" runat="server" Text="Order Number" CssClass="control-label no-padding-right"></asp:Label>
                                        <asp:TextBox ID="txtON" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <asp:Label ID="lblUploadDoc" runat="server" Text="Upload Document" CssClass="control-label no-padding-right"></asp:Label>
                                        <asp:FileUpload ID="flUploadDoc" runat="server" />
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <br />
                                        <asp:Button ID="btnUpdateDoc" Text="Update Document" OnClick="btnUpdateDoc_Click" runat="server" CssClass="btn btn-warning"></asp:Button>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row" id="SincePrevious" runat="server">
                            <div class="col-md-12">
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <asp:Label ID="lblSP" runat="server" Text="Package Id" CssClass="control-label no-padding-right"></asp:Label>
                                        <asp:TextBox ID="txtSP" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <asp:Button ID="btnSP" Text="Fix Since Previous" OnClick="btnSP_Click" runat="server" CssClass="btn btn-warning"></asp:Button>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row" id="divPaymentDone" runat="server">
                            <div class="col-md-12">
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <asp:Label ID="Label5" runat="server" Text="Payment Amount" CssClass="control-label no-padding-right"></asp:Label>
                                        <asp:TextBox ID="txtPaymentAmount" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <asp:Label ID="Label6" runat="server" Text="Upload Document (Payment Order)" CssClass="control-label no-padding-right"></asp:Label>
                                        <asp:FileUpload ID="flUploadOrder" runat="server" />
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <br />
                                        <asp:Button ID="btnApprovePayment" Text="Approve For Payment" OnClick="btnApprovePayment_Click" runat="server" CssClass="btn btn-warning"></asp:Button>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="row" id="divChageGST" runat="server">
                            <div class="col-md-12">
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <asp:Label ID="Label8" runat="server" Text="GST %" CssClass="control-label no-padding-right"></asp:Label>
                                        <asp:DropDownList ID="ddlGST" runat="server" CssClass="form-control">
                                            <asp:ListItem Text="12" Value="12"></asp:ListItem>
                                            <asp:ListItem Text="18" Value="18" Selected="True"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <asp:Button ID="btnUpdateGSTPer" Text="Update GST" OnClick="btnUpdateGSTPer_Click" runat="server" CssClass="btn btn-warning"></asp:Button>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="row" id="divFundNA" runat="server">
                            <div class="col-md-12">
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <asp:Label ID="Label9" runat="server" Text="Amount" CssClass="control-label no-padding-right"></asp:Label>
                                        <asp:TextBox ID="txtFundNAmount" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <asp:Button ID="btnFundNA" Text="Update Fund Not Availabiity" OnClick="btnFundNA_Click" runat="server" CssClass="btn btn-warning"></asp:Button>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row" id="divChangeRA" runat="server">
                            <div class="col-md-12">
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <asp:Label ID="Label10" runat="server" Text="RA Bill No" CssClass="control-label no-padding-right"></asp:Label>
                                        <asp:TextBox ID="txtRABillNo" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <asp:Button ID="btnUpdateRA" Text="Update RA Bill No" OnClick="btnUpdateRA_Click" runat="server" CssClass="btn btn-warning"></asp:Button>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row" id="divChangeMB" runat="server">
                            <div class="col-md-12">
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <asp:Label ID="Label11" runat="server" Text="RA Bill No" CssClass="control-label no-padding-right"></asp:Label>
                                        <asp:TextBox ID="txtMBNo" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <asp:Button ID="btnMBNo" Text="Update MB No" OnClick="btnMBNo_Click" runat="server" CssClass="btn btn-warning"></asp:Button>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="row" id="divLoopChange" runat="server">
                            <div class="col-md-12">
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <asp:Label ID="Label12" runat="server" Text="Designation" CssClass="control-label no-padding-right"></asp:Label>
                                        <asp:DropDownList ID="ddlDesignation" runat="server" CssClass="form-control"></asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <asp:Button ID="btnUpdateLoop" Text="Update Loop" OnClick="btnUpdateLoop_Click" runat="server" CssClass="btn btn-warning"></asp:Button>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="row" id="div_SNA_Balance" runat="server" visible="false">
                            <div class="col-md-12">
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <asp:Label ID="Label4" runat="server" Text="Project Code" CssClass="control-label no-padding-right"></asp:Label>
                                        <asp:TextBox ID="txtProjectCodeSNA" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <asp:Label ID="Label7" runat="server" Text="Amount" CssClass="control-label no-padding-right"></asp:Label>
                                        <asp:TextBox ID="txtAmountSNA" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <asp:Button ID="btnUpdateBalance" Text="Update" OnClick="btnUpdateBalance_Click" runat="server" CssClass="btn btn-warning"></asp:Button>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <asp:HiddenField ID="hf_Scheme_Id" Value="" runat="server" />
                    <asp:HiddenField ID="hf_Format" Value="" runat="server" />
                </ContentTemplate>
                <Triggers>
                    <asp:PostBackTrigger ControlID="btnUpdateDoc" />
                    <asp:PostBackTrigger ControlID="btnApprovePayment" />
                </Triggers>
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
        <!-- /.main-content -->

    </div>
</asp:Content>


