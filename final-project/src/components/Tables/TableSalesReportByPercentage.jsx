import { useEffect, useState } from "react";
import {
  ArrowLeftIcon,
  ChevronLeftIcon,
  ChevronRightIcon,
} from "@heroicons/react/24/solid";
import Link from "next/link";
export const TableSalesReportByPercentage = ({}) => {
  const [salesReport, setSalesReport] = useState([]);
  const [isLoading, setIsLoading] = useState(true);
  const [ProductCategory, setProductCategory] = useState("");

  const onGetSalesReport = async (page) => {
    try {
      const response = await fetch(
        `http://localhost:5244/api/SalesReports/getSalesReportByPercentage?ProductCategory=${ProductCategory}&Page=${page}`
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

  const GeneratePdf = async () => {
    try {
      const response = await fetch(
        `http://localhost:5244/api/SalesReports/getSalesReportPercentagePdf?ProductCategory=${ProductCategory}&Page=${salesReport.page}`
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
                    Product Name
                  </th>
                  <th height="47px" className="max-h-12">
                    Category
                  </th>
                  <th height="47px" className="max-h-12">
                    Total Sales
                  </th>
                  <th height="47px" className="max-h-12">
                    Percentage By Region
                  </th>
                  <th height="47px" className="max-h-12">
                    Percentage By Region / Category
                  </th>
                </tr>
              </thead>
              <tbody>
                {salesReport.items.map((item, index) => (
                  <tr
                    key={index}
                    className="bg-slate-50 border-2 border-slate-200"
                  >
                    <td className="font-semibold">{item.productName}</td>
                    <td height="47px">{item.productCategory}</td>
                    <td>${item.totalSales.toFixed(2)}</td>
                    <td>%{item.percentageByRegion}</td>
                    <td>%{item.percentageByRegionAndCategory}</td>
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
          <div className="relative bg-slate-200 flex flex-col justify-start items-center gap-9 px-8 py-8 h-fit shadow-xl">
            <div className=" flex flex-row justify-center items-center">
              <Link
                href={{
                  pathname: "/",
                }}
                className="absolute top-4 left-4 flex flex-row justify-center items-center bg-slate-50 p-2 rounded-lg shadow-sm hover:bg-red-500 transition hover:delay-100 ease-in-out"
              >
                <ArrowLeftIcon className="size-6 text-red-500 hover:text-slate-50 transition hover:delay-100 ease-in-out" />
              </Link>
              <div className="flex flex-col justify-center gap-6">
                <h3 className="font-semibold">Search By Category</h3>
                <input
                  type="text"
                  className="px-8 h-10 rounded-xl shadow-md"
                  onChange={(e) => setProductCategory(e.currentTarget.value)}
                />
              </div>
            </div>
            <div className="flex flex-row gap-20 items-center">
              <button
                className="bg-red-500 px-8 py-4 rounded-3xl shadow-md shadow-slate-400 font-semibold text-slate-100 hover:bg-red-700 transition ease-in-out hover:delay-50"
                onClick={() => GeneratePdf()}
              >
                Generate pdf
              </button>

              <button
                className="bg-blue-500 px-8 py-4 rounded-3xl shadow-md shadow-slate-400 font-semibold text-slate-100 hover:bg-blue-700 transition ease-in-out hover:delay-50"
                onClick={() => onGetSalesReport(1)}
              >
                Apply Filters
              </button>
            </div>
          </div>
        </div>
      )}
    </>
  );
};
