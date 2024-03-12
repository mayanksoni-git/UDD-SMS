<%@ Page Language="C#" MasterPageFile="~/TemplateMasterAdmin2.master" AutoEventWireup="true" CodeFile="GPFPassbookEntry.aspx.cs" Inherits="GPFPassbookEntry" MaintainScrollPositionOnPostback="true" EnableEventValidation="false" ValidateRequest="false"%>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="main-content">
        <div class="main-content-inner">
             <cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" EnablePartialRendering="true" EnablePageMethods="true" AsyncPostBackTimeout="6000">
            </cc1:ToolkitScriptManager>
            <asp:UpdatePanel ID="up" runat="server">               
                <ContentTemplate>
                    <style>
                        .control-label {
                            text-transform: uppercase;
                            color: #383535;
                            font-family: sans-serif;
                            transition: 0.6s;
                        }
                            .control-label:hover {
                                color: red;
                                letter-spacing: 0.5px;
                            }
                    </style>
                    <asp:Button ID="btnShowPopup" Text="Show" runat="server" Style="display: none;"></asp:Button>
                    <div class="page-content">
                        <div class="table-header">GPF Passbook Details</div>
                        <br />
                        <div class="row">
                            <div class="col-md-12">
                                <asp:GridView ID="grdView" runat="server" AutoGenerateColumns="False" CssClass="display table table-bordered" OnPreRender="grdView_PreRender">
                                    <Columns>
                                        <asp:BoundField DataField="HRMSEmployee_Name" HeaderText="Employee Name" />
                                        <asp:BoundField DataField="HRMSEmployeeSalaryInfo_BasicSalary" HeaderText="Employee Salary" />
                                        <asp:BoundField DataField="HRMSEmployee_JoinDateInService" HeaderText="Joining Date" />
                                        <asp:BoundField DataField="HRMSEmployee_EmailId" HeaderText="Email Id" />
                                        <asp:BoundField DataField="HRMSEmployee_MobileNo" HeaderText="Mobile Number" />
                                        <asp:BoundField DataField="HRMSEmployee_DepartmentalEmployeeCode" HeaderText="Employee Code" />
                                        <asp:TemplateField HeaderText="Add">
                                            <ItemTemplate>
                                                <asp:Button ID="btnAddNew" runat="server" OnClick="btnAddNew_Click" Text="Add Opening Balance" CssClass="btn btn-xs btn-pink"></asp:Button>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    
                                </asp:GridView>
                            </div>
                        </div>
                        <br />
                           <div class="row">
                            <div class="col-xs-12">
                                <div class="clearfix">
                                    
                                </div>
                            </div>
                        </div>
                  
                       <div id="divCreateNew" visible="false" runat="server">
                           <div class="row">
                               <div class="col-md-12">
                           <div class="row">
                            <div class="col-xs-12">
                                <div class="table-header">
                                   Create Opening Balance
                                </div>
                            </div>
                        </div>
                            </br>
                        <div class="row">                          
                            <div class="col-sm-4">
                                <div class="form-group">
                                    <asp:Label ID="lblMonth" runat="server" Text="Month :" CssClass="control-label no-padding-right"></asp:Label><br />
                                 <asp:DropDownList runat="server" ID="ddlMonth" Width="332px" Height="34px">
                                     <asp:ListItem Value="0" Text="------Select------"></asp:ListItem>
                                     <asp:ListItem Value="January" Text="January"></asp:ListItem>
                                     <asp:ListItem Value="February" Text="February"></asp:ListItem>
                                     <asp:ListItem Value="March" Text="March"></asp:ListItem>
                                     <asp:ListItem Value="April" Text="April"></asp:ListItem>
                                     <asp:ListItem Value="May" Text="May"></asp:ListItem>
                                     <asp:ListItem Value="June" Text="June"></asp:ListItem>
                                     <asp:ListItem Value="July" Text="July"></asp:ListItem>
                                     <asp:ListItem Value="August" Text="August"></asp:ListItem>
                                     <asp:ListItem Value="September" Text="September"></asp:ListItem>
                                     <asp:ListItem Value="October" Text="October"></asp:ListItem>
                                     <asp:ListItem Value="November" Text="November"></asp:ListItem>
                                     <asp:ListItem Value="December" Text="December"></asp:ListItem>
                                 </asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-sm-4">
                                <div class="form-group">
                                    <asp:Label ID="lblYear" runat="server" Text="Year :" CssClass="control-label no-padding-right"></asp:Label>
                                    <asp:TextBox ID="txtYear" runat="server" CssClass="form-control datepicker" MaxLength="4" onkeyup="isNumericVal(this);" AutoComplete="off"></asp:TextBox>
                                </div>
                            </div>
                              <div class="col-sm-4">
                                <div class="form-group">
                                    <asp:Label ID="lblDeposit" runat="server" Text="Deposit Amount (A):" CssClass="control-label no-padding-right"></asp:Label>
                                    <asp:TextBox ID="txtDeposit" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>
                        </div>

                       <div class="row">

                           <div class="col-sm-4">
                               <div class="form-group">
                                   <asp:Label ID="lblInterest" runat="server" Text="Interest Amount (B):" CssClass="control-label no-padding-right"></asp:Label>
                                   <asp:TextBox ID="txtInterest" runat="server" CssClass="form-control"></asp:TextBox>
                               </div>
                           </div>
                            <div class="col-sm-4">
                               <div class="form-group">
                                   <asp:Label ID="lblRefund" runat="server" Text="Refund (D):" CssClass="control-label no-padding-right"></asp:Label>
                                   <asp:TextBox ID="txtRefund" runat="server" CssClass="form-control"></asp:TextBox>
                               </div>
                           </div>
                           <div class="col-sm-4">
                               <div class="form-group">
                                   <asp:Label ID="lblWithdrawal" runat="server" Text="WithDrawal (C):" CssClass="control-label no-padding-right"></asp:Label>
                                   <asp:TextBox ID="txtWidhdrawal" runat="server" CssClass="form-control"></asp:TextBox>
                               </div>
                           </div>
                       </div>
                        <div class="row">
                            <div class="col-sm-4">
                                <div class="form-group">
                                    <asp:Label ID="lblTotal" runat="server" Text="Total (A+B+D-C):" CssClass="control-label no-padding-right"></asp:Label>
                                    <asp:TextBox ID="txtTotal" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-sm-4"></div>
                             <div class="col-sm-4"></div>
                        </div>

                        <br />
                           <div class="row">
                           <div class="col-md-3">
                               <div class="form-group">
                                       <asp:Button ID="txtSaveIncome" runat="server" CssClass="btn btn-info" Text="Save Opening Balance" OnClick="txtSaveIncome_Click" />
                               </div>
                           </div>
                           <div class="col-md-3">
                               <div class="form-group">
                                <asp:Button ID="btnReset" runat="server" CssClass="btn btn-warning" Text="Reset" OnClick="btnReset_Click" />
                               </div>
                           </div>  
                               </div>
                                   </div>
                           </div>
                          </div>
                      
                        <div class="row">
                            <div class="col-md-12">
                                <div class="row">
                            <div class="col-xs-12">
                                <div class="table-header">
                                    Opening Balance
                                </div>
                            </div>
                        </div>
                                <asp:GridView ID="grdIncome" runat="server" AutoGenerateColumns="False" CssClass="display table table-bordered" OnPreRender="grdView_PreRender">
                                    <Columns>
                                        <asp:BoundField DataField="GPF_Income_Month" HeaderText="Month" />
                                        <asp:BoundField DataField="GPF_Income_Year" HeaderText="Year" />
                                        <asp:BoundField DataField="GPF_Income_DepositAmount" HeaderText="Deposit Amount" />
                                        <asp:BoundField DataField="GPF_Income_Interest" HeaderText="Interest Amount" />
                                        <asp:BoundField DataField="GPF_Income_Withdrawal" HeaderText="Withdrawal" />
                                        <asp:BoundField DataField="GPF_Income_Total" HeaderText="Total" />
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-10"></div>
                            <div class="col-sm-2">
                                <asp:DropDownList runat="Server" ID="ddlYear" CssClass="form-control" OnSelectedIndexChanged="ddlYear_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-12">
                                <div class="clearfix" id="dtOptions" runat="server">
                                    <div class="pull-right tableTools-container"></div>
                                </div>
                                <div style="overflow: auto">
                                    <asp:GridView ID="grdGPFDetails" runat="server" AutoGenerateColumns="False" CssClass="display table table-bordered"
                                        EmptyDataText="No Records Found" OnPreRender="grdGPFDetails_PreRender" OnRowDataBound="grdGPFDetails_RowDataBound">
                                        <Columns>
                                            <asp:BoundField DataField="Month_Id" HeaderText="Month_Id">
                                                <HeaderStyle CssClass="displayStyle" />
                                                <ItemStyle CssClass="displayStyle" />
                                                <FooterStyle CssClass="displayStyle" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Month_FinancialOrder" HeaderText="Month_Id">
                                                <HeaderStyle CssClass="displayStyle" />
                                                <ItemStyle CssClass="displayStyle" />
                                                <FooterStyle CssClass="displayStyle" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="GPFDetails_BasicGPFRate" HeaderText="GPFDetails_BasicGPFRate">
                                                <HeaderStyle CssClass="displayStyle" />
                                                <ItemStyle CssClass="displayStyle" />
                                                <FooterStyle CssClass="displayStyle" />
                                            </asp:BoundField>
                                            <asp:TemplateField HeaderText="S No.">
                                                <ItemTemplate>
                                                    <%# Container.DataItemIndex + 1 %>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="Month_MonthName" HeaderText="Month Name" />
                                            <asp:TemplateField HeaderText="Subscription">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtSubscription" runat="server" CssClass="form-control" Width="60px" Text='<%# Eval("GPFDetails_Subscription") %>'></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                             <asp:TemplateField HeaderText="Refund">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtRefund" runat="server" CssClass="form-control" Width="60px" onkeyup="isNumericVal(this);" Text='<%# Eval("GPFDetails_Refund") %>'></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                              
                                             <asp:TemplateField HeaderText="Total">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtTotal" runat="server" CssClass="form-control" Width="60px" onkeyup="isNumericVal(this);" Text='<%# Eval("GPFDetails_Total") %>'></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Voucher No">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtVoucherNo" runat="server" CssClass="form-control" Width="60px" Text='<%# Eval("GPFDetails_VoucherNo") %>'></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                             <asp:TemplateField HeaderText="Voucher Date">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtVoucherDate" runat="server" CssClass="form-control date-picker" Width="120px" data-date-format="dd/mm/yyyy" Text='<%# Eval("GPFDetails_VoucherDate") %>'></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Rate of Interest">
                                                <ItemTemplate>
                                                    <asp:DropDownList ID="ddlRate" runat="server" CssClass="form-control" onkeyup="isNumericVal(this);" Width="90px"></asp:DropDownList>
                                                </ItemTemplate>
                                            </asp:TemplateField>  
                                          <asp:TemplateField HeaderText="Withdrawal">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtWithdrawal" runat="server" CssClass="form-control" Width="60px" onkeyup="isNumericVal(this);" Text='<%# Eval("GPFDetails_Withdrawal") %>'></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Monthly Balance on which Interest is Calculated (₹)">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtCalculation" runat="server" CssClass="form-control" Width="180px" onkeyup="isNumericVal(this);" Text='<%# Eval("GPFDetails_Calculation") %>'></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Remarks">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtRemarks" runat="server" CssClass="form-control" Width="60px" Text='<%# Eval("GPFDetails_Remarks") %>'></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Total Subscription">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtTotalSubscription" runat="server" CssClass="form-control" Width="60px" Text='<%# Eval("GPFDetails_TotalSubscription") %>'></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </div>
                        </div>
                        <br />
                        <div class="row">
                            <div class="col-xs-12">
                                <div class="col-md-4"></div>
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <span style="margin-left: 50px">
                                            <asp:Button ID="btnSave" runat="server" CssClass="btn btn-info" Text="Save Passbook Details" OnClick="btnSave_Click" />
                                        </span>
                                    </div>
                                </div>
                                <div class="col-md-4">
                                </div>
                            </div>
                        </div>
                        <asp:HiddenField ID="hf_Person_Id" runat="server" Value="0" />
                        </div>
                     <asp:HiddenField ID="hf_GPF_Income_Id" runat="server" Value="0" />
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
    <script type="text/javascript">

        function setTabPageActive(mainMenuId, subMenuId, contentPageId, totalCount) {
            debugger;
            for (var i = 0; i < totalCount; i++) {
                $("#w_" + (i + 1)).removeClass('active');
                $("#wt_" + (i + 1)).attr('aria-expanded', 'false');
                $("#doc" + (i + 1)).removeClass('active in');
            }
            //$('#nav nav-tabs').find('li[class^="active"]').removeClass('active');
            //$('#nav nav-tabs').find('a').removeAttr('aria-expanded');

            $("#" + mainMenuId + "").addClass('active');
            $("#" + subMenuId + "").attr('aria-expanded', 'true');
            $("#" + contentPageId + "").addClass('active in');
            sessionStorage["_activeMainTabMenu"] = mainMenuId;
            sessionStorage["_activeSubTabMenu"] = subMenuId;
            sessionStorage["_activecontentPageId"] = contentPageId;
            sessionStorage["_activetotalCount"] = totalCount;
        }
        Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(function (evt, args) {
            $(document).ready(function () {
                debugger;
                if (sessionStorage["_activeMainTabMenu"] == "" || sessionStorage["_activeSubTabMenu"] == undefined || sessionStorage["_activetotalCount"] == undefined) { }
                else {
                    //$('#nav nav-tabs').find('li').removeAttr('class');
                    var totalTabs = sessionStorage["_activetotalCount"];
                    for (var i = 0; i < totalTabs; i++) {
                        $("#w_" + (i + 1)).removeClass('active');
                        $("#wt_" + (i + 1)).attr('aria-expanded', 'false');
                        $("#doc" + (i + 1)).removeClass('active in');
                    }
                    $("#" + sessionStorage["_activeMainTabMenu"] + "").addClass('active');
                    $("#" + sessionStorage["_activecontentPageId"] + "").addClass('active in');
                    $("#" + sessionStorage["_activeSubTabMenu"] + "").attr('aria-expanded', 'true');
                }
            });
        });

    </script>
</asp:Content>

