<%@ Page Language="C#" MasterPageFile="~/TemplateMasterAdmin.master" AutoEventWireup="true"
    CodeFile="MasterProject.aspx.cs" Inherits="MasterProject" MaintainScrollPositionOnPostback="true" EnableEventValidation="false" ValidateRequest="false" %>

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
                                <div class="col-12 mb-3">
                                    <div class="page-title-box d-sm-flex align-items-center justify-content-between">
                                        <h4 class="mb-sm-0">Scheme Master</h4>
                                        <div class="page-title-right">
                                            <ol class="breadcrumb m-0">
                                                <li class="breadcrumb-item"><a href="javascript: void(0);">Home</a></li>
                                                <li class="breadcrumb-item">Project Master</li>
                                                <li class="breadcrumb-item active">Scheme Master</li>
                                            </ol>
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <div class="row">
                                <div class="col-xxl-12 col-md-12">
                                    <div>
                                        <asp:Button ID="btnAddNew" runat="server" OnClick="btnAddNew_Click" Text="Create New" CssClass="btn-filter mb-2"></asp:Button>
                                    </div>
                                </div>
                            </div>

                            <div id="divCreateNew" runat="server" visible="false">
                                <div class="row">
                                    <div class="col-lg-12">
                                        <div class="card">
                                            <div class="card-header align-items-center d-flex">
                                                <h4 class="card-title mb-0 flex-grow-1">Create / Update Scheme</h4>
                                            </div>
                                            <!-- end card header -->
                                            <div class="card-body">
                                                <div class="live-preview">
                                                    <div class="row gy-4">
                                                        <div class="col-xxl-3 col-md-6">
                                                            <div>
                                                                <asp:Label ID="lblProject" runat="server" Text="Scheme*" CssClass="control-label no-padding-right"></asp:Label>
                                                                <asp:TextBox ID="txtProject" runat="server" CssClass="form-control"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                        <div class="col-xxl-3 col-md-6">
                                                            <div>
                                                                <asp:Label ID="lblShortName" runat="server" Text="Short Name*" CssClass="control-label no-padding-right"></asp:Label>
                                                                <asp:TextBox ID="txtShortName" runat="server" CssClass="form-control"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                        <div class="col-xxl-3 col-md-6">
                                                            <div id="divZone" runat="server">
                                                                <asp:Label ID="Label3" runat="server" Text="Total Budget Allocated (In Lakhs)*" CssClass="control-label no-padding-right"></asp:Label>
                                                                <asp:TextBox ID="txtBudget" runat="server" CssClass="form-control" onkeyup="isNumericVal(this);"></asp:TextBox>
                                                            </div>
                                                        </div>

                                                        <div class="col-xxl-3 col-md-6">
                                                            <div>
                                                                <asp:Label ID="Label15" runat="server" Text="Physical Progress (%)" CssClass="form-label"></asp:Label>
                                                                <asp:TextBox ID="txtPhysicalTarget" runat="server" CssClass="form-control" onkeyup="isNumericVal(this);"></asp:TextBox>
                                                            </div>
                                                        </div>

                                                        <div class="col-xxl-3 col-md-6">
                                                            <div>
                                                                
                                                            </div>
                                                        </div>
                                                        <!--end col-->

                                                        <div class="col-xxl-3 col-md-6">
                                                            <div id="divCircle" runat="server">
                                                                <asp:Label ID="Label4" runat="server" Text="Upload GO" CssClass="control-label no-padding-right"></asp:Label>
                                                                <br />
                                                                <asp:FileUpload ID="flUploadGO" runat="server" />
                                                            </div>
                                                        </div>
                                                        <!--end col-->

                                                        <div class="col-xxl-3 col-md-6">
                                                            <div id="divDivision" runat="server">
                                                                <asp:Label ID="Label5" runat="server" Text="Upload Guideline" CssClass="control-label no-padding-right"></asp:Label>
                                                                <br />
                                                                <asp:FileUpload ID="flUploadGuideline" runat="server" />
                                                            </div>
                                                        </div>
                                                        <!--end col-->

                                                        <div class="col-xxl-3 col-md-6">
                                                            <div>
                                                                <asp:Label ID="Label12" runat="server" Text="Download GO" CssClass="control-label no-padding-right"></asp:Label>
                                                                
                                                                <asp:ImageButton ID="btnDownload" ToolTip="Download GO" runat="server" ImageUrl="~/assets/images/download.png" Width="30px" Height="30px" OnClientClick="javascript:downloadFile(this);" />
                                                            </div>
                                                        </div>

                                                        <div class="col-xxl-3 col-md-6">
                                                            <div>
                                                                <asp:Label ID="Label13" runat="server" Text="Download Guideline" CssClass="control-label no-padding-right"></asp:Label>
                                                                
                                                                <asp:ImageButton ID="btnDownloadGuideline" ToolTip="Download Guidline" runat="server" ImageUrl="~/assets/images/download.png" Width="30px" Height="30px" OnClientClick="javascript:downloadFile(this);" />
                                                            </div>
                                                        </div>
                                                        <!--end col-->
                                                    </div>
                                                    <!--end row-->
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <!--end col-->
                                </div>


                                <div class="row">
                                    <div class="col-lg-12">
                                        <div class="card">
                                            <div class="card-header align-items-center d-flex">
                                                <h4 class="card-title mb-0 flex-grow-1">Funding Pattern Breakup</h4>
                                            </div>
                                            <!-- end card header -->
                                            <div class="card-body">
                                                <div class="live-preview">
                                                    <div class="row gy-12">
                                                        <asp:GridView ID="grdFundingPattern" runat="server" CssClass="display table table-bordered" AutoGenerateColumns="False" EmptyDataText="No Records Found" OnPreRender="grdPost_PreRender" OnRowDataBound="grdFundingPattern_RowDataBound">
                                                            <Columns>
                                                                <asp:BoundField DataField="FundingPattern_Id" HeaderText="FundingPattern_Id">
                                                                    <HeaderStyle CssClass="displayStyle" />
                                                                    <ItemStyle CssClass="displayStyle" />
                                                                    <FooterStyle CssClass="displayStyle" />
                                                                </asp:BoundField>
                                                                <asp:TemplateField HeaderText="S No.">
                                                                    <ItemTemplate>
                                                                        <%# Container.DataItemIndex + 1 %>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:BoundField DataField="FundingPattern_Name" HeaderText="Funding Pattern Name" />
                                                                <asp:TemplateField HeaderText="Percentage (%)">
                                                                    <ItemTemplate>
                                                                        <asp:TextBox ID="txtShareP" runat="server" CssClass="form-control" onkeyup="isNumericVal(this);" MaxLength="3" AutoPostBack="True" OnTextChanged="txtShareP_TextChanged" Text='<%# Eval("ProjectFundingPattern_Percentage") %>'></asp:TextBox>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Value">
                                                                    <ItemTemplate>
                                                                        <asp:TextBox ID="txtShareV" runat="server" CssClass="form-control" onkeyup="isNumericVal(this);" Text='<%# Eval("ProjectFundingPattern_Value") %>'></asp:TextBox>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                            </Columns>
                                                        </asp:GridView>
                                                    </div>
                                                    <!--end row-->
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <!--end col-->
                                </div>
                                <div class="row">
                                    <div class="col-lg-12">
                                        <div class="card">
                                            <div class="card-body">
                                                <div class="live-preview">
                                                    <div class="row gy-4">
                                                        <div class="col-xxl-3 col-md-6">
                                                            <div>
                                                                <asp:Button ID="btnSave" Text="Save" OnClick="btnSave_Click" runat="server" CssClass="btn btn-info"></asp:Button>
                                                            </div>
                                                        </div>
                                                        <div class="col-xxl-3 col-md-6">
                                                            <div>
                                                                <asp:Button ID="btnDelete" runat="server" OnClick="btnDelete_Click" Text="Delete" CssClass="btn btn-warning"></asp:Button>
                                                            </div>
                                                        </div>
                                                        <div class="col-xxl-3 col-md-6">
                                                            <div>
                                                                <asp:Button ID="btnReset" runat="server" OnClick="btnReset_Click" Text="Reset" CssClass="btn"></asp:Button>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <!--end row-->
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <!--end col-->
                                </div>
                            </div>

                            <div class="row">
                                <div class="col-xs-12">

                                    <div class="row">
                                        <div class="col-xs-12">
                                            <div class="clearfix" id="dtOptions" runat="server">
                                                <div class="pull-right tableTools-container"></div>
                                            </div>

                                            <!-- div.table-responsive -->

                                            <!-- div.dataTables_borderWrap -->
                                            <div class="table-responsive">
                                                <asp:GridView ID="grdPost" runat="server" CssClass="display table table-bordered" AutoGenerateColumns="False" EmptyDataText="No Records Found" OnPreRender="grdPost_PreRender">
                                                    <Columns>
                                                        <asp:BoundField DataField="Project_Id" HeaderText="Project_Id">
                                                            <HeaderStyle CssClass="displayStyle" />
                                                            <ItemStyle CssClass="displayStyle" />
                                                            <FooterStyle CssClass="displayStyle" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="Project_GO_Path" HeaderText="Project_GO_Path">
                                                            <HeaderStyle CssClass="displayStyle" />
                                                            <ItemStyle CssClass="displayStyle" />
                                                            <FooterStyle CssClass="displayStyle" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="Project_Guideline_Path" HeaderText="Project_Guideline_Path">
                                                            <HeaderStyle CssClass="displayStyle" />
                                                            <ItemStyle CssClass="displayStyle" />
                                                            <FooterStyle CssClass="displayStyle" />
                                                        </asp:BoundField>
                                                        <asp:TemplateField HeaderText="S No.">
                                                            <ItemTemplate>
                                                                <%# Container.DataItemIndex + 1 %>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:BoundField DataField="Project_Name" HeaderText="Scheme" />
                                                        <asp:BoundField DataField="Project_Budget" HeaderText="Scheme Budget (In Lakhs)" />
                                                        <asp:BoundField DataField="CreatedBy" HeaderText="Created By" />
                                                        <asp:BoundField DataField="Created_Date" HeaderText="Created Date" />
                                                        <asp:BoundField DataField="ModifyBy" HeaderText="Modified By" />
                                                        <asp:BoundField DataField="Modify_Date" HeaderText="Modified Date" />
                                                        <asp:TemplateField HeaderText="View">
                                                            <ItemTemplate>
                                                                <asp:ImageButton ID="btnEdit" Width="20px" Height="20px" OnClick="btnEdit_Click" ImageUrl="~/assets/images/edit.png" runat="server" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </div>
                                        </div>
                                    </div>
                                    <!-- PAGE CONTENT ENDS -->
                                </div>
                                <!-- /.col -->
                            </div>
                            <!-- /.row -->
                        </div>
                    </div>
                    <asp:HiddenField ID="hf_Project_Id" runat="server" Value="0" />
                </ContentTemplate>
                <Triggers>
                    <asp:PostBackTrigger ControlID="btnSave" />
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



    <script>
        Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(function (evt, args) {
            jQuery(function ($) {
                $('.modalBackground1').click(function () {
                    var id = $(this).attr('id').replace('_backgroundElement', '');
                    $find(id).hide();
                });
            })
        });

        function downloadFile(obj) {
            var CallProcessingPhoto_FileName;
            CallProcessingPhoto_FileName = obj.title;
            if (CallProcessingPhoto_FileName == "") {
                alert('File Not Found..!');
            }
            else {
                window.open(location.origin + CallProcessingPhoto_FileName, "_blank", "", true);
            }
        }
    </script>
</asp:Content>



