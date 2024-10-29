<%@ Page Language="C#"  MasterPageFile="~/TemplateMasterAdmin_PMS.master" MaintainScrollPositionOnPostback="true"  AutoEventWireup="true" CodeFile="NewAkanshiDataList.aspx.cs" Inherits="NewAkanshiDataList" EnableEventValidation="false" ValidateRequest="false"%>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <link href="assets/css/CalendarStyle.css" rel="stylesheet" />
    <script src="https://cdnjs.cloudflare.com/ajax/libs/jquery/3.6.0/jquery.min.js"></script>
    <script src="https://cdn.jsdelivr.net/npm/chart.js"></script>
    <script src="https://cdn.jsdelivr.net/npm/chartjs-plugin-datalabels"></script>
    <div class="main-content">
        <div class="page-content">
            <!-- Modal -->
<div class="modal fade" id="exampleModal" tabindex="-1" aria-labelledby="exampleModalLabel" aria-hidden="true">
  <div class="modal-dialog modal-lg">
    <div class="modal-content">
      <div class="modal-header">
        <h1 class="modal-title fs-5" id="exampleModalLabel">New Akanshi Data Head Details</h1>
        <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
      </div>
      <div class="modal-body">
     <table class="table table-bordered">
         <thead>
             <tr>
                 <th>AkanshiHead</th>
                 <th>CostPerHead</th>
                 <th>NoOfHead</th>
                 <th>Amount</th>
             </tr>
         </thead>
         <tbody id="HeadData">

         </tbody>
     </table>
      </div>
    <%--  <div class="modal-footer">
        <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Close</button>
        <button type="button" class="btn btn-primary">Save changes</button>
      </div>--%>
    </div>
  </div>
