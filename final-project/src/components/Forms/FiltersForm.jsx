import { ArrowLeftIcon } from "@heroicons/react/24/solid";
import Link from "next/link";
import { useState, useEffect } from "react";

export const FiltersForm = ({
  sendCustomerFirstName,
  sendCustomerLastName,
  sendSalesPersonFirstName,
  sendSalesPersonLastName,
  sendProductName,
  sendProductCategory,
  sendStartDate,
  sendEndDate,
  GeneratePdf,
}) => {
  const [customerFirstName, setCustomerFirstName] = useState("");
  const [customerLastName, setCustomerLastName] = useState("");
  const [salesPersonFirstName, setSalesPersonFirstName] = useState("");
  const [salesPersonLastName, setSalesPersonLastName] = useState("");
  const [productName, setProductName] = useState("");
  const [productCategory, setProductCategory] = useState("");
  const [startDate, setStartDate] = useState("1998-01-01");
  const [endDate, setEndDate] = useState("2024-01-01");

  const SendFilters = () => {
    sendCustomerFirstName(customerFirstName);
    sendCustomerLastName(customerLastName);
    sendSalesPersonFirstName(salesPersonFirstName);
    sendSalesPersonLastName(salesPersonLastName);
    sendProductName(productName);
    sendProductCategory(productCategory);
    sendStartDate(startDate);
    sendEndDate(endDate);
  };

  const handleStartDateChange = (e) => {
    setStartDate(e.target.value); // Update the state with the new value
  };

  const handleEndDateChange = (e) => {
    setEndDate(e.target.value); // Update the state with the new value
  };

  useEffect(() => {}, []);

  return (
    <div className="flex flex-col gap-7 justify-center items-center w-[32%] min-w-[520px] h-fit py-10 bg-slate-200 rounded-md shadow-xl shadow-slate-400">
      <div className="flex flex-row gap-20 justify-center items-start mb-6">
        <Link
          href={{
            pathname: "/",
          }}
          className="flex flex-row justify-center items-center bg-slate-50 p-2 rounded-lg shadow-sm hover:bg-red-500 transition hover:delay-100 ease-in-out"
        >
          <ArrowLeftIcon className="size-12 text-red-500 hover:text-slate-50 transition hover:delay-100 ease-in-out" />
        </Link>
        <h2 className="text-5xl uppercase font-semibold">Filters</h2>
      </div>
      <div className="flex flex-col text-center gap-3 text-xl font-medium">
        <div className="">
          <h3>Customer</h3>
        </div>
        <div className="flex flex-row gap-4 text-left">
          <div className="flex flex-col gap-2">
            <label htmlFor="">First Name</label>
            <input
              className="rounded-md pl-1"
              type="text"
              onChange={(e) => setCustomerFirstName(e.currentTarget.value)}
            />
          </div>
          <div className="flex flex-col gap-2">
            <label htmlFor="">Last Name</label>
            <input
              className="rounded-md pl-1"
              type="text"
              onChange={(e) => setCustomerLastName(e.currentTarget.value)}
            />
          </div>
        </div>
      </div>

      <div className="flex flex-col text-center gap-3 text-xl font-medium">
        <h3>Sales Person</h3>
        <div className="flex flex-row gap-4 text-left">
          <div className="flex flex-col gap-2">
            <label htmlFor="">First Name</label>
            <input
              className="rounded-md pl-1"
              type="text"
              onChange={(e) => setSalesPersonFirstName(e.currentTarget.value)}
            />
          </div>
          <div className="flex flex-col gap-2">
            <label htmlFor="">Last Name</label>
            <input
              className="rounded-md pl-1"
              type="text"
              onChange={(e) => setSalesPersonLastName(e.currentTarget.value)}
            />
          </div>
        </div>
      </div>

      <div className="flex flex-col text-center gap-3 text-xl font-medium">
        <h3>Product</h3>
        <div className="flex flex-row gap-4 text-left">
          <div className="flex flex-col gap-2">
            <label htmlFor="">Name of Product</label>
            <input
              className="rounded-md pl-1"
              type="text"
              onChange={(e) => setProductName(e.currentTarget.value)}
            />
          </div>
          <div className="flex flex-col gap-2">
            <label htmlFor="">Category</label>
            <input
              className="rounded-md pl-1"
              type="text"
              onChange={(e) => setProductCategory(e.currentTarget.value)}
            />
          </div>
        </div>
      </div>

      <div className="flex flex-col text-center gap-3 text-xl font-medium">
        <h3>Order Date</h3>
        <div className="flex flex-row gap-4 text-left">
          <div className="flex flex-col gap-2">
            <label htmlFor="">From</label>
            <input
              className="rounded-md pl-1 w-[220px] max-w-[220px]"
              type="date"
              value={startDate}
              onChange={handleStartDateChange}
            />
          </div>
          <div className="flex flex-col gap-2">
            <label htmlFor="">To</label>
            <input
              className="rounded-md pl-1 w-[220px] max-w-[220px]"
              type="date"
              value={endDate}
              onChange={handleEndDateChange}
            />
          </div>
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
          onClick={() => SendFilters()}
        >
          Apply Filters
        </button>
      </div>
    </div>
  );
};
