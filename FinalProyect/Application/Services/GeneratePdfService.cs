using System.ComponentModel;
using FinalProyect.Application.DTOs;
using FinalProyect.Domain.Interface;
using FinalProyect.Domain.Pagination;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace FinalProyect.Application.Services
{
    public class GeneratePdfService: IGeneratePdfService
    {
        [Obsolete]
        public Document GeneratePdfQuest(PagedList<GetSalesReportDto> salesReport)
        {
            return Document.Create(container => {
                container.Page(page => {
                    page.Margin(13);
                    page.Size(PageSizes.A4);
                    page.PageColor(Colors.White);
                    page.DefaultTextStyle(x => x.FontSize(12));

                    page.Header().PaddingVertical(12)
                        .AlignCenter()
                        .Text("General Sales Report")
                        .SemiBold().FontSize(24).FontColor(Colors.Blue.Darken2);

                    page.Content().PaddingTop(12)
                        .Table(table => {
                                table.ColumnsDefinition(columns => {

                                    columns.RelativeColumn();
                                    columns.RelativeColumn();
                                    columns.RelativeColumn();
                                    columns.RelativeColumn();
                                    columns.RelativeColumn();
                                    columns.RelativeColumn();
                                    columns.RelativeColumn();
                                    columns.RelativeColumn();
                                    columns.RelativeColumn();
                                   // columns.RelativeColumn();
                                    //columns.RelativeColumn();
                                });

                                table.Header(header => {
                                    header.Cell().BorderColor("#019cde").BorderBottom(1).PaddingBottom(4).Text("Order Id").FontSize(9);
                                    header.Cell().BorderColor("#019cde").BorderBottom(1).PaddingBottom(4).Text("Customer").FontSize(9);
                                    header.Cell().BorderColor("#019cde").BorderBottom(1).PaddingBottom(4).Text("Sales Person").FontSize(9);
                                    header.Cell().BorderColor("#019cde").BorderBottom(1).PaddingBottom(4).Text("Product").FontSize(9);
                                    header.Cell().BorderColor("#019cde").BorderBottom(1).PaddingBottom(4).Text("Category").FontSize(9);
                                    header.Cell().BorderColor("#019cde").BorderBottom(1).PaddingBottom(4).Text("Unit Price").FontSize(9);
                                    header.Cell().BorderColor("#019cde").BorderBottom(1).PaddingBottom(4).Text("Quantity").FontSize(9);
                                    header.Cell().BorderColor("#019cde").BorderBottom(1).PaddingBottom(4).Text("Line Total").FontSize(9);
                                    header.Cell().BorderColor("#019cde").BorderBottom(1).PaddingBottom(4).Text("Order Date").FontSize(9);
                                    //header.Cell().Text("Shipping Address").FontSize(9);
                                    //header.Cell().Text("Billing Address").FontSize(9);
                                });

                                
                                foreach(var sale in salesReport.Items)
                                {
                                    table.Cell().BorderColor("#707175").BorderBottom(1).PaddingBottom(4).Text(sale.SalesOrderId).FontSize(9);
                                    table.Cell().BorderColor("#707175").BorderBottom(1).PaddingBottom(4).Text(sale.CustomerName).FontSize(9);
                                    table.Cell().BorderColor("#707175").BorderBottom(1).PaddingBottom(4).Text(sale.SalesPersonName).FontSize(9);
                                    table.Cell().BorderColor("#707175").BorderBottom(1).PaddingBottom(4).Text(sale.ProductName).FontSize(9);
                                    table.Cell().BorderColor("#707175").BorderBottom(1).PaddingBottom(4).Text(sale.ProductCategory).FontSize(9);
                                    table.Cell().BorderColor("#707175").BorderBottom(1).PaddingBottom(4).Text($"${sale.UnitPrice.ToString("0.00")}").FontSize(9);
                                    table.Cell().BorderColor("#707175").BorderBottom(1).PaddingBottom(4).Text(sale.OrderQty).FontSize(9);
                                    table.Cell().BorderColor("#707175").BorderBottom(1).PaddingBottom(4).Text($"${sale.LineTotal.ToString("0.00")}").FontSize(9);
                                    table.Cell().BorderColor("#707175").BorderBottom(1).PaddingBottom(4).Text(sale.OrderDate.ToString("yyyy-MM-dd")).FontSize(9);
                                   // table.Cell().Text(sale.ShippingAddress).FontSize(9);
                                    //table.Cell().Text(sale.BillingAddress).FontSize(9);
                                }
                        });
                    page.Footer().AlignCenter().Text(x => {
                        x.CurrentPageNumber();
                        x.Span(" / ");
                        x.TotalPages();
                    });
                });
            });
        }

        [Obsolete]
        public Document GeneratePdfQuest(PagedList<GetSalesReportByPercentageDto> salesReport)
        {
            return Document.Create(container => {
                container.Page(page => {
                    page.Margin(13);
                    page.Size(PageSizes.A4);
                    page.PageColor(Colors.White);
                    page.DefaultTextStyle(x => x.FontSize(12));

                    page.Header().PaddingVertical(12)
                        .AlignCenter()
                        .Text("General Sales Report")
                        .SemiBold().FontSize(24).FontColor(Colors.Blue.Darken2);

                    page.Content().PaddingTop(12)
                        .Table(table => {
                                table.ColumnsDefinition(columns => {

                                    columns.RelativeColumn();
                                    columns.RelativeColumn();
                                    columns.RelativeColumn();
                                    columns.RelativeColumn();
                                    columns.RelativeColumn();
                                });

                                table.Header(header => {
                                    header.Cell().BorderColor("#019cde").BorderBottom(1).PaddingBottom(4).Text("Product Name").FontSize(9);
                                    header.Cell().BorderColor("#019cde").BorderBottom(1).PaddingBottom(4).Text("Category").FontSize(9);
                                    header.Cell().BorderColor("#019cde").BorderBottom(1).PaddingBottom(4).Text("Total Sales").FontSize(9);
                                    header.Cell().BorderColor("#019cde").BorderBottom(1).PaddingBottom(4).Text("Percentage By Region").FontSize(9);
                                    header.Cell().BorderColor("#019cde").BorderBottom(1).PaddingBottom(4).Text("Percentage By Region/Category").FontSize(9);
                                });

                                
                                foreach(var sale in salesReport.Items)
                                {
                                    table.Cell().BorderColor("#707175").BorderBottom(1).PaddingBottom(4).Text(sale.ProductName).FontSize(9);
                                    table.Cell().BorderColor("#707175").BorderBottom(1).PaddingBottom(4).Text(sale.ProductCategory).FontSize(9);
                                    table.Cell().BorderColor("#707175").BorderBottom(1).PaddingBottom(4).Text(sale.TotalSales).FontSize(9);
                                    table.Cell().BorderColor("#707175").BorderBottom(1).PaddingBottom(4).Text(sale.PercentageByRegion).FontSize(9);
                                    table.Cell().BorderColor("#707175").BorderBottom(1).PaddingBottom(4).Text(sale.PercentageByRegionAndCategory).FontSize(9);
                                }
                        });
                    page.Footer().AlignCenter().Text(x => {
                        x.CurrentPageNumber();
                        x.Span(" / ");
                        x.TotalPages();
                    });
                });
            });
        }
    }
}