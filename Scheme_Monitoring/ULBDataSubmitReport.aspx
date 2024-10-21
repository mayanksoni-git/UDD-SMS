<%@ Page MasterPageFile="~/TemplateMasterAdmin_PMS.master" MaintainScrollPositionOnPostback="true"  Language="C#" AutoEventWireup="true" CodeFile="ULBDataSubmitReport.aspx.cs" Inherits="ULBDataSubmitReport" EnableEventValidation="false" ValidateRequest="false"%>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <link href="assets/css/CalendarStyle.css" rel="stylesheet" />
    <script src="https://cdnjs.cloudflare.com/ajax/libs/jquery/3.6.0/jquery.min.js"></script>
    <script src="https://cdn.jsdelivr.net/npm/chart.js"></script>
    <script src="https://cdn.jsdelivr.net/npm/chartjs-plugin-datalabels"></script>
    <div class="main-content">
        <div class="page-content">
            <!-- Button trigger modal -->
<%--<button type="button" class="btn btn-primary" >
  Launch demo modal
</button>--%>

<!-- Modal -->
<div class="modal fade" id="exampleModal" tabindex="-1" aria-labelledby="exampleModalLabel" aria-hidden="true">
  <div class="modal-dialog">
    <div class="modal-content">
      <div class="modal-header">
        <h1 class="modal-title fs-5" id="exampleModalLabel">ULB Data Submit Report</h1>
        <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
      </div>
      <div class="modal-body">
     <table class="table table-bordered">
         <thead>
             <tr>
                 <th>ULB(Which Data Submitted)</th>
                 <th>ULB(Which Data Not Submitted)</th>
             </tr>
         </thead>
         <tbody id="ULBData">

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
                                    <h4 class="mb-sm-0">Park Adoption Status And progress Report</h4>
                                    <div class="page-title-right">
                                        <ol class="breadcrumb m-0">
                                            <li class="breadcrumb-item"><a href="javascript: void(0);">Home</a></li>
                                            <li class="breadcrumb-item">Construction Of Parks</li>
                                            <li class="breadcrumb-item active">ULB Submitted Data Report</li>
                                        </ol>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-lg-12">
                                <div class="card">
                                    <div class="card-header align-items-center d-flex">
                                        <h4 class="card-title mb-0 flex-grow-1">ULB Submitted Data Report</h4>
                                    </div>
                                    <div class="card-body">
                                        <div class="live-preview">
                                            <div class="row gy-4">
                                                    <div class="col-xxl-3 col-md-6">
                                                    <div id="divCircle" runat="server">
                                                        <asp:Label ID="lblCircleH" runat="server" Text="District" CssClass="form-label"></asp:Label>
                                                        <asp:DropDownList ID="ddlCircle" runat="server" CssClass="form-select" AutoPostBack="true" OnSelectedIndexChanged="ddlCircle_SelectedIndexChanged"></asp:DropDownList>
                                                    </div>
                                                </div>
                                                <div class="col-xxl-3 col-md-6" id="divDivision" runat="server">
                                                    <div>
                                                        <asp:Label ID="lblDivisionH" runat="server" Text="Division*" CssClass="control-label no-padding-right"></asp:Label>
                                                        <asp:DropDownList ID="ddlDivision" runat="server" CssClass="form-select" AutoPostBack="true"></asp:DropDownList>
                                                    </div>
                                                </div>
                                           <%--     <div class="col-xxl-3 col-md-6">
                                                    <div id="divFY" runat="server">
                                                        <asp:Label ID="lblFY" runat="server" Text="Financial Year" CssClass="form-label"></asp:Label>
                                                        <asp:DropDownList ID="ddlFY" runat="server" CssClass="form-select"></asp:DropDownList>
                                                    </div>
                                                </div>
                                                 <div class="col-xxl-3 col-md-6">
                                                    <div id="divMonth" runat="server">
                                                        <asp:Label ID="lblMonth" runat="server" Text="Month" CssClass="form-label"></asp:Label>
                                                        <asp:DropDownList ID="ddlMonth" runat="server" CssClass="form-select"></asp:DropDownList>
                                                    </div>
                                                </div>--%>
                                                <div class="col-xxl-3 col-md-6">
                                                    <div>
                                                        <label class="d-block">&nbsp;</label>
                                                        <asp:Button ID="btnSearch" Text="Search" OnClick="btnSearch_Click" runat="server" CssClass="btn bg-success text-white"></asp:Button>
                                                        <asp:Button ID="btnCancel" Text="Cancel / Reset" OnClick="btnCancel_Click" runat="server" CssClass="btn bg-secondary text-white"></asp:Button>
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
                    
                    <div class="row">
                            <div class="col-lg-12">
                                <div class="card">
                                    <div class="card-header align-items-center d-flex">
                                        <h4 class="card-title mb-0 flex-grow-1">Adopted Park Detail and Fascilities</h4>
                                    </div>
                                    <!-- end card header -->
                                    <div class="card-body">
                                        <div class="live-preview">
                                            <div class="row gy-12">
                                                   <div class="clearfix" id="dtOptions" runat="server">
                                                        <div class="pull-right tableTools-container"></div>
                                                    </div>
                                                 <div style="overflow: auto">
                                                <asp:GridView runat="server" ID="grdPost" CssClass="display table table-bordered"   AutoGenerateColumns="False" EmptyDataText="No Records Found" OnPreRender="grdPost_PreRender">
                                                    <Columns>
                                                          <asp:BoundField DataField="Circle_Id" HeaderText="Circle_Id">
                                                        <HeaderStyle CssClass="displayStyle" />
                                                        <ItemStyle CssClass="displayStyle" />
                                                        <FooterStyle CssClass="displayStyle" />
                                                    </asp:BoundField>
                                                        <asp:TemplateField HeaderText="Sr. No.">
                                                            <ItemTemplate>
                                                                <%# Container.DataItemIndex + 1 %>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                         <asp:TemplateField HeaderText="Circle Name">
                                                            <ItemTemplate>
                                                                <%--<asp:LinkButton ID="lnkULBDetails" runat="server" Text='<%# Eval("Circle_Name") %>' OnClick="lnkULBDetails_Click"></asp:LinkButton>--%>
                                                                <a  href="javascript:void(0);" class="btn-open-modal" data-circle-name='<%# Eval("Circle_Id") %>' onclick="GetULBDetails(<%# Eval("Circle_Id") %>)">
                                                                    <%# Eval("Circle_Name") %>
                                                                </a>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <%--<asp:BoundField HeaderText="District" DataField="Circle_Name" />--%>
                                                        <%--<asp:BoundField HeaderText="ULB Name" DataField="Division_Name" />--%>
                                                        <asp:BoundField HeaderText="No. of ULB Submitted The data" DataField="SUBMITTED_DATA" />
                                                        <asp:BoundField HeaderText="No. of ULB not Submitted the data" DataField="NOT_SUBMITTED_DATA" />
                                                                
                                                    </Columns>
                                                    <EmptyDataTemplate>
                                                        <tr>
                                                            <td colspan="2" style="text-align: center; font-weight: bold; color: red;">No records found</td>
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
                </ContentTemplate>
                <Triggers>
                    <asp:PostBackTrigger ControlID="btnSearch" />
                    <asp:PostBackTrigger ControlID="ddlCircle" />
                    <%--<asp:PostBackTrigger ControlID="ddlDivision" />--%>
                    <%--<asp:PostBackTrigger ControlID="ddlProjectMaster" />--%>
                </Triggers>
            </asp:UpdatePanel>
        </div>
    </div>
     <%-- <script>
          function GetULBDetails(Circle_Id) {
              debugger
              $.ajax({
                  url: "ULBDataSubmitReport.aspx/GetULBDetails",
                  type: "POST",
                  contentType: "Application/json;charset=utf-8;",
                  dataType: "json",
                  data: JSON.stringify({ Circle_Id: Circle_Id }),
                  success: function (data) {
                      console.log(data); // Inspect the response
                      if (data.d) {
                          var result = Array.isArray(data.d) ? data.d : [data.d]; // Ensure it's an array
                          var html = '';
                          result.forEach(function (item) {
                              html += '<tr>';
                              html += '<td>' + (item.SUBMITTED_DATA || 'N/A') + '</td>';
                              html += '<td>' + (item.NOT_SUBMITTED_DATA || 'N/A') + '</td>';
                              html += '</tr>';
                          });
                          $('#ULBData').html(html);
                          $("#exampleModal").modal('show');
                      } else {
                          console.error("No data returned");
                      }
                  }

               

                 
              })
          }
      </script>--%>
   <script>
       function GetULBDetails(Circle_Id) {
           $.ajax({
               url: "ULBDataSubmitReport.aspx/GetULBDetails",
               type: "POST",
               contentType: "application/json;charset=utf-8;",
               dataType: "json",
               data: JSON.stringify({ Circle_Id: Circle_Id }),
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
                           html += '<td>' + (item.SUBMITTED_DATA || '') + '</td>';
                           html += '<td>' + (item.NOT_SUBMITTED_DATA || '') + '</td>';
                           html += '</tr>';
                       }

                       $('#ULBData').html(html); // Replace with new content
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
