// components/Header.tsx
"use client";

import Image from "next/image";
import Link from "next/link";
import React, { useState } from "react";
import { AuthModale } from "../Modales/AuthModale";
import { useUser } from "@/contexts/UserContext";

interface HeaderProps {
  isConnected: boolean;
}

const Header: React.FC<HeaderProps> = ({ isConnected }) => {
  const [authModale, setAuthModale] = useState<boolean>(true);
  const [authUserChoice, setAuthUserChoice] = useState<string>("");

  const { user, setUser } = useUser();
  return (
    <header className="flex items-center justify-between bg-white border-b border-dark_gray md:px-8 xl:px-64">
      <Link href="/">
        <Image
          src="/logo/png/black/logo_black.png"
          height={50}
          width={180}
          alt="Logo Goodfood"
        ></Image>
      </Link>

      <nav className="flex items-center">
        {!user ? (
          <div className="pt-4">
            <button
              className="px-4 py-2 text-white bg-black border-r hover:bg-secondary_purple"
              onClick={() => (setAuthModale(true), setAuthUserChoice("SignIn"))}
            >
              Se connecter
            </button>
            <button className="px-4 py-2 text-white bg-black hover:bg-secondary_green" onClick={() => (setAuthModale(true), setAuthUserChoice("SignUp"))}>
              S'inscrire
            </button>
          </div>
        ) : (
          <div className="flex pt-4 items-end">
            <button className="px-4 py-2 text-white bg-black border-r hover:bg-secondary_green h-10">
              Panier
            </button>
            <button className="px-4 py-2 text-white bg-black hover:bg-secondary_purple h-10">
              <Image
                src="/icons/png/white/user_icon.png"
                width={20}
                height={20}
                alt="User icon picture"
              ></Image>
            </button>
          </div>
        )}
      </nav>

      {/* Modales */}
      <div className="fixed top-0 left-0 z-[9999]">
        {authModale ? (
          <AuthModale
            setAuthModale={setAuthModale}
            authUserChoice={authUserChoice}
            setAuthUserChoice={setAuthUserChoice}
          />
        ) : (
          <></>
        )}
      </div>
    </header>
  );
};

export default Header;
