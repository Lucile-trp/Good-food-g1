// components/Header.tsx

import Image from "next/image";
import Link from "next/link";
import React from "react";

interface HeaderProps {
  isConnected: boolean;
}

const Header: React.FC<HeaderProps> = ({ isConnected }) => {
  return (
    <header className="flex items-center justify-between bg-white border-b border-dark_gray md:px-8 xl:px-64">
      <Link href="/">
      <Image src="/logo/png/black/logo_black.png" height={50} width={180} alt="Logo Goodfood"></Image>
      </Link>

      <nav className="flex items-center">
        {!isConnected ? (
          <div className="pt-4">
            <button className="px-4 py-2 text-white bg-black border-r hover:bg-secondary_purple">
              Se connecter
            </button>
            <button className="px-4 py-2 text-white bg-black hover:bg-secondary_green">
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
    </header>
  );
};

export default Header;
