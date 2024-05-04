import { useEffect, useState } from "react";
import { ChevronLeftIcon, ChevronRightIcon } from "@heroicons/react/24/solid";
import { FiltersForm } from "../Forms/FiltersForm";
export const TableGeneralSalesReport = ({}) => {
  const [salesReport, setSalesReport] = useState([]);
  const [isLoading, setIsLoading] = useState(true);
  const [CustomerFirstName, setCustomerFirstName] = useState("");
  const [CustomerLastName, setCustomerLastName] = useState("");
  const [SalesPersonFirstName, setSalesPersonFirstName] = useState("");
  const [SalesPersonLastName, setSalesPersonLastName] = useState("");
  const [ProductName, setProductName] = useState("");
  const [ProductCategory, setProductCategory] = useState("");
  const [StartDate, setStartDate] = useState("1998-01-01");
  const [EndDate, setEndDate] = useState("2024-01-01");

  const onGetSalesReport = async (page) => {
    try {
      const response = await fetch(
        `http://localhost:5244/api/SalesReports/getGeneralSalesReport?CustomerFirstName=${CustomerFirstName}&CustomerLastName=${CustomerLastName}&SalesPersonFirstName=${SalesPersonFirstName}&SalesPersonLastName=${SalesPersonLastName}&ProductName=${ProductName}&ProductCategory=${ProductCategory}&StartDate=${StartDate}&EndDate=${EndDate}&Page=${page}`
      );
      const data = await response.json();
      setSalesReport(data);
      setIsLoading(false);
    } catch (error) {
      console.log(error);
    }
  };

  const onFetchNextData = () => {
    let next = salesReport.page + 1;
    onGetSalesReport(next);
  };

  const onFetchPreviousData = () => {
    let prev = salesReport.page - 1;
    onGetSalesReport(prev);
  };

  const formattedDate = (dateString) => {
    const date = new Date(dateString);

    const year = date.getFullYear();
    const month = String(date.getMonth() + 1).padStart(2, "0");
    const day = String(date.getDate()).padStart(2, "0");

    const formattedDate = `${year}-${month}-${day}`;
    return formattedDate;
  };

  const GeneratePdf = async () => {
    try {
      const response = await fetch(
        `http://localhost:5244/api/SalesReports/getGeneralSalesReportPdf?CustomerFirstName=${CustomerFirstName}&CustomerLastName=${CustomerLastName}&SalesPersonFirstName=${SalesPersonFirstName}&SalesPersonLastName=${SalesPersonLastName}&ProductName=${ProductName}&ProductCategory=${ProductCategory}&StartDate=${StartDate}&EndDate=${EndDate}&Page=${salesReport.page}`
      );
      const pdfBlob = await response.blob();
      const url = window.URL.createObjectURL(pdfBlob);
      window.open(url, "_blank");
      console.log(blob);
    } catch (error) {
      console.error("There was a problem with the fetch operation:", error);
    }
  };

  useEffect(() => {
    onGetSalesReport(1);
  }, []);

  useEffect(() => {
    onGetSalesReport(1);
  }, [
    CustomerFirstName,
    CustomerLastName,
    SalesPersonFirstName,
    SalesPersonLastName,
    ProductName,
    ProductCategory,
    StartDate,
    EndDate,
  ]);

  return (
    <>
      {isLoading ? (
        <h1>Loading...</h1>
      ) : salesReport === undefined ? (
        <h1>
          Ooops, looks like there is nothing but us here, Try with a different
          sets of filters
        </h1>
      ) : (
        <div className="flex flex-row justify-around">
          <div className="flex flex-col w-3/5 h-4/5 text-center text-sm gap-5">
            <table className="shadow-lg shadow-slate-300 w-full h-[798px]">
              <thead>
                <tr className="bg-red-500 border-4 border-slate-200">
                  <th height="47px" className="max-h-12">
                    Order Id
                  </th>
                  <th height="47px" className="max-h-12">
                    Customer
                  </th>
                  <th height="47px" className="max-h-12">
                    Sales Person
                  </th>
                  <th height="47px" className="max-h-12">
                    Product
                  </th>
                  <th height="47px" className="max-h-12">
                    Category
                  </th>
                  <th height="47px" className="max-h-12">
                    Unit Price
                  </th>
                  <th height="47px" className="max-h-12">
                    Quantity
                  </th>
                  <th height="47px" className="max-h-12">
                    Line Total
                  </th>
                  <th height="47px" className="max-h-12">
                    Order Date
                  </th>
                  <th height="47px" className="max-h-12">
                    Shipping Address
                  </th>
                  <th className="">Billing Address</th>
                </tr>
              </thead>
              <tbody>
                {salesReport.items.map((item, index) => (
                  <tr
                    key={index}
                    className="bg-slate-50 border-2 border-slate-200"
                  >
                    <td className="font-semibold">{item.salesOrderId}</td>
                    <td height="47px">{item.customerName}</td>
                    <td>{item.salesPersonName}</td>
                    <td>{item.productName}</td>
                    <td>{item.productCategory}</td>
                    <td>${item.unitPrice.toFixed(2)}</td>
                    <td>{item.orderQty}</td>
                    <td>${item.lineTotal.toFixed(2)}</td>
                    <td>{formattedDate(item.orderDate)}</td>
                    <td>{item.shippingAddress}</td>
                    <td>{item.billingAddress}</td>
                  </tr>
                ))}
              </tbody>
            </table>
            <div className="flex flex-row justify-center items-center">
              {salesReport.hasPreviousPage && (
                <button
                  className="flex justify-center items-center w-10 h-10 bg-slate-50 rounded-full hover:bg-blue-400 transition ease-in-out shadow-md shadow-slate-400"
                  onClick={() => onFetchPreviousData()}
                >
                  <ChevronLeftIcon className="size-6 text-black  hover:text-slate-50" />
                </button>
              )}
              <div className="flex justify-center items-center w-fit h-10 px-4 font-medium text-lg">
                {salesReport.page} / {Math.ceil(salesReport.totalCount / 17)}
              </div>
              {salesReport.hasNextPage && (
                <button
                  className="flex justify-center items-center w-10 h-10 bg-slate-50 rounded-full hover:bg-blue-400 transition ease-in-out shadow-md shadow-slate-400"
                  onClick={() => onFetchNextData()}
                >
                  <ChevronRightIcon className="size-6 text-black hover:text-slate-50" />
                </button>
              )}
            </div>
          </div>
          <FiltersForm
            sendCustomerFirstName={setCustomerFirstName}
            sendCustomerLastName={setCustomerLastName}
            sendSalesPersonFirstName={setSalesPersonFirstName}
            sendSalesPersonLastName={setSalesPersonLastName}
            sendProductName={setProductName}
            sendProductCategory={setProductCategory}
            sendStartDate={setStartDate}
            sendEndDate={setEndDate}
            GeneratePdf={GeneratePdf}
          />
        </div>
      )}
    </>
  );
};
