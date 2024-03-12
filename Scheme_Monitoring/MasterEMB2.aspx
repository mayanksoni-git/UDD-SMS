<%@ Page Language="C#" MasterPageFile="~/TemplateMasterAdmin.master" AutoEventWireup="true"
    CodeFile="MasterEMB2.aspx.cs" Inherits="MasterEMB2" MaintainScrollPositionOnPostback="true" EnableEventValidation="false" ValidateRequest="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="main-content">
        <div class="main-content-inner">
            <cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" EnablePartialRendering="true" EnablePageMethods="true" AsyncPostBackTimeout="6000">
            </cc1:ToolkitScriptManager>

            <div class="page-content">
                <div class="row">
                    <div class="col-xs-12">
                        <div class="table-header">
                            Details Of EMB Items                                       
                            <div class="form-group" style="float: right; padding-right: 10px">
                                <asp:CheckBox ID="chkAbstractOnly" Checked="true" Text="Fill Only Abstract" runat="server" />
                            </div>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-sm-12">
                        <div class="col-md-4">
                            <div class="form-group">
                                <asp:Button ID="btnLoadFirstHalf" Text="Load First Half Portion Of MB" OnClick="btnLoadFirstHalf_Click" runat="server" CssClass="btn btn-info"></asp:Button>
                            </div>
                        </div>
                        <div class="col-md-4">
                            <div class="form-group">
                                <asp:Button ID="btnLoadSecondHalf" Text="Load Second Half Portion Of MB" OnClick="btnLoadSecondHalf_Click" runat="server" CssClass="btn btn-info"></asp:Button>
                            </div>
                        </div>
                        <div class="col-md-4">
                            <div class="form-group">
                                <asp:Button ID="btnLoadFull" Text="Load Complete MB" OnClick="btnLoadFull_Click" runat="server" CssClass="btn btn-warning"></asp:Button>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-12">
                        <div class="col-md-12">
                            <div style="overflow: auto">
                                <asp:GridView ID="grdEMB" runat="server" CssClass="display table table-bordered" AutoGenerateColumns="False" EmptyDataText="No Records Found" OnPreRender="grdEMB_PreRender" OnRowDataBound="grdEMB_RowDataBound" OnPageIndexChanging="GrdEMB_PageIndexChanging" AllowPaging="true" PageSize="50">
                                    <Columns>
                                        <asp:BoundField DataField="PackageEMB_Id" HeaderText="PackageEMB_Id">
                                            <HeaderStyle CssClass="displayStyle" />
                                            <ItemStyle CssClass="displayStyle" />
                                            <FooterStyle CssClass="displayStyle" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="PackageBOQ_Id" HeaderText="PackageBOQ_Id">
                                            <HeaderStyle CssClass="displayStyle" />
                                            <ItemStyle CssClass="displayStyle" />
                                            <FooterStyle CssClass="displayStyle" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="PackageEMB_Package_Id" HeaderText="PackageEMB_Package_Id">
                                            <HeaderStyle CssClass="displayStyle" />
                                            <ItemStyle CssClass="displayStyle" />
                                            <FooterStyle CssClass="displayStyle" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="PackageEMB_Unit_Id" HeaderText="PackageEMB_Unit_Id">
                                            <HeaderStyle CssClass="displayStyle" />
                                            <ItemStyle CssClass="displayStyle" />
                                            <FooterStyle CssClass="displayStyle" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="PackageEMB_Approval_Id" HeaderText="PackageEMB_Approval_Id">
                                            <HeaderStyle CssClass="displayStyle" />
                                            <ItemStyle CssClass="displayStyle" />
                                            <FooterStyle CssClass="displayStyle" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="PackageBOQ_RateEstimated" HeaderText="PackageBOQ_RateEstimated">
                                            <HeaderStyle CssClass="displayStyle" />
                                            <ItemStyle CssClass="displayStyle" />
                                            <FooterStyle CssClass="displayStyle" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="PackageBOQ_AmountEstimated" HeaderText="PackageBOQ_AmountEstimated">
                                            <HeaderStyle CssClass="displayStyle" />
                                            <ItemStyle CssClass="displayStyle" />
                                            <FooterStyle CssClass="displayStyle" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="PackageBOQ_RateQuoted" HeaderText="PackageBOQ_RateQuoted">
                                            <HeaderStyle CssClass="displayStyle" />
                                            <ItemStyle CssClass="displayStyle" />
                                            <FooterStyle CssClass="displayStyle" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="PackageBOQ_AmountQuoted" HeaderText="PackageBOQ_AmountQuoted">
                                            <HeaderStyle CssClass="displayStyle" />
                                            <ItemStyle CssClass="displayStyle" />
                                            <FooterStyle CssClass="displayStyle" />
                                        </asp:BoundField>
                                        <asp:TemplateField HeaderText="S No.">
                                            <ItemTemplate>
                                                <%# Container.DataItemIndex + 1 %>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Description Of Goods">
                                            <ItemTemplate>
                                                <asp:Label ID="lblSpecification" runat="server" CssClass="control-label no-padding-right" Text='<%# Eval("PackageEMB_Specification") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Unit">
                                            <ItemTemplate>
                                                <asp:DropDownList ID="ddlUnit" runat="server" CssClass="form-control" Enabled="false" Width="60px"></asp:DropDownList>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Quantity">
                                            <ItemTemplate>
                                                <asp:Label ID="lblQty" runat="server" CssClass="control-label no-padding-right" Text='<%# Eval("PackageBOQ_Qty") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="PackageBOQ_RateEstimated" HeaderText="Rate Estimated" />
                                        <asp:BoundField DataField="PackageBOQ_RateQuoted" HeaderText="Rate Quoted" />
                                        <asp:TemplateField HeaderText="Length">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtLength" runat="server" CssClass="form-control" Text='<%# Eval("PackageEMB_Length") %>' Width="80px" onchange="return calculateQty(this);" MaxLength="10" onkeyup="isNumericVal(this);"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Breadth">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtBreadth" runat="server" CssClass="form-control" Text='<%# Eval("PackageEMB_Breadth") %>' Width="80px" onchange="return calculateQty(this);" MaxLength="10" onkeyup="isNumericVal(this);"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Height">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtHeight" runat="server" CssClass="form-control" Text='<%# Eval("PackageEMB_Height") %>' Width="80px" onchange="return calculateQty(this);" MaxLength="10" onkeyup="isNumericVal(this);"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Current Quantity">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtQty" runat="server" CssClass="form-control" Text='<%# Eval("PackageEMB_Qty") %>' Width="80px" MaxLength="10" onkeyup="isNumericVal(this);" onchange="return calculateQtyA(this);"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Contents or Area / Volume">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtContents" runat="server" CssClass="form-control" Text='<%# Eval("PackageEMB_Contents") %>' TextMode="MultiLine"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="Unit_Length_Applicable" HeaderText="Unit_Length_Applicable">
                                            <HeaderStyle CssClass="displayStyle" />
                                            <ItemStyle CssClass="displayStyle" />
                                            <FooterStyle CssClass="displayStyle" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Unit_Bredth_Applicable" HeaderText="Unit_Bredth_Applicable">
                                            <HeaderStyle CssClass="displayStyle" />
                                            <ItemStyle CssClass="displayStyle" />
                                            <FooterStyle CssClass="displayStyle" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Unit_Height_Applicable" HeaderText="Unit_Height_Applicable">
                                            <HeaderStyle CssClass="displayStyle" />
                                            <ItemStyle CssClass="displayStyle" />
                                            <FooterStyle CssClass="displayStyle" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="PackageEMB_PackageEMB_Master_Id" HeaderText="PackageEMB_PackageEMB_Master_Id">
                                            <HeaderStyle CssClass="displayStyle" />
                                            <ItemStyle CssClass="displayStyle" />
                                            <FooterStyle CssClass="displayStyle" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="GSTType" HeaderText="GSTType">
                                            <HeaderStyle CssClass="displayStyle" />
                                            <ItemStyle CssClass="displayStyle" />
                                            <FooterStyle CssClass="displayStyle" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="GSTPercenatge" HeaderText="GSTPercenatge">
                                            <HeaderStyle CssClass="displayStyle" />
                                            <ItemStyle CssClass="displayStyle" />
                                            <FooterStyle CssClass="displayStyle" />
                                        </asp:BoundField>
                                        <asp:TemplateField HeaderText="Percentage Amount To Be Released">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtPercentageToBeReleased" runat="server" CssClass="form-control" MaxLength="11" onkeyup="isNumericVal(this);" Text='<%# Eval("PackageEMB_PercentageToBeReleased") %>'></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Quantity Paid Till Date">
                                            <ItemTemplate>
                                                <asp:Label ID="lblQtyMax" runat="server" CssClass="control-label no-padding-right" Text='<%# Eval("PackageBOQ_QtyPaid") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Percentage Value Paid Till Date">
                                            <ItemTemplate>
                                                <asp:Label ID="lblPercentageValuePaidTillDate" runat="server" CssClass="control-label no-padding-right" Text='<%# Eval("PercentageValuePaidTillDate") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="PackageEMB_PackageBOQ_OrderNo" HeaderText="PackageEMB_PackageBOQ_OrderNo">
                                            <HeaderStyle CssClass="displayStyle" />
                                            <ItemStyle CssClass="displayStyle" />
                                            <FooterStyle CssClass="displayStyle" />
                                        </asp:BoundField>
                                    </Columns>
                                    <PagerStyle HorizontalAlign="Center" CssClass="GridPager" />
                                </asp:GridView>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-sm-12">
                        <div class="col-md-4">
                            <div class="form-group">
                                <asp:Label ID="Label1" runat="server" Text="RA Bill No" CssClass="control-label no-padding-right"></asp:Label>
                                <%--<asp:TextBox ID="txtRABillNo" onkeyup="isNumericVal(this);" runat="server" CssClass="form-control"></asp:TextBox>--%>
                                <asp:DropDownList ID="ddlRABillNo" runat="server" CssClass="form-control"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-4">
                            <div class="form-group">
                                <asp:Label ID="lblDeduction" runat="server" Text="MB Ref. No (Physical)" CssClass="control-label no-padding-right"></asp:Label>
                                <asp:TextBox ID="txtMB_No" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-4">
                            <div class="form-group">
                                <asp:Label ID="Label2" runat="server" Text="MB Date" CssClass="control-label no-padding-right"></asp:Label>
                                <asp:TextBox ID="txtMBDate" runat="server" CssClass="form-control date-picker" autocomplete="off" data-date-format="dd/mm/yyyy"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-sm-12">
                        <div class="col-md-12">
                            <span class="label label-danger arrowed">
                                <i class="ace-icon fa fa-angle-double-right"></i>
                                MB With Same RA Bill Number will be combined into One Invoice After Approval.
                            </span>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-sm-12">
                        <div style="overflow: auto">
                            <asp:GridView ID="grdDeductions" runat="server" CssClass="display table table-bordered" AutoGenerateColumns="False" EmptyDataText="No Records Found">
                                <Columns>
                                    <asp:BoundField DataField="Deduction_Id" HeaderText="Deduction_Id">
                                        <HeaderStyle CssClass="displayStyle" />
                                        <ItemStyle CssClass="displayStyle" />
                                        <FooterStyle CssClass="displayStyle" />
                                    </asp:BoundField>
                                    <asp:TemplateField HeaderText="Select">
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chkSelect" runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="S No.">
                                        <ItemTemplate>
                                            <%# Container.DataItemIndex + 1 %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField HeaderText="Deduction" DataField="Deduction_Name" />
                                    <asp:TemplateField HeaderText="Deduction Type">
                                        <ItemTemplate>
                                            <asp:RadioButtonList ID="rblDeductionType" runat="server" RepeatDirection="Horizontal">
                                                <asp:ListItem Text="%" Value="Per" Selected="True" />
                                            </asp:RadioButtonList>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Deduction Value">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtDeductionValue" onkeyup="isNumericVal(this);" runat="server" CssClass="form-control"></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                <FooterStyle Font-Bold="true" BackColor="LightGray" />
                            </asp:GridView>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-sm-12">
                        <div class="col-md-3">
                            <div class="form-group">
                                <br />
                                <%--<asp:Button ID="btnSaveEMB" Text="Save EMB" OnClick="btnSaveEMB_Click" runat="server" CssClass="btn btn-warning"></asp:Button>--%>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <div class="form-group">
                                <br />
                                <asp:Button ID="btnSaveAndForward" Text="Save EMB and Forward For Approval" OnClick="btnSaveAndForward_Click" runat="server" CssClass="btn btn-info"></asp:Button>
                            </div>
                        </div>
                    </div>
                </div>
                <asp:HiddenField ID="hf_Project_Id" runat="server" Value="0" />
                <asp:HiddenField ID="hf_ProjectWork_Id" runat="server" Value="0" />
                <asp:HiddenField ID="hf_ProjectWorkPkg_LastRABillNo" runat="server" Value="0" />
            </div>
        </div>
    </div>
    <!-- DataTable specific plugin scripts -->
    <script src="assets/js/jquery-2.1.4.min.js"></script>
    <script src="assets/js/jquery.dataTables.min.js"></script>
    <script src="assets/js/jquery.dataTables.bootstrap.min.js"></script>
    <script src="assets/js/dataTables.buttons.min.js"></script>
    <script src="assets/js/buttons.flash.min.js"></script>
    <script src="assets/js/buttons.html5.min.js"></script>
    <script src="assets/js/buttons.print.min.js"></script>
    <script src="assets/js/buttons.colVis.min.js"></script>
    <script src="assets/js/dataTables.select.min.js"></script>
    <script src="assets/js/ace-elements.min.js"></script>
    <script src="assets/js/ace.min.js"></script>
    <script src="assets/js/dataTables.fixedHeader.min.js"></script>
    <script src="assets/js/jquery.mark.min.js"></script>
    <script src="assets/js/datatables.mark.js"></script>
    <script src="assets/js/dataTables.colReorder.min.js"></script>

    <script>
        Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(function (evt, args) {
            jQuery(function ($) {
                $('.modalBackground1').click(function () {
                    var id = $(this).attr('id').replace('_backgroundElement', '');
                    $find(id).hide();
                });
            })
        });

        //Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(function (evt, args) {
        //    jQuery(function ($) {
        function calculateQty(obj) {
            debugger;
            var id = obj.id;
            var row = obj.parentNode.parentNode;
            var id1 = id.replace('txtLength', '').replace('txtBreadth', '').replace('txtHeight', '') + 'txtLength';
            var id2 = id.replace('txtLength', '').replace('txtBreadth', '').replace('txtHeight', '') + 'txtBreadth';
            var id3 = id.replace('txtLength', '').replace('txtBreadth', '').replace('txtHeight', '') + 'txtHeight';
            var Qty_Master = 0;
            try {
                Qty_Master = parseFloat(row.childNodes[13].innerText);
            }
            catch (ee) { }
            var Qty_Paid_Till = 0;
            try {
                Qty_Paid_Till = parseFloat(row.childNodes[26].innerText);
            }
            catch (ee) { }
            var unit_id = id.replace('txtLength', '').replace('txtBreadth', '').replace('txtHeight', '') + 'ddlUnit';
            var comments_id = id.replace('txtLength', '').replace('txtBreadth', '').replace('txtHeight', '') + 'txtContents';
            var qty_id = id.replace('txtLength', '').replace('txtBreadth', '').replace('txtHeight', '') + 'txtQty';

            var Qty_Available = Qty_Master - Qty_Paid_Till;
            var val1 = parseFloat(document.getElementById(id1).value);
            var val2 = parseFloat(document.getElementById(id2).value);
            var val3 = parseFloat(document.getElementById(id3).value);

            if ($.isNumeric(val1)) {
                val1 = val1;
            }
            else {
                val1 = 1;
            }

            if ($.isNumeric(val2)) {
                val2 = val2;
            }
            else {
                val2 = 1;
            }

            if ($.isNumeric(val3)) {
                val3 = val3;
            }
            else {
                val3 = 1;
            }

            var qty_val = parseFloat(document.getElementById(qty_id).value);

            var ddlunit = document.getElementById(unit_id);
            var unit_text = ddlunit.options[ddlunit.selectedIndex].text;

            if ((val1 * val2 * val3) > Qty_Available) {
                document.getElementById(comments_id).value = '';
                document.getElementById(qty_id).value = '';
                document.getElementById(id1).value = '';
                document.getElementById(id2).value = '';
                document.getElementById(id3).value = '';
            }
            else {
                document.getElementById(comments_id).value = (val1 * val2 * val3) + ' ' + unit_text;
                document.getElementById(qty_id).value = (val1 * val2 * val3);
            }
        }
        //    })
        //});

        function calculateQtyA(obj) {
            debugger;
            if (document.getElementById('ctl00_ContentPlaceHolder1_chkAbstractOnly').checked == true) {
                var id = obj.id;
                var row = obj.parentNode.parentNode;
                var id1 = id.replace('txtLength', '').replace('txtBreadth', '').replace('txtHeight', '').replace('txtQty', '') + 'txtLength';
                var id2 = id.replace('txtLength', '').replace('txtBreadth', '').replace('txtHeight', '').replace('txtQty', '') + 'txtBreadth';
                var id3 = id.replace('txtLength', '').replace('txtBreadth', '').replace('txtHeight', '').replace('txtQty', '') + 'txtHeight';
                var Qty_Master = 0;
                try {
                    Qty_Master = parseFloat(row.childNodes[13].innerText);
                }
                catch (ee) { }
                var Qty_Paid_Till = 0;
                try {
                    Qty_Paid_Till = parseFloat(row.childNodes[26].innerText);
                }
                catch (ee) { }
                var unit_id = id.replace('txtLength', '').replace('txtBreadth', '').replace('txtHeight', '').replace('txtQty', '') + 'ddlUnit';
                var comments_id = id.replace('txtLength', '').replace('txtBreadth', '').replace('txtHeight', '').replace('txtQty', '') + 'txtContents';
                var qty_id = id.replace('txtLength', '').replace('txtBreadth', '').replace('txtHeight', '').replace('txtQty', '') + 'txtQty';

                var Qty_Available = Qty_Master - Qty_Paid_Till;

                var qty_Current = 0;
                try {
                    qty_Current = parseFloat(obj.value);
                }
                catch
                {
                    qty_Current = 0;
                }
                var ddlunit = document.getElementById(unit_id);
                var unit_text = ddlunit.options[ddlunit.selectedIndex].text;

                //if (qty_Current > Qty_Available) {
                //    document.getElementById(comments_id).value = '';
                //    document.getElementById(qty_id).value = '';
                //    document.getElementById(id1).value = '';
                //    document.getElementById(id2).value = '';
                //    document.getElementById(id3).value = '';
                //}
                //else {
                //    document.getElementById(comments_id).value = qty_Current + ' ' + unit_text;
                //    document.getElementById(qty_id).value = qty_Current;
                //}
                document.getElementById(comments_id).value = qty_Current + ' ' + unit_text;
                document.getElementById(qty_id).value = qty_Current;
            }
        }
    </script>
</asp:Content>

