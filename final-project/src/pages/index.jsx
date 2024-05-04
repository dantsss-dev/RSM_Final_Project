import Image from "next/image";
import { Inter } from "next/font/google";
import { LoginForm } from "@/components/Forms/LoginForm";

const inter = Inter({ subsets: ["latin"] });

export default function Home() {
  return (
    <main className={`min-w-full min-h-full ${inter.className}`}>
      <LoginForm />
    </main>
  );
}
