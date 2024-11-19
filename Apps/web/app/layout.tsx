import type { Metadata } from "next";
import "./globals.css";
import Header from "@/components/UI/Header";
import { Footer } from "@/components/UI/Footer";
import AppProviders from "@/components/AppProviders";

export const metadata: Metadata = {
  title: "GoodFood",
  description: "Made with Love ❤️",
};

export default function RootLayout({
  children,
}: {
  children: React.ReactNode;
}) {
  return (
    <html lang="fr">
      <head>
        <link rel="preconnect" href="https://fonts.googleapis.com" />
        <link rel="preconnect" href="https://fonts.gstatic.com" />
        <link
          href="https://fonts.googleapis.com/css2?family=Space+Grotesk:wght@300..700&display=swap"
          rel="stylesheet"
        ></link>
        <link rel="preconnect" href="https://fonts.googleapis.com" />
        <link rel="preconnect" href="https://fonts.gstatic.com" />
        <link
          href="https://fonts.googleapis.com/css2?family=Open+Sans:ital,wght@0,300..800;1,300..800&family=Space+Grotesk:wght@300..700&display=swap"
          rel="stylesheet"
        ></link>
      </head>
      <body className="w-screen h-screen overflow-x-hidden bg-background">
        <AppProviders>
          <Header></Header>
          {children}
          <Footer></Footer>
        </AppProviders>
      </body>
    </html>
  );
}