</div>
            <asp:UpdatePanel ID="up" runat="server">
                <ContentTemplate>
                    <cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" EnablePartialRendering="true" EnablePageMethods="true" AsyncPostBackTimeout="6000">
                    </cc1:ToolkitScriptManager>
                    <div class="container-fluid">
                        <div class="row">
                            <div class="col-12">
                                <div class="page-title-box d-sm-flex align-items-center justify-content-between">
                                    <h4 class="mb-sm-0">New Akankshi Yojna Data List</h4>
                                    <div class="page-title-right">
                                        <ol class="breadcrumb m-0">
                                            <li class="breadcrumb-item"><a href="javascript: void(0);">Home</a></li>
                                            <li class="breadcrumb-item">Akankshi Yojna</li>
                                            <li class="breadcrumb-item active">New Akankshi Yojna Data List</li>
                                        </ol>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-lg-12">
                                <div class="card">
                                    <div class="card-header align-items-center d-flex">
                                        <h4 class="card-title mb-0 flex-grow-1">Filter :</h4>
                                          <a href="AddNewAkanshiData.aspx"  class="filter-btn" style="float:right;width:155px"><i class="icon-download"></i>Create New</a>

                                    </div>
                                    <div class="card-body">
                                        <div class="live-preview">
                                            <div class="row gy-4">
                                                    <div class="col-xxl-3 col-md-6">
                                                    <div id="divCircle" runat="server">
                                                        <asp:Label ID="lblCircleH" runat="server" Text="District" CssClass="form-label"></asp:Label>
                                                        <asp:DropDownList ID="ddlCircle" runat="server" CssClass="form-select" AutoPostBack="true" OnSelectedIndexChanged="ddlCircle_SelectedIndexChanged" ></asp:DropDownList>
                                                    </div>
                                                </div>
                                                <div class="col-xxl-3 col-md-6">
                                                    <div id="divFY" runat="server">
                                                        <asp:Label ID="lblFY" runat="server" Text="Financial Year" CssClass="form-label"></asp:Label>
                                                        <asp:DropDownList ID="ddlFY" runat="server" CssClass="form-select"></asp:DropDownList>
                                                    </div>
                                                </div>
                                                 <div class="col-xxl-3 col-md-6">
                                                    <div id="divMonth" runat="server">
                                                        <asp:Label ID="lblDivisionH" runat="server" Text="Division" CssClass="form-label"></asp:Label>
                                                        <asp:DropDownList ID="ddlDivision" runat="server" CssClass="form-select"></asp:DropDownList>
                                                    </div>
                                                </div>
                                                <div class="col-xxl-3 col-md-6">
                                                    <div>
                                                        <label class="d-block">&nbsp;</label>
                                                        <asp:Button ID="btnSearch" Text="Search" OnClick="btnSearch_Click" runat="server" CssClass="btn bg-success text-white"></asp:Button>
                                                        <%--<asp:Button ID="btnCancel" Text="Cancel / Reset" OnClick="btnCancel_Click" runat="server" CssClass="btn bg-secondary text-white"></asp:Button>--%>
                                                        <asp:Label ID="lblMessage" runat="server" ForeColor="Red"></asp:Label>
                                                        <asp:HiddenField ID="hfWorkProposalId" runat="server" />
                                                    </div>
                                                </div>
                                            </div>

                                         
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    
                   
                        
                        </div>
                </ContentTemplate>
                <Triggers>
                    <asp:PostBackTrigger ControlID="btnSearch" />
                    <asp:PostBackTrigger ControlID="ddlCircle" />
                    <asp:PostBackTrigger ControlID="ddlFY" />
                    <asp:PostBackTrigger ControlID="ddlDivision" />
                </Triggers>
            </asp:UpdatePanel>
            <div class="container-fluid">
                 <div class="row">
                            <div class="col-lg-12">
                                <div class="card">
                                    <div class="card-header align-items-center d-flex">
                                        <h4 class="card-title mb-0 flex-grow-1">Park Adoption Status And Progress Report</h4>
                                    </div>
                                    <!-- end card header -->
                                    <div class="card-body">
                                        <div class="live-preview">
                                            <div class="row gy-12">
                                                   <div class="clearfix" id="dtOptions" runat="server">
                                                        <div class="pull-right tableTools-container"></div>
                                                    </div>
                                                 <div style="overflow: auto">
                                                <asp:GridView DataKeyNames="newAkanshi_Id" runat="server" ID="grdPost" CssClass="display table table-bordered"   AutoGenerateColumns="False" EmptyDataText="No Records Found" OnPreRender="grdPost_PreRender">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="Sr. No.">
                                                            <ItemTemplate>
                                                                <%# Container.DataItemIndex + 1 %>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                           <asp:TemplateField HeaderText="Select">
                                                                    <ItemTemplate>
                                                                        <asp:ImageButton ID="btnEdit" Width="20px" Height="20px" OnClick="btnEdit_Click" ImageUrl="~/assets/images/edit_btn.png" runat="server" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                        <asp:BoundField HeaderText="District" DataField="Circle_Name" />
                                                        <asp:BoundField HeaderText="ULB Name" DataField="Division_Name" />
                                                        <asp:BoundField HeaderText="Finn Year" DataField="FinancialYear_Comments" />
                                                        <asp:BoundField HeaderText="Cm Fellow Name" DataField="CmFellowName" />
                                                        <asp:BoundField HeaderText="Total Transferred" DataField="Total_Transferred" />
                                                        <asp:BoundField HeaderText="Total Amount" DataField="Total_Amount" />
                                                         <asp:TemplateField HeaderText="Total Head">
                                                            <ItemTemplate>
                                                                <%--<asp:LinkButton ID="lnkULBDetails" runat="server" Text='<%# Eval("Circle_Name") %>' OnClick="lnkULBDetails_Click"></asp:LinkButton>--%>
                                                                <a  href="javascript:void(0);" class="btn-open-modal" data-total-head='<%# Eval("newAkanshi_Id") %>' onclick="GetHeadDetails(<%# Eval("newAkanshi_Id") %>)">
                                                                    <%# Eval("Total_Head") %>
                                                                </a>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <%--<asp:BoundField HeaderText="Total Head" DataField="Total_Head" />--%>
                                                        <%--<asp:BoundField HeaderText="Cost Per Head " DataField="CostPerHead" />                                            
                                                        <asp:BoundField HeaderText="Akanshi Head" DataField="AkanshiHead" />                                            
                                                        <asp:BoundField HeaderText="No Of Head" DataField="NoOfHead" />                                            
                                                        <asp:BoundField HeaderText="Amount" DataField="Amount" />--%>                                            
                                                
                                                          <%--  <asp:TemplateField HeaderText="Edit">
                                                            <ItemTemplate>
                                                                <asp:Button ID="btnEdit" runat="server" Text='Edit' CommandName="EditAdoptedPark" OnCommand="Edit_Command" CommandArgument='<%# Eval("AdoptedParkId") %>' CssClass="btn btn-primary drill_btn" />
                                                            </ItemTemplate>                                                             
                                                        </asp:TemplateField>
                                                       <asp:TemplateField HeaderText="Delete">
                                                        <ItemTemplate>
                                                             <asp:Button ID="btnDelete" OnClientClick="return confirm('Are You Sure You Want to Delete this Item?')" runat="server" Text='Delete' CommandName="DeleteAnnualAction" OnCommand="btnDelete_Command" CommandArgument='<%# Eval("Id") %>' CssClass="btn btn-danger drill_btn" />
                                                         </ItemTemplate>

                                                        </asp:TemplateField>--%>
                                                    </Columns>
                                                    <EmptyDataTemplate>
                                                        <tr>
                                                            <td colspan="10" style="text-align: center; font-weight: bold; color: red;">No records found</td>
                                                        </tr>
                                                    </EmptyDataTemplate>
                                                </asp:GridView>
                                            </div>
                                         </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
             </div>
        </div>
    </div>

       <script>
           function GetHeadDetails(newAkanshi_Id) {
           $.ajax({
               url: "NewAkanshidataList.aspx/GetHeadDetails",
               type: "POST",
               contentType: "application/json;charset=utf-8;",
               dataType: "json",
               data: JSON.stringify({ newAkanshi_Id: newAkanshi_Id }),
               success: function (data) {
                   console.log(data); // Inspect the response
                   if (data.d) {
                       // Parse the JSON string to get an array
                       var result = JSON.parse(data.d);
                       console.log(result); // Log the parsed result

                       $('#ULBData').empty(); // Clear existing rows
                       var html = '';

                       // Iterate through the result using a for loop
                       for (var i = 0; i < result.length; i++) {
                           var item = result[i];
                           html += '<tr>';
                           html += '<td>' + (item.AkanshiHead || '') + '</td>';
                           html += '<td>' + (item.CostPerHead || '') + '</td>';
                           html += '<td>' + (item.NoOfHead || '') + '</td>';
                           html += '<td>' + (item.Amount || '') + '</td>';
                           html += '</tr>';
                       }

                       $('#HeadData').html(html); // Replace with new content
                       $("#exampleModal").modal('show');
                   } else {
                       console.error("No data returned");
                   }
               },
               error: function (xhr, status, error) {
                   console.error("AJAX error: " + status + " - " + error);
               }
           });
       }
       </script>
</asp:Content>
