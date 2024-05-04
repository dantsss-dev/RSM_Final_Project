import {
  ClipboardDocumentListIcon,
  DocumentChartBarIcon,
} from "@heroicons/react/24/solid";
import Link from "next/link";

export const LoginForm = () => {
  return (
    <div className="flex justify-center items-center w-full min-h-screen h-full bg-slate-200 text-black">
      <form
        className="flex flex-row justify-center items-center gap-5 w-[600px] h-[450px] bg-slate-100 p-8 rounded-xl shadow-2xl"
        action=""
      >
        <Link
          href={{
            pathname: "/sales-report",
          }}
          className="bg-blue-400 flex flex-col justify-center items-center gap-5 py-4 px-8 w-[250px] h-[280px] text-xl rounded-2xl text-slate-100 shadow-lg hover:bg-blue-500 transition hover:duration-500 ease-in-out"
        >
          <ClipboardDocumentListIcon className="size-36 text-white" />
          Generate a General Sales Report
        </Link>

        <Link
          href={{
            pathname: "/sales-report-by-percentage",
          }}
          className="flex flex-col justify-center items-center gap-5 bg-green-500 py-4 px-8 w-[250px] h-[280px] text-xl rounded-2xl text-slate-100 shadow-lg hover:bg-green-700 transition hover:duration-500 ease-in-out"
        >
          <DocumentChartBarIcon className="size-36 text-white" />
          Generate Total of Sales Product by Percentage
        </Link>
      </form>
    </div>
  );
};
